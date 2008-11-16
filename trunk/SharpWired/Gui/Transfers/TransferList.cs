using System;
using System.Collections.Generic;
using System.Drawing;
using SharpWired.Controller;
using SharpWired.Model;
using SharpWired.Model.Transfers;
using System.Diagnostics;

namespace SharpWired.Gui.Transfers {
    public partial class TransferList : SharpWiredGuiBase {
        List<TransferItem> Items { get; set; }
        delegate void ItemModifier(TransferItem ti, bool odd);

        public TransferList() {
            InitializeComponent();
            this.Items = new List<TransferItem>();
        }

        public override void Init() {
            base.Init();

            // TODO: Ugly to know about parent! Fix!
            Parent.VisibleChanged += TransferList_VisibleChanged;
        }

        private void TransferList_VisibleChanged(object sender, EventArgs e) {
            if(Parent.Visible == true) 
                RefreshStart();
            else
                RefreshStop();
        }

        protected override void OnOnline() {
            Model.Server.Transfers.TransferAdded += OnTransferAdded;
        }

        protected override void OnOffline() {
            RefreshStop();
            Model.Server.Transfers.TransferAdded -= OnTransferAdded;
        }

        void OnTransferAdded(ITransfer t) {
            AddTransferItem(t);
        }

        void AddTransferItem(ITransfer t) {

            TransferItem ti = new TransferItem();
            ti.Init(t);

            Items.Add(ti);
            RefreshStart();
        }

        void Repaint() {
            int currentPos = 0;

            ModifyItems(
                delegate(TransferItem current, bool odd) {
                    current.Width = this.transferScrollPanel.Width - 2;
                    current.Top = currentPos * current.Height;
                    current.Clicked += OnItemClicked; // TODO: This shouldn't be done on each repaint!
                    current.Repaint();

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

        private void refreshTimer_Tick(object sender, EventArgs e) {
            Repaint();
        }

        private void RefreshStart() {
            if(Visible == true && Items.Count > 0 && !refreshTimer.Enabled)
                refreshTimer.Start();
        }

        private void RefreshStop() {
            if(refreshTimer.Enabled)
                refreshTimer.Stop();
        }
    }
}
