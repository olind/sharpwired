#region Information and licence agreements
/*
 * NewsUserControl.cs
 * Created by Ola Lindberg, 2006-12-10
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
using SharpWired.Model.News;
using SharpWired.Model.Users;
using System.Web;
using System.Diagnostics;

namespace SharpWired.Gui.News {
    public partial class NewsUserControl : UserControl {
        #region Fields
        LogicManager logicManager;
        StringBuilder newsBodyContent = new StringBuilder();
        string newsStyleSheet;
        string newsJavaScript;
        string newsHeader;
        string newsFooter;
        string chatCSSFilePath;
        private int altItemCounter;
        #endregion

        #region Constructor + Init
        /// <summary>
        /// Constructor
        /// </summary>
        public NewsUserControl() {
            InitializeComponent();

            newsStyleSheet = "<link href=\"" + CSSFilePath + "\\GUI\\SharpWiredStyleSheet.css\" rel=\"stylesheet\" type=\"text/css\" />";
            newsJavaScript = "<script>function pageDown () { if (window.scrollBy) window.scrollBy(0, window.innerHeight ? window.innerHeight : document.body.clientHeight); }</script>";

            newsHeader = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\" lang=\"en\">" +
                "<head><title>SharpWired</title>" +
                    newsJavaScript +
                    newsStyleSheet +
                "</head><body onload=\"pageDown(); return false;\">";

            newsFooter = "</body></html>";
            newsWebBrowser.DocumentText = newsHeader + newsFooter;
        }
        /// <summary>
        /// Inits the news class - Call when logged in correctly
        /// </summary>
        /// <param name="logicManager"></param>
        public void Init(LogicManager logicManager) {
            this.logicManager = logicManager;
            logicManager.LoggedIn += OnLoggedIn;
            logicManager.LoggedOut += OnLoggedOut;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the file path for the CSS-file
        /// </summary>
        public string CSSFilePath {
            get {
                if (chatCSSFilePath == null)
                    return System.Environment.CurrentDirectory;
                return chatCSSFilePath;
            }
            set { chatCSSFilePath = value; }
        }

        /// <summary>
        /// Get the HTML-code to output for the standard/alt-items
        /// </summary>
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
        #endregion

        # region Methods: Sending messages
        private void postNewsButton_Click(object sender, EventArgs e) {
            string text = this.postNewsTextBox.Text.Trim();
            if (text.Length > 0)
                logicManager.ConnectionManager.Commands.Post(this.postNewsTextBox.Text);
            postNewsTextBox.Clear();
        }
        #endregion

        #region Methods: Receiving messages
        /// <summary>
        /// Writes the HTML-formated string to the GUI
        /// </summary>
        /// <param name="guiMessage"></param>
        private void WriteHTMLToNews(GuiMessageItem guiMessage) {
            if (this.InvokeRequired) {
                WriteToNewsCallback writeToNewsCallback =
                    new WriteToNewsCallback(WriteHTMLToNews);
                this.Invoke(writeToNewsCallback, new object[] { guiMessage });
            } else {
                newsBodyContent.Append(this.AltItemBeginningHtml);
                newsBodyContent.Append(guiMessage.GeneratedHTML);
                newsBodyContent.Append("</div>");
                newsWebBrowser.DocumentText = newsHeader +
                    newsBodyContent + newsFooter;
            }
        }

        void OnNewsListingDone(List<NewsPost> newsList) {
            foreach (NewsPost n in newsList) {
                GuiMessageItem m = new GuiMessageItem(n);
                WriteHTMLToNews(m);
            }
        }

        void OnNewsPostReceived(NewsPost newPost) {
            GuiMessageItem m = new GuiMessageItem(newPost);
            WriteHTMLToNews(m);
        }
        #endregion

        #region Events & Listeners
        delegate void WriteToNewsCallback(GuiMessageItem guiMessage);

        void OnLoggedIn() {
            logicManager.Server.News.NewsPostedEvent += OnNewsPostReceived;
            logicManager.Server.News.NewsListingDoneEvent += OnNewsListingDone;

        }
        void OnLoggedOut() {
            logicManager.Server.News.NewsPostedEvent -= OnNewsPostReceived;
            logicManager.Server.News.NewsListingDoneEvent += OnNewsListingDone;
        }
        #endregion
    }
}
