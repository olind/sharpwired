#region Information and licence agreements
/**
 * BookmarkManager.cs
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
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Security;

namespace SharpWired.Connection.Bookmarks
{
	/// <summary>
	/// This class loads and stores the Bookmarks.
	/// The access is static. There exists only one bookmark file per user.
	/// To maintain some sort of stability, only one acces 
	/// </summary>
	class BookmarkManager
	{
		private static string BookmarkFileName = "Bookmarks.wwb";
		private static string BookmarkFolder = Application.UserAppDataPath;
		private static string BookmarkFileFullName;

		// private DateTime lastWriteTime;

		/// <summary>
		/// Private hidden constructor.
		/// </summary>
		private BookmarkManager()
		{
		}

		/// <summary>
		/// The static constructor.
		/// </summary>
		static BookmarkManager()
		{
			DirectoryInfo dir = new DirectoryInfo(BookmarkFolder);
			if (dir.Parent != null)
				BookmarkFolder = dir.Parent.FullName;
			BookmarkFileFullName = Path.Combine(BookmarkFolder, BookmarkFileName);
		}

		/// <summary>
		/// Gets a List of Bookmarks. Only one caller at a time!
		/// </summary>
		/// <returns></returns>
		public static List<Bookmark> GetBookmarks()
		{
			lock (typeof(BookmarkManager))
			{

				FileInfo file = new FileInfo(BookmarkFileFullName);
				if (!file.Exists)
				{
					file = CreateBookmarkFile();
				}
				if (file != null && file.Exists)
				{
					Bookmark[] bms = LoadBookMarks(file);
					List<Bookmark> list = new List<Bookmark>(bms);
					return list;
				}
				else
				{
					throw new BookmarkException("Could not load the bookmarks from file: "
							+ BookmarkFileFullName
							+ ", becouse the file didn't exist, not was it created!");
				}
			}
		}

		#region Add Bookmark(s)
		/// <summary>
		/// Adds a bookmark to the bookmark file.
		/// </summary>
		/// <returns></returns>
		public static bool AddBookmark(Bookmark bookmark, bool allowDuplicate)
		{
			return BookmarkManager.AddBookmarks(new Bookmark[] { bookmark }, allowDuplicate);
		}

		/// <summary>
		/// Adds several bookmarks.
		/// </summary>
		/// <param name="bookmarks"></param>
		/// <param name="allowDuplicate">If false, no bookmarks are saves if theres a duplicate.</param>
		/// <returns>True if succeded. False if not saved becouse of adding duplicates.</returns>
		/// <remarks>Reads the bookmark file and add the bookmark to the list, then saves the file.</remarks>
		public static bool AddBookmarks(Bookmark[] bookmarks, bool allowDuplicate)
		{
			lock (typeof(BookmarkManager))
			{
				try
				{
					bool save = true;
					List<Bookmark> bms = GetBookmarks();
					foreach (Bookmark bm in bookmarks)
					{
						if (!allowDuplicate && bms.Contains(bm))
						{
							save = false;
							break;
						}
						bms.Add(bm);
					}
					if (save)
					{
						SaveBookmarks(bms, new FileInfo(BookmarkFileFullName));
						return true;
					}
					return false;
				}
				catch (Exception e)
				{
					throw new BookmarkException("Error adding bookmark to file " + BookmarkFileFullName + ".", e);
				}
			}
		}
		#endregion

		#region Remove Bookmark
		/// <summary>
		/// Removes a bookmark.
		/// </summary>
		/// <param name="bookmark"></param>
		/// <returns>Null if the bookmark wasn't in the list, otherwise the bookmark that is removed.</returns>
		/// <remarks>Opens the file, loads the list, removes the bookmark, saves the file.</remarks>
		public static Bookmark RemoveBookmark(Bookmark bookmark)
		{
			lock (typeof(BookmarkManager))
			{
				try
				{
					List<Bookmark> bms = GetBookmarks();
					if (bms.Contains(bookmark))
					{
						bms.Remove(bookmark);
						SaveBookmarks(bms, new FileInfo(BookmarkFileFullName));
						return bookmark;
					}
					return null;
				}
				catch (Exception e)
				{
					throw new BookmarkException("Error removing Bookmark from bookmark file " + BookmarkFileFullName + ".", e);
				}
			}
		}
		#endregion

		#region Load
		/// <summary>
		/// Deserializes bookmarks from the given FileInfo.
		/// </summary>
		/// <param name="file"></param>
		/// <returns>A List with the bookmarks</Bookmark></returns>
		private static Bookmark[] LoadBookMarks(FileInfo file)
		{
			lock (typeof(BookmarkManager))
			{
				try
				{
					file.Decrypt();

					// If the file is empty, just return an empty list.
					if (file.Length == 0)
					{
						return new Bookmark[] { };
					}

					XmlSerializer ser = new XmlSerializer(typeof(Bookmark[]));

					using (Stream s = System.IO.File.Open(
														file.ToString(),
														FileMode.Open,
														FileAccess.Read,
														FileShare.Read))
					{
						Bookmark[] bookmarks = (Bookmark[])ser.Deserialize(s);

						return bookmarks;
					}
				}
				#region Catch And Throw
				catch (System.InvalidOperationException invE)
				{
					throw new BookmarkException("Error loading bookmarks (" + BookmarkFileFullName + ").", invE);
				}
				catch (ArgumentNullException ane)
				{
					throw new BookmarkException("Error loading bookmarks (" + BookmarkFileFullName + "); The serializationStream is a null reference", ane);
				}
				catch (SerializationException se)
				{
					throw new BookmarkException("Error loading bookmarks (" + BookmarkFileFullName + ").", se);
				}
				catch (SecurityException sec)
				{
					throw new BookmarkException("Error loading bookmarks (" + BookmarkFileFullName + "); The caller does not have the required permission.", sec);
				}
				catch (Exception e)
				{
					throw new BookmarkException("Error loading bookmarks (" + BookmarkFileFullName + ").", e);
				}
				#endregion
				finally
				{
					file.Encrypt();
				}
			}
		}
		#endregion

		#region Save

		/// <summary>
		/// Saves the bookmarks to the given file.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		private static bool SaveBookmarks(List<Bookmark> bookmarks, FileInfo file)
		{
			// Lock object so we can't edit it while saving.
			lock (typeof(BookmarkManager))
			{
				try
				{
					// Writing data to memory first in case of error.
					// If everything went ok, save data to file.
					MemoryStream s = new MemoryStream();
					XmlSerializer ser = new XmlSerializer(typeof(Bookmark[]));
					ser.Serialize(s, bookmarks.ToArray());

					using (Stream stream = System.IO.File.Open(file.ToString(), FileMode.Create, FileAccess.Write))
					{
						// Up to ~2GB.
						if (s.Length < int.MaxValue)
							stream.Write(s.GetBuffer(), 0, (int)s.Length);
						else
						{
							//TODO: :-))
						}
					}
					file.Encrypt();
					return true;
				}
				#region Catch and throw
				catch (ArgumentNullException ane)
				{
					throw new BookmarkException("Error saving the bookmark file (" + BookmarkFileFullName + "); The serializationStream is a null reference", ane);
				}
				catch (SerializationException se)
				{
					throw new BookmarkException("Error saving the bookmark file (" + BookmarkFileFullName + "); The serializationStream supports seeking, but its length is 0", se);
				}
				catch (SecurityException sec)
				{
					throw new BookmarkException("Error saving the bookmark file (" + BookmarkFileFullName + "); The caller does not have the required permission.", sec);
				}
				catch (Exception e)
				{
					throw new BookmarkException("Error saving the bookmark file (" + BookmarkFileFullName + "); Unknown reason.", e);
				}
				#endregion
			}
		}
		#endregion

		#region Create Bookmark file
		/// <summary>
		/// Creates the Bookmark file.
		/// </summary>
		private static FileInfo CreateBookmarkFile()
		{
			FileInfo file = new FileInfo(BookmarkFileFullName);
			try
			{
				file.Create();
				// Add some stuff to it, so that it looks like an XML file.
				SaveBookmarks(new List<Bookmark>(), file);
				return file;
			}
			catch (Exception e)
			{
				throw new BookmarkException("Error trying to create file: " + BookmarkFileFullName, e);
			}
		}
		#endregion

	}

	#region Bookmark Exception
	public class BookmarkException : Exception
	{
		public BookmarkException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public BookmarkException(string message)
			: base(message)
		{
		}
	}
	#endregion
}
