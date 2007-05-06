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


		private void manageBookmarksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (BookmarkManagerDialog diag = new BookmarkManagerDialog())
			{
				if (diag.ShowDialog(this) == DialogResult.Yes)
				{
					Bookmark bookmark = diag.BookmarkToConnect;
					logicManager.Connect(bookmark);
				}
			}
		}

		private void bookmarksToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			AddBookmarks();
		}

		private void AddBookmarks()
		{
			this.UseWaitCursor = true;

			if (bookmarkItems.Count > 0)
			{
				foreach (ToolStripMenuItem item in bookmarkItems)
				{
					this.bookmarksToolStripMenuItem.DropDownItems.Remove(item);
				}
			}
			bookmarkItems.Clear();

			List<Bookmark> bookmarks = BookmarkManager.GetBookmarks();
			
			foreach (Bookmark bookmark in bookmarks)
			{
				ToolStripMenuItem item = new ToolStripMenuItem(bookmark.ToShortString());
				item.Tag = bookmark;
				item.Click += new EventHandler(item_Click);
				bookmarkItems.Add(item);
				this.bookmarksToolStripMenuItem.DropDownItems.Add(item);
			}

			this.UseWaitCursor = false;
		}

		void item_Click(object sender, EventArgs e)
		{
			if ((sender as ToolStripMenuItem).Tag is Bookmark)
				logicManager.Connect((sender as ToolStripMenuItem).Tag as Bookmark);
		}

		private List<ToolStripMenuItem> bookmarkItems = new List<ToolStripMenuItem>();
    }
}