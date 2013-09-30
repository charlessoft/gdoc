namespace Google_DocumentsList
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.documentsView = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.Export = new System.Windows.Forms.Button();
            this.exportDialog = new System.Windows.Forms.SaveFileDialog();
            this.ShowRevisions = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // documentsView
            // 
            this.documentsView.Dock = System.Windows.Forms.DockStyle.Left;
            this.documentsView.ImageIndex = 0;
            this.documentsView.ImageList = this.imageList;
            this.documentsView.Location = new System.Drawing.Point(0, 0);
            this.documentsView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.documentsView.Name = "documentsView";
            this.documentsView.SelectedImageIndex = 0;
            this.documentsView.Size = new System.Drawing.Size(295, 431);
            this.documentsView.TabIndex = 1;
            this.documentsView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.documentsView_AfterCollapse);
            this.documentsView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.documentsView_AfterExpand);
            this.documentsView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.documentsView_AfterSelect);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "CLSDFOLD.BMP");
            this.imageList.Images.SetKeyName(1, "OPENFOLD.BMP");
            this.imageList.Images.SetKeyName(2, "DOC.BMP");
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(296, 0);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(317, 386);
            this.propertyGrid1.TabIndex = 2;
            // 
            // Export
            // 
            this.Export.Enabled = false;
            this.Export.Location = new System.Drawing.Point(37, 9);
            this.Export.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(95, 25);
            this.Export.TabIndex = 3;
            this.Export.Text = "&Export";
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // ShowRevisions
            // 
            this.ShowRevisions.Enabled = false;
            this.ShowRevisions.Location = new System.Drawing.Point(158, 9);
            this.ShowRevisions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ShowRevisions.Name = "ShowRevisions";
            this.ShowRevisions.Size = new System.Drawing.Size(126, 25);
            this.ShowRevisions.TabIndex = 4;
            this.ShowRevisions.Text = "&Show Revisions";
            this.ShowRevisions.UseVisualStyleBackColor = true;
            this.ShowRevisions.Click += new System.EventHandler(this.ShowRevisions_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Export);
            this.panel1.Controls.Add(this.ShowRevisions);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(295, 382);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 49);
            this.panel1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 431);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.documentsView);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Not logged in yet";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView documentsView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.SaveFileDialog exportDialog;
        private System.Windows.Forms.Button ShowRevisions;
        private System.Windows.Forms.Panel panel1;
    }
}

