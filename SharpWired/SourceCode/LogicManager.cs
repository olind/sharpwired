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

using SharpWired.Connection;
using SharpWired.Connection.Bookmarks;
using SharpWired.Connection.Transfers;
using SharpWired.Controller;
using SharpWired.Controller.Errors;
using SharpWired.Controller.PrivateMessages;
using SharpWired.MessageEvents;

namespace SharpWired {
    /// <summary>
    /// Central class. Holds references to a number of objects and listens to connection layer.
    /// Initializes the other controllers
    /// </summary>
    public class LogicManager {
        #region Fields
        private ConnectionManager connectionManager;
        private ChatController chatController;
        private UserController userController;
        private NewsController newsController;
        private FileListingController fileListingController;
        private FileTransferHandler fileTransferController;
        private SharpWired.Model.Server server;
        private GroupController groupController;
        private ErrorController errorController;
        private PrivateMessageController privateMessagesController;
        private HeartBeatTimer heartBeatTimer;
        #endregion

        #region Properties
        /// <summary>
        /// Get the connection manager
        /// </summary>
        public ConnectionManager ConnectionManager {
            get { return connectionManager; }
        }

        /// <summary>
        /// Get the private messages handler
        /// </summary>
        public PrivateMessageController PrivateMessagesController {
            get { return privateMessagesController; }
        }

        /// <summary>
        /// Get the error handler
        /// </summary>
        public ErrorController ErrorController {
            get { return errorController; }
        }

        /// <summary>
        /// Get the server information
        /// </summary>
        public SharpWired.Model.Server ServerInformation {
            get { return server; }
        }

        /// <summary>
        /// Get the chat handler
        /// </summary>
        public ChatController ChatController {
            get { return chatController; }
        }

        /// <summary>
        /// Get the user handler
        /// </summary>
        public UserController UserController {
            get { return userController; }
        }

        /// <summary>
        /// Get the news handler
        /// </summary>
        public NewsController NewsController {
            get { return newsController; }
        }

        /// <summary>
        /// Get the FileListingController
        /// </summary>
        public FileListingController FileListingController {
            get { return fileListingController; }
        }

        /// <summary>
        /// Get 
        /// </summary>
        public FileTransferHandler FileTransferController {
            get { return fileTransferController; }
        }

        public Model.Server Server {
            get { return server; }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Connects the client to the server.
        /// </summary>
        public void Connect(Bookmark bookmark) {
            try {
                connectionManager.Connect(bookmark);
            } catch (ConnectionException ce) {
                errorController.ReportConnectionExceptionError(ce);
            }
        }

        #endregion

        public delegate void ConnectionDelegate();
        public event ConnectionDelegate LoggedIn;
        public event ConnectionDelegate LoggedOut;

        void OnConnected(MessageEventArgs_200 message) {
            this.server = new SharpWired.Model.Server(this, message);
        }
        void OnDisconnected() {
            server = null;
        }

        void OnLoggedIn(object sender, MessageEventArgs_201 messageEventArgs) {
            //TODO: We shouldn't set the user icon here but instead have
            //      some user object so we can change the icon. Maybe by setting the
            //      icon on the bookmark.
            SharpWired.Gui.Resources.Icons.IconHandler iconHandler = new SharpWired.Gui.Resources.Icons.IconHandler();
            connectionManager.Commands.Icon(1, iconHandler.UserImage);

            //Starts the heart beat pings to the server
            heartBeatTimer = new HeartBeatTimer(connectionManager);
            heartBeatTimer.StartTimer();

            chatController = new ChatController(this);
            userController = new UserController(this);
            groupController = new GroupController(this);
            newsController = new NewsController(this);
            fileListingController = new FileListingController(this);
            fileTransferController = new FileTransferHandler(this);
            errorController = new ErrorController(this);
            privateMessagesController = new PrivateMessageController(this);

            if (LoggedIn != null)
                LoggedIn();
        }

        void OnLoggedOut() {
            if (LoggedOut != null)
                LoggedOut();

            connectionManager.Messages.LoginSucceededEvent -= OnLoggedIn;
            connectionManager.Connected -= OnConnected;

            chatController = null;
            userController = null;
            groupController = null;
            newsController = null;
            fileListingController = null;
            fileTransferController = null;
            errorController = null;
            privateMessagesController = null;
        }

        #region Commands to server
        /// <summary>
        /// Dissconnect from the server
        /// </summary>
        public void Disconnect() {
            if (heartBeatTimer != null)
                heartBeatTimer.StopTimer();

            // TODO: Create enum for chat id 1
            connectionManager.Commands.Leave(1);
            connectionManager.Disconnect();
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        public LogicManager() {
            connectionManager = new ConnectionManager();
            connectionManager.Messages.LoginSucceededEvent += OnLoggedIn;

            connectionManager.Connected += OnConnected;
            connectionManager.Disconnected += OnDisconnected;
        }
        #endregion
    }
}
