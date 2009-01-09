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
using System.Collections;

namespace SharpWired.Model.Files {
    public class FileTree : Folder {

        Dictionary<string, List<MessageEventArgs_410420>> Listings = new Dictionary<string, List<MessageEventArgs_410420>>();        
        //home/ola/ -> message1, message2

        public FileTree() : base("/", DateTime.Now, DateTime.Now, 0) {
            ConnectionManager.Messages.FileListingEvent += OnFileListingEvent;
            ConnectionManager.Messages.FileListingDoneEvent += OnFileListingDoneEvent;
        }

        void OnFileListingEvent(MessageEventArgs_410420 message) {
            var p = message.Path;
            var name = p.Substring(p.LastIndexOf('/') + 1);
            var folder = p.Substring(0, p.LastIndexOf('/') + 1);

            if (Listings.ContainsKey(folder)) {
                Listings[folder].Add(message);
            } else {
                var children = new List<MessageEventArgs_410420>();
                children.Add(message);
                Listings.Add(folder, children);
            }
        }

        void OnFileListingDoneEvent(MessageEventArgs_411 message) {
            string folder = message.Path;
            Folder n = (Folder)this.Get(folder); //path is always to a folder

            if (Listings.ContainsKey(folder)) {
                n.AddChildren(Listings[folder]);
            } else {
                n.AddChildren(new List<MessageEventArgs_410420>());
            }
            Listings.Remove(folder);
        }

        public override INode Get(string path) {
            if (path == Path)
                return this;
            else
                return base.Get(path);
        }

        

        /*
        void OnFileListingEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_410420 m) {
            ANode newNode;
            if (m.FileType == "0")
                newNode = new ANode(m);
            else if (m.FileType == "1")
                newNode = new FolderNode(m);
            else if (m.FileType == "2")
                newNode = new FolderNodeUploads(m);
            else if (m.FileType == "3")
                newNode = new FolderNodeDropBox(m);
            else
                throw new Exception("File or Folder type is not of any recognable type.");

            // Add the node newNode anywhere below our root node in our file tree
            this.Add(newNode, this.Root);
        }

        /// <summary>
        /// File listing done message was received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="m"></param>
        void OnFileListingDoneEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_411 m) {
            ANode searchedNode = GetNode(m.Path, (ANode)Root);
            if (searchedNode != null) {
                if (searchedNode is FolderNode) {
                    (searchedNode as FolderNode).DoneUpdating();
                }
            } else {
                // NOTE: Experimental!
                Root.DoneUpdating();
            }

            if (FileModelUpdatedEvent != null
                    && recentlyAddedNodes != null
                    && recentlyAddedNodes.Count > 0) {
                FileModelUpdatedEvent(recentlyAddedNodes);
                recentlyAddedNodes.Clear();
            }
        }
        


        /** Old Stuff **********************************************

        private List<ANode> recentlyAddedNodes = new List<ANode>();

    

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m"></param>
        public FileTree(Messages m) {
            m.FileListingEvent += OnFileListingEvent;
            m.FileListingDoneEvent += OnFileListingDoneEvent;
            Root = new FolderNode();
        }

        /// <summary>
        /// Gets the node at the given destination.
        /// </summary>
        /// <param name="requestedNodePath">The destination from were to get the node</param>
        /// <returns></returns>
        public ANode GetNode(string requestedNodePath) {
            return GetNode(requestedNodePath, Root);
        }

        /// <summary>
        /// Gets the node at the given nodePath
        /// </summary>
        /// <param name="requestedNodePath">The destination. Must be not be null or empty.</param>
        /// <param name="traversingNode">The node from where search should be started</param>
        /// <returns>If the node exists in the model; the node at whe given destination nodePath otherwise null</returns>
        public ANode GetNode(string requestedNodePath, ANode traversingNode) {
            if (string.IsNullOrEmpty(requestedNodePath))
                return null;

            if (traversingNode.Path == requestedNodePath) //If we are searching for a FileNode this will return
            {
                return traversingNode;
            }

            // 1 is because all destination arrays start with ""._______________________________1__
            return GetNode(ANode.SplitPath(requestedNodePath), traversingNode, 1);
        }

        /// <summary>
        /// Searches from the given FileSystemEntry as starting point, for another FSE with the given destination.
        /// At each step in the tree, looking for a FSE with the FSE.PathArray[level] equal to destination[level].
        /// </summary>
        /// <param name="destination">The searches destination.</param>
        /// <param name="startFromHere">We need a node from the model so that we can iterate the file tree somehow.</param>
        /// <param name="level">The level to look at in the destination. Typically starts out with 0. </param>
        /// <returns>FSE if found, null else.</returns>
        private ANode GetNode(string[] path, ANode startFromHere, int level) {
            // The special case when the root is asked for.
            if (path.Length == 1 && path[0] == "")
                return null;
            if (path == null || path.Length == 0)
                throw new ArgumentException("Path can't be null or empty!", "path");
            if (startFromHere == null)
                throw new ArgumentException("We need a real FileSystemEntry to start from, not null!", "startFromHere");
            if (level < 0 || level >= path.Length)
                throw new ArgumentException("We can't be searching on a deeper level than the path is long! Perhaps the path array didn't start with '\"\" as all other path arrays does.", "level");

            if (startFromHere is ANode) {
                // If at the end of the destination.
                if (path.Length - 1 == level) {
                    string searched = path[level];
                    string compareTo = startFromHere.PathArray[level];
                    if (searched == compareTo) {
                        // NOTE: do full comparasion of destination?!
                        return startFromHere;
                    } else
                        return null;
                }
                    // Could'nt find anythin.
                else
                    return null;
            } else if (startFromHere is FolderNode) {
                foreach (ANode entry in (startFromHere as FolderNode).Children) {
                    string searchedPart = path[level];
                    string entryPart = entry.PathArray[level];
                    // NOTE: descide what comparation is good!
                    if (searchedPart == entryPart) {
                        // If at the end of the destination.
                        if (path.Length - 1 == level) {
                            return entry;
                        } else {
                            if (entry is FolderNode)
                                return GetNode(path, entry, level + 1);
                            else if (entry is ANode) {
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
        private bool Add(ANode newNode, FolderNode superParentNode, int depth) {
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
            foreach (ANode parent in superParentNode.FolderNodes) {
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
        public bool Add(ANode newNode, FolderNode superParentNode) {
            return this.Add(newNode, superParentNode, 0);
        }

        /// <summary>
        /// Delegate for FileModelUpdatedEvent
        /// </summary>
        /// <param name="newNodes"></param>
        public delegate void FileModelUpdatedDelegate(List<ANode> addedNodes);
        /// <summary>
        /// Raised when new files are added to the model
        /// </summary>
        public event FileModelUpdatedDelegate FileModelUpdatedEvent;

        /// <summary>
        /// A file listing message was received (Listener (from message layer) for FileListingEvents)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="m"></param>
        */
    }
}
