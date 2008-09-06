using System;
using System.Collections.Generic;
using System.Drawing;
using SharpWired.Controller;
using SharpWired.Model;
using SharpWired.Model.Transfers;

namespace SharpWired.Gui.Transfers {
    public partial class TransferList : SharpWiredGuiBase {
        List<TransferItem> Items { get; set; }
        delegate void ItemModifier(TransferItem ti, bool odd);

        public TransferList() {
            InitializeComponent();
            this.Items = new List<TransferItem>();
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
            AddTransferItem(t);
        }

        void AddTransferItem(Transfer t) {

            TransferItem ti = new TransferItem();
            ti.Init(t);

            this.Items.Add(ti);

            Repaint();
        }

        void Repaint() {
            int currentPos = 0;

            ModifyItems(
                delegate(TransferItem current, bool odd) {
                    current.Width = this.transferScrollPanel.Width - 2;
                    current.Top = currentPos * current.Height;
                    current.Clicked += OnItemClicked;

                    SetItemColor(current, odd);
                    this.transferScrollPanel.Controls.Add(current);

                    currentPos += 1;
                    odd = !odd;
                }
            );
        }

        void OnClicked(object sender, EventArgs e) {
            ModifyItems(
                delegate(TransferItem current, bool odd) {
                    current.Selected = false;
                    SetItemColor(current, odd);
                }
            );
        }


        void OnItemClicked(TransferItem ti, bool control) {
            ModifyItems(
                delegate(TransferItem current, bool odd) {
                    bool clicked = ti == current;

                    if (clicked && control)
                        current.Selected = !current.Selected;
                    else if (clicked)
                        current.Selected = true;
                    else if (!control)
                        current.Selected = false;

                    SetItemColor(current, odd);
                }
            );
        }

        void SetItemColor(TransferItem ti, bool odd) {
            if (ti.Selected)
                ti.BackColor = SystemColors.MenuHighlight;
            else if (odd)
                ti.BackColor = SystemColors.Window;
            else
                ti.BackColor = SystemColors.Control;
        }

        void ModifyItems(ItemModifier modify) {
            bool odd = true;
            foreach (TransferItem current in Items) {
                modify(current, odd);
                odd = !odd;
            }
        }
    }
}
