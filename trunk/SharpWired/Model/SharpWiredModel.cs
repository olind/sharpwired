#region Information and licence agreements
/*
 * SharpWiredModel.cs 
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
using SharpWired.MessageEvents;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace SharpWired.Model {
    /// <summary>
    /// Central class. Holds references to a number of objects and listens to connection layer.
    /// Initializes the other models
    /// </summary>
    public sealed class SharpWiredModel {
        private ConnectionManager connectionManager;
        private HeartBeatTimer heartBeatTimer;
        private SharpWired.Model.Server server;
        private static SharpWiredModel instance;

        public static SharpWiredModel Instance {
            get { return instance; }
            set {
                if (instance == null)
                    instance = value;
                else
                    throw new SingletonException("Singleton already created");
            }
        }
       

        #region Properties
        public ConnectionManager ConnectionManager {
            get { return connectionManager; }
        }

        public SharpWired.Model.Server ServerInformation {
            get { return server; }
        }

        public Server Server {
            get { return server; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Connect to the given bookmark. Note! Dissconnects any current connection.
        /// </summary>
        /// <param name="bookmark"></param>
        public void Connect(Bookmark bookmark) {
            try {
                //TODO: Probably want to let the user confirm dissconnecting the current connection
                if(server != null && server.PublicChat != null)
                    Disconnect();
                
                connectionManager.Connect(bookmark);
            } catch (ConnectionException ce) {
                Debug.WriteLine("Failed to connect: '" + ce + "'");
            }
        }
        #endregion

        public delegate void ServerChanged(Server server);
        /// <summary>
        /// We have a connection to the server but are NOT yet logged in.
        /// </summary>
        public event ServerChanged Connected;

        void OnBanned(MessageEventArgs_Messages message) {
            //TODO: Implement handling for banned
            throw new NotImplementedException("Client banned from server. Not implemented yet, please report to SharpWired bug tracker.");
        }

        void OnConnected(MessageEventArgs_200 message) {
            server = new SharpWired.Model.Server(message);

            UserInformation ui = connectionManager.CurrentBookmark.UserInformation;
            SharpWired.Gui.Resources.Icons.IconHandler ih = new SharpWired.Gui.Resources.Icons.IconHandler();

            Commands c = connectionManager.Commands;
            c.Nick(ui.Nick);            //Required
            c.Icon(1, ih.UserImage);    //Optional
            //STATUS                    //Optional TODO: Set status
            c.Client();                 //Optional but highly required

            c.User(ui.UserName);
            c.Pass(ui.Password);

            if (Connected != null)
                Connected(server);
        }

        void OnDisconnected() { }

        /// <summary>
        /// Dissconnect from the server
        /// </summary>
        public void Disconnect() {
            if (heartBeatTimer != null)
                heartBeatTimer.StopTimer();

            if(server != null)
                server.GoOffline();

            // TODO: Create enum for chat id 1
            connectionManager.Commands.Leave(1);

            server = null;

            connectionManager.Disconnect();
        }

        #region Initialization
        /// <summary>
        /// Constructor
        /// </summary>
        public SharpWiredModel() {
            connectionManager = new ConnectionManager();

            Messages m = connectionManager.Messages;
            m.ServerInformationEvent += OnConnected;
            m.BannedEvent += OnBanned;
        }
        #endregion
    }
}
