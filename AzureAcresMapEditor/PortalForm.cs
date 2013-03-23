using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using AzureAcresData;
using System.Windows.Forms;

namespace AzureAcresMapEditor
{
    public partial class PortalForm : Form
    {
        private Map _currentMap;
        public Map CurrentMap
        {
            get { return _currentMap; }
            set { _currentMap = value; }
        }

        private Portal _selectedPortal;
        public Portal SelectedPortal
        {
            get { return _selectedPortal; }
            set { _selectedPortal = value; }
        }

        public PortalForm(ref Map currentMap)
        {
            InitializeComponent();
            _currentMap = currentMap;
        }

        private void PortalForm_Load(object sender, EventArgs e)
        {
            for (int x = 0; x < CurrentMap.ContentMap.MapWidth; ++x)
                cbCoordsX.Items.Add(x);
            for (int y = 0; y < CurrentMap.ContentMap.MapHeight; ++y)
                cbCoordsY.Items.Add(y);
            LoadPortalList();
        }

        private void LoadPortalList()
        {
            lstPortals.Items.Clear();
            foreach (Portal p in CurrentMap.ContentMap.Portals)
                lstPortals.Items.Add(p.Name);
        }

        private Portal LoadPortal(string name)
        {
            Portal portal = CurrentMap.ContentMap.Portals.FirstOrDefault(p => p.Name == name);
            if (portal == null)
                return new Portal();
            else
                return portal;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstPortals_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedPortal = LoadPortal((string)((ListBox)sender).Text);
            try
            {
                txtPortalName.Text = SelectedPortal.Name;
                txtDestinationPortalName.Text = SelectedPortal.ToPortalName;
                txtDestinationMapName.Text = SelectedPortal.ToMapName;
                cbCoordsX.SelectedItem = (int)(SelectedPortal.Coordinates.X / 32);
                cbCoordsY.SelectedItem = (int)(SelectedPortal.Coordinates.Y / 32);
                cbDirection.SelectedItem = SelectedPortal.Direction.ToString();
            }
            catch (Exception ex) { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SelectedPortal == null ||
                String.IsNullOrEmpty(SelectedPortal.Name))
                SelectedPortal = new Portal();
            SelectedPortal.Name = txtPortalName.Text;
            SelectedPortal.ToMapName = txtDestinationMapName.Text;
            SelectedPortal.ToPortalName = txtDestinationPortalName.Text;
            int x = 0;
            int y = 0;
            Int32.TryParse(cbCoordsX.Text, out x);
            Int32.TryParse(cbCoordsY.Text, out y);
            SelectedPortal.Coordinates = new Microsoft.Xna.Framework.Point(x*32, y*32);
            SelectedPortal.Direction = cbDirection.Text;
            if (CurrentMap.ContentMap.Portals.Exists(p => p.Name == SelectedPortal.Name))
            {
                Portal removeP = CurrentMap.ContentMap.Portals.First(p => p.Name == SelectedPortal.Name);
                CurrentMap.ContentMap.Portals.Remove(removeP);

            }
            CurrentMap.ContentMap.Portals.Add(SelectedPortal);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Portal p = new Portal();
            SelectedPortal = p;
            p.Name = "New Portal " + lstPortals.Items.Count.ToString();
            txtPortalName.Text = "New Portal " + lstPortals.Items.Count.ToString();
            CurrentMap.ContentMap.Portals.Add(p);
            LoadPortalList();
            lstPortals.SelectedValue = p.Name;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (SelectedPortal != null)
            {
                CurrentMap.ContentMap.Portals.Remove(SelectedPortal);
                lstPortals.Items.Remove(SelectedPortal.Name);
            }
            LoadPortalList();
        }
    }
}
