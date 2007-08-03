using System;
using System.Collections.Generic;
using System.Text;

namespace SharpWired.Connection.Transfers.Entries
{
	class TransferStatusItem
	{
		private int mBytesTransfered;

		public int Bytes
		{
			get { return mBytesTransfered; }
			set { mBytesTransfered = value; }
		}


		private float mPercentageOfTotal;

		public float Percentage
		{
			get { return mPercentageOfTotal; }
			set { mPercentageOfTotal = value; }
		}
		
		public TransferStatusItem(int bytes, float percentage)
		{
			mBytesTransfered = bytes;
			mPercentageOfTotal = percentage;
		}
	}
}