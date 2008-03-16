#region Information and licence agreements
/*
 * Server.cs
 * Created by Ola Lindberg and Peter Holmdahl, 2006-11-10
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
using SharpWired.Connection.Sockets;
using SharpWired.Connection.Bookmarks;
using System.IO;
using System.Net.Sockets;

namespace SharpWired.Connection
{
	/// <summary>
	/// Manages connections
	/// </summary>
    public class ConnectionManager
    {
		#region Fields
		private Messages messages;
		private Commands commands;
		private SecureSocket commandSocket;
        /// <summary>
        /// For Files-transfers.
        /// </summary>
		protected BinarySecureSocket binarySocket;
        LagHandler lagHandler;
		#endregion

		#region Properties

		/// <summary>
		/// Get the class that exposes the message events.
		/// </summary>
		public Messages Messages
		{
			get { return messages; }
		}

		/// <summary>
		/// Get the Commands for an eventual connection. Used to send commands over the connection.
		/// </summary>
		public Commands Commands
		{
			get { return commands; }
		}

		private Bookmark mCurrentBookmark;
		/// <summary>
		/// Get the bookmark used to connect.
		/// </summary>
		public Bookmark CurrentBookmark
		{
			get { return mCurrentBookmark; }
		}

        /// <summary>
        /// Gets the current lag
        /// </summary>
        public Nullable<TimeSpan> CurrentLag
        {
            get { return lagHandler.CurrentLag; }
        }

		#endregion

		/// <summary>
		/// Connect to the Server in the Bookmark using the UserInfo from the
        /// bookmark as well.
		/// </summary>
		/// <param name="bookmark">The info about Server and
        /// UserInformation.</param>
        public void Connect(Bookmark bookmark)
        {
            try
            {
                if (bookmark != null) {
                    commandSocket.Connect(bookmark.Server);
                    commands.InitConnection(bookmark.UserInformation);
                    mCurrentBookmark = bookmark;
                } else {
                    // TODO: Log instead of write to std out
                    Console.WriteLine("ERROR - ConnectionManager.Connect(): " +
                                      "Trying to connect to a null bookmark.");
                }
            }
            catch (ConnectionException ce)
            {
                ce.Bookmark = bookmark;
                throw (ce);
            }
        }

		/// <summary>
		/// Increases the port number for the server by one, so that it corresponds
		/// to the transfer port.
		/// </summary>
		/// <remarks>
		/// 1.3
		/// -- snip --
		///
		/// Wired communication takes place over a TCP/IP connection using TLS
		/// [1]. The default port is TCP 2000, but other ports can be used. The
		/// transfer port is the default port incremented by one, or 2001 by
		/// default.
		/// </remarks>
		/// <param name="bookmark">The Bookmark to use as base.</param>
		/// <returns>A new Bookmark.</returns>
		private Bookmark MakeTransferBookmark(Bookmark bookmark)
		{
			Server server = new Server(bookmark.Server.ServerPort + 1, bookmark.Server.MachineName, bookmark.Server.ServerName);
			return new Bookmark(server, bookmark.UserInformation);
		}

		/// <summary>
		/// Should only give out this once.
		/// </summary>
		/// <returns>A BinarySecureSocket</returns>
		private BinarySecureSocket GetFileTransferSocket()
		{
			if (binarySocket == null)
				binarySocket = new BinarySecureSocket();
			return binarySocket;
		}

		internal BinarySecureSocket ConnectFileTransfer(FileStream fileStream, long fileSize, long offset)
		{
			Bookmark bookmark = MakeTransferBookmark(CurrentBookmark);
			BinarySecureSocket binarySocket = GetFileTransferSocket();
			binarySocket.Connect(bookmark.Server, fileStream, fileSize, offset);
			return binarySocket;
		}

        #region Constructor - Creates commands, messages and sockets
        /// <summary>
        /// Constructs a ConnectionManager. Creates a SecureSocket, a Message, and a Commands.
        /// </summary>
        public ConnectionManager()
        {
            this.commandSocket = new SecureSocket();
            this.messages = new Messages(this.commandSocket);
            this.commands = new Commands(this.commandSocket);

            //Set up the lag handler
            this.lagHandler = new LagHandler();
            messages.PingReplyEvent += new Messages.PingReplyEventHandler(lagHandler.OnPingReceived);
            commands.PingSentEvent +=new Commands.PingSentDelegate(lagHandler.OnPingSent);
        }
        #endregion
	}
}