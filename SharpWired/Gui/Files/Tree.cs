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
using System.Collections;

namespace SharpWired.Gui.Files {
    /// <summary>
    /// Shows a representation of the File Model, which models the file tree on the server.
    /// </summary>
    public partial class Tree : FilesGuiBase {

        ArrayList nodeList = new ArrayList();

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

        delegate void PopulateFileTreeCallBack(TreeView treeView, List<FileSystemEntry> addedNodes);

        protected override void OnOffline() {
            base.OnOffline();
            ClearControl(this);
        }

        /// <summary>
        /// Call this method when new nodes are added.
        /// </summary>
        /// <param name="newNodes"></param>
        public void OnNewNodesAdded(List<FileSystemEntry> addedNodes) {
            PopulateFileTree(this.rootTreeView, addedNodes);
        }
        #endregion

        #region Methods
        public void Clear() {
            rootTreeView.Nodes.Clear();
        }

        public override void Init(SharpWiredModel model, SharpWiredController controller) {
            base.Init(model, controller);

            ImageList rootTreeViewIcons = new ImageList();
            rootTreeViewIcons.ColorDepth = ColorDepth.Depth32Bit;
            IconHandler iconHandler = IconHandler.Instance;
            rootTreeViewIcons.Images.Add(iconHandler.GetFolderIconFromSystem());
            rootTreeView.ImageList = rootTreeViewIcons;
        }
        
        /// <summary>
        /// Populates the filetree from the given super root.
        /// </summary>
        /// <remarks>Uses callback if necessary.</remarks>
        /// <param name="rootTreeView">The TreeView to populate.</param>
        /// <param name="superRootNode">The root node from the model to populate from.</param>
        private void PopulateFileTree(TreeView rootTreeView, List<FileSystemEntry> newNodes) {
            if (InvokeRequired) {
                PopulateFileTreeCallBack callback = new PopulateFileTreeCallBack(PopulateFileTree);
                Invoke(callback, new object[] { rootTreeView, newNodes });
                return;
            }


            // TODO: Move to model!
            // Request biggest common denominator for all FileSystemEntry items
            FileSystemEntry currentPath = GetBiggestCommonDenominator(newNodes);

            if (currentPath.Path == "/") {
                foreach (FileSystemEntry node in newNodes)
                    if (node is FolderNode)
                        rootTreeView.Nodes.Add(node.Path, node.Name);
            } else {
                TreeNode[] foundNodes = rootTreeView.Nodes.Find(currentPath.Path, true);
                TreeNode currentNode = foundNodes[0];

                foreach (FileSystemEntry node in newNodes)
                    if (node is FolderNode)
                        currentNode.Nodes.Add(node.Path, node.Name);
            }
        }

        private FileSystemEntry GetBiggestCommonDenominator(List<FileSystemEntry> entries) {
            FileSystemEntry parent = null;

            if (entries.Count > 0)
                parent = entries[0].Parent;

            return parent;
        }
        #endregion

        private void rootTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
            TreeNode node = rootTreeView.SelectedNode;
            if (node != null && SelectFolderNodeChange != null) {
                SelectFolderNodeChange(model.Server.FileListingModel.GetNode(node.Name));
            }
        }
    }
}
