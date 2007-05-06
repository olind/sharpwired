#region Information and licence agreements
/**
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
	public partial class BookmarkManagerDialog : Form
	{
		public BookmarkManagerDialog()
		{
			InitializeComponent();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.No;
			this.Close();
		}

		private void connectButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Yes;
			this.BookmarkToConnect = this.bookmarkManagerGUI1.CurrentBookmark;
			this.Close();
		}

		private Bookmark bookmarkToConnect;

		public Bookmark BookmarkToConnect
		{
			get { return bookmarkToConnect; }
			set { bookmarkToConnect = value; }
		}

		private void bookmarkManagerGUI1_CurrentBookmarkChangedEvent(object sender, Bookmark currentBookmark)
		{
			if (currentBookmark == null)
			{
				this.connectButton.Enabled = false;
			}
			else
			{
				this.connectButton.Enabled = true;
			}
		}
	}
}