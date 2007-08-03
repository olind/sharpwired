using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection;
using SharpWired.Model;
using SharpWired.Connection.Transfers.Entries;
using SharpWired.Model.Files;
using System.Windows.Forms;
using System.IO;

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

		public DownloadEntry EnqueueDownload(Server server, FileNode pSourceFile, string pDestinationFolder)
		{
			DownloadEntry entry = new DownloadEntry(server, pSourceFile, pDestinationFolder);
			mDownloadQueue.Enqueue(entry);
			return entry;
		}
		#endregion
		#endregion
	}
}