#region Information and licence agreements
/*
 * BookmarkManagerDialog.cs
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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SharpWired.Connection.Bookmarks;

namespace SharpWired.Gui.Bookmarks
{
    /// <summary>
    /// The Bookmark manager dialog GUI
    /// </summary>
	public partial class BookmarkManagerDialog : Form {
        #region Properties
        private Bookmark selectedBookmark;

        /// <summary>
        /// The bookmark currently selected in the bookmarks dialog.
        /// </summary>
        public Bookmark SelectedBookmark {
            get { return selectedBookmark; }
            set { selectedBookmark = value; }
        }

        private Bookmark bookmarkToConnect = null;

        /// <summary>
        /// The bookmark selected for connection.
        /// </summary>
        public Bookmark BookmarkToConnect {
            get { return bookmarkToConnect; }
            set { bookmarkToConnect = value; }
        }
        #endregion

        #region Init
        /// <summary>
        /// Constructor
        /// </summary>
		public BookmarkManagerDialog()
		{
			InitializeComponent();
            PopulateList();
            this.bookmarkEntryControl.Enabled = false;
        }
        #endregion

        #region EventHandlers
        private void closeButton_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void connectButton_Click(object sender, EventArgs e) {
            SaveAndSelectBookmark(SelectedBookmark);
            this.BookmarkToConnect = this.SelectedBookmark;
			this.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            if (bookmarkList.SelectedItems.Count == 1) {
                BookmarkManager.RemoveBookmark(bookmarkList.SelectedItems[0].Tag as Bookmark);
                this.bookmarkEntryControl.SetBookmark(null);
                PopulateList();
            }
        }

        private void addButton_Click(object sender, EventArgs e) {
            Bookmark bookmark = new Bookmark();
            //TODO: When we add bookmarks we should make the name New Bookmark
            //      for the first bookmark. New Bookmark (2) for the second
            //      New Bookmark (3) for the third...
            bookmark.Name = "New Bookmark";
            BookmarkManager.AddBookmark(bookmark, false);
            selectedBookmark = bookmark;
            PopulateList();
            this.bookmarkEntryControl.ShowPasswordBox(true);
            this.bookmarkEntryControl.Focus();
        }

        private void saveButton_Click(object sender, EventArgs e) {
            SaveAndSelectBookmark(this.bookmarkEntryControl.GetBookmark());
            PopulateList();
        }

        private void bookmarkList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            ChangeButtonStatus();
        }
        #endregion

        private void SaveAndSelectBookmark(Bookmark bookmark) {
            BookmarkManager.RemoveBookmark(SelectedBookmark);
            BookmarkManager.AddBookmark(bookmark, false);
            SelectedBookmark = bookmark;
        }

        private void ChangeButtonStatus() {
            bool buttonsEnabled = true;
            if (bookmarkList.SelectedItems.Count > 0) {
                Bookmark bookmark = this.bookmarkList.SelectedItems[0].Tag as Bookmark;
                this.selectedBookmark = bookmark;
                this.bookmarkEntryControl.SetBookmark(bookmark);
            } else {
                this.selectedBookmark = null;
                this.bookmarkEntryControl.Clear();
                buttonsEnabled = false;
            }

            this.connectButton.Enabled = buttonsEnabled;
            this.saveBookmarkButton.Enabled = buttonsEnabled;
            this.deleteButton.Enabled = buttonsEnabled;
            this.bookmarkEntryControl.Enabled = buttonsEnabled;
        }

        private void PopulateList() {
            try {
                bookmarkList.Clear();
                if (BookmarkManager.Bookmarks == null)
                    BookmarkManager.GetBookmarks();
            
                foreach (Bookmark bookmark in BookmarkManager.Bookmarks) {
                    if (bookmark != null) {
                        // TODO: Use bookmark name instead!
                        ListViewItem item = new ListViewItem(bookmark.ToShortString());
                        item.Tag = bookmark;
                        this.bookmarkList.Items.Add(item);
                    }
                }
                
                int i = 0;
                foreach (ListViewItem item in bookmarkList.Items) {
                    bookmarkList.Items[i].Selected = (item.Tag == selectedBookmark);
                    i++;
                }

                ChangeButtonStatus();
            } catch (BookmarkException e) {
                MessageBox.Show(e.ToString(), "Bookmark Error");
            }
        }
	}
}