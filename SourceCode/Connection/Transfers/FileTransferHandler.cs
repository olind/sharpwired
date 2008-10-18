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
        private ConnectionManager connectionManager;
        
        //Holds references to all transferrs awaiting a 400 response from the server
        List<TransferEntry> pendingTransfers = new List<TransferEntry>();
        //All transferrs that doesn'transfer have a socket
        List<TransferEntry> idleTransfers = new List<TransferEntry>();
        //All sockets that have a connected transfer socket transferring bytes
        List<TransferEntry> activeTransfers = new List<TransferEntry>();

        private bool downloading = false;
        Queue<UploadEntry> mUploadQueue = new Queue<UploadEntry>();
        List<DownloadEntry> mDownloadQueue = new List<DownloadEntry>();

        public delegate void DownloadChangedDelegate(DownloadEntry d);
        public event DownloadChangedDelegate DownloadAdded;
        public event DownloadChangedDelegate DownloadRemoved;

        /// <summary>
        /// Construct and set up.
        /// </summary>
        /// <param name="logic">The SharpWiredModel to get ConnectionManager and more from.</param>
        public FileTransferHandler(ConnectionManager connectionManager) {
            this.connectionManager = connectionManager;
            connectionManager.Messages.TransferQueuedEvent += Messages_TransferQueuedEvent;
            connectionManager.Messages.FileOrDirectoryNotFoundEvent += Messages_FileOrDirectoryNotFoundEvent;
            connectionManager.Messages.QueueLimitExceededEvent += Messages_QueueLimitExceededEvent;
        }

        /// <summary>
        /// Request the upload queue.
        /// </summary>
        /// <returns>UploadEntry array.</returns>
        public UploadEntry[] GetQueuedUploads() {
            return mUploadQueue.ToArray();
        }

        /// <summary>
        /// Request the download queue.
        /// </summary>
        /// <returns>A DownloadEntry array.</returns>
        public DownloadEntry[] GetQueuedDownloads() {
            return mDownloadQueue.ToArray();
        }

        /// <summary>
        /// Add an upload to the queue.
        /// </summary>
        /// <param name="server">The server to get file from. May be null.</param>
        /// <param name="pSourceFileName">The full local destination.</param>
        /// <param name="pDestinationFolder">The destinatin destination on server.</param>
        /// <returns></returns>
        public UploadEntry EnqueueUpload(Server server, string pSourceFileName, FolderNode pDestinationFolder) {
            UploadEntry entry = new UploadEntry(connectionManager, pSourceFileName, pDestinationFolder);
            mUploadQueue.Enqueue(entry);
            return entry;
        }
        
        void Messages_QueueLimitExceededEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_Messages messageEventArgs) {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        void Messages_FileOrDirectoryNotFoundEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_Messages messageEventArgs) {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        void Messages_TransferQueuedEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_401 messageEventArgs) {
            Debug.WriteLine("The Transfer have been queued! File '" + messageEventArgs.Path + "'.");
        }

        /***************/

        public void RemoveDownload(TransferEntry entry) {
            if(idleTransfers.Contains(entry)) {
                idleTransfers.Remove(entry);
                if(DownloadRemoved != null)
                    DownloadRemoved((DownloadEntry)entry);
            } else {
                throw new TransferException("Cannot remove active or pending transfer", entry);
            }
        }
    }
}

