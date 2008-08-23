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
            this.transferTable = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // transferTable
            // 
            this.transferTable.AutoScroll = true;
            this.transferTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.transferTable.ColumnCount = 1;
            this.transferTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.transferTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transferTable.Location = new System.Drawing.Point(5, 5);
            this.transferTable.Name = "transferTable";
            this.transferTable.RowCount = 1;
            this.transferTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.transferTable.Size = new System.Drawing.Size(460, 252);
            this.transferTable.TabIndex = 0;
            // 
            // TransferList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.transferTable);
            this.Name = "TransferList";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(470, 262);
            this.VisibleChanged += new System.EventHandler(this.TransferList_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel transferTable;




    }
}
