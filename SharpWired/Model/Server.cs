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
using SharpWired.Model.Files;
using SharpWired.Model.News;
using SharpWired.Connection;
using System.Diagnostics;

namespace SharpWired.Model {
    /// <summary>
    /// Represents the connected server
    /// </summary>
    public class Server : ModelBase {
        #region Fields
        string appVersion;
        int filesCount;
        long fileSize;
        string protocolVersion;
        string serverDescription;
        string serverName;
        DateTime startTime;
        Chat publicChat;
        SharpWired.Model.News.News news;
        FileListingModel fileListingModel;
        Transfers.Transfers transfers;
        private int ownUserId;
        private HeartBeatTimer HeartBeat { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor - Empty
        /// </summary>
        public Server(MessageEventArgs_200 message) {
            this.appVersion = message.AppVersion;
            this.filesCount = message.FilesCount;
            this.fileSize = message.FilesSize;
            this.protocolVersion = message.ProtocolVersion;
            this.serverDescription = message.ServerDescription;
            this.serverName = message.ServerName;
            this.startTime = message.StartTime;

            ConnectionManager.Messages.LoginSucceededEvent += OnLoginSucceeded;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Request or set the server app version
        /// </summary>
        public string AppVersion {
            get { return appVersion; }
            set { appVersion = value; }
        }

        /// <summary>
        /// Request or set the servers file count
        /// </summary>
        public int FilesCount {
            get { return filesCount; }
            set { filesCount = value; }
        }

        /// <summary>
        /// Request or set the file size on the server
        /// </summary>
        public long FileSize {
            get { return fileSize; }
            set { fileSize = value; }
        }

        /// <summary>
        /// Request or set the server protocol version
        /// </summary>
        public string ProtocolVersion {
            get { return protocolVersion; }
            set { protocolVersion = value; }
        }

        /// <summary>
        /// Request or set the server description
        /// </summary>
        public string ServerDescription {
            get { return serverDescription; }
            set { serverDescription = value; }
        }

        /// <summary>
        /// Request or set the server name
        /// </summary>
        public string ServerName {
            get { return serverName; }
            set { serverName = value; }
        }

        /// <summary>
        /// Request or set the server start time
        /// </summary>
        public DateTime StartTime {
            get { return startTime; }
            set { startTime = value; }
        }

        /// <summary>
        /// Request the public chat for this server
        /// </summary>
        public Chat PublicChat {
            get { return publicChat; }
        }

        /// <summary>
        /// Request the news for this server
        /// </summary>
        public SharpWired.Model.News.News News {
            get { return news; }
        }

        /// <summary>
        /// Gets the file listing model
        /// </summary>
        public FileListingModel FileListingModel { get { return fileListingModel; } }

        public Transfers.Transfers Transfers { get { return transfers; } }

        /// <summary>
        /// Sets the user id for this user.
        /// </summary>
        public int OwnUserId { set { ownUserId = value; } }

        #endregion

        #region Events & Listeners
        public delegate void ServerStatus();

        public event ServerStatus Online;
        public event ServerStatus Offline;
        #endregion

        #region Methods
        public void GoOffline() {
            if(Offline != null)
                Offline();

            publicChat = null;
            news = null;
            fileListingModel = null;
        }

        void OnLoginSucceeded(object sender, MessageEventArgs_201 message) {
            ConnectionManager.Messages.LoginSucceededEvent -= OnLoginSucceeded;

            OwnUserId = message.UserId;

            ConnectionManager.Commands.Who(1); //1 = Public Chat
            ConnectionManager.Commands.Ping(this);

            //Starts the heart beat pings to the server
            HeartBeat = new HeartBeatTimer(ConnectionManager);
            HeartBeat.StartTimer();

            publicChat = new Chat(ConnectionManager.Messages, 1); // 1 = chat id for public chat
            news = new News.News(ConnectionManager.Messages);
            fileListingModel = new FileListingModel(ConnectionManager.Messages);
            transfers = new Transfers.Transfers(ConnectionManager);

            if(Online != null)
                Online();
        }
        #endregion
    }
}
