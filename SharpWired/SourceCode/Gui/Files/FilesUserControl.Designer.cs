namespace SharpWired.Gui.Files
{
    partial class FilesUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fileTreeControl = new SharpWired.Gui.Files.FileTreeControl();
            this.fileDetailsControl = new SharpWired.Gui.Files.FileDetailsControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fileTreeControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fileDetailsControl);
            this.splitContainer1.Size = new System.Drawing.Size(404, 232);
            this.splitContainer1.SplitterDistance = 126;
            this.splitContainer1.TabIndex = 6;
            // 
            // fileTreeControl
            // 
            this.fileTreeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTreeControl.Location = new System.Drawing.Point(0, 0);
            this.fileTreeControl.Name = "fileTreeControl";
            this.fileTreeControl.Size = new System.Drawing.Size(126, 232);
            this.fileTreeControl.TabIndex = 6;
            // 
            // fileDetailsControl
            // 
            this.fileDetailsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileDetailsControl.Location = new System.Drawing.Point(0, 0);
            this.fileDetailsControl.Name = "fileDetailsControl";
            this.fileDetailsControl.Size = new System.Drawing.Size(274, 232);
            this.fileDetailsControl.TabIndex = 0;
            // 
            // FilesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FilesUserControl";
            this.Size = new System.Drawing.Size(410, 238);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private FileDetailsControl fileDetailsControl;
        private FileTreeControl fileTreeControl;
    }
}
