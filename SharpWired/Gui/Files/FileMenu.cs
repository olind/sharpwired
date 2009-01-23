using System;
using System.Drawing;
using System.Windows.Forms;
using SharpWired.Controller;
using SharpWired.Model.Files;

namespace SharpWired.Gui.Files {
    public class FileMenu : ContextMenu {
        private MenuItem DownloadItem { get; set; }
        private SharpWiredController Controller { get; set; }
        private Control Parent { get; set; }

        public FileMenu(SharpWiredController controller, Control parent) {
            Controller = controller;
            Parent = parent;

            MenuItems.Add(new MenuItem("&Refresh", OnRefresh));
            MenuItems.Add(new MenuItem("-"));

            DownloadItem = new MenuItem("&Download", OnDownload);
            MenuItems.Add(DownloadItem);
            DownloadItem.Visible = false;
        }

        public void Show(Control control, Point location) {
            var details = Parent as FolderListing;

            if (details != null) {
                if (details.SelectedItems.Count > 0) {
                    DownloadItem.Visible = true;
                } else {
                    DownloadItem.Visible = false;
                }

                base.Show(control, location);
            }
        }

        private void OnRefresh(Object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void OnDownload(Object sender, EventArgs e) {
            var details = Parent as FolderListing;

            if (details != null) {
                foreach (var n in details.SelectedItems) {
                    Download(n);
                }
            }
        }

        private void Download(INode node) {
            var entry = Controller.FileTransferController.AddDownload(node);
            Controller.FileTransferController.StartDownload(entry);
        }
    }
}