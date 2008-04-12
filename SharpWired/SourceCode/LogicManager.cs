#region Information and licence agreements
/*
 * LogicManager.cs 
 * Created by Ola Lindberg, 2006-11-25
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
using SharpWired.Model.Chat;
using SharpWired.Model.Users;
using SharpWired.Connection;
using SharpWired.Connection.Bookmarks;
using SharpWired.Model.News;
using SharpWired.Model.Files;
using SharpWired.MessageEvents;
using SharpWired.Connection.Transfers;
using System.Drawing;
using SharpWired.Controller;
using SharpWired.Model.PrivateMessages;
using SharpWired.Model;
using SharpWired.Controller.Errors;
using SharpWired.Controller.PrivateMessages;

namespace SharpWired
{
    /// <summary>
    /// Central class. Holds references to a number of objects and listens to connection layer.
    /// Initializes the other handlers
    /// </summary>
    public class LogicManager
    {
        #region Variables

        private ConnectionManager connectionManager;
        private ChatController chatHandler;
        private UserController userHandler;
        private NewsController newsHandler;
        private FileListingController fileListingHandler;
		private FileTransferHandler fileTransferHandler;
        private ServerInformation serverInformation;
        private GroupController groupHandler;
        private ErrorController errorHandler;
        private PrivateMessageController privateMessagesHandler;
        private HeartBeatTimer heartBeatTimer;
        #endregion

        #region Properties

        /// <summary>
        /// Get the connection manager
        /// </summary>
        public ConnectionManager ConnectionManager
        {
            get { return connectionManager; }
        }

        /// <summary>
        /// Get the private messages handler
        /// </summary>
        public PrivateMessageController PrivateMessagesHandler
        {
            get { return privateMessagesHandler; }
        }

        /// <summary>
        /// Get the error handler
        /// </summary>
        public ErrorController ErrorHandler
        {
            get { return errorHandler; }
        }

        /// <summary>
        /// Get the server information
        /// </summary>
        public ServerInformation ServerInformation
        {
            get { return serverInformation; }
        }

        /// <summary>
        /// Get the chat handler
        /// </summary>
        public ChatController ChatHandler
        {
            get { return chatHandler; }
        }

        /// <summary>
        /// Get the user handler
        /// </summary>
        public UserController UserHandler
        {
            get { return userHandler; }
        }

        /// <summary>
        /// Get the news handler
        /// </summary>
        public NewsController NewsHandler
        {
            get { return newsHandler; }
        }

        /// <summary>
        /// Get the FileListingHandler
        /// </summary>
        public FileListingController FileListingHandler
        {
            get { return fileListingHandler;}
        }

        /// <summary>
        /// Get 
        /// </summary>
		public FileTransferHandler FileTransferHandler
		{
			get { return fileTransferHandler; }
		}

        #endregion

        #region Methods

        /// <summary>
        /// Connects the client to the server.
        /// </summary>
        public void Connect(Bookmark bookmark) {
            try {
                connectionManager.Connect(bookmark);

                // Listen to events
                connectionManager.Messages.LoginSucceededEvent += new Messages.LoginSucceededEventHandler(Messages_LoginSucceededEvent);
                connectionManager.Messages.ServerInformationEvent += new Messages.ServerInformationEventHandler(Messages_ServerInformationEvent);
            } catch (ConnectionException ce) {
                errorHandler.ReportConnectionExceptionError(ce);   
            }
        }

        #endregion

        #region Events listeners: from connection layer
        void Messages_LoginSucceededEvent(object sender, global::SharpWired.MessageEvents.MessageEventArgs_201 messageEventArgs)
        {
            //TODO: We shouldn't set the user icon here but instead have
            //      some user object so we can change the icon. Maybe by setting the
            //      icon on the bookmark.
            SharpWired.Gui.Resources.Icons.IconHandler iconHandler = new SharpWired.Gui.Resources.Icons.IconHandler();
            connectionManager.Commands.Icon(1, iconHandler.UserImage);

            serverInformation.Connected = true;

            //Starts the heart beat pings to the server
            heartBeatTimer = new HeartBeatTimer(connectionManager);
            heartBeatTimer.StartTimer();
        }

        /// <summary>
        /// Listen to server information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
        void Messages_ServerInformationEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_200 messageEventArgs)
        {
            serverInformation.Init(messageEventArgs);
        }

        public delegate void ConnectionDelegate();
        public event ConnectionDelegate Connected;
        public event ConnectionDelegate Disconnected;

        void OnConnected() {
            if (Connected != null)
                Connected();
        }

        void OnDisconnected() {
            if (Disconnected != null)
                Disconnected();
        }
        #endregion

        #region Commands to server
        /// <summary>
        /// Dissconnect from the server
        /// </summary>
        public void Disconnect()
        {
            if (heartBeatTimer != null)
                heartBeatTimer.StopTimer();

            // TODO: Create enum for chat id 1
            connectionManager.Commands.Leave(1);
            connectionManager.Disconnect();
            serverInformation.Connected = false;
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        public LogicManager()
        {
            connectionManager = new ConnectionManager();
            connectionManager.Connected += OnConnected;
            connectionManager.Disconnected += OnDisconnected;

            chatHandler = new ChatController(this);
            userHandler = new UserController(this);
            groupHandler = new GroupController(this);
            newsHandler = new NewsController(this);
            fileListingHandler = new FileListingController(this);
			fileTransferHandler = new FileTransferHandler(this);
            // TODO: Should listen for ConnectionManager.Connected?
            serverInformation = new ServerInformation();

            // TODO: Should listen for ConnectionManager.Connected?
            errorHandler = new ErrorController(this);
            privateMessagesHandler = new PrivateMessageController(this);
        }
        #endregion
    }
}
