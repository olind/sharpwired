using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection;
using SharpWired.Model;
using SharpWired.Connection.Transfers.Entries;
using SharpWired.Model.Files;
using System.Windows.Forms;
using System.IO;
using SharpWired.Connection.Bookmarks;
using SharpWired.Connection.Sockets;
using SharpWired.Gui.Files;
using SharpWired.Controller;
using System.Diagnostics;

namespace SharpWired.Connection.Transfers {
    /// <summary>
    /// Handler for file transfers.
    /// </summary>
    public class FileTransferHandler {
        #region Fields
        private ConnectionManager connectionManager;
        
        //Holds references to all transferrs awaiting a 400 response from the server
        List<TransferEntry> pendingTransfers = new List<TransferEntry>();
        //All transferrs that doesn't have a socket
        List<TransferEntry> idleTransfers = new List<TransferEntry>();
        //All sockets that have a connected transfer socket transferring bytes
        List<TransferEntry> activeTransfers = new List<TransferEntry>();

        private bool downloading = false;
        Queue<UploadEntry> mUploadQueue = new Queue<UploadEntry>();
        List<DownloadEntry> mDownloadQueue = new List<DownloadEntry>();
        #endregion

        public delegate void DownloadChangedDelegate(DownloadEntry d);
        public event DownloadChangedDelegate DownloadAdded;

        #region Constructors

        /// <summary>
        /// Construct and set up.
        /// </summary>
        /// <param name="logic">The SharpWiredModel to get ConnectionManager and more from.</param>
        public FileTransferHandler(ConnectionManager connectionManager) {
            this.connectionManager = connectionManager;

            #region Attach event listeners
            connectionManager.Messages.TransferQueuedEvent += Messages_TransferQueuedEvent;
            connectionManager.Messages.FileOrDirectoryNotFoundEvent += Messages_FileOrDirectoryNotFoundEvent;
            connectionManager.Messages.QueueLimitExceededEvent += Messages_QueueLimitExceededEvent;

            connectionManager.Messages.TransferReadyEvent += OnTransferReady;
            #endregion
        }

        #endregion

        #region Methods
        /// <summary>
        /// Get the upload queue.
        /// </summary>
        /// <returns>UploadEntry array.</returns>
        public UploadEntry[] GetQueuedUploads() {
            return mUploadQueue.ToArray();
        }

        /// <summary>
        /// Get the download queue.
        /// </summary>
        /// <returns>A DownloadEntry array.</returns>
        public DownloadEntry[] GetQueuedDownloads() {
            return mDownloadQueue.ToArray();
        }

        #region Queueing
        /// <summary>
        /// Add an upload to the queue.
        /// </summary>
        /// <param name="server">The server to get file from. May be null.</param>
        /// <param name="pSourceFileName">The full local path.</param>
        /// <param name="pDestinationFolder">The destinatin path on server.</param>
        /// <returns></returns>
        public UploadEntry EnqueueUpload(Server server, string pSourceFileName, FolderNode pDestinationFolder) {
            UploadEntry entry = new UploadEntry(connectionManager, pSourceFileName, pDestinationFolder);
            mUploadQueue.Enqueue(entry);
            return entry;
        }
        #endregion
        #endregion

        /*
        private void TryStartDownload() {
            if (mDownloadQueue.Count > 0 && !downloading) {
                StartDownload(mDownloadQueue[0]);
            }
        }
        
        private void StartDownload(DownloadEntry entry) {
            downloading = true;
            // NOTE: check server here too.
            // No offset for now.
            connectionManager.Commands.Get(entry.FromLocation, 0);
        }*/

        #region Server Message EventHandlers.
        
        void Messages_QueueLimitExceededEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_Messages messageEventArgs) {
            throw new Exception("The method or operation is not implemented.");
        }

        void Messages_FileOrDirectoryNotFoundEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_Messages messageEventArgs) {
            throw new Exception("The method or operation is not implemented.");
        }

        void Messages_TransferQueuedEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_401 messageEventArgs) {
            Debug.WriteLine("The Transfer have been queued! File '" + messageEventArgs.Path + "'.");
        }
        #endregion

        /***************/

        public TransferEntry AddDownload(FileNode source, string destination) {
            DownloadEntry entry = new DownloadEntry(connectionManager, source, destination);
            //TODO: Check filesystem for existing partial download and use offset from that file
            entry.Offset = 0;
            idleTransfers.Add(entry);
            if (DownloadAdded != null)
                DownloadAdded(entry);

            return entry;
        }

        public void StartDownload(TransferEntry entry) {
            idleTransfers.Remove(entry);
            pendingTransfers.Add(entry);
            connectionManager.Commands.Get(((DownloadEntry)entry).Source.Path, entry.Offset);
        }

        void OnTransferReady(SharpWired.MessageEvents.MessageEventArgs_400 messageEventArgs) {
            TransferEntry te = pendingTransfers.Find(t => ((DownloadEntry)t).Source.Path == messageEventArgs.Path);
            te.Id = messageEventArgs.Hash;
            pendingTransfers.Remove(te);
            activeTransfers.Add(te);
            ((DownloadEntry)te).Start();
        }
    }
}

