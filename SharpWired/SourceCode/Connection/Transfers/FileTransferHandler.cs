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

namespace SharpWired.Connection.Transfers
{
	/// <summary>
	/// Handler for file transfers.
	/// </summary>
	public class FileTransferHandler: HandlerBase
	{
		#region Constructors
		public FileTransferHandler(LogicManager logic)
			: base(logic)
		{
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

			#region Attach event listeners.
			logicManager.ConnectionManager.Messages.TransferReadyEvent += new Messages.TransferReadyEventHandler(Messages_TransferReadyEvent);
			logicManager.ConnectionManager.Messages.TransferQueuedEvent += new Messages.TransferQueuedEventHandler(Messages_TransferQueuedEvent);
			logicManager.ConnectionManager.Messages.FileOrDirectoryNotFoundEvent += new Messages.FileOrDirectoryNotFoundEventHandler(Messages_FileOrDirectoryNotFoundEvent);
			logicManager.ConnectionManager.Messages.QueueLimitExceededEvent += new Messages.QueueLimitExceededEventHandler(Messages_QueueLimitExceededEvent);
			#endregion
		}

		#endregion

		#region Fields
		private string mDefaultDownloadFolder;
		Queue<UploadEntry> mUploadQueue = new Queue<UploadEntry>();
		Queue<DownloadEntry> mDownloadQueue = new Queue<DownloadEntry>();
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
		public UploadEntry[] GetQueuedUploads()
		{
			return mUploadQueue.ToArray();
		}

		public DownloadEntry[] GetQueuedDownloads()
		{
			return mDownloadQueue.ToArray();
		}
		
		#region Queueing
		public UploadEntry EnqueueUpload(Server server, string pSourceFileName, FolderNode pDestinationFolder)
		{
			UploadEntry entry = new UploadEntry(server, pSourceFileName, pDestinationFolder);
			mUploadQueue.Enqueue(entry);
			return entry;
		}

		public DownloadEntry EnqueueDownload(Bookmark bookmark, FileNode pSourceFile, string pDestinationFolder)
		{
			DownloadEntry entry = new DownloadEntry(bookmark.Server, pSourceFile, pDestinationFolder);
			mDownloadQueue.Enqueue(entry);
			return entry;
		}
		#endregion
		#endregion

		internal void StartTestDownload()
		{
			// No offset when starting, I guess?
			logicManager.ConnectionManager.Commands.Get(testPath, 0);
		}

		#region Server Message EventHandlers.
		void Messages_TransferReadyEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_400 messageEventArgs)
		{
			string hash = messageEventArgs.Hash;
			Console.WriteLine("Transfer is ready! File '" + messageEventArgs.Path + "', with ID '" + hash + "'.");

			Bookmark currentBookmark = logicManager.ConnectionManager.CurrentBookmark;
			ConnectionManager manager = new ConnectionManager();
			Bookmark transferBookmark = manager.MakeTransferBookmark(currentBookmark);
			manager.Connect(transferBookmark);
			manager.Commands.Transfer(hash);
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

		private string testPath = "/f1/f11/Fi111.test";
	}
}