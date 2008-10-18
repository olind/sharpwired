using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model;
using SharpWired.Controller;
using SharpWired.Model.Transfers;
using System.Diagnostics;

namespace SharpWired.Gui.Transfers {
    public partial class TransferItem : SharpWiredGuiBase {
        Transfer transfer;
        public bool Selected { get; set; }

        public delegate void ClickedArgs(TransferItem ti, bool control);
        public event ClickedArgs Clicked;

        public TransferItem() {
            InitializeComponent();
        }

        public void Init(Transfer t) {
            this.transfer = t;

            UpdateDelegate update = delegate() {
                this.fileName.Text = t.Source.Name;
                this.info.Text = "";
                this.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            };

            UpdateControl(update);
        }

        private void OnClicked(object sender, EventArgs e) {
            bool control = false;

            // TODO: Handle shift selecting.
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                control = true;
                
            if (Clicked != null)
                Clicked(this, control);
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            controller.FileTransferController.RemoveDownload(transfer);
        }

        internal void Repaint() {
            progressBar.Value = (int)(transfer.Progress * 1000.0);

            string timeLeft;
            TimeSpan? estimateTimeLeft = transfer.EstimatedTimeLeft;
            if (estimateTimeLeft == null)
                timeLeft = "∞";
            else
                timeLeft = GuiUtil.FormatTimeSpan((TimeSpan)estimateTimeLeft);

            info.Text = timeLeft + " remaining — " +
                     GuiUtil.FormatByte(transfer.Received) + " of " + GuiUtil.FormatByte(transfer.Size) +
                     " (" + GuiUtil.FormatByte(transfer.Speed) + "/s" + ")";
        }
    }
}
