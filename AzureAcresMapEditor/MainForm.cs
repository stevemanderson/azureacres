using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AzureAcresData;
using System.IO;
using System.Xml;
using System.Drawing.Imaging;
using System.Threading;

namespace AzureAcresMapEditor
{
    public partial class MainForm : Form
    {
        private enum LAYER { BASE, SECONDBASE, FRINGE, OBJECT }
        private LAYER _selectedLayer = LAYER.BASE;
        private Bitmap _drawArea;
        private Map _currentMap = null;
        private List<int> _indexes = new List<int>();
        private int _lastClicked = 0;
        private bool[] _hideLayers = new bool[4] { false, false, false, false };
        private bool _mouseDown = false;
        private object _lock = new object();
        private Thread _workerThread;

        public MainForm()
        {
            InitializeComponent();
        }

        protected void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            XmlDocument document = _currentMap.SaveMap();
            document.Save(saveFileDialog.FileName);
        }

        protected void pbMap_MouseLeave(object sender, EventArgs e)
        {
            _mouseDown = false;
        }

        protected void pbMap_MouseUp(object sender, MouseEventArgs e)
        {
            DrawTile(e.X, e.Y);
            _mouseDown = false;
        }

        protected void pbMap_MouseMove(object sender, MouseEventArgs e)
        {
            DrawTile(e.X, e.Y);
        }

