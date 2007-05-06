#region Information and licence agreements
/**
 * BookmarkManagerGUI.cs
 * Created by Peter Holmdal, 2006-12-03
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
using SharpWired.Connection.Bookmarks;

namespace SharpWired.Gui.Bookmarks
{
	public partial class BookmarkManagerGUI : UserControl
	{
		public BookmarkManagerGUI()
		{
			InitializeComponent();
		}

		private void PopulateList()
		{
			try
			{
				bookmarkList.Clear();
				List<Bookmark> bookmarks = BookmarkManager.GetBookmarks();
				
				foreach (Bookmark bookmark in bookmarks)
				{
					if (bookmark != null)
					{
						ListViewItem item = new ListViewItem(
							bookmark.UserInformation.UserName
							+ " @ " + bookmark.Server.ServerName
							+ ":" + bookmark.Server.ServerPort.ToString());
						item.Tag = bookmark;
						this.bookmarkList.Items.Add(item);
					}
				}
			}
			catch (BookmarkException e)
			{
				// TODO: do some smart stuff here.
				MessageBox.Show(e.ToString(), "Shitpommesfrittes!");
			}
						
		}

		private void BookmarkManagerGUI_Load(object sender, EventArgs e)
		{
			PopulateList();
		}

		private void bookmarkList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (bookmarkList.SelectedItems.Count > 0)
			{
				Select(bookmarkList.SelectedItems[0].Tag as Bookmark);
			}
			else
			{
				Select(null);
				this.deleteButton.Enabled = false;
			}
		}

		private void Select(Bookmark bookmark)
		{
			if (bookmark != null)
			{
				this.currentBookmark = bookmark;
				this.bookmarkEntryControl1.SetBookmark(bookmark);
				this.deleteButton.Enabled = true;
				this.addButton.Enabled = false;
				this.applyButton.Enabled = false;
			}
			else
			{
				this.currentBookmark = null;
			}
		}

		private void bookmarkEntryControl1_ValueChanged(object sender)
		{
			this.deleteButton.Enabled = false;
			this.addButton.Enabled = true;
			this.applyButton.Enabled = true;
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			if (bookmarkList.SelectedItems.Count == 1)
			{
				BookmarkManager.RemoveBookmark(bookmarkList.SelectedItems[0].Tag as Bookmark);
				this.bookmarkEntryControl1.SetBookmark(null);
				PopulateList();
			}
		}

		private Bookmark currentBookmark;

		public Bookmark CurrentBookmark
		{
			get { return currentBookmark; }
			set { currentBookmark = value; }
		}

		public delegate void CurrentBookmarkChangedDelegate(object sender, Bookmark currentBookmark);
		public event CurrentBookmarkChangedDelegate CurrentBookmarkChangedEvent;

		protected virtual void OnCurrentBookMarkChanged()
		{
			if (this.CurrentBookmarkChangedEvent != null)
			{
				CurrentBookmarkChangedEvent(this, currentBookmark);
			}
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			Bookmark bookmark = this.bookmarkEntryControl1.GetBookmark();
			if (bookmark != null)
			{
				BookmarkManager.AddBookmark(bookmark, false);
				PopulateList();
			}
		}

		private void applyButton_Click(object sender, EventArgs e)
		{
			Bookmark bookmark = this.bookmarkEntryControl1.GetBookmark();
			if (bookmark != null)
			{
				Bookmark remove = CurrentBookmark;
				BookmarkManager.RemoveBookmark(remove);
				BookmarkManager.AddBookmark(bookmark, false);
				PopulateList();
			}
		}

	}
}
