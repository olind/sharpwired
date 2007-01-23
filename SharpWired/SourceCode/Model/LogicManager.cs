#region Information and licence agreements
/**
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

namespace SharpWired.Model
{
    public class LogicManager
    {
        #region Variables

        private ConnectionManager connectionManager;
        private ChatHandler chatHandler;
        private UserHandler userHandler;
        private NewsHandler newsHandler;

        #endregion

        #region Properties

        public ConnectionManager ConnectionManager
        {
            get { return connectionManager; }
        }

        public ChatHandler ChatHandler
        {
            get { return chatHandler; }
        }

        public UserHandler UserHandler
        {
            get { return userHandler; }
        }

        public NewsHandler NewsHandler
        {
            get { return newsHandler; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Connects the client to the server.
        /// </summary>
        public void Connect(Bookmark bookmark)
        {
            connectionManager.Connect(bookmark);

            // Listen to events
            connectionManager.Messages.LoginFailedEvent += new Messages.LoginFailedEventHandler(Messages_LoginFailedEvent);
            connectionManager.Messages.LoginSucceededEvent += new Messages.LoginSucceededEventHandler(Messages_LoginSucceededEvent);
        }

        #endregion

        #region Events listeners: from connection layer

        void Messages_LoginSucceededEvent(object sender, global::SharpWired.MessageEvents.MessageEventArgs_201 messageEventArgs)
        {
            chatHandler.Init(connectionManager);
            newsHandler.Init(connectionManager);
        }

        void Messages_LoginFailedEvent(object sender, global::SharpWired.MessageEvents.MessageEventArgs_Messages messageEventArgs)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        public LogicManager()
        {
            connectionManager = new ConnectionManager();

            chatHandler = new ChatHandler(this);
            userHandler = new UserHandler(this);
            newsHandler = new NewsHandler(this);
        }

        #endregion
    }
}
