#region Information and licence agreements
/*
 * GuiFilesController.cs
 * Created by Ola Lindberg, 2007-11-03
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
using SharpWired.Model;
using SharpWired.Model.Files;

namespace SharpWired.Gui.Files
{
    /// <summary>
    /// A controller class for the file handling in GUI. 
    ///  * Updates the model.
    ///  * Holds a referense to the currently selected node
    /// </summary>
    public class GuiFilesController
    {
        private LogicManager logicManager;
        private FileSystemEntry selectedNode;

        /// <summary>
        /// Get the selected node.
        /// </summary>
        public FileSystemEntry SelectedNode
        {
            get { return selectedNode; }
        }

        /// <summary>
        /// Change the selected node. 
        /// When the selected node is set an event is raised.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="selectedNode"></param>
        public void ChangeSelectedNode(object sender, FileSystemEntry selectedNode)
        {
            this.selectedNode = selectedNode;

            if (selectedNode is FolderNode)
            {
                WiredNodeArgs nodeArgs = new WiredNodeArgs(selectedNode);
                if (SelectedFolderNodeChangedEvent != null)
                {
                    SelectedFolderNodeChangedEvent(sender, nodeArgs);
                }

                this.logicManager.FileListingHandler.ReloadFileList((FolderNode)selectedNode);
            }
            else if (selectedNode is FileNode)
            {
                Console.WriteLine("TODO: Dealing with file nodes are not implemented");
            }
        }

        /// <summary>
        /// Change selected node to the given path
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="path">The path to change as selected</param>
        public void ChangeSelectedNode(object sender, string path)
        {
            ChangeSelectedNode(sender, this.logicManager.FileListingHandler.FileListingModel.GetNode(path));
        }

        /// <summary>
        /// Call to request a download of a node
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="node">The node where download should begin. 
        ///     All subnodes will be requested as well.
        ///     Note: All subnodes might not be loaded from model yet
        /// </param>
        public void RequestNodeDownload(object sender, FileSystemEntry node)
        {
            logicManager.FileTransferHandler.EnqueEntry(node);
        }

        /// <summary>
        /// Raised when the selected node has changed
        /// </summary>
        public event EventHandler<WiredNodeArgs> SelectedFolderNodeChangedEvent;

        /// <summary>
        /// Changes the selected node to be the root node
        /// </summary>
        /// <param name="sender"></param>
        public void ChangeSelectedNodeToRootNode(object sender)
        {
            ChangeSelectedNode(sender, this.logicManager.FileListingHandler.FileListingModel.RootNode);
        }

        /// <summary>
        /// Constructor - Inits the other GUI-classes
        /// </summary>
        /// <param name="logicManager"></param>
        /// <param name="fileTreeControl"></param>
        /// <param name="fileDetailsControl"></param>
        /// <param name="breadCrumbControl"></param>
        public GuiFilesController(LogicManager logicManager, 
            FileTreeControl fileTreeControl, FileDetailsControl fileDetailsControl, 
            BreadCrumbControl breadCrumbControl)
        {
            this.logicManager = logicManager;
            fileTreeControl.Init(logicManager, this);
            fileDetailsControl.Init(this);
            breadCrumbControl.Init(this);
            

            // Attach listeners to other GUI files
            this.SelectedFolderNodeChangedEvent+=new EventHandler<WiredNodeArgs>(fileDetailsControl.OnFolderNodeChanged);
            this.SelectedFolderNodeChangedEvent += new EventHandler<WiredNodeArgs>(breadCrumbControl.OnFolderNodeChanged);
            logicManager.FileListingHandler.FileModelUpdatedEvent += new FileListingController.FileModelUpdatedDelegate(fileTreeControl.OnNewNodesAdded);
            // To get the initial listing in the details view
            logicManager.FileListingHandler.FileModelUpdatedEvent+=new FileListingController.FileModelUpdatedDelegate(fileDetailsControl.OnRootNodeInitialized);
            logicManager.FileListingHandler.FileModelUpdatedEvent += new FileListingController.FileModelUpdatedDelegate(breadCrumbControl.OnRootNodeInitialized);
        }
    }
}
