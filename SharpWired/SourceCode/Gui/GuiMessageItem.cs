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
using SharpWired.Model.Messaging;
using SharpWired.Model.Users;
using SharpWired.Connection.Bookmarks;
using SharpWired.Model.PrivateMessages;
using System.Web;
using SharpWired.Model.News;
using SharpWired.MessageEvents;

namespace SharpWired.Gui {
    /// <summary>
    /// An object that takes ChatEvents or TopicsEvents and provides
    /// common get methods for printing to GUI
    /// </summary>
    public class GuiMessageItem {

        // General
        private DateTime timeStamp;
        private string messageType;
        
        //For chat and topic messages
        private string nickName;
        private string message;

        //For error messages
        private bool isErrorMessage = false;
        private string errorDescription;
        private string solutionIdea;
        private Bookmark bookmark;

        /// <summary>
        /// Request the timestamp for this message
        /// </summary>
        public DateTime TimeStamp {
            get { return timeStamp; }
        }

        /// <summary>
        /// Request the nick for this message
        /// </summary>
        public string Nick {
            get { return nickName; }
        }

        /// <summary>
        /// Request the message string for this message
        /// </summary>
        public string Message {
            get { return message; }
        }

        /// <summary>
        /// Request the HTML for this object.
        /// NOTE! All fields are HTML encoded
        /// </summary>
        public string GeneratedHTML {
            get {
                if (isEmptyMessage)
                    return "";

                StringBuilder html = new StringBuilder();
                html.Append("<div class=\"" + messageType + "\">");
                html.Append("<div class=\"time\">" + timeStamp + "</div>");

                if (!isErrorMessage) {
                    if (nickName != null)
                        nickName = HttpUtility.HtmlEncode(nickName);
                    if (message != null)
                        message = HttpUtility.HtmlEncode(message);

                    message = message.Replace("\\r\\n", "<br />");

                    html.Append("<div class=\"userName\">" + nickName + "</div>");
                    html.Append("<div class=\"message\">" + message + "</div>");
                } else if (isErrorMessage) {
                    if (errorDescription != null)
                        errorDescription = HttpUtility.HtmlEncode(errorDescription);
                    if (solutionIdea != null)
                        solutionIdea = HttpUtility.HtmlEncode(solutionIdea);

                    html.Append("<div class=\"errorDescription\"><em>Problem: </em>" + errorDescription + "</div>");
                    html.Append("<div class=\"solutionIdea\"><em>Resolution: </em>" + solutionIdea + "</div>");
                    html.Append("<div class=\"serverInformation\"><em>Server: </em>" + bookmark.Server.ServerName + "</div>");
                }

                html.Append("</div>");
                return html.ToString();
            }
        }

        /// <summary>
        /// Creates a HTML writable object from a ChatTopicItem
        /// </summary>
        /// <param name="messageArgs"></param>
        public GuiMessageItem(MessageEventArgs_341 messageArgs) {
            messageType = "topicEntry";

            timeStamp = messageArgs.Time;
            nickName = messageArgs.Nick;
            message = messageArgs.Topic;
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

        /// <summary>
        /// Constructor for error messages
        /// </summary>
        /// <param name="errorDescription"></param>
        /// <param name="solutionIdea"></param>
        /// <param name="bookmark"></param>
        public GuiMessageItem(string errorDescription, string solutionIdea, 
            Bookmark bookmark) {

            this.messageType = "errorEntry";
            this.isErrorMessage = true;
            this.timeStamp = DateTime.Now;
            this.errorDescription = errorDescription;
            this.solutionIdea = solutionIdea;
            this.bookmark = bookmark;
        }

        /// <summary>
        /// Constructor for private messages
        /// </summary>
        /// <param name="item"></param>
        public GuiMessageItem(PrivateMessageItem item) {
            messageType = "privateMessageEntry privateMessageEntry_received";
            timeStamp = item.TimeStamp;
            nickName = item.UserItem.Nick;
            message = item.Message;
        }

        /// <summary>
        /// Constructor for news post
        /// </summary>
        /// <param name="newPost"></param>
        public GuiMessageItem(NewsPost newPost) {
            messageType = "newsEntry";
            timeStamp = newPost.PostTime;
            nickName = newPost.Nick;
            message = newPost.Post;
        }

        /// <summary>
        /// Constructor for Client Information
        /// </summary>
        /// <param name="e"></param>
        public GuiMessageItem(MessageEvents.MessageEventArgs_308 e) {
            // TODO: Make generic info output instead of specific for all events
            // in connection layer.

            this.messageType = "infoEntry";
            this.nickName = "";
            this.timeStamp = DateTime.Now;
            StringBuilder s = new StringBuilder();
            s.Append("User Information" + "\\r\\n\\r\\n");
            s.Append("Nick: " + e.Nick + "\\r\\n");
            s.Append("Login: " + e.Login + "\\r\\n");
            s.Append("UserId: " + e.UserId + "\\r\\n");
            s.Append("Idle: " + e.Idle + "\\r\\n");
            s.Append("IdleTime: " + e.IdleTime + "\\r\\n");
            s.Append("LoginTime: " + e.LoginTime + "\\r\\n");
            s.Append("Status: " + e.Status + "\\r\\n");
            s.Append("Admin: " + e.Admin + "\\r\\n");
            s.Append("Host: " + e.Host + "\\r\\n");
            s.Append("Ip:  "+ e.Ip + "\\r\\n");
            s.Append("ClientVersion: " + e.ClientVersion + "\\r\\n");
            s.Append("Downloads: " + e.Downloads + "\\r\\n");
            s.Append("Path: " + e.Path + "\\r\\n");
            s.Append("Size: " + e.Size + "\\r\\n");
            s.Append("Speed: " + e.Speed + "\\r\\n");
            s.Append("Transfer: " + e.Transfer + "\\r\\n");
            s.Append("Transferred: " + e.Transferred + "\\r\\n");
            s.Append("Uploads: " + e.Uploads + "\\r\\n");
            s.Append("CipherBits: " + e.CipherBits + "\\r\\n");
            s.Append("CipherName: " + e.CipherName + "\\r\\n");
            //s.Append("Icon" + e.Icon + "\\r\\n");
            //s.Append("Image" + e.Image + "\\r\\n");

            this.message = s.ToString();
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public GuiMessageItem() {
            isEmptyMessage = true;
        }

        private bool isEmptyMessage = false;
    }
}
