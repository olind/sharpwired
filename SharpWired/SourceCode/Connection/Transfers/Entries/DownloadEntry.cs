using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection;
using SharpWired.Model.Files;

namespace SharpWired.Connection.Transfers.Entries
{
	/// <summary>
	/// A entry in the download queue.
	/// </summary>
	public class DownloadEntry: TransferEntry
	{
		/// <summary>
		/// Constructs.
		/// </summary>
		/// <param name="server">The server to get file from.</param>
		/// <param name="pSourceFile">The source path to file, including name n' all.</param>
		/// <param name="pDestinationFolder">The local destionation folde (but not name).</param>
		public DownloadEntry(Server server, FileNode pSourceFile, string pDestinationFolder)
			: base(server)
		{
			mToLocation = pDestinationFolder;
			mFromLocation = pSourceFile.Path;
		}


		/// <summary>
		/// Returns the filename part of server path.
		/// </summary>
		/// <returns>The file.name.</returns>
		public string GetRemoteFileName()
		{
			if (mFromLocation == null)
				return null;
			int index = mFromLocation.LastIndexOf("/");
			if (index < 0)
				return null;
			if (index < mFromLocation.Length - 1)
				return mFromLocation.Substring(index + 1);
			return null;
		}
	}
}