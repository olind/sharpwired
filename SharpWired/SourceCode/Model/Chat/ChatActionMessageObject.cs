#region Information and licence agreements
/*
 * ChatActionMessageObject.cs 
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
    /// Action chat message object
    /// </summary>
    public class ChatActionMessageObject : ChatMessageItem
    {
        private MessageEventArgs_300301 messageEventArgs;

        /// <summary>
        /// Gets or sets the chat message event
        /// </summary>
        public MessageEventArgs_300301 MessageEventArgs
        {
            get { return messageEventArgs; }
            set { messageEventArgs = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageEventArgs"></param>
        public ChatActionMessageObject()
        {
            this.messageEventArgs = messageEventArgs;
        }
    }
}
