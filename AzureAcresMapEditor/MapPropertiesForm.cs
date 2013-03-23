using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AzureAcresMapEditor
{
    public partial class MapPropertiesForm : Form
    {
        private Map _currentMap;
        public Map CurrentMap
        {
            get { return _currentMap; }
            set { _currentMap = value; }
        }

        public MapPropertiesForm(Map currentMap)
        {
            InitializeComponent();
            CurrentMap = currentMap;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _currentMap.ContentMap.Name = txtMapName.Text;
        }

        private void MapPropertiesForm_Load(object sender, EventArgs e)
        {
            if (CurrentMap != null)
                txtMapName.Text = CurrentMap.ContentMap.Name;
        }
    }
}
