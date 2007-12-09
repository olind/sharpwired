using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection;

namespace SharpWired.Connection.Transfers.Entries
{
	/// <summary>
	/// And entry in the Transfer Queue. Consists of a from location, a to location and a server.
	/// </summary>
	public class TransferEntry
	{
		#region Constructors
		/// <summary>
		/// Create a queue item with this server.
		/// </summary>
		/// <param name="server">The Server to transfer from.</param>
		public TransferEntry(Server server)
		{
			mServer = server;
		}
		#endregion


		#region Properties
		protected string mFromLocation;
		/// <summary>
		/// Get/Set the location to get transfer from.
		/// </summary>
		public virtual string FromLocation
		{
			get { return mFromLocation; }
			set { mFromLocation = value; }
		}

		protected string mToLocation;
		/// <summary>
		/// Get/Set the lovation to store transfer.
		/// </summary>
		public virtual string ToLocation
		{
			get { return mToLocation; }
			set { mToLocation = value; }
		}

		protected Server mServer;
		/// <summary>
		/// Get/Set the Server to transfer from.
		/// </summary>
		public virtual Server Server
		{
			get { return mServer; }
			set { mServer = value; }
		}
		#endregion

	}
}