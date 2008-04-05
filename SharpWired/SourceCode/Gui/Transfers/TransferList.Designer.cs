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
            this.transfer1 = new SharpWired.Gui.Transfers.Transfer();
            this.transfer2 = new SharpWired.Gui.Transfers.Transfer();
            this.SuspendLayout();
            // 
            // transfer1
            // 
            this.transfer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.transfer1.Location = new System.Drawing.Point(3, 3);
            this.transfer1.Name = "transfer1";
            this.transfer1.Size = new System.Drawing.Size(464, 63);
            this.transfer1.TabIndex = 0;
            // 
            // transfer2
            // 
            this.transfer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.transfer2.Location = new System.Drawing.Point(3, 72);
            this.transfer2.Name = "transfer2";
            this.transfer2.Size = new System.Drawing.Size(464, 63);
            this.transfer2.TabIndex = 1;
            // 
            // TransferList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.transfer2);
            this.Controls.Add(this.transfer1);
            this.Name = "TransferList";
            this.Size = new System.Drawing.Size(470, 262);
            this.ResumeLayout(false);

        }

        #endregion

        private Transfer transfer1;
        private Transfer transfer2;
    }
}
