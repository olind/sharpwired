#region Information and licence agreements
/*
 * Chat.cs 
 * Created by Ola Lindberg, 2006-09-28
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
using System.Text;
using System.Windows.Forms;
using SharpWired.Connection.Bookmarks;
using SharpWired.MessageEvents;
using SharpWired.Model.Messaging;
using SharpWired.Controller;
using SharpWired.Model;
using System.Diagnostics;
using System.Drawing;

namespace SharpWired.Gui.Chat {
    /// <summary>
    /// Control for chats
    /// </summary>
    public partial class Chat : WebBrowserGuiBase {

        private delegate void ChangeTopicCallback(GuiMessageItem guiMessage);

        #region Initialization
        public Chat() {
            InitializeComponent();
        }
        #endregion

        protected override void OnOnline() {
            model.Server.PublicChat.ChatMessageReceivedEvent += OnChatMessageArrived;
            model.Server.PublicChat.ChatTopicChangedEvent += OnChatTopicChanged;

            ToggleWindowsFormControl(chatInputTextBox);
            ToggleWindowsFormControl(sendChatButton);
            ToggleWindowsFormControl(topicDisplayLabel);
            ToggleWindowsFormControl(setByLabel);

            ResetWebBrowser(chatWebBrowser);
        }

        protected override void OnOffline() {
            model.Server.Offline -= OnOffline;
            model.Server.PublicChat.ChatMessageReceivedEvent -= OnChatMessageArrived;
            model.Server.PublicChat.ChatTopicChangedEvent -= OnChatTopicChanged;

            ToggleWindowsFormControl(chatInputTextBox);
            ToggleWindowsFormControl(sendChatButton);
            ToggleWindowsFormControl(topicDisplayLabel);
            ToggleWindowsFormControl(setByLabel);
        }

        /// <summary>
        /// Formats and writes the text on an Chat Event to the GUI
        /// </summary>
        /// <param name="message"></param>
        public void OnChatTopicChanged(MessageEventArgs_341 message) {
            GuiMessageItem guiMessage = new GuiMessageItem(message);
            ChangeTopic(guiMessage);
        }

        /// <summary>
        /// Formats and writes the text on a Chat Event to the GUI
        /// </summary>
        /// <param name="chatMessageItem">The chat message item that was received</param>
        public void OnChatMessageArrived(ChatMessageItem chatMessageItem) {
            GuiMessageItem guiMessage = new GuiMessageItem(chatMessageItem);
            AppendHTMLToWebBrowser(chatWebBrowser, guiMessage);
        }

        /// <summary>
        /// Call this method to report an error that should be printed to chat window
        /// </summary>
        /// <param name="errorDescription"></param>
        /// <param name="solutionIdea"></param>
        /// <param name="bookmark"></param>
        public void OnErrorEvent(string errorDescription, string solutionIdea,
            Bookmark bookmark) {
            GuiMessageItem gmi = new GuiMessageItem(errorDescription, solutionIdea, bookmark);
            AppendHTMLToWebBrowser(chatWebBrowser, gmi);
        }

        public override void Init(SharpWiredModel model, SharpWiredController controller) {
            base.Init(model, controller);
        }
        
        private void ChangeTopic(GuiMessageItem guiMessage) {
            if (this.InvokeRequired) {
                ChangeTopicCallback changeTopicCallback = new ChangeTopicCallback(ChangeTopic);
                this.Invoke(changeTopicCallback, new object[] { guiMessage });
            } else {
                this.topicDisplayLabel.Text = guiMessage.Message;
                StringBuilder sb = new StringBuilder();
                sb.Append(guiMessage.Nick);
                sb.Append(" - ");
                sb.Append(guiMessage.TimeStamp);
                this.setByLabel.Text = sb.ToString();
            }
        }

        #region Send chat messages
        private void sendChatButton_MouseUp(object sender, MouseEventArgs e) {
            controller.ChatController.SendChatMessage(chatInputTextBox.Text);
            chatInputTextBox.Clear();
        }

        private void chatInputTextBox_KeyUp(object sender, KeyEventArgs e) {
            if (!e.Shift && e.KeyCode == Keys.Enter) {
                controller.ChatController.SendChatMessage(chatInputTextBox.Text);
                chatInputTextBox.Clear();
            }
            if (e.KeyCode == Keys.Escape) {
                chatInputTextBox.Clear();
            }
        }
        #endregion

        #region Edit topic
        //TODO: Only enable topic changing if we are online
        //TODO: Only enable topic changing if the user has permissions to change it
        private void topicTextBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                enableTopicEditing(false);

                controller.ChatController.ChangeTopic(topicTextBox.Text);

                topicDisplayLabel.Text = "Updating topic on server.";
                setByLabel.Text = "";
            } else if (e.KeyCode == Keys.Escape) {
                enableTopicEditing(false);
            }
        }

        private void topicDisplayLabel_MouseUp(object sender, MouseEventArgs e) {
            //TODO: Make better check to see if we are online/offline
            if (this.model.Server != null && this.model.Server.PublicChat != null) {
                topicTextBox.Text = topicDisplayLabel.Text;
                enableTopicEditing(true); 
            }
        }

        private void topicDisplayLabel_MouseLeave(object sender, EventArgs e) {
            topicDisplayLabel.Cursor = Cursors.Default;
        }

        private void topicDisplayLabel_MouseEnter(object sender, EventArgs e) {
            if(this.model.Server != null && this.model.Server.PublicChat != null) //TODO: Make better check to see if we are online/offline
                topicDisplayLabel.Cursor = Cursors.Hand;
        }

        private void topicTextBox_Leave(object sender, EventArgs e) {
            enableTopicEditing(false);
        }

        private void enableTopicEditing(bool editable) {
            if (editable) {
                topicDisplayLabel.Visible = false;
                topicTextBox.Visible = true;
                topicTextBox.BackColor = Color.White;
                topicTextBox.Focus();
            } else {
                topicDisplayLabel.Visible = true;
                topicTextBox.Visible = false;
            }
        }
        #endregion
    }
}
