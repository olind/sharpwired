using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SharpWired.Model;
using SharpWired.Model.Files;
using SharpWired.Model.Transfers;

namespace SharpWired.Controller {
    public class FileTransferController : ControllerBase {
        private readonly string defaultDownloadFolder;
        private Transfers Transfers { get; set; }

        public FileTransferController(SharpWiredModel model) : base(model) {
            Transfers = model.Server.Transfers;

            defaultDownloadFolder = Path.Combine(Application.StartupPath, "Downloads");

            if (!Directory.Exists(defaultDownloadFolder)) {
                try {
                    var di = Directory.CreateDirectory(defaultDownloadFolder);
                } catch (Exception e) {
                    Debug.WriteLine("Error trying to create default download dir '"
                                    + defaultDownloadFolder + "'.\n" + e);
                }
            }
        }

        public ITransfer AddDownload(INode node) {
            //TODO: File exists? Resume?
            var target = Path.Combine(defaultDownloadFolder, node.Name);
            return Transfers.Add(node, target);
        }

        public void StartDownload(ITransfer transfer) {
            transfer.Start();
        }

        public void PauseDownload(ITransfer transfer) {
            transfer.Pause();
        }

        public void RemoveDownload(ITransfer transfer) {
            if (transfer.Status == Status.Idle) {
                Transfers.Remove(transfer);
            }
        }
    }
}