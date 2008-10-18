using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SharpWired.Connection.Transfers.Entries;

namespace SharpWired.Connection.Transfers
{
	class TranserWorker: BackgroundWorker
	{
		#region Constructors
		private TranserWorker()
		{
			this.WorkerReportsProgress = true;
			this.WorkerSupportsCancellation = true;

			DoWork += new DoWorkEventHandler(TranserWorker_DoWork);
		}

		void UpdateProgress()
		{
			ReportProgress(1337, new TransferStatusItem(1337, 1.2f));
		}

		/// <summary>
		/// Create a TransferWorker with the thing to transfer.
		/// </summary>
		/// <param name="entry"></param>
		public TranserWorker(TransferEntry entry)
		{
			mTransferEntry = entry;
		}
		#endregion

		void TranserWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			// Soccets and fun stuff here I guess.
		}

		
		#region Properties
		private TransferEntry mTransferEntry;
		/// <summary>
		/// The thing to transfer.
		/// </summary>
		public TransferEntry TransferEntry
		{
			get { return mTransferEntry; }
			set { mTransferEntry = value; }
		}
		#endregion
	}
}