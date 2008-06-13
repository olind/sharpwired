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

namespace SharpWired.Connection.Transfers
{
	/// <summary>
	/// Handler for file transfers.
	/// </summary>
	public class FileTransferHandler : ControllerBase
	{
		#region Fields
		private bool downloading = false;

		private string mDefaultDownloadFolder;
		Queue<UploadEntry> mUploadQueue = new Queue<UploadEntry>();
		List<DownloadEntry> mDownloadQueue = new List<DownloadEntry>();

		private ConnectionManager connectionManager;
		#endregion

		#region Constructors

		/// <summary>
		/// Construct and set up.
		/// </summary>
		/// <param name="logic">The SharpWiredModel to get ConnectionManager and more from.</param>
		public FileTransferHandler(SharpWiredModel logic)
			: base(logic)
		{
			connectionManager = logic.ConnectionManager;

			mDefaultDownloadFolder = Path.Combine(Application.StartupPath, "Downloads");
			if (!Directory.Exists(mDefaultDownloadFolder))
			{
				try
				{
					Directory.CreateDirectory(mDefaultDownloadFolder);
				}
				catch (Exception e)
				{
					Console.Error.WriteLine("Error trying to create default download dir '"
						+ mDefaultDownloadFolder + "'.\n" + e.ToString());
				}
			}

			#region Attach event listeners
            messages.TransferReadyEvent += Messages_TransferReadyEvent;
			messages.TransferQueuedEvent += Messages_TransferQueuedEvent;
			messages.FileOrDirectoryNotFoundEvent += Messages_FileOrDirectoryNotFoundEvent;
			messages.QueueLimitExceededEvent += Messages_QueueLimitExceededEvent;
			#endregion
		}

		#endregion


		#region Properties

		/// <summary>
		/// The default download folder.
		/// </summary>
		public string DefaultDownloadFolder
		{
			get { return mDefaultDownloadFolder; }
		}
		#endregion


		#region Methods
		/// <summary>
		/// Get the upload queue.
		/// </summary>
		/// <returns>UploadEntry array.</returns>
		public UploadEntry[] GetQueuedUploads()
		{
			return mUploadQueue.ToArray();
		}

		/// <summary>
		/// Get the download queue.
		/// </summary>
		/// <returns>A DownloadEntry array.</returns>
		public DownloadEntry[] GetQueuedDownloads()
		{
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
		public UploadEntry EnqueueUpload(Server server, string pSourceFileName, FolderNode pDestinationFolder)
		{
			UploadEntry entry = new UploadEntry(server, pSourceFileName, pDestinationFolder);
			mUploadQueue.Enqueue(entry);
			return entry;
		}

		/// <summary>
		/// Enqueue a download.
		/// </summary>
		/// <param name="bookmark">The Bookmark with username and serverinfo about where to download from. May be null.</param>
		/// <param name="pSourceFile">The full path to file on server.</param>
		/// <param name="pDestinationFolder">The full path to the local folder where to store file. May be null.</param>
		/// <returns></returns>
		public DownloadEntry EnqueueDownload(Bookmark bookmark, FileNode pSourceFile, string pDestinationFolder)
		{
			DownloadEntry entry = null;
			if (bookmark != null)
				entry = new DownloadEntry(bookmark.Server, pSourceFile, pDestinationFolder);
			else
				entry = new DownloadEntry(null, pSourceFile, pDestinationFolder);

			//mDownloadQueue.Enqueue(entry);
			mDownloadQueue.Add(entry);
			TryStartDownload();
			return entry;
		}

		/// <summary>
		/// Enqueue a download.
		/// </summary>
		/// <param name="pSourceFile">The full path to file on server.</param>
		/// <returns>A DownloadEntry.</returns>
		public DownloadEntry EnqueueDownload(FileNode pSourceFile)
		{
			return EnqueueDownload(null, pSourceFile, null);
		}

		#endregion
		#endregion

		private void TryStartDownload()
		{
			if (mDownloadQueue.Count > 0 && !downloading)
			{
				StartDownload(mDownloadQueue[0]);
			}
		}

		private void StartDownload(DownloadEntry entry)
		{
			downloading = true;
			// NOTE: check server here too.
			// No offset for now.
			commands.Get(entry.FromLocation, 0);			
		}

		#region Server Message EventHandlers.
		void Messages_TransferReadyEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_400 messageEventArgs)
		{
			StartTransfer(messageEventArgs.Path, messageEventArgs.Offset, messageEventArgs.Hash);
		}

