#region Information and licence agreements
/*
 * FileSystemEntry.cs 
 * Created by Ola Lindberg, 2007-05-01
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
using SharpWired.MessageEvents;
using System.Collections;

namespace SharpWired.Model.Files
{
    /// <summary>
    /// The base class for files and folders.
    /// </summary>
    public abstract class FileSystemEntry : IComparable
    {
        #region Variables
        private MessageEventArgs_410420 messageEventArgs;
        private DateTime created;
        private DateTime modified;
        private string path;
        private string parentPath;
        private string name;
        private string[] pathArray;
        #endregion

        #region Properties
		private FileSystemEntry mParent;
		/// <summary>
		/// Get/Set the Parent FileSystemEntry to this.
		/// </summary>
		public FileSystemEntry Parent
		{
			get { return mParent; }
			set { mParent = value; }
		}
		
        /// <summary>
        /// Get the time when this file or folder was created.
        /// </summary>
        public DateTime Created
        {
            get { return created; }
        }

        /// <summary>
        /// Get the time when this file or folder was modyfied.
        /// </summary>
        public DateTime Modified
        {
            get { return modified; }
        }

        /// <summary>
        /// Get the complete file path for this file or folder.
        /// For example: "/folder1/folder2" or "/folder1/folder2/file1" 
        /// </summary>
        public string Path
        {
            get { return path; }
        }

        /// <summary>
        /// Get the path to the folder where this file or folder is located.
        /// For example: If file1 is located in the folder "/folder1/folder2" this property will return "/folder1/folder2".
        /// If folder 2 is located in the folder "/folder1" this property will return "/folder1".
        /// </summary>
        public string ParentPath
        {
            get { return parentPath; }
        }

        /// <summary>
        /// Get or set the name of this file or folder
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Returns the path of this file or folder in an array where each entry represent one folder.
        /// The first entry [0] is the the root node (/)
        /// </summary>
        public string[] PathArray
        {
            get { return pathArray; }
        }

        /// <summary>
        /// Gets the FolderNodes that are childrens of this node.
        /// </summary>
        public abstract IEnumerable<FolderNode> FolderNodes
        { 
            get; 
        }

        /// <summary>
        /// Finds out if this node has any childrens or not.
        /// </summary>
        /// <returns>True if this node has 1 or more children nodes.</returns>
        public abstract bool HasChildren();
        #endregion

        #region Initialization
        /// <summary>
        /// Constructor
        /// </summary>
        public FileSystemEntry(MessageEventArgs_410420 messageEventArgs)
        {
            this.messageEventArgs = messageEventArgs;
            this.created = messageEventArgs.Created;
            this.modified = messageEventArgs.Modified;
            this.path = messageEventArgs.Path;

			this.pathArray = SplitPath(messageEventArgs.Path);
			if (pathArray != null && pathArray.Length > 0)
				this.name = pathArray[pathArray.Length - 1];
			else
				this.name = ""; // NOTE: Throw Exception?

			this.parentPath = BuildParentPath(pathArray);
        }

        /// <summary>
        /// Constructor - Sets up this node with default values
        /// </summary>
        public FileSystemEntry() {
            this.created = DateTime.Now;
            this.modified = this.created;
            this.path = "/";
            this.pathArray = new string[1];
            this.pathArray[0] = "";
        }
        #endregion

        #region Methods
        /// <summary>
		/// Splits the path to an array.
		/// </summary>
		/// <param name="path">The path like "folder/hejsan/file.fil".</param>
		/// <returns>Something like { "folder", "hejsan", "file.fil" }.</returns>
		public static string[] SplitPath(string path)
		{
			string[] pathArray = path.Split(SharpWired.Utility.PATH_SEPARATOR[0]);
			return pathArray;
		}

		/// <summary>
		/// Builds the path to the parent node.
		/// </summary>
		/// <param name="pathArray">The parts of this path.</param>
		/// <returns>The path to parnet node.</returns>
		private string BuildParentPath(string[] pathArray)
		{
			StringBuilder buildPath = new StringBuilder(20);
			// Build the parent path string
			for (int i = 0; i < pathArray.Length - 1; i++)
			{
				if (i == 0)
				{
                    buildPath.Append(SharpWired.Utility.PATH_SEPARATOR); //Avoids the first element to have 2 slashes "//"
				}
				else if (i == pathArray.Length - 2)
				{
					buildPath.Append(pathArray[i]); //The last element should not have any trailing /
				}
				else
				{
                    buildPath.Append(pathArray[i] + SharpWired.Utility.PATH_SEPARATOR); //All non root or non last elements
				}
			}
			return buildPath.ToString();
        }
        #endregion

        #region Overrides
        /// <summary>
		/// Path.
		/// </summary>
		/// <returns>The Path.</returns>
		public override string ToString()
		{
			return path;	
		}
		#endregion

        #region IComparable Members

        public int CompareTo(object obj) {
            if (obj is FileSystemEntry) {
                return this.Path.CompareTo(((FileSystemEntry)obj).Path);
            } else {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
