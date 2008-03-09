using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model.Chat;
using SharpWired.Connection.Bookmarks;

namespace SharpWired.Gui.Chat {

    /// <summary>
    /// Control for chats
    /// </summary>
    public partial class ChatControl : UserControl {

        private String chatHeader;
        private String chatFooter;
        private StringBuilder chatBodyContent = new StringBuilder();
        private delegate void WriteToChatCallback(GuiMessageItem guiMessage);
        private delegate void ChangeTopicCallback(GuiMessageItem guiMessage);
        private GuiChatController guiChatController;
        private int chatId;
        private int altItemCounter = 0;

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

        /// <summary>
        /// Formats and writes the text on an Chat Event to the GUI
        /// </summary>
        /// <param name="chatTopicItem"></param>
        public void OnChatTopicChanged(ChatTopicItem chatTopicItem) {
            GuiMessageItem guiMessage = new GuiMessageItem(chatTopicItem);
            ChangeTopic(guiMessage);
        }

        /// <summary>
        /// Formats and writes the text on a Chat Event to the GUI
        /// </summary>
        /// <param name="chatMessageItem">The chat message item that was received</param>
        public void OnChatMessageArrived(ChatMessageItem chatMessageItem) {
            GuiMessageItem guiMessage = new GuiMessageItem(chatMessageItem);
            WriteHTMLToChat(guiMessage);       
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
            WriteHTMLToChat(gmi);
        }

        /// <summary>
        /// Call this method to write the given private message to chat window
        /// </summary>
        /// <param name="receivedPrivateMessage"></param>
        public void OnPrivateMessageReceived(SharpWired.Model.PrivateMessages.PrivateMessageItem receivedPrivateMessage) {
            GuiMessageItem gmi = new GuiMessageItem(receivedPrivateMessage);
            WriteHTMLToChat(gmi);
        }

        public void OnUserInformation(object sender, MessageEvents.MessageEventArgs_308 e) {
            GuiMessageItem gmi = new GuiMessageItem(e);
            WriteHTMLToChat(gmi);
        }

        private void WriteHTMLToChat(GuiMessageItem guiMessage)
        {
            if(this.InvokeRequired){
                WriteToChatCallback writeToChatCallback = new WriteToChatCallback(WriteHTMLToChat);
                this.Invoke(writeToChatCallback, new object[] { guiMessage });
            } else {
                chatBodyContent.Append(this.AltItemBeginningHtml);
                chatBodyContent.Append(guiMessage.GeneratedHTML);
                chatBodyContent.Append("</div>");
                chatWebBrowser.DocumentText = chatHeader + chatBodyContent + chatFooter;
            }
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
            this.guiChatController.SendChatMessage(chatInputTextBox.Text);
            chatInputTextBox.Clear();
        }

        private void chatInputTextBox_KeyUp(object sender, KeyEventArgs e) {
            if (!e.Shift && e.KeyCode == Keys.Enter) {
                this.guiChatController.SendChatMessage(chatInputTextBox.Text);
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
                guiChatController.ChangeTopic(topicTextBox.Text);
                topicDisplayLabel.Text = "Updating topic on server.";
                setByLabel.Text = "";
            } else if (e.KeyCode == Keys.Escape) {
                enableTopicEditing(false);
            }
        }

        private void topicDisplayLabel_MouseUp(object sender, MouseEventArgs e) {
            topicTextBox.Text = topicDisplayLabel.Text;
            enableTopicEditing(true);
        }

        private void topicDisplayLabel_MouseLeave(object sender, EventArgs e) {
            topicDisplayLabel.Cursor = Cursors.Default;
        }

        private void topicDisplayLabel_MouseEnter(object sender, EventArgs e) {
            topicDisplayLabel.Cursor = Cursors.Hand;
        }

        private void topicTextBox_Leave(object sender, EventArgs e) {
            enableTopicEditing(false);
        }

        private void enableTopicEditing(bool editable) {
            if (editable) {
                topicDisplayLabel.Visible = false;
                topicTextBox.Visible = true;
                topicTextBox.Focus();
            } else {
                topicDisplayLabel.Visible = true;
                topicTextBox.Visible = false;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Constructor
        /// </summary>
        public ChatControl() {
            InitializeComponent();

            String chatStyleSheet = "<link href=\"" + GuiUtil.CSSFilePath + "\\GUI\\SharpWiredStyleSheet.css\" rel=\"stylesheet\" type=\"text/css\" />";
            String chatJavaScript = "<script>function pageDown () { if (window.scrollBy) window.scrollBy(0, window.innerHeight ? window.innerHeight : document.body.clientHeight); }</script>";

            chatHeader = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\" lang=\"en\">" +
                "<head><title>SharpWired</title>" +
                    chatJavaScript +
                    chatStyleSheet +
                "</head><body onload=\"pageDown(); return false;\">";

            chatFooter = "</body></html>";

            chatWebBrowser.DocumentText = chatHeader + chatFooter;
        }

        /// <summary>
        /// Initializes this component
        /// </summary>
        /// <param name="guiChatController"></param>
        /// <param name="chatId">The id for this chat</param>
        public void Init(GuiChatController guiChatController, int chatId) {
            this.guiChatController = guiChatController;
            this.chatId = chatId;
        }
        #endregion
    }
}
