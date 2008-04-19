#region Information and licence agreements
/*
 * ChatController.cs 
 * Created by Ola Lindberg and Peter Holmdahl, 2006-11-25
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
using SharpWired.Connection;
using SharpWired.Model.Users;
using SharpWired.Model.Messaging;

namespace SharpWired.Controller
{
    /// <summary>
    /// The logic for the chats. Provides functionality for for example sending messages
    /// </summary>
    public class ChatController : ControllerBase
    {
        #region Constructor
        public ChatController(LogicManager logicManager) : base(logicManager) { }
        #endregion

        #region Methods
        /// <summary>
        /// Send a chat message to the server
        /// </summary>
        /// <param name="message">The message to send to the server</param>
        public void SendChatMessage(string message)
        {
            this.LogicManager.ConnectionManager.Commands.Say(message);
        }

        /// <summary>
        /// Change the topic for this chat
        /// </summary>
        /// <param name="topic"></param>
        public void ChangeTopic(string topic) {
            //TODO: Handle more than public chat
            //TODO: Check permissions before setting topic
            this.logicManager.ConnectionManager.Commands.Topic(1, topic);
        }
        #endregion
    }
}
