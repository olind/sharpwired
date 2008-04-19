#region Information and licence agreements
/*
 * ServerInformation.cs 
 * Created by Ola Lindberg, 2007-12-13
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
using SharpWired.MessageEvents;
using SharpWired.Model.Messaging;

namespace SharpWired.Model
{
    /// <summary>
    /// Represents the connected server
    /// </summary>
    public class Server {
        #region Fields
        string appVersion;
        int filesCount;
        long fileSize;
        string protocolVersion;
        string serverDescription;
        string serverName;
        DateTime startTime;
        bool connected;
        Chat publicChat;
        LogicManager logicManager;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor - Empty
        /// </summary>
        public Server(LogicManager logicManager, MessageEventArgs_200 message) {
            this.logicManager = logicManager;

            this.appVersion = message.AppVersion;
            this.filesCount = message.FilesCount;
            this.fileSize = message.FilesSize;
            this.protocolVersion = message.ProtocolVersion;
            this.serverDescription = message.ServerDescription;
            this.serverName = message.ServerName;
            this.startTime = message.StartTime;

            this.publicChat = new Chat(logicManager, 1);

            logicManager.LoggedIn += OnLoggedIn;
            logicManager.LoggedOut += OnLoggedOut;

            // TODO: If done in OnLoggedIn() creates a race with the GUI.
            publicChat = new Chat(logicManager, 1); // 1 = public chat
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the server app version
        /// </summary>
        public string AppVersion
        {
            get { return appVersion; }
            set { appVersion = value; }
        }

        /// <summary>
        /// Get or set the servers file count
        /// </summary>
        public int FilesCount
        {
            get { return filesCount; }
            set { filesCount = value; }
        }

        /// <summary>
        /// Get or set the file size on the server
        /// </summary>
        public long FileSize
        {
            get { return fileSize; }
            set { fileSize = value; }
        }

        /// <summary>
        /// Get or set the server protocol version
        /// </summary>
        public string ProtocolVersion
        {
            get { return protocolVersion; }
            set { protocolVersion = value; }
        }

        /// <summary>
        /// Get or set the server description
        /// </summary>
        public string ServerDescription
        {
            get { return serverDescription; }
            set { serverDescription = value; }
        }

        /// <summary>
        /// Get or set the server name
        /// </summary>
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        /// <summary>
        /// Get or set the server start time
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public Chat PublicChat {
            get { return publicChat; }
        }
        #endregion

        #region Events & Listeners
        public void OnLoggedIn() { }

        public void OnLoggedOut() {
            publicChat = null;
        }
        #endregion
    }
}
