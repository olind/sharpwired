#region Information and licence agreements
/*
 * News.cs
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
using SharpWired.Controller;

namespace SharpWired.Gui.News {
    /// <summary>
    /// The News view
    /// </summary>
    public partial class NewsContainer : WebBrowserGuiBase {

        delegate void WriteToNewsCallback(GuiMessageItem guiMessage);

        public NewsContainer() {
            InitializeComponent();
        }

        public override void Init(SharpWiredModel model, SharpWiredController controller) {
            base.Init(model, controller);
        }

        protected override void OnOnline() {
            model.Server.News.NewsPostedEvent += OnNewsPostReceived;
            model.Server.News.NewsListingDoneEvent += OnNewsListingDone;

            ToggleWindowsFormControl(postNewsButton);
            ToggleWindowsFormControl(postNewsTextBox);
            ResetWebBrowser(newsWebBrowser);
        }
        protected override void OnOffline() {
            model.Server.News.NewsPostedEvent -= OnNewsPostReceived;
            model.Server.News.NewsListingDoneEvent += OnNewsListingDone;

            ToggleWindowsFormControl(postNewsButton);
            ToggleWindowsFormControl(postNewsTextBox);
        }

        #region Receiving from model
        void OnNewsListingDone(List<NewsPost> newsList) {
            foreach (NewsPost n in newsList) {
                OnNewsPostReceived(n);
            }
        }

        void OnNewsPostReceived(NewsPost newPost) {
            GuiMessageItem m = new GuiMessageItem(newPost);
            AppendHTMLToWebBrowser(newsWebBrowser, m);
        }
        #endregion

        #region Send to controller
        private void postNewsButton_Click(object sender, EventArgs e) {
            //TODO: Privileges: Check if we are allowed to post news
            string text = this.postNewsTextBox.Text.Trim();
            if (text.Length > 0)
                model.ConnectionManager.Commands.Post(this.postNewsTextBox.Text);

            postNewsTextBox.Clear();
        }
        #endregion
    }
}
