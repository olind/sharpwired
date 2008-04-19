#region Information and licence agreements
/*
 * ChatModel.cs 
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
using SharpWired.Model.Users;

namespace SharpWired.Model.Chat
{
    /// <summary>
    /// The model that the gui can listen to for changes in the chat
    /// </summary>
    public class ChatModel
    {
        private LogicManager logicManager;
        private List<ChatTopicItem> chatTopics;
        private List<ChatMessageItem> chatMessages;
        
        /// <summary>
        /// Call this method when a new message has been received.
        /// </summary>
        /// <param name="messageEventArgs">The message event args associated 
        ///     with the received chat message.</param>
        /// <param name="fromUser">The user that sent the message</param>
        /// <param name="isActionChat">Tells if this is an action chat message 
        ///     or not. True means action chat message. False means normal chat 
        ///     message.</param>
        public void OnChatMessageItemReceived (MessageEventArgs_300301 messageEventArgs, 
            UserItem fromUser, bool isActionChat){

            if (ChatMessageReceivedEvent != null) {
                ChatMessageItem cmi = new ChatMessageItem(messageEventArgs, fromUser, isActionChat);
                chatMessages.Add(cmi);
                ChatMessageReceivedEvent(cmi);
            }
        }

        /// <summary>
        /// Call this method when the chat topic has been changed
        /// </summary>
        /// <param name="chatTopicObject">The chat topic object.</param>
        public void OnChatTopicChanged(ChatTopicItem chatTopicObject)
        {
            if (ChatTopicChangedEvent != null) {
                chatTopics.Add(chatTopicObject);
                ChatTopicChangedEvent(chatTopicObject);
            }
        }

        #region Delegate and Event declarations
        /// <summary>
        /// Delegate for the ChatMessageReceivedEvent
        /// </summary>
        /// <param name="chatMessageItem"></param>
        public delegate void ChatMessageReceivedDelegate(ChatMessageItem chatMessageItem);
        /// <summary>
        /// Event that's raised when a chat message has arrived
        /// </summary>
        public event ChatMessageReceivedDelegate ChatMessageReceivedEvent;

        /// <summary>
        /// Delegate for the ChatTopicChangedEvent
        /// </summary>
        /// <param name="chatTopicObject"></param>
        public delegate void ChatTopicChangedDelegate(ChatTopicItem chatTopicObject);
        /// <summary>
        /// Event that's raised when the chat topic has been changed
        /// </summary>
        public event ChatTopicChangedDelegate ChatTopicChangedEvent;
        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logicManager"></param>
        public ChatModel(LogicManager logicManager)
        {
            this.logicManager = logicManager;
            chatTopics = new List<ChatTopicItem>();
            chatMessages = new List<ChatMessageItem>();
        }

        #endregion

    }
}
