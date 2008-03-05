#region Information and licence agreements
/*
 * TransferEntry.cs
 * Created by Peter Holmdahl, 2007-11-03
 * 
 * SharpWired - a Wired client.
 * See: http://www.zankasoftware.com/wired/ for more infromation about Wired
 * 
 * Copyright (C) Ola Lindberg (http://olalindberg.com)
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301 USA
 */
#endregion

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
        /// <summary>
        /// The server side location to get the transfer from.
        /// </summary>
		protected string mFromLocation;
		/// <summary>
		/// Get/Set the location to get transfer from.
		/// </summary>
		public virtual string FromLocation
		{
			get { return mFromLocation; }
			set { mFromLocation = value; }
		}

        /// <summary>
        /// The location to store the transfer
        /// </summary>
		protected string mToLocation;
		/// <summary>
		/// Get/Set the lovation to store transfer.
		/// </summary>
		public virtual string ToLocation
		{
			get { return mToLocation; }
			set { mToLocation = value; }
		}

        /// <summary>
        /// The server to transfer from
        /// </summary>
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