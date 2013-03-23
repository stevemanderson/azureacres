using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using AzureAcresData;

namespace AzureAcres
{
    public class TopInventory
    {
        private Texture2D _selectedTexture;
        private Texture2D _texture;
        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private string _selectedItemName;
        public int Width { get { return _texture.Width; } }
        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; }
        }
        private Vector2 _hoveringItem;

        public TopInventory(ContentManager Content)
        {
            _texture = Content.Load<Texture2D>("Textures/HUD/InventoryBoxes");
            _selectedTexture = Content.Load<Texture2D>("Textures/HUD/SelectedItem");
            _hoveringItem = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch spriteBatch, float elapsedMilliseconds)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
            Vector2 drawpos = new Vector2(Position.X, Position.Y + 32);
            for (int i = 0; i < Session.TopInventory.Length; ++i)
            {
                ICollectableItem item = Session.TopInventory[i];
                if (i == _selectedIndex)
                {
                    if(item != null)
                        _selectedItemName = item.GetItemName();
                    spriteBatch.Draw(_selectedTexture, drawpos, Color.White);
                }

                if (item != null)
                {
                    Texture2D tex = item.GetItemTexture();
                    if (tex != null)
                        spriteBatch.Draw(item.GetItemTexture(), drawpos, Color.White);
                }
                drawpos += new Vector2(32, 0);
            }
            Fonts.DrawCenteredText(spriteBatch, Fonts.DebugFont, _selectedItemName, Position + new Vector2(Width / 2, 15), Color.Blue);
        }
    }
}
