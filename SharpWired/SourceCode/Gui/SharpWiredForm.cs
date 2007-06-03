#region Information and licence agreements
/**
 * SharpWiredForm.cs 
 * Created by Ola Lindberg, 2006-07-23
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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;
using SharpWired.MessageEvents;

using SharpWired;
using SharpWired.Connection;
using SharpWired.Model;
using SharpWired.Gui.Chat;
using SharpWired.Connection.Bookmarks;
using SharpWired.Gui.Bookmarks;
using WiredControls.ToolStripItems;

namespace SharpWired.Gui
{
    public partial class SharpWiredForm : Form
    {
        private LogicManager logicManager;

        public SharpWiredForm()
        {
            logicManager = new LogicManager();
            
            InitializeComponent();
            chatUserControl1.Init(logicManager);
            newsUserControl1.Init(logicManager);
            filesUserControl1.Init(logicManager);
        }

        private void ExitToolStripButton_Click(object sender, EventArgs e)
        {
            logicManager.ConnectionManager.Commands.Leave(1);
            Application.Exit();
		}

		#region Bookmark in the menu.
		/// <summary>
		/// User wants to manage bookmarks. Open the bookmarmanager gui.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void manageBookmarksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (BookmarkManagerDialog diag = new BookmarkManagerDialog())
			{
				// NOTE: Bookmark mangar could be shown as a modless dialog?
				if (diag.ShowDialog(this) == DialogResult.Yes)
				{
					Bookmark bookmark = diag.BookmarkToConnect;
					logicManager.Connect(bookmark);
				}
			}
		}

		/// <summary>
		/// When opening the Bookmark menu item, we start a background worker that
		/// reads the bookmark file (which takes some time > 0.1s) and adds the items
		/// as they are created.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bookmarksToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			// Removing should be quick, even if its n^2 or so.
			if (bookmarkItems.Count > 0)
				foreach (ToolStripMenuItem item in bookmarkItems)
					bookmarksToolStripMenuItem.DropDownItems.Remove(item);

			if (mLoadingToolStripMenuItem != null
				|| bookmarksToolStripMenuItem.DropDownItems.Contains(mLoadingToolStripMenuItem))
			{
				bookmarksToolStripMenuItem.DropDownItems.Remove(mLoadingToolStripMenuItem);
			}
			// Add the haxxor (Loading...) item again.
			mLoadingToolStripMenuItem = new AnimatedLoaderItem("(Loading...)");
			(mLoadingToolStripMenuItem as AnimatedLoaderItem).Start();
			bookmarksToolStripMenuItem.DropDownItems.Add(mLoadingToolStripMenuItem);

			// Create a loader that can read the bookmark file in the background and then
			// report to us the items to add.
			if(mBookmarkBackgroundLoader == null)
				mBookmarkBackgroundLoader = new BookmarkBackgroundLoader();

			mBookmarkBackgroundLoader.ProgressChanged += new ProgressChangedEventHandler(mBookmarkBackgroundLoader_ProgressChanged);
			mBookmarkBackgroundLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mBookmarkBackgroundLoader_RunWorkerCompleted);

			// If the loader is working, try cancel and the invoke again.
			if (mBookmarkBackgroundLoader.IsBusy)
				mBookmarkBackgroundLoader.CancelAsync();
			if (!mBookmarkBackgroundLoader.IsBusy)
			{
				mBookmarkLoadingTimer.Start();
				mBookmarkBackgroundLoader.LoadBookmarks(bookmarkItems, bookmarksToolStripMenuItem, BookmarkItemClick);
			}
		}

		/// <summary>
		/// The background loader for bookmarks.
		/// </summary>
		BookmarkBackgroundLoader mBookmarkBackgroundLoader = null;

		/// <summary>
		/// The worker is done. Remove the (Loading...) menu item and remove event listeners.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void mBookmarkBackgroundLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.bookmarksToolStripMenuItem.DropDownItems.Remove(mLoadingToolStripMenuItem);
			mBookmarkBackgroundLoader.ProgressChanged -= new ProgressChangedEventHandler(mBookmarkBackgroundLoader_ProgressChanged);
			mBookmarkBackgroundLoader.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(mBookmarkBackgroundLoader_RunWorkerCompleted);
			mBookmarkLoadingTimer.Stop();
			(mLoadingToolStripMenuItem as AnimatedLoaderItem).Stop();
		}

		/// <summary>
		/// The loader have loaded something. Add it to the menu.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void mBookmarkBackgroundLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.UserState is ToolStripMenuItem)
				bookmarksToolStripMenuItem.DropDownItems.Add(e.UserState as ToolStripMenuItem);
		}
		
		/// <summary>
		/// The method that is invoked when a bookmark item is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BookmarkItemClick(object sender, EventArgs e)
		{
			if ((sender as ToolStripMenuItem).Tag is Bookmark)
				logicManager.Connect((sender as ToolStripMenuItem).Tag as Bookmark);
		}

		/// <summary>
		/// A list of the ToolStripMenuItems that represents bookmarks.
		/// </summary>
		private List<ToolStripMenuItem> bookmarkItems = new List<ToolStripMenuItem>();
		#endregion

		private void mBookmarkLoadingTimer_Tick(object sender, EventArgs e)
		{
			string text = mLoadingToolStripMenuItem.Text;
			// cut out loading.
			string t = text.Substring(1, text.Length - 2);
			// move one char from beginning to end, or vice versa.
			string nt = t.Substring(1, t.Length - 1) + t[0].ToString();
			mLoadingToolStripMenuItem.Text = "(" + nt + ")";
		}
	}
}