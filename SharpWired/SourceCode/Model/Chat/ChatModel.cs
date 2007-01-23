#region Information and licence agreements
/**
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

namespace SharpWired.Model.Chat
{
    /// <summary>
    /// The model that the gui can listen to for changes in the chat
    /// </summary>
    public class ChatModel
    {
        private List<ChatObject> chatTopicEvents;
        private List<ChatObject> chatMessageEvents;
        private List<ChatObject> chatActionMessageEvents;
        private LogicManager logicManager;

        internal void AddTopic(ChatTopicObject chatTopicObject)
        {
            chatTopicEvents.Add(chatTopicObject);
            OnChatTopicChangedEvent(chatTopicObject);
        }

        internal void AddMessage(ChatMessageObject chatMessageObject)
        {
            chatMessageEvents.Add(chatMessageObject);
            OnChatMessageChangedEvent(chatMessageObject);
        }

        internal void AddActionMessage(ChatActionMessageObject chatActionMessageObject)
        {
            chatActionMessageEvents.Add(chatActionMessageObject);
            OnChatActionMessageChangedEvent(chatActionMessageObject);
        }

        //
        // Delegates, events and raisers
        //

        private void OnChatTopicChangedEvent(ChatTopicObject chatTopicObject){
            if (ChatTopicChangedEvent != null)
                ChatTopicChangedEvent(this, chatTopicObject);
        }
        public delegate void ChatTopicChangedDelegate(object sender, ChatTopicObject chatTopicObject);
        public event ChatTopicChangedDelegate ChatTopicChangedEvent;

        private void OnChatMessageChangedEvent(ChatMessageObject chatMessageObject)
        {
            if (ChatMessageChangedEvent != null)
                ChatMessageChangedEvent(this, chatMessageObject);
        }
        public delegate void ChatMessageChangedDelegate(object sender, ChatMessageObject chatMessageObject);
        public event ChatMessageChangedDelegate ChatMessageChangedEvent;

        private void OnChatActionMessageChangedEvent(ChatActionMessageObject chatActionMessageObject)
        {
            if (ChatActionMessageChangedEvent != null)
                ChatActionMessageChangedEvent(this, chatActionMessageObject);
        }
        public delegate void ChatActionMessageChangedDelegate(object sender, ChatActionMessageObject chatActionMessageObject);
        public event ChatActionMessageChangedDelegate ChatActionMessageChangedEvent;

        #region Initialization

        public ChatModel(LogicManager logicManager)
        {
            this.logicManager = logicManager;
            chatTopicEvents = new List<ChatObject>();
            chatMessageEvents = new List<ChatObject>();
            chatActionMessageEvents = new List<ChatObject>();
        }

        #endregion

    }
}
