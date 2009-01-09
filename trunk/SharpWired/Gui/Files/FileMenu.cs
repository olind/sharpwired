using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SharpWired.Controller;
using SharpWired.Model.Files;
using SharpWired.Model.Transfers;

namespace SharpWired.Gui.Files {
    public class FileMenu : ContextMenu {
        MenuItem DownloadItem { get; set; }
        SharpWiredController Controller { get; set; }
        Control Parent { get; set; }

        public FileMenu(SharpWiredController controller, Control parent) : base() {
            Controller = controller;
            Parent = parent;

            MenuItems.Add(new MenuItem("&Refresh", OnRefresh));
            MenuItems.Add(new MenuItem("-"));

            DownloadItem = new MenuItem("&Download", OnDownload);
            MenuItems.Add(DownloadItem);
            DownloadItem.Visible = false;
        }

        public void Show(Control control, Point location) {
            FolderListing details = Parent as FolderListing;

            if(details != null) {
                if(details.SelectedItems.Count > 0)
                    DownloadItem.Visible = true;
                else
                    DownloadItem.Visible = false;

                base.Show(control, location);
            }
        }

        private void OnRefresh(Object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void OnDownload(Object sender, EventArgs e) {
            FolderListing details = Parent as FolderListing;

            if(details != null) {
                foreach (var n in details.SelectedItems)
                    Download(n);
            }
        }

        private void Download(INode node) {
            ITransfer entry = Controller.FileTransferController.AddDownload(node);
            Controller.FileTransferController.StartDownload(entry);
        }
    }
}
