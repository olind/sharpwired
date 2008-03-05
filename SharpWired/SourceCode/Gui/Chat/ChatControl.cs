using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model.Chat;

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

        private void WriteHTMLToChat(GuiMessageItem guiMessage)
        {
            if(this.InvokeRequired){
                WriteToChatCallback writeToChatCallback = new WriteToChatCallback(WriteHTMLToChat);
                this.Invoke(writeToChatCallback, new object[] { guiMessage });
            } else {

                //TODO: HTML encode formatedText
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
                this.topicTextBox.Text = guiMessage.Message;
                StringBuilder sb = new StringBuilder();
                sb.Append(guiMessage.Nick);
                sb.Append(" - ");
                sb.Append(guiMessage.TimeStamp);
                this.setByLabel.Text = sb.ToString();
            }
        }

        private void sendChatButton_MouseUp(object sender, MouseEventArgs e) {
            this.guiChatController.SendChatMessage(sendChatRichTextBox.Text);
            //TODO: after clearing sendChatRichTextBox it receives a newline that I can't remove
            sendChatRichTextBox.Clear();
        }

        private void sendChatRichTextBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                this.guiChatController.SendChatMessage(sendChatRichTextBox.Text);
                //TODO: after clearing sendChatRichTextBox it receives a newline that I can't remove
                sendChatRichTextBox.Clear();
            } else if (e.KeyCode == Keys.Escape) {
                //TODO: after clearing sendChatRichTextBox it receives a newline that I can't remove
                sendChatRichTextBox.Clear();
            }
        }

        private void chatTopicTextBox_MouseUp(object sender, MouseEventArgs e) {
            if (topicTextBox.ReadOnly) {
                topicTextBox.ReadOnly = false;
                topicTextBox.BackColor = Control.DefaultBackColor;
                topicTextBox.BorderStyle = BorderStyle.FixedSingle;
                Point p = new Point(topicTextBox.Location.X, topicTextBox.Location.Y - 1);
                topicTextBox.Location = p;
            }
        }

        private void topicTextBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                topicTextBox.ReadOnly = true;
                topicTextBox.BackColor = Color.White;
                topicTextBox.BorderStyle = BorderStyle.None;
                Point p = new Point(topicTextBox.Location.X, topicTextBox.Location.Y + 1);
                topicTextBox.Location = p;

                guiChatController.ChangeTopic(topicTextBox.Text);
                topicTextBox.Text = "Updating topic on server..."; //Empty topic - Set once it's set on the server (topic event received)
            }
        }

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
