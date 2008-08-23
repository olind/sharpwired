using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection;
using SharpWired.Model.Files;
using System.Diagnostics;
using System.IO;
using SharpWired.Connection.Sockets;
using System.Windows.Forms;

namespace SharpWired.Connection.Transfers.Entries {
    /// <summary>
    /// A entry in the download queue.
    /// </summary>
    public class DownloadEntry : TransferEntry {

        protected FileSystemEntry source;
        protected string destination;
        BinarySecureSocket socket;

        public DownloadEntry(ConnectionManager connectionManager, 
                FileNode source, string destination) : base(connectionManager) {
            this.destination = destination;
            this.source = source;
        }

        public virtual FileSystemEntry Source {
            get { return source; }
            set { source = value; }
        }
        public virtual string Destination {
            get { return destination; }
            set { destination = value; }
        }

        public Int64 BytesReceived {
            get {
                if (socket != null)
                    return socket.BytesTransferred;
                else
                    return -1;
            }
        }

        public delegate void CompletedDelegate();
        public event CompletedDelegate Completed;

        public void Start() {
            Debug.WriteLine("Transfer is ready! File '" + Source + "', with ID '" + Id + "'.");

            FileStream fileStream = new FileStream(Destination, FileMode.Append);
            socket = new BinarySecureSocket();
            socket.DataReceivedDoneEvent += OnDataReceivedDone;
            socket.Connect(connectionManager.CurrentBookmark.Transfer, 
                fileStream, ((FileNode)Source).Size, Offset);

            socket.SendMessage("TRANSFER" + Utility.SP + Id);
        }

        void OnDataReceivedDone() {
            if (socket != null)
                socket.DataReceivedDoneEvent -= OnDataReceivedDone;
            if (Completed != null)
                Completed();
        }
    }
}