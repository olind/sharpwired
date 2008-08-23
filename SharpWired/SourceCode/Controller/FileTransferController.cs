using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Model.Files;
using SharpWired.Model;
using SharpWired.Connection.Transfers;
using SharpWired.Connection.Transfers.Entries;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace SharpWired.Controller {
    public class FileTransferController : ControllerBase {

        string defaultDownloadFolder;

        public FileTransferController(SharpWiredModel model) : base(model) {
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

        public TransferEntry AddDownload(FileSystemEntry node) {
            TransferEntry entry = null;
            if (node is FileNode)
                entry = fileTransferHandler.AddDownload(node as FileNode, defaultDownloadFolder + "\\" + node.Name);
            else
                System.Console.WriteLine("Sorry, but we can only download files right now, and not folders.");

            return entry;
        }

        public void StartDownload(TransferEntry entry) {
            fileTransferHandler.StartDownload(entry);
        }

        public void PauseDownload(FileSystemEntry node) { }

        public void RemoveDownload(FileSystemEntry node) { }
    }
}
