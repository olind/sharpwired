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

namespace SharpWired.Model
{
    /// <summary>
    /// Represents the connected server
    /// </summary>
    public class ServerInformation
    {
        private MessageEventArgs_200 messageEventArgs;
        private string appVersion;
        private int filesCount;
        private int fileSize;
        private string protocolVersion;
        private string serverDescription;
        private string serverName;
        private DateTime startTime;
        private bool connected;

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
        public int FileSize
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

        /// <summary>
        /// Get or set the online status
        /// </summary>
        public bool Connected
        {
            get {
                if (ServerOnlineStatusChangedEvent != null)
                {
                    ServerOnlineStatusChangedEvent(connected);
                }
                return connected;
            }
            set { 
                connected = value;
                if (ServerOnlineStatusChangedEvent != null)
                {
                    ServerOnlineStatusChangedEvent(connected);
                }
            }
        }

        /// <summary>
        /// Delegate for ServerOnlineStatusChangedEvent
        /// </summary>
        /// <param name="connected"></param>
        public delegate void ServerOnlineStatusChangedDelegate(bool connected);
        /// <summary>
        /// Event raised when the server online status changed ie you get connected or disconnected
        /// </summary>
        public event ServerOnlineStatusChangedDelegate ServerOnlineStatusChangedEvent;

        /// <summary>
        /// Init the server information when we are online
        /// </summary>
        /// <param name="messageEventArgs"></param>
        public void Init(MessageEventArgs_200 messageEventArgs)
        {
            this.messageEventArgs = messageEventArgs;
            this.appVersion = messageEventArgs.AppVersion;
            this.filesCount = messageEventArgs.FilesCount;
            this.fileSize = messageEventArgs.FilesSize;
            this.protocolVersion = messageEventArgs.ProtocolVersion;
            this.serverDescription = messageEventArgs.ServerDescription;
            this.serverName = messageEventArgs.ServerName;
            this.startTime = messageEventArgs.StartTime;
        }

        /// <summary>
        /// Constructor - Empty
        /// </summary>
        public ServerInformation()
        {
        }
    }
}
