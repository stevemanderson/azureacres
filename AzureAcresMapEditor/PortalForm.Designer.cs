namespace AzureAcresMapEditor
{
    partial class PortalForm
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
            this.lstPortals = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPortalName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDestinationPortalName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDestinationMapName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCoordsX = new System.Windows.Forms.ComboBox();
            this.cbCoordsY = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbDirection = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstPortals
            // 
            this.lstPortals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstPortals.FormattingEnabled = true;
            this.lstPortals.Location = new System.Drawing.Point(12, 12);
            this.lstPortals.Name = "lstPortals";
            this.lstPortals.Size = new System.Drawing.Size(146, 173);
            this.lstPortals.TabIndex = 0;
            this.lstPortals.SelectedIndexChanged += new System.EventHandler(this.lstPortals_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Portal Name";
            // 
            // txtPortalName
            // 
            this.txtPortalName.Location = new System.Drawing.Point(168, 30);
            this.txtPortalName.Name = "txtPortalName";
            this.txtPortalName.Size = new System.Drawing.Size(146, 20);
            this.txtPortalName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destination Portal Name";
            // 
            // txtDestinationPortalName
            // 
            this.txtDestinationPortalName.Location = new System.Drawing.Point(168, 74);
            this.txtDestinationPortalName.Name = "txtDestinationPortalName";
            this.txtDestinationPortalName.Size = new System.Drawing.Size(146, 20);
            this.txtDestinationPortalName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Destination Map Name";
            // 
            // txtDestinationMapName
            // 
            this.txtDestinationMapName.Location = new System.Drawing.Point(320, 74);
            this.txtDestinationMapName.Name = "txtDestinationMapName";
            this.txtDestinationMapName.Size = new System.Drawing.Size(146, 20);
            this.txtDestinationMapName.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Coordinates";
            // 
            // cbCoordsX
            // 
            this.cbCoordsX.FormattingEnabled = true;
            this.cbCoordsX.Location = new System.Drawing.Point(171, 118);
            this.cbCoordsX.Name = "cbCoordsX";
            this.cbCoordsX.Size = new System.Drawing.Size(44, 21);
            this.cbCoordsX.TabIndex = 8;
            // 
            // cbCoordsY
            // 
            this.cbCoordsY.FormattingEnabled = true;
            this.cbCoordsY.Location = new System.Drawing.Point(241, 118);
            this.cbCoordsY.Name = "cbCoordsY";
            this.cbCoordsY.Size = new System.Drawing.Size(44, 21);
            this.cbCoordsY.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(221, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(291, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(317, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Direction";
            // 
            // cbDirection
            // 
            this.cbDirection.FormattingEnabled = true;
            this.cbDirection.Items.AddRange(new object[] {
            "North",
            "South",
            "East",
            "West"});
            this.cbDirection.Location = new System.Drawing.Point(320, 118);
            this.cbDirection.Name = "cbDirection";
            this.cbDirection.Size = new System.Drawing.Size(146, 21);
            this.cbDirection.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(391, 192);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(24, 192);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(63, 23);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(93, 192);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(65, 23);
            this.btnRemove.TabIndex = 17;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // PortalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 227);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbDirection);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbCoordsY);
            this.Controls.Add(this.cbCoordsX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDestinationMapName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDestinationPortalName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPortalName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPortals);
            this.Name = "PortalForm";
            this.Text = "Portals";
            this.Load += new System.EventHandler(this.PortalForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstPortals;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPortalName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDestinationPortalName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDestinationMapName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbCoordsX;
        private System.Windows.Forms.ComboBox cbCoordsY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbDirection;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
    }
}