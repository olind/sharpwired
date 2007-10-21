#region Information and licence agreements
/*
 * FileListingHandler.cs 
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
using SharpWired.Connection;

namespace SharpWired.Model.Files
{
    /// <summary>
    /// Handles the local model for all file interactions
    /// </summary>
    public class FileListingHandler : HandlerBase
    {

        #region Variables
        FileListingModel fileListingModel;
        private List<FileSystemEntry> recentlyAddedNodes = new List<FileSystemEntry>();
        #endregion

        #region Properties

        private FolderNode fileTreeRootNode;

        /// <summary>
        /// Gets the file listing model
        /// </summary>
        public FileListingModel FileListingModel
        {
            get { return fileListingModel; }
        }
	
        #endregion

        #region Methods: Sending to command layer
        /// <summary>
        /// Requests a reload of the filelisting from root node on this server.
        /// </summary>
        public void ReloadFileList()
        {
            ReloadFileList(""); //Reloads from the root directory
        }

        /// <summary>
        /// Requests a reload of the filelisting on this server on the given path
        /// </summary>
        /// <param name="path">The path node where reloading should be requested.</param>
        private void ReloadFileList(string path)
        {
            //TODO: When we reload the filelisting today we request the reload and when the response 
            //returns from the server we check if the node equals an existing node and if it does we don't
            //add the node to the model but raises the updated event instead.
            //Instead of requesting reload we should first check if it exists in the model and if it does
            //we should not request the reload from server but just raise the event
            this.LogicManager.ConnectionManager.Commands.List(path);
        }

        /// <summary>
        /// Requests a reload of the filelisting on this server on the given node
        /// </summary>
        /// <param name="node">The node where reloading should be requested.</param>
        public void ReloadFileList(FolderNode node)
        {
            ReloadFileList(node.Path);
        }
        #endregion

        #region Listeners from message layer
        /// <summary>
        /// A file listing message was received (Listener (from message layer) for FileListingEvents)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
        void Messages_FileListingEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_410420 messageEventArgs)
        {
            FileSystemEntry newNode;
            if (messageEventArgs.FileType == "0")
            {
                newNode = new FileNode(messageEventArgs);
            }
            else if(messageEventArgs.FileType == "1")
            {
                newNode = new FolderNode(messageEventArgs);
            }
            else if (messageEventArgs.FileType == "2")
            {
                newNode = new FolderNodeUploads(messageEventArgs);
            }
            else if (messageEventArgs.FileType == "3")
            {
                newNode = new FolderNodeDropBox(messageEventArgs);
            }
            else
            {
                throw new Exception("File or Folder type is not of any recognable type.");
            }

            // Add the node newNode anywhere below our root node in our file tree
            this.Add(newNode, fileListingModel.RootNode);
        }

        /// <summary>
        /// File listing done message was received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
        void Messages_FileListingDoneEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_411 messageEventArgs)
        {
            fileListingModel.FileListingDone(messageEventArgs); //TODONow: Who is listening here? If it is the model that will tell the nodes to update it might be ok!

            if (FileModelUpdatedEvent != null)
            {
                FileModelUpdatedEvent(recentlyAddedNodes);
                recentlyAddedNodes.Clear();
            }
        }

        /// <summary>
        /// Delegate for FileModelUpdatedEvent
        /// </summary>
        /// <param name="addedNodes"></param>
        public delegate void FileModelUpdatedDelegate(List<FileSystemEntry> addedNodes);
        /// <summary>
        /// Raised when new files are added to the model
        /// </summary>
        public event FileModelUpdatedDelegate FileModelUpdatedEvent;
        #endregion

        #region Edit the file model

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
                    if (newNode is FolderNode)
                        ((FolderNode)newNode).DoneUpdating();

                    return false; //Return false if the file/folder already exists
                }
                else
                {
                    superParentNode.AddChildren(newNode);
                    newNode.Parent = superParentNode;
                    recentlyAddedNodes.Add(newNode);
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
            return this.Add(newNode, superParentNode, 0);
        }

        #endregion

        #region Initialization
        /// <summary>
        /// Initializor
        /// </summary>
        /// <param name="connectionManager"></param>
        public override void Init(ConnectionManager connectionManager)
        {
            base.Init(connectionManager);

            // Setting up listerens
            connectionManager.Messages.FileListingEvent += new Messages.FileListingEventHandler(Messages_FileListingEvent);
            connectionManager.Messages.FileListingDoneEvent += new Messages.FileListingDoneEventHandler(Messages_FileListingDoneEvent);

            this.ReloadFileList();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FileListingHandler(LogicManager logicManager)
            : base(logicManager)
        {
            fileListingModel = new FileListingModel(logicManager);
            fileTreeRootNode = fileListingModel.RootNode;
        }
        #endregion
    }
}
