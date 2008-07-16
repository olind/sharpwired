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
using System.Diagnostics;
using SharpWired.Controller;

namespace SharpWired.Gui.Files {
    /// <summary>
    /// Shows a representation of the File Model, which models the file tree on the server.
    /// </summary>
    public partial class Tree : FilesGuiBase {

        #region Constructors
        /// <summary>
        /// Creates and inits components.
        /// </summary>
        public Tree() {
            InitializeComponent();
        }
        #endregion

        #region Events & Listeners
        public delegate void SelectFolderNodeChangeDelegate(FileSystemEntry node);
        public event SelectFolderNodeChangeDelegate SelectFolderNodeChange;

        delegate void PopulateFileTreeCallBack(TreeView treeView, FolderNode superRootNode);

        protected override void OnOffline() {
            base.OnOffline();
            ClearControl(rootTreeView);
        }

        /// <summary>
        /// Call this method when new nodes are added.
        /// </summary>
        /// <param name="addedNodes"></param>
        public void OnNewNodesAdded(List<FileSystemEntry> addedNodes) {
            PopulateFileTree(this.rootTreeView, model.Server.FileListingModel.RootNode);
        }
        
        /// <summary>
        /// The mouse was clicked in the TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rootTreeView_MouseClick(object sender, MouseEventArgs e) {
            WiredTreeNode node = (WiredTreeNode)rootTreeView.GetNodeAt(e.Location);
            if (node != null && SelectFolderNodeChange != null) {
                SelectFolderNodeChange(node.ModelNode);
            }
        }
        #endregion

        #region Methods
        public override void Init(SharpWiredModel model, SharpWiredController controller) {
            base.Init(model, controller);

            ImageList rootTreeViewIcons = new ImageList();
            rootTreeViewIcons.ColorDepth = ColorDepth.Depth32Bit;
            IconHandler iconHandler = new IconHandler();
            try {
                rootTreeViewIcons.Images.Add(iconHandler.FolderClosed);
                rootTreeViewIcons.Images.Add(iconHandler.File);
            } catch (Exception e) {
                //TODO: Error handling?
                Debug.WriteLine("FileUserControl.cs | Failed to add images for rootTreView. Exception: " + e);
            }
            rootTreeView.ImageList = rootTreeViewIcons;
        }
        
        /// <summary>
        /// Populates the filetree from the given super root.
        /// </summary>
        /// <remarks>Uses callback if necessary.</remarks>
        /// <param name="rootTreeView">The TreeView to populate.</param>
        /// <param name="superRootNode">The root node from the model to populate from.</param>
        private void PopulateFileTree(TreeView rootTreeView, FolderNode superRootNode) {
            if (InvokeRequired) {
                PopulateFileTreeCallBack callback = new PopulateFileTreeCallBack(PopulateFileTree);
                Invoke(callback, new object[] { rootTreeView, superRootNode });
                return;
            }

            if (rootTreeView.Nodes.Count > 0)
                ClearControl(rootTreeView);

            // Just to put a name on the root in the filetree. alternatively,
            // the tree can skip the server root node, and have several nodes
            // at "level 0" in the tree.
            if (string.IsNullOrEmpty(superRootNode.Name))
                superRootNode.Name = "Server";
            rootTreeView.Nodes.Add(MakeFileNode(superRootNode));

            // Expand all nodes make the test easy and nice.
            rootTreeView.ExpandAll();
        }

        /// <summary>
        /// Takes a FileSystemEntry and build a subtree from that,
        /// returnung the WiredTreeNode that represent the FSE given.
        /// </summary>
        /// <param name="fileSystemEntry">The FSE to build a WiredNode from (a subtree).</param>
        /// <returns>The WiredNode that represents the given FSE.</returns>
        private WiredTreeNode MakeFileNode(FileSystemEntry fileSystemEntry) {
            if (fileSystemEntry != null) {
                WiredTreeNode node = new WiredTreeNode(fileSystemEntry);
                node.ImageIndex = node.IconIndex;
                node.SelectedImageIndex = node.IconIndex;
                if (fileSystemEntry is FolderNode
                    && fileSystemEntry.HasChildren()) {
                    foreach (FolderNode child in fileSystemEntry.FolderNodes) {
                        node.Nodes.Add(MakeFileNode(child));
                    }
                }
                return node;
            }
            return new WiredTreeNode("The given node was null, but I didn't feel like trowing an exception!");
        }
        #endregion
    }
}
