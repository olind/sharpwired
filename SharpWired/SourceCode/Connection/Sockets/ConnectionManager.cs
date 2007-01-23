#region Information and licence agreements
/**
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

namespace SharpWired.Connection
{
    public class ConnectionManager
    {

        /// <summary>
        /// The messages that this connection uses.
        /// </summary>
        private Messages messages;

        /// <summary>
        /// The commands that this connection uses.
        /// </summary>
        private Commands commands;

        private SecureSocket commandSocket;

        public ConnectionManager()
        {
            this.commandSocket = new SecureSocket();
            this.messages = new Messages(this.commandSocket);
            this.commands = new Commands(this.commandSocket);
        }

        public Messages Messages
        {
            get
            {
                return messages;
            }
        }

        public Commands Commands
        {
            get {
                return commands;
            }
        }

        public void Connect(Bookmark bookmark)
        {
            commandSocket.Connect(bookmark.Server);
            commands.InitConnection(bookmark.UserInformation);
        }
    }
}
