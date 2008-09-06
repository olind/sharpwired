namespace SharpWired.Gui.Transfers {
    partial class TransferList {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.transferScrollPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // transferScrollPanel
            // 
            this.transferScrollPanel.AutoScroll = true;
            this.transferScrollPanel.BackColor = System.Drawing.SystemColors.Window;
            this.transferScrollPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.transferScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transferScrollPanel.Location = new System.Drawing.Point(0, 0);
            this.transferScrollPanel.Margin = new System.Windows.Forms.Padding(0);
            this.transferScrollPanel.Name = "transferScrollPanel";
            this.transferScrollPanel.Size = new System.Drawing.Size(641, 418);
            this.transferScrollPanel.TabIndex = 0;
            this.transferScrollPanel.Click += new System.EventHandler(this.OnClicked);
            // 
            // TransferList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.transferScrollPanel);
            this.Name = "TransferList";
            this.Size = new System.Drawing.Size(641, 418);
            this.VisibleChanged += new System.EventHandler(this.TransferList_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel transferScrollPanel;






    }
}
