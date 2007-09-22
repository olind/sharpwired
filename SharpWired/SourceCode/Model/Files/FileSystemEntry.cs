#region Information and licence agreements
/**
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
    public abstract class FileSystemEntry
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

        #region Methods
        /// <summary>
        /// Adds the given newNode to the correct location below the given superParentNode.
        /// 
        /// Note! If we try to load the content of a foldernode that are below what we have 
        /// loaded so far we will not add that node to our tree. It might be necessary to load the file
        ///  tree from the root node since we need to get additional folder information from the server (comments, file size, etc)
        ///  Example: If we load the folder /Folder1 before we load / we will never add /Folder1
        /// </summary>
        /// <param name="newNode">The node to add.</param>
        /// <param name="superParentNode">The parent or grandparent node where newNode should be added to.</param>
        /// <param name="depth"></param>
        /// <returns>True if newNode was added successfully or if the node already existed. False otherwise.</returns>
        private bool Add(FileSystemEntry newNode, FolderNode superParentNode, int depth)
        {
            // We are at the correct location and the node should be added
            if (superParentNode.Path == newNode.ParentPath)
            {
                if (superParentNode.HasChild(newNode))
                {
                    return false; //Return false if the file/folder already exists
                }
                else
                {
                    superParentNode.Children.Add(newNode);
                    return true;
                }
            }

            // Traverse the tree to find the correct location
            foreach (FileSystemEntry parent in superParentNode.FolderNodes)
            {
                if (parent.PathArray[depth] == newNode.PathArray[depth])
                {
                    bool added = Add(newNode, ((FolderNode)parent), depth + 1);
                    if (added)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add a new node to the corresponding location in the tree
        /// </summary>
        /// <param name="newNode">The new node to add</param>
        /// <param name="superParentNode">A node in the tree where the new node should be added to. 
        /// Note! New node might be added further down in the tree.</param>
        /// <returns></returns>
        public bool Add(FileSystemEntry newNode, FolderNode superParentNode)
        {
            return Add(newNode, superParentNode, 0);
        }

        /// <summary>
        /// Removes the given node nodeToRemove from the file tree anywhere below the given FolderNode superParentNode.
        /// </summary>
        /// <param name="nodeToRemove">The node to remove</param>
        /// <param name="superParentNode">The FolderNode where this node should be removed from. Note! The given node nodeToRemove can be a child or a grandchild of superParentNode.</param>
        /// <returns>If it succeeds to remode the given node nodeToRemove that node is returned. Otherwise null is returned.</returns>
        public FileSystemEntry Remove(FileSystemEntry nodeToRemove, FolderNode superParentNode)
        {
            throw new NotImplementedException(); //TODO: Implement
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the root node of a tree
        /// </summary>
        public void InitRootNode()
        {
            this.created = DateTime.Now;
            this.modified = this.created;
            this.path = "/";
            this.pathArray = new string[1];
            this.pathArray[0] = "";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FileSystemEntry(MessageEventArgs_410420 messageEventArgs)
        {
            this.messageEventArgs = messageEventArgs;
            this.created = messageEventArgs.Created;
            this.modified = messageEventArgs.Modified;
            this.path = messageEventArgs.Path;

            this.pathArray = messageEventArgs.Path.Split('/');
            this.name = PathArray[PathArray.Length - 1];

            // Build the parent path string
            for (int i = 0; i < pathArray.Length - 1; i++)
            {
                if (i == 0)
                {
                    this.parentPath += "/"; //Avoids the first element to have 2 slashes "//"
                }
                else if(i == pathArray.Length - 2) 
                {
                    this.parentPath += pathArray[i]; //The last element should not have any trailing /
                }
                else
                {
                    this.parentPath += pathArray[i] + "/"; //All non root or non last elements
                }
            }
        }

        /// <summary>
        /// Constructor - Empty
        /// </summary>
        public FileSystemEntry()
        {
        }
        #endregion
    }
}
