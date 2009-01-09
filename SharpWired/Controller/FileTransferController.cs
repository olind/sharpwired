using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Model.Files;
using SharpWired.Model;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using SharpWired.Model.Transfers;
using SharpWired.Model;

namespace SharpWired.Controller {
    public class FileTransferController : ControllerBase {

        string defaultDownloadFolder;
        Transfers Transfers { get; set; }

        public FileTransferController(SharpWiredModel model) : base(model) {
            Transfers = model.Server.Transfers;

            defaultDownloadFolder = Path.Combine(Application.StartupPath, "Downloads");

            if (!Directory.Exists(defaultDownloadFolder)) {
                try {
                    DirectoryInfo di = Directory.CreateDirectory(defaultDownloadFolder);
                } catch (Exception e) {
                    Debug.WriteLine("Error trying to create default download dir '"
                        + defaultDownloadFolder + "'.\n" + e.ToString());
                }
            }
        }

        public ITransfer AddDownload(INode node) {
            //TODO: File exists? Resume?
            string target = Path.Combine(defaultDownloadFolder, node.Name);
            return Transfers.Add(node, target);
        }

        public void StartDownload(ITransfer transfer) {
            transfer.Start();
        }

        public void PauseDownload(ITransfer transfer) {
            transfer.Pause();
        }

        public void RemoveDownload(ITransfer transfer) {
            if(transfer.Status == SharpWired.Model.Transfers.Status.Idle)
                Transfers.Remove(transfer);
        }
    }
}
