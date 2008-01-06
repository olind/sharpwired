#region Information and licence agreements
/*
 * PrivateMessageHandler.cs 
 * Created by Ola Lindberg, 2007-12-20
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
using SharpWired.Model.Users;

namespace SharpWired.Model.PrivateMessages
{
    /// <summary>
    /// The logic for private messages. Provides functionality for 
    /// sending and receiving private messages.
    /// </summary>
    public class PrivateMessageHandler : HandlerBase
    {
        private ConnectionManager connectionManager;
        private PrivateMessageModel privateMessageModel;

        #region Properties
        /// <summary>
        /// Get the private message model
        /// </summary>
        public PrivateMessageModel PrivateMessageModel
        {
            get { return privateMessageModel; }
        }
        #endregion

        #region Listeners from socket layer
        void Messages_PrivateMessageEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_305309 messageEventArgs)
        {
            UserItem u = logicManager.UserHandler.UserModel.GetUser(messageEventArgs.UserId);
            privateMessageModel.AddReceivedPrivateMessage(new PrivateMessageItem(u, messageEventArgs.Message));
        }
        #endregion

        #region Sending to connection layer
        /// <summary>
        /// Send the given private message to the given user
        /// </summary>
        /// <param name="user">The user to receive the message</param>
        /// <param name="message">The message to send to the user</param>
        public void Msg(UserItem user, String message)
        {
            //TODO: Make some error checking (empty message etc)
            connectionManager.Commands.Msg(user.UserId, message);
            PrivateMessageItem newSentMessage = new PrivateMessageItem(user, message);
            privateMessageModel.AddSentPrivateMessage(newSentMessage);
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initiate the private message handler
        /// </summary>
        /// <param name="connectionManager"></param>
        public override void Init(ConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
            base.Init(connectionManager);
            connectionManager.Messages.PrivateMessageEvent += new Messages.PrivateMessageEventHandler(Messages_PrivateMessageEvent);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logicManager"></param>
        public PrivateMessageHandler(LogicManager logicManager) 
            : base(logicManager)
        {
            privateMessageModel = new PrivateMessageModel();
        }
        #endregion
    }
}