        protected void pbMap_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
        }

        protected void DrawTile(int x, int y)
        {
            if (_currentMap == null) return;
            if (_indexes.Count() == 0 && !btnErase.Checked) return;
            if (!_mouseDown) return;

            int tileX = x - (x % _currentMap.ContentMap.TileWidth);
            int tileY = y - (y % _currentMap.ContentMap.TileHeight);
            int indexX = tileX / _currentMap.ContentMap.TileWidth;
            int indexY = tileY / _currentMap.ContentMap.TileHeight;

            if (!btnErase.Checked)
            {
                int pivot = _indexes[0];
                foreach (int i in _indexes)
                {
                    int index = indexX + indexY * _currentMap.ContentMap.MapWidth;
                    index += TextureIndexToMapIndex(pivot, i);
                    switch (_selectedLayer)
                    {
                        case LAYER.BASE:
                            _currentMap.ContentMap.BaseLayer[index] = i;
                            break;
                        case LAYER.SECONDBASE:
                            _currentMap.ContentMap.SecondBaseLayer[index] = i;
                            break;
                        case LAYER.FRINGE:
                            _currentMap.ContentMap.FringeLayer[index] = i;
                            break;
                        case LAYER.OBJECT:
                            _currentMap.ContentMap.ObjectLayer[index] = i;
                            break;
                    }
                }
            }
            else
            {
                int index = indexX + indexY * _currentMap.ContentMap.MapWidth;
                switch (_selectedLayer)
                {
                    case LAYER.BASE:
                        _currentMap.ContentMap.BaseLayer[index] = -1;
                        break;
                    case LAYER.SECONDBASE:
                        _currentMap.ContentMap.SecondBaseLayer[index] = -1;
                        break;
                    case LAYER.FRINGE:
                        _currentMap.ContentMap.FringeLayer[index] = -1;
                        break;
                    case LAYER.OBJECT:
                        _currentMap.ContentMap.ObjectLayer[index] = -1;
                        break;
                }
            }
            DrawMap();
        }

        private int TextureIndexToMapIndex(int pivot, int index)
        {
            int pivotX = pivot % (_currentMap.Texture.Width / _currentMap.ContentMap.TileWidth);
            int pivotY = (pivot - pivotX) / (_currentMap.Texture.Width / _currentMap.ContentMap.TileWidth);
            int indexX = index % (_currentMap.Texture.Width / _currentMap.ContentMap.TileWidth);
            int indexY = (index - indexX) / (_currentMap.Texture.Width / _currentMap.ContentMap.TileWidth);
            int diffX = indexX - pivotX;
            int diffY = indexY - pivotY;
            return diffX + (diffY * _currentMap.ContentMap.MapWidth);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapPropertiesForm mapProperties = new MapPropertiesForm(_currentMap);
            this.AddOwnedForm(mapProperties);
            mapProperties.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DrawMap()
        {
            _drawArea = new Bitmap(pbMap.Width, pbMap.Height);
            if (!_hideLayers[0]) DrawLayer(_currentMap.ContentMap.BaseLayer);
            if (!_hideLayers[1]) DrawLayer(_currentMap.ContentMap.SecondBaseLayer);
            if (!_hideLayers[3]) DrawLayer(_currentMap.ContentMap.ObjectLayer);
            if (!_hideLayers[2]) DrawLayer(_currentMap.ContentMap.FringeLayer);
            if (showPortalsToolStripMenuItem.Checked) DrawPortals();
        }

        private void DrawLayer(int[] layer)
        {
            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                for (int y = 0; y < _currentMap.ContentMap.MapHeight; ++y)
                {
                    for (int x = 0; x < _currentMap.ContentMap.MapWidth; ++x)
                    {
                        int mapIndex = (x + y * _currentMap.ContentMap.MapWidth);
                        int textureIndex = layer[mapIndex];
                        if (textureIndex == -1) continue;
                        int textureX = textureIndex % (_currentMap.Texture.Width / _currentMap.ContentMap.TileWidth);
                        int textureY = (int)Math.Floor((double)textureIndex / (_currentMap.Texture.Width / _currentMap.ContentMap.TileWidth));
                        Rectangle src = new Rectangle(textureX * _currentMap.ContentMap.TileWidth, textureY * _currentMap.ContentMap.TileHeight, _currentMap.ContentMap.TileWidth, _currentMap.ContentMap.TileHeight);
                        Rectangle dest = new Rectangle(x * _currentMap.ContentMap.TileWidth, y * _currentMap.ContentMap.TileHeight, _currentMap.ContentMap.TileWidth, _currentMap.ContentMap.TileHeight);
                        g.DrawImage(_currentMap.Texture, dest, src, GraphicsUnit.Pixel);
                    }
                }
            }
            pbMap.BackgroundImage = _drawArea;
        }

        private void DrawPortals()
        {
            foreach (Portal p in _currentMap.ContentMap.Portals)
            {
                using (Graphics g = Graphics.FromImage(_drawArea))
                {
                    g.FillRectangle(Brushes.Blue, new Rectangle(p.Coordinates.X, p.Coordinates.Y, 32, 32));
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            LoadMap();
        }

        private void LoadMap()
        {
            _currentMap = new Map();
            _currentMap.LoadMap(openFileDialog.OpenFile());
            _currentMap.ContentBasePath = Path.GetDirectoryName(Path.GetDirectoryName(openFileDialog.FileName));
            _currentMap.LoadTexture(_currentMap.TexturePath);
            LoadTexturePanel();
            pbMap.Width = _currentMap.ContentMap.MapWidthInPixels;
            pbMap.Height = _currentMap.ContentMap.MapHeightInPixels;
            DrawMap();
            tsbHideBaseLayer.Enabled = true;
            tsbHideSecondBaseLayer.Enabled = true;
            tsbHideFringeLayer.Enabled = true;
            tsbHideObjectLayer.Enabled = true;
            toolStripDropDownButton1.Enabled = true;
        }

        private void LoadTexturePanel()
        {
            int textureTileWidth = _currentMap.Texture.Width / _currentMap.ContentMap.TileWidth;
            int textureTileHeight = _currentMap.Texture.Height / _currentMap.ContentMap.TileHeight;

            for (int y = 0; y < textureTileHeight; ++y)
            {
                for (int x = 0; x < textureTileWidth; ++x)
                {
                    Rectangle bounds = new Rectangle(
                        x * _currentMap.ContentMap.TileWidth,
                        y * _currentMap.ContentMap.TileHeight,
                        _currentMap.ContentMap.TileWidth,
                        _currentMap.ContentMap.TileHeight);
                    Image bgImage = CropImage(_currentMap.Texture.Clone() as Image, bounds);
                    Button b = new Button()
                        {
                            Name = (x + (y * textureTileWidth)).ToString(),
                            BackgroundImage = bgImage,
                            Text = "",
                            Location = new Point(x * 32, y * 32),
                            Width = 32,
                            Height = 32,
                        };
                    b.MouseDown += new MouseEventHandler(OnTile_Click);
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderColor = Color.White;
                    pnlTexture.Controls.Add(b);
                }
            }
        }

        protected void OnTile_Click(object sender, MouseEventArgs e)
        {
            int index = Int32.Parse(((Button)sender).Name);
            if (_indexes.Contains(index))
                _indexes.Remove(index);
            else
            {
                _indexes.Add(index);
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    _lastClicked = index;
            }
            _indexes.Sort();
            ((Button)sender).FlatAppearance.BorderColor = (_indexes.Contains(index)) ? Color.Blue : Color.White;
        }

        private Image CropImage(Image img, Rectangle rect)
        {
            Bitmap crop = new Bitmap(rect.Width, rect.Height, PixelFormat.Alpha | img.PixelFormat);
            using (Graphics g = Graphics.FromImage(crop))
            {
                g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            }
            return crop;
        }

        private void tsbHideBaseLayer_Click(object sender, EventArgs e)
        {
            _hideLayers[0] = !_hideLayers[0];
            tsbHideBaseLayer.Checked = _hideLayers[0];
            DrawMap();
        }

        private void tsbHideSecondBaseLayer_Click(object sender, EventArgs e)
        {
            _hideLayers[1] = !_hideLayers[1];
            tsbHideSecondBaseLayer.Checked = _hideLayers[1];
            DrawMap();
        }

        private void tsbHideFringeLayer_Click(object sender, EventArgs e)
        {
            _hideLayers[2] = !_hideLayers[2];
            tsbHideFringeLayer.Checked = _hideLayers[2];
            DrawMap();
        }

        private void tsbHideObjectLayer_Click(object sender, EventArgs e)
        {
            _hideLayers[3] = !_hideLayers[3];
            tsbHideObjectLayer.Checked = _hideLayers[3];
            DrawMap();
        }

        private void baseLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedLayer = LAYER.BASE;
            ClearLayerChecked();
            baseLayerToolStripMenuItem.Checked = true;
        }

        private void secondBaseLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedLayer = LAYER.SECONDBASE;
            ClearLayerChecked();
            secondBaseLayerToolStripMenuItem.Checked = true;
        }

        private void fringeLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedLayer = LAYER.FRINGE;
            ClearLayerChecked();
            fringeLayerToolStripMenuItem.Checked = true;
        }

        private void objectLyaerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedLayer = LAYER.OBJECT;
            ClearLayerChecked();
            objectLyaerToolStripMenuItem.Checked = true;
        }

        private void ClearLayerChecked()
        {
            baseLayerToolStripMenuItem.Checked = false;
            secondBaseLayerToolStripMenuItem.Checked = false;
            fringeLayerToolStripMenuItem.Checked = false;
            objectLyaerToolStripMenuItem.Checked = false;
        }

        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void btnErase_Click(object sender, EventArgs e)
        {
            btnErase.Checked = !btnErase.Checked;
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            _indexes.Clear();
            _lastClicked = -1;
            foreach (Button b in pnlTexture.Controls)
                b.FlatAppearance.BorderColor = Color.White;
        }

        private void showPortalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPortalsToolStripMenuItem.Checked = !showPortalsToolStripMenuItem.Checked;
            DrawMap();
        }

        private void portalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentMap == null)
            {
                MessageBox.Show("You must load a map first.");
                return;
            }
            PortalForm portals = new PortalForm(ref _currentMap);
            this.AddOwnedForm(portals);
            portals.Show();
        }
    }
}
