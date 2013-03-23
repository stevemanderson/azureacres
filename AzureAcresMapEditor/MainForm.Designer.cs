namespace AzureAcresMapEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mnuMainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPortalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbHideBaseLayer = new System.Windows.Forms.ToolStripButton();
            this.tsbHideSecondBaseLayer = new System.Windows.Forms.ToolStripButton();
            this.tsbHideFringeLayer = new System.Windows.Forms.ToolStripButton();
            this.tsbHideObjectLayer = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.baseLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondBaseLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fringeLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectLyaerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnErase = new System.Windows.Forms.ToolStripButton();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.pbMap = new System.Windows.Forms.PictureBox();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.pnlTexture = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTexture = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnLoadTexture = new System.Windows.Forms.Button();
            this.mnuMainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMainMenu
            // 
            this.mnuMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.mapToolStripMenuItem});
            this.mnuMainMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMainMenu.Name = "mnuMainMenu";
            this.mnuMainMenu.Size = new System.Drawing.Size(1199, 24);
            this.mnuMainMenu.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveMapToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.saveMapToolStripMenuItem.Text = "&Save Map";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.saveMapToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem,
            this.showPortalsToolStripMenuItem,
            this.portalsToolStripMenuItem});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "&Map";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.propertiesToolStripMenuItem.Text = "&Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // showPortalsToolStripMenuItem
            // 
            this.showPortalsToolStripMenuItem.Name = "showPortalsToolStripMenuItem";
            this.showPortalsToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.showPortalsToolStripMenuItem.Text = "Show &Portals";
            this.showPortalsToolStripMenuItem.Click += new System.EventHandler(this.showPortalsToolStripMenuItem_Click);
            // 
            // portalsToolStripMenuItem
            // 
            this.portalsToolStripMenuItem.Name = "portalsToolStripMenuItem";
            this.portalsToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.portalsToolStripMenuItem.Text = "Portals";
            this.portalsToolStripMenuItem.Click += new System.EventHandler(this.portalsToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.pnlMap);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnLoadTexture);
            this.splitContainer1.Panel2.Controls.Add(this.btnClearSelection);
            this.splitContainer1.Panel2.Controls.Add(this.pnlTexture);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1199, 437);
            this.splitContainer1.SplitterDistance = 860;
            this.splitContainer1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbHideBaseLayer,
            this.tsbHideSecondBaseLayer,
            this.tsbHideFringeLayer,
            this.tsbHideObjectLayer,
            this.toolStripDropDownButton1,
            this.btnErase});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(566, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbHideBaseLayer
            // 
            this.tsbHideBaseLayer.Enabled = false;
            this.tsbHideBaseLayer.Image = global::AzureAcresMapEditor.Properties.Resources.layer_new;
            this.tsbHideBaseLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHideBaseLayer.Name = "tsbHideBaseLayer";
            this.tsbHideBaseLayer.Size = new System.Drawing.Size(110, 22);
            this.tsbHideBaseLayer.Text = "Hide Base Layer";
            this.tsbHideBaseLayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbHideBaseLayer.Click += new System.EventHandler(this.tsbHideBaseLayer_Click);
            // 
            // tsbHideSecondBaseLayer
            // 
            this.tsbHideSecondBaseLayer.Enabled = false;
            this.tsbHideSecondBaseLayer.Image = global::AzureAcresMapEditor.Properties.Resources.layer_new;
            this.tsbHideSecondBaseLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHideSecondBaseLayer.Name = "tsbHideSecondBaseLayer";
            this.tsbHideSecondBaseLayer.Size = new System.Drawing.Size(152, 22);
            this.tsbHideSecondBaseLayer.Text = "Hide Second Base Layer";
            this.tsbHideSecondBaseLayer.Click += new System.EventHandler(this.tsbHideSecondBaseLayer_Click);
            // 
            // tsbHideFringeLayer
            // 
            this.tsbHideFringeLayer.Enabled = false;
            this.tsbHideFringeLayer.Image = global::AzureAcresMapEditor.Properties.Resources.layer_new;
            this.tsbHideFringeLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHideFringeLayer.Name = "tsbHideFringeLayer";
            this.tsbHideFringeLayer.Size = new System.Drawing.Size(119, 22);
            this.tsbHideFringeLayer.Text = "Hide Fringe Layer";
            this.tsbHideFringeLayer.Click += new System.EventHandler(this.tsbHideFringeLayer_Click);
            // 
            // tsbHideObjectLayer
            // 
            this.tsbHideObjectLayer.Enabled = false;
            this.tsbHideObjectLayer.Image = global::AzureAcresMapEditor.Properties.Resources.layer_new;
            this.tsbHideObjectLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHideObjectLayer.Name = "tsbHideObjectLayer";
            this.tsbHideObjectLayer.Size = new System.Drawing.Size(121, 22);
            this.tsbHideObjectLayer.Text = "Hide Object Layer";
            this.tsbHideObjectLayer.Click += new System.EventHandler(this.tsbHideObjectLayer_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.baseLayerToolStripMenuItem,
            this.secondBaseLayerToolStripMenuItem,
            this.fringeLayerToolStripMenuItem,
            this.objectLyaerToolStripMenuItem});
            this.toolStripDropDownButton1.Enabled = false;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // baseLayerToolStripMenuItem
            // 
            this.baseLayerToolStripMenuItem.Checked = true;
            this.baseLayerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.baseLayerToolStripMenuItem.Name = "baseLayerToolStripMenuItem";
            this.baseLayerToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.baseLayerToolStripMenuItem.Text = "&Base Layer";
            this.baseLayerToolStripMenuItem.Click += new System.EventHandler(this.baseLayerToolStripMenuItem_Click);
            // 
            // secondBaseLayerToolStripMenuItem
            // 
            this.secondBaseLayerToolStripMenuItem.Name = "secondBaseLayerToolStripMenuItem";
            this.secondBaseLayerToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.secondBaseLayerToolStripMenuItem.Text = "&Second Base Layer";
            this.secondBaseLayerToolStripMenuItem.Click += new System.EventHandler(this.secondBaseLayerToolStripMenuItem_Click);
            // 
            // fringeLayerToolStripMenuItem
            // 
            this.fringeLayerToolStripMenuItem.Name = "fringeLayerToolStripMenuItem";
            this.fringeLayerToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.fringeLayerToolStripMenuItem.Text = "&Fringe Layer";
            this.fringeLayerToolStripMenuItem.Click += new System.EventHandler(this.fringeLayerToolStripMenuItem_Click);
            // 
            // objectLyaerToolStripMenuItem
            // 
            this.objectLyaerToolStripMenuItem.Name = "objectLyaerToolStripMenuItem";
            this.objectLyaerToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.objectLyaerToolStripMenuItem.Text = "&Object Layer";
            this.objectLyaerToolStripMenuItem.Click += new System.EventHandler(this.objectLyaerToolStripMenuItem_Click);
            // 
            // btnErase
            // 
            this.btnErase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnErase.Image = global::AzureAcresMapEditor.Properties.Resources.clear;
            this.btnErase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnErase.Name = "btnErase";
            this.btnErase.Size = new System.Drawing.Size(23, 22);
            this.btnErase.Click += new System.EventHandler(this.btnErase_Click);
            // 
            // pnlMap
            // 
            this.pnlMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMap.AutoScroll = true;
            this.pnlMap.Controls.Add(this.pbMap);
            this.pnlMap.Location = new System.Drawing.Point(3, 33);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(857, 404);
            this.pnlMap.TabIndex = 0;
            // 
            // pbMap
            // 
            this.pbMap.Location = new System.Drawing.Point(0, 0);
            this.pbMap.Name = "pbMap";
            this.pbMap.Size = new System.Drawing.Size(100, 50);
            this.pbMap.TabIndex = 0;
            this.pbMap.TabStop = false;
            this.pbMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMap_MouseDown);
            this.pbMap.MouseLeave += new System.EventHandler(this.pbMap_MouseLeave);
            this.pbMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMap_MouseMove);
            this.pbMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMap_MouseUp);
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearSelection.Location = new System.Drawing.Point(88, 409);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(95, 23);
            this.btnClearSelection.TabIndex = 2;
            this.btnClearSelection.Text = "Clear Selection";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // pnlTexture
            // 
            this.pnlTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTexture.AutoScroll = true;
            this.pnlTexture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTexture.Location = new System.Drawing.Point(0, 21);
            this.pnlTexture.Name = "pnlTexture";
            this.pnlTexture.Size = new System.Drawing.Size(335, 384);
            this.pnlTexture.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTexture);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(335, 21);
            this.panel1.TabIndex = 0;
            // 
            // lblTexture
            // 
            this.lblTexture.AutoSize = true;
            this.lblTexture.Location = new System.Drawing.Point(4, 4);
            this.lblTexture.Name = "lblTexture";
            this.lblTexture.Size = new System.Drawing.Size(43, 13);
            this.lblTexture.TabIndex = 0;
            this.lblTexture.Text = "Texture";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "*.xml|*.*";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "*.xml|*.*";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // btnLoadTexture
            // 
            this.btnLoadTexture.Location = new System.Drawing.Point(7, 409);
            this.btnLoadTexture.Name = "btnLoadTexture";
            this.btnLoadTexture.Size = new System.Drawing.Size(75, 23);
            this.btnLoadTexture.TabIndex = 3;
            this.btnLoadTexture.Text = "Load";
            this.btnLoadTexture.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 461);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mnuMainMenu);
            this.MainMenuStrip = this.mnuMainMenu;
            this.Name = "MainForm";
            this.Text = "Azure Acres Map Editor";
            this.mnuMainMenu.ResumeLayout(false);
            this.mnuMainMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel pnlTexture;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTexture;
        private System.Windows.Forms.Panel pnlMap;
        private System.Windows.Forms.PictureBox pbMap;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbHideBaseLayer;
        private System.Windows.Forms.ToolStripButton tsbHideFringeLayer;
        private System.Windows.Forms.ToolStripButton tsbHideObjectLayer;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem baseLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fringeLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectLyaerToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnErase;
        private System.Windows.Forms.Button btnClearSelection;
        private System.Windows.Forms.ToolStripButton tsbHideSecondBaseLayer;
        private System.Windows.Forms.ToolStripMenuItem secondBaseLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPortalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portalsToolStripMenuItem;
        private System.Windows.Forms.Button btnLoadTexture;
    }
}

