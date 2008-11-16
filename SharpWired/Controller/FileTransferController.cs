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
        Transfers TransferList { get; set; }

        public FileTransferController(SharpWiredModel model) : base(model) {
            TransferList = model.Server.Transfers;

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

        public ITransfer AddDownload(FileSystemEntry node) {
            string target = Path.Combine(defaultDownloadFolder, node.Name);

            if(node is FileNode)
                return TransferList.Add((FileNode)node, target);

            System.Console.WriteLine("Sorry, but we can only download files right now, and not folders.");
            return null;
        }

        public void StartDownload(ITransfer transfer) {
            transfer.Start();
        }

        public void PauseDownload(ITransfer transfer) {
            transfer.Pause();
        }

        public void RemoveDownload(ITransfer transfer) {
            if(transfer.Status == SharpWired.Model.Transfers.Status.Idle)
                TransferList.Remove(transfer);
        }
    }
}
