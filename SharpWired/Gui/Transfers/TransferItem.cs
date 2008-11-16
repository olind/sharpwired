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
using SharpWired.Gui.Resources.Icons;

namespace SharpWired.Gui.Transfers {
    public partial class TransferItem : SharpWiredGuiBase {
        ITransfer transfer;
        public bool Selected { get; set; }
        public Status Status { get { return transfer.Status; } }
        
        public delegate void ClickedArgs(TransferItem ti, bool control);
        public event ClickedArgs Clicked;

        bool frozen = true; //Stops repainting a paused/idle transfer

        public TransferItem() {
            InitializeComponent();
        }

        public void Init(ITransfer t) {
            this.transfer = t;

            IconHandler icons = IconHandler.Instance;
            this.pauseButton.Image = icons.MediaPlaybackPause;
            this.deleteButton.Image = icons.ProcessStop;

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

        private void pauseButton_Click(object sender, EventArgs e) {
            if (transfer.Status == Status.Idle) {
                //TODO
            } else {
                Controller.FileTransferController.PauseDownload(transfer);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            pauseButton_Click(sender, e);
            Controller.FileTransferController.RemoveDownload(transfer);
        }

        internal void Repaint() {
            if (transfer.Status == Status.Idle) {
                if (!frozen) {
                    pauseButton.Image = IconHandler.Instance.MediaPlaybackStart;
                    this.pauseButton.Enabled = false;

                    info.Text = "Paused — " + GuiUtil.FormatByte(transfer.Received)
                        + " of " + GuiUtil.FormatByte(transfer.Size);
                    frozen = true;
                }
            } else {                
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

                frozen = false;
            }
        }
    }
}
