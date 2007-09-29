#region Information and licence agreements
/**
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
    /// Represents the file three availale on the server
    /// </summary>
    public partial class FileTreeControl : UserControl
    {
        private FilesUserControl filesUserControl;
        private LogicManager logicManager;

        #region Listerens from Model
        void FileListingModel_FileListingDoneEvent(FolderNode superRootNode)
        {
            ClearTreeView(rootTreeView);
            PopulateFileTree(rootTreeView, superRootNode);
        }
        #endregion

        #region Handler for the TreeNode
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
        delegate void PopulateFileTreeCallBack(TreeView treeView, FolderNode rootNode);

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
                    List<FileSystemEntry> children = (fileSystemEntry as FolderNode).Children;
                    foreach (FileSystemEntry child in children)
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

        #region Listeners from GUI
        /// <summary>
        /// The mouse was clicked in the TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rootTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            WiredTreeNode node = (WiredTreeNode)rootTreeView.GetNodeAt(e.Location);
            if (node != null && node.ModelNode is FolderNode)
            {
                node.TriggerClicked(e);
                this.logicManager.FileListingHandler.ReloadFileList(node.ModelNode.Path);
            }

            WiredTreeNodeArgs nodeArgs = new WiredTreeNodeArgs(node);
            if (FolderSelectedEvent != null && node.ModelNode is FolderNode)
            {
                FolderSelectedEvent(sender, nodeArgs);
            }
        }
        public event EventHandler<WiredTreeNodeArgs> FolderSelectedEvent;

        /// <summary>
        /// The mouse was double clicked in the TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rootTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WiredTreeNode node = (WiredTreeNode)rootTreeView.GetNodeAt(e.Location);
            if (node != null)
            {
                node.TriggerDoubleClicked(e);
                if (node.Tag is FileNode)
                {
                    WantDownloadFile(node.Tag as FileNode);
                }
            }
        }
        #endregion
        
        #region File downloads
        private void WantDownloadFile(FileNode fileNode)
        {
            logicManager.FileTransferHandler.EnqueueDownload(
                logicManager.ConnectionManager.CurrentServer,
                fileNode,
                logicManager.FileTransferHandler.DefaultDownloadFolder);
        }
        #endregion

        #region Initialization
        public FileTreeControl()
        {
            InitializeComponent();
        }

        public void Init(FilesUserControl filesUserControl, LogicManager logicManager)
        {
            this.filesUserControl = filesUserControl;
            this.logicManager = logicManager;
            //filesUserControl.FolderSelectedEvent += new EventHandler<WiredTreeNodeArgs>(filesUserControl_FolderSelectedEvent);

            logicManager.FileListingHandler.FileListingModel.FileListingDoneEvent += new FileListingModel.FileListingDoneDelegate(FileListingModel_FileListingDoneEvent);
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
    }
}
