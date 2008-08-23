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

namespace SharpWired.Gui.Transfers {
    public partial class TransferItem : SharpWiredGuiBase {
        Transfer transfer;

        public TransferItem() {
            InitializeComponent();
        }

        public void Init(Transfer t) {
            this.transfer = t;

            UpdateDelegate update = delegate() {
                this.fileName.Text = t.ServerFilePath.Name;
                this.info.Text = "";
                this.Dock = DockStyle.Fill;
            };

            UpdateControl(update);
        }
    }
}
