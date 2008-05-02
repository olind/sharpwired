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

using System.Collections.Generic;
using SharpWired.Connection;
using SharpWired.MessageEvents;
using SharpWired.Model.Users;

namespace SharpWired.Model.Messaging {
    /// <summary>
    /// The model that the gui can listen to for changes in the chat
    /// </summary>
    public class Chat {

        #region Fields
        LogicManager logicManager;
        int chatId;
        UserList users;
        MessageEventArgs_341 topic;
        List<ChatMessageItem> chatMessages;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logicManager"></param>
        /// <param name="chatId"></param>
        public Chat(LogicManager logicManager, int chatId) {
            Messages m = logicManager.ConnectionManager.Messages;

            this.logicManager = logicManager;
            this.chatId = chatId;
            this.chatMessages = new List<ChatMessageItem>();
            this.users = new UserList(m);

            m.ChatTopicEvent += OnTopicChanged;
            m.ChatEvent += OnChatEvent;
            m.ActionChatEvent += OnActionChatEvent;
        }
        #endregion

        #region Properties
        public UserList Users {
            get { return users; }
        }
        #endregion

        #region Events & Listeners
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
        /// <param name="message"></param>
        public delegate void ChatTopicChangedDelegate(MessageEventArgs_341 message);
        /// <summary>
        /// Event that's raised when the chat topic has been changed
        /// </summary>
        public event ChatTopicChangedDelegate ChatTopicChangedEvent;

        public void OnTopicChanged(MessageEventArgs_341 message) {
            if (message.ChatId == this.chatId) {
                this.topic = message;

                if (ChatTopicChangedEvent != null)
                    ChatTopicChangedEvent(message);
            }
        }

        public void OnChatEvent(object sender, MessageEventArgs_300301 message) {
            HandleMessage(message, false);
        }

        public void OnActionChatEvent(object sender, MessageEventArgs_300301 message) {
            HandleMessage(message, true);
        }

        // TODO: MessageEventArgs_3003001 could contain isActionChat from the beginning!
        private void HandleMessage(MessageEventArgs_300301 message, bool isActionChat) {
            if (message.ChatId == this.chatId) {
                User u = this.users.GetUser(message.UserId);
                ChatMessageItem cmi = new ChatMessageItem(message, u, isActionChat);
                chatMessages.Add(cmi);

                if (ChatMessageReceivedEvent != null)
                    ChatMessageReceivedEvent(cmi);
            }
        }
        #endregion
    }
}
