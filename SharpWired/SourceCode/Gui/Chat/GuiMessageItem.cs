#region Information and licence agreements
/*
 * StandardHTMLMessage.cs
 * Created by Ola Lindberg, 2008-03-05
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

namespace SharpWired.Gui.Chat {
    /// <summary>
    /// An object that takes ChatEvents or TopicsEvents and provides
    /// common get methods for printing to GUI
    /// </summary>
    public class GuiMessageItem {

        private string messageType;
        private DateTime timeStamp;
        private string nickName;
        private string message;

        /// <summary>
        /// Get the timestamp for this message
        /// </summary>
        public DateTime TimeStamp {
            get { return timeStamp; }
        }

        /// <summary>
        /// Get the nick for this message
        /// </summary>
        public string Nick {
            get { return nickName; }
        }

        /// <summary>
        /// Get the message string for this message
        /// </summary>
        public string Message {
            get { return message; }
        }

        /// <summary>
        /// Get the HTML for this object
        /// </summary>
        public string GeneratedHTML {
            get {
                StringBuilder html = new StringBuilder();
                html.Append("<div class=\"" + messageType + "\">");
                html.Append("<div class=\"time\">" + timeStamp + "</div>");
                html.Append("<div class=\"userName\">" + nickName + "</div>");
                html.Append("<div class=\"message\">" + message + "</div>");
                html.Append("</div>");

                return html.ToString();
            }
        }

        /// <summary>
        /// Creates a HTML writable object from a ChatTopicItem
        /// </summary>
        /// <param name="item"></param>
        public GuiMessageItem(ChatTopicItem item) {
            messageType = "topicEntry";

            timeStamp = item.MessageEventArgs.Time;
            nickName = item.MessageEventArgs.Nick;
            message = item.MessageEventArgs.Topic;
        }

        /// <summary>
        /// Creates a HTML writable object from a ChatMessageItem
        /// </summary>
        /// <param name="item"></param>
        public GuiMessageItem(ChatMessageItem item) {

            if (!item.IsActionChatMessage)
                messageType = "chatEntry";
            else
                messageType = "actionChatEntry";

            timeStamp = item.TimeStamp;
            nickName = item.FromUser.Nick;
            message = item.ChatMessage;
        }
    }
}
