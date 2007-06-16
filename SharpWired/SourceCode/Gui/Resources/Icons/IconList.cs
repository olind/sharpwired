using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Utils;

namespace SharpWired.Gui.Resources.Icons
{
	/// <summary>
	/// A list with the icons and their filenames.
	/// </summary>
	class IconList
	{
		private static Pair<string, string> sFile = 
			new Pair<string,string>("File", "file.png");
		/// <summary>
		/// File icon name an filename pair.
		/// </summary>
		public static Pair<string, string> File
		{
			get { return sFile; }
			//set { sFile = value; }
		}

		private static Pair<string, string>	sFolderClosed = 
			new Pair<string,string>("FolderClosed", "folderClosed.png");
		/// <summary>
		/// Folder Closed.
		/// </summary>
		public static Pair<string, string> FolderClosed
		{
			get { return sFolderClosed; }
			//set { sFolderClosed = value; }
		}

		private static SortedDictionary<string, Pair<string, string>> sIcons = new SortedDictionary<string,Pair<string,string>>();

		/// <summary>
		/// Gets the list of icon pairs.
		/// </summary>
		public static SortedDictionary<string, Pair<string, string>> Icons
		{
			get { return sIcons; }
		}

		/// <summary>
		/// Adds all the icon pairs to a list. Add your added properties to
		/// this list!
		/// </summary>
		static IconList()
		{
			sIcons.Add(sFile.Key, sFile);
			sIcons.Add(sFolderClosed.Key, sFolderClosed);
		}
	}
}