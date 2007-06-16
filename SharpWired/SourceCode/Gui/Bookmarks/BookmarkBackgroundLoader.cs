using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using SharpWired.Connection.Bookmarks;
using SharpWired.Gui.Resources.Icons;

namespace SharpWired.Gui.Bookmarks
{
	/// <summary>
	/// This class takes a MenuItem and adds the bookmarks to it, after they have been read.
	/// Argument must be a BookmarkLoaderArgument!
	/// </summary>
	internal class BookmarkBackgroundLoader: BackgroundWorker
	{
		/// <summary>
		/// Set some properties we want.
		/// </summary>
		public BookmarkBackgroundLoader()
		{
			this.WorkerReportsProgress = true;
			this.WorkerSupportsCancellation = false;
		}

		/// <summary>
		/// This runs the loader for you!
		/// </summary>
		/// <param name="pBookmarkItems">The list of items that represents bookmarks.</param>
		/// <param name="pMenuItem">The menu item for bookmarks.</param>
		/// <param name="pItemClickMethod">The method to invoke upon item.Click.</param>
		internal void LoadBookmarks(List<ToolStripMenuItem> pBookmarkItems, ToolStripMenuItem pMenuItem, EventHandler pItemClickMethod)
		{
			BookmarkLoaderArgument arg = new BookmarkLoaderArgument(pBookmarkItems, pMenuItem, pItemClickMethod);
			RunWorkerAsync(arg);
		}

		/// <summary>
		/// The e.Argument must be BookmarkLoaderArgument.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDoWork(DoWorkEventArgs e)
		{
			base.OnDoWork(e);

			if (e.Argument is BookmarkLoaderArgument)
				AddBookmarks(e.Argument as BookmarkLoaderArgument);
			else
				throw new ArgumentException("The argument must be of type " + typeof(BookmarkLoaderArgument).ToString() + "!");
		}

		/// <summary>
		/// Read the bookmarks and report them.
		/// </summary>
		/// <param name="item"></param>
		private void AddBookmarks(BookmarkLoaderArgument arg)
		{
			// This is what takes time.
			List<Bookmark> bookmarks = BookmarkManager.GetBookmarks();

			int done = 0;
			foreach (Bookmark bookmark in bookmarks)
			{
				ToolStripMenuItem item = new ToolStripMenuItem(bookmark.ToShortString());
				item.Tag = bookmark;
				item.Click += new EventHandler(arg.ItemClickMethod);
				item.Image = IconHandler.Instane[IconList.File];
				arg.BookmarkItems.Add(item);
				//arg.MenuItem.DropDownItems.Add(item);

				// This is just for show ;-)
				done++;
				ReportProgress(done / bookmarks.Count, item);
			}
		}

		#region Argument Class.
		/// <summary>
		/// Holds the arguments that is to be passed to the background loader.
		/// </summary>
		internal class BookmarkLoaderArgument
		{
			private EventHandler mItemClickMethod;

			public EventHandler ItemClickMethod
			{
				get { return mItemClickMethod; }
				set { mItemClickMethod = value; }
			}


			private ToolStripMenuItem mItem;

			public ToolStripMenuItem MenuItem
			{
				get { return mItem; }
				set { mItem = value; }
			}

			private List<ToolStripMenuItem> mBookmarkItems;

			public List<ToolStripMenuItem> BookmarkItems
			{
				get { return mBookmarkItems; }
				set { mBookmarkItems = value; }
			}

			internal BookmarkLoaderArgument(List<ToolStripMenuItem> pBookmarkItems, ToolStripMenuItem pItem, EventHandler pClickMethod)
			{
				mItem = pItem;
				mBookmarkItems = pBookmarkItems;
				mItemClickMethod = pClickMethod;
			}
		}
		#endregion
	}
}