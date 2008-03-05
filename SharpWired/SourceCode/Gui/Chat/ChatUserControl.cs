#region Information and licence agreements
/*
 * ChatUserControl.cs
 * Created by Ola Lindberg, 2006-10-12
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model;
using SharpWired.Model.Users;
using SharpWired.Model.Chat;

namespace SharpWired.Gui.Chat
{
    /// <summary>
    /// User control for the chat
    /// </summary>
    public partial class ChatUserControl : UserControl
    {
        LogicManager logicManager;
        private int altItemCounter = 0;

        //TODO: Find a better way for calculating alt/standard divs
        private string AltItemBeginningHtml {
            get {
                if (altItemCounter % 2 == 0) {
                    altItemCounter++;
                    return "<div class=\"standard\">";
                }
                altItemCounter++;
                return "<div class=\"alternative\">";
            }
        }

        #region Listeners: From model
        void ErrorHandler_LoginToServerFailedEvent(string errorDescription, string solutionIdea, SharpWired.Connection.Bookmarks.Bookmark bookmark)
        {
            string formatedText = this.AltItemBeginningHtml +
                "<div class=\"errorEntry\">" +
                    "<div class=\"time\">" + DateTime.Now + "</div>" +
                    "<div class=\"errorDescription\"><em>Problem: </em>" + errorDescription + "</div>" +
                    "<div class=\"solutionIdea\"><em>Resolution: </em>" + solutionIdea + "</div>" +
                    "<div class=\"serverInformation\"><em>Server: </em>" + bookmark.Server.ServerName + "</div>" +
                "</div>" +
            "</div>";

//            WriteHTMLToChat(formatedText);
        }

        void PrivateMessageModel_ReceivedPrivateMessageEvent(SharpWired.Model.PrivateMessages.PrivateMessageItem receivedPrivateMessage)
        {
            OnPrivateMessageEvent(receivedPrivateMessage, "received");
        }

        void PrivateMessageModel_SentPrivateMessageEvent(SharpWired.Model.PrivateMessages.PrivateMessageItem sentPrivateMessage)
        {
            OnPrivateMessageEvent(sentPrivateMessage, "sent");
        }
        #endregion

        #region Methods: Receiving messages



        /// <summary>
        /// Format HTML for current lag
        /// </summary>
        private void FormatAndWriteHTMLForCurrentLag(TimeSpan lag)
        {
            string formatedText = this.AltItemBeginningHtml +
                "<div class=\"infoEntry\">" +
                    "<div class=\"message\"> Current lag is: " + lag.Seconds + " seconds.</div>" +
                "</div>" +
            "</div>";

//            WriteHTMLToChat(formatedText);
        }

        #endregion

        #region Methods: Sending messages

        private void OnPrivateMessageEvent(SharpWired.Model.PrivateMessages.PrivateMessageItem messageItem, string receivedOrSent)
        {
            string formatedText = this.AltItemBeginningHtml +
                "<div class=\"privateMessageEntry privateMessageEntry_" + receivedOrSent + "\">" +
                    "<div class=\"time\">" + messageItem.TimeStamp + "</div>" +
                    "<div class=\"userName\">" + messageItem.UserItem.Nick + "</div>" +
                    "<div class=\"message\">" + messageItem.Message + "</div>" +
                "</div>" +
            "</div>";

//            WriteHTMLToChat(formatedText);
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logicManager"></param>
        public void Init(LogicManager logicManager)
        {
            this.logicManager = logicManager;
            //TODO: It's a bit aquard that this gui class initializes the controller class
            GuiChatController guiChatController = new GuiChatController(logicManager, this.chatControl, this.userListControl);

            logicManager.PrivateMessagesHandler.PrivateMessageModel.ReceivedPrivateMessageEvent += new SharpWired.Model.PrivateMessages.PrivateMessageModel.ReceivedPrivateMessageDelegate(PrivateMessageModel_ReceivedPrivateMessageEvent);
            logicManager.PrivateMessagesHandler.PrivateMessageModel.SentPrivateMessageEvent += new SharpWired.Model.PrivateMessages.PrivateMessageModel.SentPrivateMessageDelegate(PrivateMessageModel_SentPrivateMessageEvent);

            logicManager.ErrorHandler.LoginToServerFailedEvent += new SharpWired.Model.Errors.ErrorHandler.LoginToServerFailedDelegate(ErrorHandler_LoginToServerFailedEvent);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ChatUserControl()
        {
            InitializeComponent();
        }

        #endregion
    }
}
