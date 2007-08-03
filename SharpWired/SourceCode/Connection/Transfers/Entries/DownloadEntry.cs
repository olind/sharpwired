using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection;
using SharpWired.Model.Files;

namespace SharpWired.Connection.Transfers.Entries
{
	public class DownloadEntry: TransferEntry
	{
		public DownloadEntry(Server server, FileNode pSourceFile, string pDestinationFolder)
			: base(server)
		{
			mToLocation = pDestinationFolder;
			mFromLocation = pSourceFile.Path;
		}
	}
}
