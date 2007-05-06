#region Information and licence agreements
/**
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
    public class FileListingHandler : HandlerBase
    {

        #region Variables
        FileListingModel fileListingModel;
        #endregion

        #region Properties

        private FolderNode fileTreeRootNode;

        /// <summary>
        /// Get the root node of the file tree.
        /// TODO: remove this and use .FileListingModel.RootNode instead
        /// </summary>
        public FolderNode FileTreeRootNode
        {
            get { return fileTreeRootNode; }
        }

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
        /// <param name="fromNode">The path to wher the reload should appear.</param>
        public void ReloadFileList(string fromPath)
        {
            Console.WriteLine("Reloading file listing.");
            this.LogicManager.ConnectionManager.Commands.List(fromPath);
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
                //TODO: Create some type of exception
                throw new Exception("File or Folder type is not of any recognable type.");
            }

            // Add the node newNode anywhere below our root node in our file tree
            fileListingModel.RootNode.Add(newNode, fileListingModel.RootNode);
        }

        /// <summary>
        /// File listing done message was received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
        void Messages_FileListingDoneEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_411 messageEventArgs)
        {
            fileListingModel.FileListingDone(FileTreeRootNode);
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
