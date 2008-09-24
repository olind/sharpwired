using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection;
using SharpWired.Model.Files;
using System.Diagnostics;
using System.IO;
using SharpWired.Connection.Sockets;
using System.Windows.Forms;
using SharpWired.Connection.Bookmarks;

namespace SharpWired.Connection.Transfers.Entries {
    /// <summary>
    /// A entry in the download queue.
    /// </summary>
    public class DownloadEntry : TransferEntry {
        private BinarySecureSocket Socket { get; set; }

        public DownloadEntry(Server transferServer, FileNode source,
                             string destination, string hash, Int64 offset) {
            Debug.WriteLine("Transfer is ready! File '" + source.Name + "', with ID '" + hash + "'.");

            FileStream fileStream = new FileStream(destination, FileMode.Append);

            this.Socket = new BinarySecureSocket();
            this.Socket.DataReceivedDoneEvent += OnDataReceivedDone;
            this.Socket.Connect(transferServer,
                fileStream, source.Size, offset);

            this.Socket.SendMessage("TRANSFER" + Utility.SP + hash);
        }

        public Int64 BytesReceived {
            get {
                if (Socket != null)
                    return Socket.BytesTransferred;
                else
                    return -1;
            }
        }

        public delegate void CompletedDelegate();
        public event CompletedDelegate Completed;

        void OnDataReceivedDone() {
            if (Socket != null)
                Socket.DataReceivedDoneEvent -= OnDataReceivedDone;
            if (Completed != null)
                Completed();
        }
    }
}