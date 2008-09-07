using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model;
using SharpWired.Controller;

namespace SharpWired.Gui.Transfers {
    public partial class TransferContainer : SharpWiredGuiBase {
        public TransferContainer() {
            InitializeComponent();
        }

        public void Init(SharpWiredModel model, SharpWiredController controller) {
            this.transferList1.Init(model, controller);
        }
    }
}
