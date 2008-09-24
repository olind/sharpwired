using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Model.Files;

namespace SharpWired.Gui.Files
{
	/// <summary>
	/// Event args for when the gui wants to transfer a file.
	/// </summary>
	public class TransferRequestEventArgs : EventArgs
	{
		/// <summary>
		/// Constructs.
		/// </summary>
		/// <param name="fse">The array of destination segments</param>
		public TransferRequestEventArgs(FileSystemEntry fse)
		{
			mFileSystemEntry = fse;
		}


		#region properties
		
		private FileSystemEntry mFileSystemEntry;
		/// <summary>
		/// Request or sets the FileSystemNode representing what to transfer.
		/// </summary>
		public FileSystemEntry FileSystemEntry
		{
			get { return mFileSystemEntry; }
			set { mFileSystemEntry = value; }
		}

		#endregion
	}
}