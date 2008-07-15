#region Information and licence agreements
/*
 * FileListingModel.cs 
 * Created by Ola Lindberg, 2007-01-28
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
using SharpWired.Connection;
using System.Diagnostics;

namespace SharpWired.Model.Files
{
	/// <summary>
	/// The class that deals with listening and building trees of the file on the server.
	/// </summary>
    public class FileListingModel
    {
        #region Fields
        private FolderNode rootNode;
        private List<FileSystemEntry> recentlyAddedNodes = new List<FileSystemEntry>();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m"></param>
        public FileListingModel(Messages m) {
            m.FileListingEvent += OnFileListingEvent;
            m.FileListingDoneEvent += OnFileListingDoneEvent;
            rootNode = new FolderNode();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the root node for this file three
        /// </summary>
        public FolderNode RootNode {
            get { return rootNode; }
        }
        #endregion

		#region Methods: Search Node
        /// <summary>
        /// Gets the node at the given path.
        /// </summary>
        /// <param name="requestedNodePath">The path from were to get the node</param>
        /// <returns></returns>
        public FileSystemEntry GetNode(string requestedNodePath)
        {
            return GetNode(requestedNodePath, rootNode);
        }

		/// <summary>
        /// Gets the node at the given nodePath
        /// </summary>
		/// <param name="requestedNodePath">The path. Must be not be null or empty.</param>
		/// <param name="traversingNode">The node from where search should be started</param>
        /// <returns>If the node exists in the model; the node at whe given path nodePath otherwise null</returns>
        public FileSystemEntry GetNode(string requestedNodePath, FileSystemEntry traversingNode) {
			if (string.IsNullOrEmpty(requestedNodePath))
				return null;
			
            if (traversingNode.Path == requestedNodePath) //If we are searching for a FileNode this will return
            {
                return traversingNode;
            }

			// 1 is because all path arrays start with ""._______________________________1__
			return GetNode(FileSystemEntry.SplitPath(requestedNodePath), traversingNode, 1);
        }

		/// <summary>
		/// Searches from the given FileSystemEntry as starting point, for another FSE with the given path.
		/// At each step in the tree, looking for a FSE with the FSE.PathArray[level] equal to path[level].
		/// </summary>
		/// <param name="path">The searches path.</param>
		/// <param name="startFromHere">We need a node from the model so that we can iterate the file tree somehow.</param>
		/// <param name="level">The level to look at in the path. Typically starts out with 0. </param>
		/// <returns>FSE if found, null else.</returns>
		private FileSystemEntry GetNode(string[] path, FileSystemEntry startFromHere, int level)
		{
			// The special case when the root is asked for.
			if (path.Length == 1 && path[0] == "")
				return null;
			if (path == null || path.Length == 0)
				throw new ArgumentException("Path can't be null or empty!", "path");
			if (startFromHere == null)
				throw new ArgumentException("We need a real FileSystemEntry to start from, not null!", "startFromHere");
			if (level < 0 || level >= path.Length)
				throw new ArgumentException("We can't be searching on a deeper level than the path is long! Perhaps the path array didn't start with '\"\" as all other path arrays does.", "level");

			if (startFromHere is FileNode)
			{
				// If at the end of the path.
				if (path.Length - 1 == level)
				{
					string searched = path[level];
					string compareTo = startFromHere.PathArray[level];
					if(searched == compareTo)
					{
						// NOTE: do full comparasion of path?!
						return startFromHere;
					}
					else
						return null;
				}
					// Could'nt find anythin.
				else
					return null;
			}
			else if (startFromHere is FolderNode)
			{
				foreach (FileSystemEntry entry in (startFromHere as FolderNode).Children)
				{
					string searchedPart = path[level];
					string entryPart = entry.PathArray[level];
					// NOTE: descide what comparation is good!
					if (searchedPart == entryPart)
					{
						// If at the end of the path.
						if(path.Length -1 == level)
						{
							return entry;
						}
						else
						{
							if (entry is FolderNode)
								return GetNode(path, entry, level + 1);
							else if (entry is FileNode)
							{
								throw new ApplicationException("The path seacrhed form must be malformed; it pointed to a file, but then kept on!");
							}
						}
					}
				}
				return null;
			}
			// Fall through.
			return null;
		}
		#endregion

        #region Methods: Edit the model
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
        private bool Add(FileSystemEntry newNode, FolderNode superParentNode, int depth) {
            // We are at the correct location and the node should be added
            if (superParentNode.Path == newNode.ParentPath) {
                if (superParentNode.HasChild(newNode)) {
                    if (newNode is FolderNode)
                        ((FolderNode)newNode).DoneUpdating();

                    return false; //Return false if the file/folder already exists
                } else {
                    superParentNode.AddChildren(newNode);
                    newNode.Parent = superParentNode;
                    recentlyAddedNodes.Add(newNode);
                    return true;
                }
            }

            // Traverse the tree to find the correct location
            foreach (FileSystemEntry parent in superParentNode.FolderNodes) {
                if (parent.PathArray[depth] == newNode.PathArray[depth]) {
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
        public bool Add(FileSystemEntry newNode, FolderNode superParentNode) {
            return this.Add(newNode, superParentNode, 0);
        }
        #endregion

        #region Events & Listeners
        /// <summary>
        /// Delegate for FileModelUpdatedEvent
        /// </summary>
        /// <param name="addedNodes"></param>
        public delegate void FileModelUpdatedDelegate(List<FileSystemEntry> addedNodes);
        /// <summary>
        /// Raised when new files are added to the model
        /// </summary>
        public event FileModelUpdatedDelegate FileModelUpdatedEvent;

        /// <summary>
        /// A file listing message was received (Listener (from message layer) for FileListingEvents)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="m"></param>
        void OnFileListingEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_410420 m) {
            FileSystemEntry newNode;
            if (m.FileType == "0") 
                newNode = new FileNode(m);
            else if (m.FileType == "1")
                newNode = new FolderNode(m);
            else if (m.FileType == "2") 
                newNode = new FolderNodeUploads(m);
            else if (m.FileType == "3")
                newNode = new FolderNodeDropBox(m);
            else
                throw new Exception("File or Folder type is not of any recognable type.");

            // Add the node newNode anywhere below our root node in our file tree
            this.Add(newNode, this.rootNode);
        }

        /// <summary>
        /// File listing done message was received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="m"></param>
        void OnFileListingDoneEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_411 m) {
            FileSystemEntry searchedNode = GetNode(m.Path, (FileSystemEntry)rootNode);
            if (searchedNode != null) {
                if (searchedNode is FolderNode) {
                    (searchedNode as FolderNode).DoneUpdating();
                }
            } else {
                // NOTE: Experimental!
                rootNode.DoneUpdating();
            }

            if (FileModelUpdatedEvent != null 
                    && recentlyAddedNodes != null 
                    && recentlyAddedNodes.Count > 0) {
                FileModelUpdatedEvent(recentlyAddedNodes);
                recentlyAddedNodes.Clear();
            }
        }
        #endregion
    }
}
