using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection;
using SharpWired.Model.Files;

namespace SharpWired.Connection.Transfers.Entries
{
	public class UploadEntry: TransferEntry
	{
		public UploadEntry(Server server, string pSourceFileName, FolderNode pDestinationFolder)
			:base(server)
		{
			
		}
	}
}
