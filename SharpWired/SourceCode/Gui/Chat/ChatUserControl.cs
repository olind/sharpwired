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
        #region Variables

        private LogicManager logicManager;
        private string chatBodyContent;
        private string chatCSSFilePath;

        // HTML output
        string chatJavaScript;
        string chatStyleSheet;
        string chatHeader;
        string chatFooter;
        private int altItemCounter;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the file path for the CSS-file
        /// </summary>
        public string CSSFilePath
        {
            get
            {
                if (chatCSSFilePath == null)
                    return System.Environment.CurrentDirectory; //TODO: The path to the CSS-file should probably be set in some other way
                return chatCSSFilePath;
            }
            set
            {
                chatCSSFilePath = value;
            }
        }

        /// <summary>
        /// Get the HTML-code to output for the standard/alt-items
        /// </summary>
        private string AltItemBeginningHtml
        {
            get
            {
                if (altItemCounter % 2 == 0)
                {
                    altItemCounter++;
                    return "<div class=\"standard\">";
                }
                altItemCounter++;
                return "<div class=\"alternative\">";
            }
        }

        #endregion

        #region Listeners: From model

        void ChatModel_ChatTopicChangedEvent(object sender, global::SharpWired.Model.Chat.ChatTopicObject chatTopicObject)
        {
            OnChatTopicChangedEvent(chatTopicObject);
        }

        void ChatModel_ChatMessageChangedEvent(object sender, global::SharpWired.Model.Chat.ChatMessageObject chatMessageObject)
        {
            OnChatMessageChangedEvent(chatMessageObject);
        }

        void ChatModel_ChatActionMessageChangedEvent(object sender, global::SharpWired.Model.Chat.ChatActionMessageObject chatActionMessageObject)
        {
            OnChatActionMessageChangedEvent(chatActionMessageObject);
        }

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

            WriteHTMLToChat(formatedText);
        }

        void PrivateMessageModel_ReceivedPrivateMessageEvent(SharpWired.Model.PrivateMessages.PrivateMessageItem receivedPrivateMessage)
        {
            string formatedText = this.AltItemBeginningHtml +
                "<div class=\"privateMessageEntry\">" +
                    "<div class=\"time\">" + receivedPrivateMessage.TimeStamp + "</div>" +
                    "<div class=\"userName\">" + receivedPrivateMessage.UserItem.Nick + "</div>" +
                    "<div class=\"message\">" + receivedPrivateMessage.Message + "</div>" +
                "</div>" +
            "</div>";

            WriteHTMLToChat(formatedText);
        }

        #endregion

        #region Listeners: From GUI-Window

        private void sendChatButton_Click(object sender, EventArgs e)
        {
            SendChatMessage();
            sendChatRichTextBox.Clear();
        }

        private void sendChatRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendChatMessage();
                sendChatRichTextBox.Clear();
            }
            if (e.KeyCode == Keys.Escape)
            {
                sendChatRichTextBox.Clear();
            }
        }

        #endregion

        #region Methods: Receiving messages

        /// <summary>
        /// Formats and writes the text on an Chat Event to the GUI
        /// </summary>
        /// <param name="chatTopicObject"></param>
        private void OnChatTopicChangedEvent(ChatTopicObject chatTopicObject)
        {
            /*
             * Example of the html we want to produce here. class="standard" is class="alt" every other time
             * 
             *   <div class="standard">
             *      <div class="topicEntry">
             *          <div class="time">2006-11-25 19:54:44</div>
             *          <div class="userName">Ola (Wire)</div>
             *          <div class="message">Detta är toppentoppic!</div>
             *      </div>
             *   </div>
             */

            string formatedText = this.AltItemBeginningHtml +
                "<div class=\"topicEntry\">" +
                    "<div class=\"time\">" + chatTopicObject.MessageEventArgs.Time + "</div>" +
                    "<div class=\"userName\">" + chatTopicObject.MessageEventArgs.Nick + "</div>" +
                    "<div class=\"message\">" + chatTopicObject.MessageEventArgs.Topic + "</div>" +
                "</div>" +
            "</div>";

            WriteHTMLToChat(formatedText);
        }

        /// <summary>
        /// Formats and writes the text on an Chat Event to the GUI
        /// </summary>
        /// <param name="chatMessageObject"></param>
        private void OnChatMessageChangedEvent(ChatMessageObject chatMessageObject)
        {
            /*
             * Example of the html we want to produce here. class="standard" is class="alt" every other time
             * 
             *   <div class="standard">
	         *       <div class="chatEntry">
		     *           <div class="time">2006-12-01 00:00:00 22:28:50.2851648</div>
		     *           <div class="userName">Ola (SharpWired)</div>
		     *           <div class="message">ueoahdue</div>
	         *       </div>
             *   </div>
             */

            UserItem u = logicManager.UserHandler.UserModel.GetUser(chatMessageObject.MessageEventArgs.UserId);

            string formatedText = this.AltItemBeginningHtml +
                "<div class=\"chatEntry\">" +
                    "<div class=\"time\">" + DateTime.Now + "</div>" +
                    "<div class=\"userName\">" + u.Nick + "</div>" +
                    "<div class=\"message\">" + chatMessageObject.MessageEventArgs.Message + "</div>" +
                "</div>" +
            "</div>";

            WriteHTMLToChat(formatedText);
        }

        /// <summary>
        /// Formats and writes the text on an Action Chat Event to the GUI
        /// </summary>
        /// <param name="chatActionMessageObject"></param>
        private void OnChatActionMessageChangedEvent(ChatActionMessageObject chatActionMessageObject)
        {
            /*
             * Example of the html we want to produce here. class="standard" is class="alt" every other time
             * 
             *   <div class="standard">
	         *       <div class="chatEntry">
		     *           <div class="time">2006-12-01 00:00:00 22:28:50.2851648</div>
		     *           <div class="userName">Ola (SharpWired)</div>
		     *           <div class="message">ueoahdue</div>
	         *       </div>
             *   </div>
             */

            UserItem u = logicManager.UserHandler.UserModel.GetUser(chatActionMessageObject.MessageEventArgs.UserId);

            string formatedText = this.AltItemBeginningHtml +
                "<div class=\"actionChatEntry\">" +
                    "<div class=\"time\">" + DateTime.Now.Date + "</div>" +
                    "<div class=\"userName\">" + u.Nick + "</div>" +
                    "<div class=\"message\">" + chatActionMessageObject.MessageEventArgs.Message + "</div>" +
                "</div>" +
            "</div>";

            WriteHTMLToChat(formatedText);
        }

        /// <summary>
        /// Writes the HTML-formated string to the GUI
        /// </summary>
        /// <param name="formatedText"></param>
        private void WriteHTMLToChat(string formatedText)
        {
            if(this.InvokeRequired){
                WriteToChatCallback writeToChatCallback = new WriteToChatCallback(WriteHTMLToChat);
                this.Invoke(writeToChatCallback, new object[] { formatedText });
            } else {
                chatBodyContent += formatedText;
                chatWebBrowser.DocumentText = chatHeader + chatBodyContent + chatFooter;
            }
        }
        delegate void WriteToChatCallback(string formatedText);

        #endregion

        #region Methods: Sending messages

        /// <summary>
        /// Sends a chat message to the server
        /// </summary>
        public void SendChatMessage()
        {
            logicManager.ChatHandler.SendChatMessage(sendChatRichTextBox.Text);
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
            userListControl.Init(logicManager);

            logicManager.ChatHandler.ChatModel.ChatActionMessageChangedEvent += new global::SharpWired.Model.Chat.ChatModel.ChatActionMessageChangedDelegate(ChatModel_ChatActionMessageChangedEvent);
            logicManager.ChatHandler.ChatModel.ChatMessageChangedEvent += new global::SharpWired.Model.Chat.ChatModel.ChatMessageChangedDelegate(ChatModel_ChatMessageChangedEvent);
            logicManager.ChatHandler.ChatModel.ChatTopicChangedEvent += new global::SharpWired.Model.Chat.ChatModel.ChatTopicChangedDelegate(ChatModel_ChatTopicChangedEvent);

            logicManager.PrivateMessagesHandler.PrivateMessageModel.ReceivedPrivateMessageEvent += new SharpWired.Model.PrivateMessages.PrivateMessageModel.ReceivedPrivateMessageDelegate(PrivateMessageModel_ReceivedPrivateMessageEvent);

            logicManager.ErrorHandler.LoginToServerFailedEvent += new SharpWired.Model.Errors.ErrorHandler.LoginToServerFailedDelegate(ErrorHandler_LoginToServerFailedEvent);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ChatUserControl()
        {
            InitializeComponent();

            chatStyleSheet = "<link href=\"" + CSSFilePath + "\\GUI\\SharpWiredStyleSheet.css\" rel=\"stylesheet\" type=\"text/css\" />";
            chatJavaScript = "<script>function pageDown () { if (window.scrollBy) window.scrollBy(0, window.innerHeight ? window.innerHeight : document.body.clientHeight); }</script>";

            chatHeader = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\" lang=\"en\">" + 
                "<head><title>SharpWired</title>" + 
                    chatJavaScript + 
                    chatStyleSheet + 
                "</head><body onload=\"pageDown(); return false;\">";

            chatFooter = "</body></html>";

            chatWebBrowser.DocumentText = chatHeader + chatFooter;
        }

        #endregion
    }
}
