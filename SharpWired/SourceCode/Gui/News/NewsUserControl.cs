#region Information and licence agreements
/**
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

namespace SharpWired.Gui.News
{
    public partial class NewsUserControl : UserControl
    {
        #region Variables

        LogicManager logicManager;

        string newsBodyContent;

        string newsStyleSheet;
        string newsJavaScript;
        string newsHeader;
        string newsFooter;
        string chatCSSFilePath;
        private int altItemCounter;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the file path for the CSS-file
        /// TODO: Move this to some central part instead since both chat and news access it.
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

        #region Listerers : From connection layer

        void NewsModel_NewsListReplacedEvent(List<SharpWired.Model.News.NewsObject> newsList)
        {
            List<SharpWired.Model.News.NewsObject> reversedNewsList = newsList; //FIXME: Implement a better solution than copying the lists and reversing them
            reversedNewsList.Reverse();
            foreach (NewsObject n in reversedNewsList)
                WriteHTMLToNews(FormatNewsPostHTML(n));
        }

        void NewsModel_NewsPostedEvent(SharpWired.Model.News.NewsObject newPost)
        {
            WriteHTMLToNews(FormatNewsPostHTML(newPost));
        }

        #endregion

        # region Listerens: From gui

        private void postNewsButton_Click(object sender, EventArgs e)
        {
            PostNewsMessage();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Writes the HTML-formated string to the GUI
        /// </summary>
        /// <param name="formatedText"></param>
        private void WriteHTMLToNews(string formatedText)
        {
            if (this.InvokeRequired)
            {
                WriteToNewsCallback writeToNewsCallback = new WriteToNewsCallback(WriteHTMLToNews);
                this.Invoke(writeToNewsCallback, new object[] { formatedText });
            }
            else
            {
                newsBodyContent += formatedText;
                newsWebBrowser.DocumentText = newsHeader + newsBodyContent + newsFooter;
            }
        }
        delegate void WriteToNewsCallback(string formatedText);

        /// <summary>
        /// Format the post for HTML output
        /// </summary>
        /// <param name="newPost"></param>
        /// <returns></returns>
        private string FormatNewsPostHTML(NewsObject newPost)
        {
            /** 
             * Example of the html we want to produce here. 
             * class="standard" is class="alt" every other time
             * 
             *   <div class="standard">
             *       <div class="newsEntry">
             *           <div class="time">2006-12-01 00:00:00 22:28:50.2851648</div>
             *           <div class="userName">Ola (SharpWired)</div>
             *           <div class="message">The news post goes here.</div>
             *       </div>
             *   </div>
             */

            string postMessage = newPost.Post;
            //TODO: Regex newlines to get nice looking posts!
            postMessage = postMessage.Replace("\n","<br />");

            string formatedText = this.AltItemBeginningHtml +
                "<div class=\"newsEntry\">" +
                    "<div class=\"time\">" + newPost.PostTime + "</div>" +
                    "<div class=\"nickName\">" + newPost.Nick + "</div>" +
                    "<div class=\"message\">" + postMessage + "</div>" +
                "</div>" +
            "</div>";

            return formatedText;
        }

        /// <summary>
        /// Posts the new news post to the server
        /// </summary>
        private void PostNewsMessage()
        {
            logicManager.ConnectionManager.Commands.Post(this.postNewsRichTextBox.Text);
            postNewsRichTextBox.Clear();
        }

        #endregion

        #region Initialization

        public void Init(LogicManager logicManager)
        {
            this.logicManager = logicManager;

            logicManager.NewsHandler.NewsModel.NewsPostedEvent += new SharpWired.Model.News.NewsModel.NewsPostedDelegate(NewsModel_NewsPostedEvent);
            logicManager.NewsHandler.NewsModel.NewsListReplacedEvent += new SharpWired.Model.News.NewsModel.NewsListReplacedDelegate(NewsModel_NewsListReplacedEvent);
        }

        public NewsUserControl()
        {
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

        #endregion


    }
}
