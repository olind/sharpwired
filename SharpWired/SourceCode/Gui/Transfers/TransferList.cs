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
    public partial class TransferList : SharpWiredGuiBase {
        public TransferList() {
            InitializeComponent();
        }

        public override void Init(SharpWiredModel model, SharpWiredController controller) {
            base.Init(model, controller);
        }

        private void TransferList_VisibleChanged(object sender, EventArgs e) {
            if (this.Visible == true)
                System.Console.WriteLine("transfer visible");
        }

        protected override void OnOnline() {
            model.Server.Transfers.TransferAdded += OnTransferAdded;
        }

        protected override void OnOffline() {
            model.Server.Transfers.TransferAdded -= OnTransferAdded;
        }

        void OnTransferAdded(Transfer t) {
            TransferItem ti = new TransferItem();
            ti.Init(t);

            transferTable.Controls.Add(ti);
        }
    }
}
