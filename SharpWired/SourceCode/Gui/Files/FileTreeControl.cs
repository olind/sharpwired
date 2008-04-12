#region Information and licence agreements
/*
 * FileTreeControls.cs
 * Created by Ola Lindberg and Peter Holmdahl, 2007-09-29
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpWired.Gui.Files;
using SharpWired.Model;
using SharpWired.Gui.Resources.Icons;
using SharpWired.Model.Files;

namespace SharpWired.Gui.Files
{
    /// <summary>
    /// Shows a representation of the File Model, which models the file tree on the server.
    /// </summary>
    public partial class FileTreeControl : UserControl
    {
        #region Variables
        private LogicManager logicManager;
        private GuiFilesController guiFilesController;
        #endregion

        #region Listeners from GUI
        /// <summary>
        /// The mouse was clicked in the TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rootTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            WiredTreeNode node = (WiredTreeNode)rootTreeView.GetNodeAt(e.Location);
            guiFilesController.ChangeSelectedNode(this, node.ModelNode);
        }

        private void FileTreeControl_KeyUp(object sender, KeyEventArgs e)
        {
            /*
                TODO: The following shortcuts is windows standard should be implemented. We might want to 
                      make some more general functionallity for this to make it work native on other 
                      plattforms as well.

                Refresh window.                     F5
                Rename item.                        F2
                Select all items.                   CTRL+A (Should select them in the details view)
                View an item's properties.          ALT+ENTER or ALT+DOUBLE-CLICK
                Collapse the current selection if   LEFT ARROW
                   it is expanded or Select the 
                   parent folder.  	 
                Collapse the selected folder. 	    NUM LOCK+MINUS SIGN (-)
                Expand the current selection if     RIGHT ARROW
                   it is collapsed or Select 
                   the first subfolder 	
                Expand all folders below the        NUM LOCK+*
                   current selection. 	
                Expand the selected folder. 	    NUM LOCK+PLUS SIGN (+)
                Switch between left and             F6
                   right panes. 	
            */
        }
        #endregion

        #region Old populate file tree functionallity Controller for the TreeNode
        /// <summary>
        /// Populates the filetree from the given super root.
        /// </summary>
        /// <remarks>Uses callback if necessary.</remarks>
        /// <param name="rootTreeView">The TreeView to populate.</param>
        /// <param name="superRootNode">The root node from the model to populate from.</param>
        private void PopulateFileTree(TreeView rootTreeView, FolderNode superRootNode)
        {
            if (InvokeRequired)
            {
                PopulateFileTreeCallBack callback = new PopulateFileTreeCallBack(PopulateFileTree);
                Invoke(callback, new object[] { rootTreeView, superRootNode });
                return;
            }

            if (rootTreeView.Nodes.Count > 0)
                rootTreeView.Nodes.Clear();

            // Just to put a name on the root in the filetree. alternatively,
            // the tree can skip the server root node, and have several nodes
            // at "level 0" in the tree.
            if (string.IsNullOrEmpty(superRootNode.Name))
                superRootNode.Name = "Server";
            rootTreeView.Nodes.Add(MakeFileNode(superRootNode));

            // Expand all nodes make the test easy and nice.
            rootTreeView.ExpandAll();
        }
        delegate void PopulateFileTreeCallBack(TreeView treeView, FolderNode superRootNode);

        /// <summary>
        /// Takes a FileSystemEntry and build a subtree from that,
        /// returnung the WiredTreeNode that represent the FSE given.
        /// </summary>
        /// <param name="fileSystemEntry">The FSE to build a WiredNode from (a subtree).</param>
        /// <returns>The WiredNode that represents the given FSE.</returns>
        private WiredTreeNode MakeFileNode(FileSystemEntry fileSystemEntry)
        {
            if (fileSystemEntry != null)
            {
                WiredTreeNode node = new WiredTreeNode(fileSystemEntry);
                node.ImageIndex = node.IconIndex;
                node.SelectedImageIndex = node.IconIndex;
                if (fileSystemEntry is FolderNode
                    && fileSystemEntry.HasChildren())
                {
                    foreach (FolderNode child in fileSystemEntry.FolderNodes)
                    {
                        node.Nodes.Add(MakeFileNode(child));
                    }
                }
                return node;
            }
            return new WiredTreeNode("The given node was null, but I didn't feel like trowing an exception!");
        }

        /// <summary>
        /// Clears the given tree view.
        /// </summary>
        /// <param name="tree"></param>
        private void ClearTreeView(TreeView tree)
        {
            if (this.InvokeRequired)
            {
                ClearTreeViewCallback clearTreeViewCallback = new ClearTreeViewCallback(ClearTreeView);
                this.Invoke(clearTreeViewCallback, new object[] { tree });
            }
            else
            {
                tree.Nodes.Clear();
            }
        }
        delegate void ClearTreeViewCallback(TreeView tree);
        #endregion
        
        #region File downloads
        /// <summary>
        /// The mouse was double clicked in the TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rootTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //TODO: Remove or move code below to the GuiFilesController
            //WiredTreeNode node = (WiredTreeNode)rootTreeView.GetNodeAt(e.Location);
            //if (node != null)
            //{
            //    node.TriggerDoubleClicked(e);
            //    if (node.Tag is FileNode)
            //    {
            //        WantDownloadFile(node.Tag as FileNode);
            //    }
            //}
        }

        private void WantDownloadFile(FileNode fileNode)
        {
            logicManager.FileTransferController.EnqueueDownload(
                logicManager.ConnectionManager.CurrentBookmark,
                fileNode,
                logicManager.FileTransferController.DefaultDownloadFolder);
        }

        #endregion

        #region Initialization
		/// <summary>
		/// Creates and inits components.
		/// </summary>
        public FileTreeControl()
        {
            InitializeComponent();
        }

		/// <summary>
		/// Inits the FieTreeController
		/// </summary>
		/// <param name="logicManager"></param>
		/// <param name="guiFilesController"></param>
        public void Init(LogicManager logicManager, GuiFilesController guiFilesController)
        {
            this.logicManager = logicManager;
            this.guiFilesController = guiFilesController;

            ImageList rootTreeViewIcons = new ImageList();
            rootTreeViewIcons.ColorDepth = ColorDepth.Depth32Bit;
            IconHandler iconHandler = new IconHandler();
            try
            {
                rootTreeViewIcons.Images.Add(iconHandler.FolderClosed);
                rootTreeViewIcons.Images.Add(iconHandler.File);
            }
            catch (Exception e)
            {
                Console.WriteLine("FileUserControl.cs | Failed to add images for rootTreView. Exception: " + e); //TODO: Throw exception
            }
            rootTreeView.ImageList = rootTreeViewIcons;
        }

        #endregion

        #region New populate file tree functionallity
        /// <summary>
        /// Call this method when new nodes are added.
        /// </summary>
        /// <param name="addedNodes"></param>
        public void OnNewNodesAdded(List<FileSystemEntry> addedNodes)
        {
            //TODO: My original idea was to use the addedNodes and update the file tree with those nodes only
            //but that requires a remake of the populateFileTree - functionallity.
            PopulateFileTree(this.rootTreeView, logicManager.FileListingController.FileListingModel.RootNode);
        }
        #endregion
    }
}