		private void StartTransfer(string path, int offset, string id)
		{
			Console.WriteLine("Transfer is ready! File '" + path + "', with ID '" + id + "'.");

			Bookmark currentBookmark = model.ConnectionManager.CurrentBookmark;

			DownloadEntry entry = GetDownloadItem(path);
			mDownloadQueue.Remove(entry);

			if (entry == null)
				throw new InvalidOperationException("Server said download was ready for file with path '" + path + "',"
					+ " but download queue didn't have an entry with that path.");

			FileStream fileStream = new FileStream(MakeDownloadFile(entry), FileMode.Append);
			BinarySecureSocket socket = connectionManager.ConnectFileTransfer(fileStream, 0, 0);
			socket.DataRecivedDoneEvent += new EventHandler<DataRecievedEventArgs>(socket_DataRecivedDoneEvent);
			/*
				om servern säger OK ( meddelande 400) då ska man öppna en uppkoppling till servern filetransfer port
				och skicka in "TRANSFER" SP hash EOT på filetransfer porten alltså

				sedan lägger kallar du på beginRead på den socketens sslStream
				(med FileTransfer som dess state obkect, och FileTransferReadCallback som AsyncCallback)
			*/
			Console.WriteLine("Sending 'TRANSFER' commanf for file now...");
			socket.SendMessage("TRANSFER" + Utility.SP + id);
		}

		private string MakeDownloadFile(DownloadEntry entry)
		{
			if (string.IsNullOrEmpty(entry.ToLocation))
			{
				string fileName = entry.GetRemoteFileName();
				string path = Path.Combine(mDefaultDownloadFolder, fileName);
				return path;
			}
			else return
				entry.ToLocation;
		}

		private DownloadEntry GetDownloadItem(string path)
		{
			// NOTE: This should be done smoother?!
			foreach (DownloadEntry entry in mDownloadQueue)
			{
				if (entry.FromLocation == path)
					return entry;
			}
			return null;
		}

		void socket_DataRecivedDoneEvent(object sender, DataRecievedEventArgs e)
		{
			downloading = false;
			MessageBox.Show("Data recieved!");
			BinarySecureSocket socket = sender as BinarySecureSocket;
			if (socket != null)
			{
				socket.DataRecivedDoneEvent -= new EventHandler<DataRecievedEventArgs>(socket_DataRecivedDoneEvent);
			}
		}

		void Messages_QueueLimitExceededEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_Messages messageEventArgs)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		void Messages_FileOrDirectoryNotFoundEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_Messages messageEventArgs)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		void Messages_TransferQueuedEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_401 messageEventArgs)
		{
			Console.WriteLine("The Transfer have been queued! File '" + messageEventArgs.Path + "'.");
		}
		#endregion

		/// <summary>
		/// The GUI or someonw else wants to transfer (download) a file. Enqueue if file.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The args with the File Model node.</param>
		public void RequstDownloadHandler(object sender, TransferRequestEventArgs args)
		{
			EnqueEntry(args.FileSystemEntry);
		}

		/// <summary>
		/// Enqueue if file. Else?!
		/// </summary>
		/// <param name="fileSystemEntry">The File Model node.</param>
		public void EnqueEntry(FileSystemEntry fileSystemEntry)
		{
			if (fileSystemEntry is FileNode)
				this.EnqueueDownload(null, fileSystemEntry as FileNode, null);
			else
				MessageBox.Show("Sorry, but we can only download files right now, and not folders.");
		}
	}
}