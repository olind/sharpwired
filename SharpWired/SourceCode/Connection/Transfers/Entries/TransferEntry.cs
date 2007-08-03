using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection;

namespace SharpWired.Connection.Transfers.Entries
{
	public class TransferEntry
	{
		#region Constructors
		public TransferEntry(Server server)
		{
			mServer = server;
		}
		#endregion

		#region properties
		protected string mFromLocation;

		public virtual string FromLocation
		{
			get { return mFromLocation; }
			set { mFromLocation = value; }
		}

		protected string mToLocation;

		public virtual string ToLocation
		{
			get { return mToLocation; }
			set { mToLocation = value; }
		}

		protected Server mServer;

		public virtual Server Server
		{
			get { return mServer; }
			set { mServer = value; }
		}
		#endregion
	}
}