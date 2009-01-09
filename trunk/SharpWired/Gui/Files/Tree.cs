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
    public partial class Tree : SharpWiredGuiBase {

        ArrayList nodeList = new ArrayList();

        delegate void Callback();

        /// <summary>
        /// Creates and inits components.
        /// </summary>
        public Tree() {
            InitializeComponent();
        }

        public delegate void SelectFolderNodeChangeDelegate(INode node);
        public event SelectFolderNodeChangeDelegate SelectedFolderChanged;

        protected override void OnOnline() {
            if (this.InvokeRequired) {
                this.Invoke(new Callback(OnOnline));
            } else {
                base.OnOnline();
                rootTreeView.Nodes.Add(new WiredTreeNode(Model.Server.FileRoot));
            }
        }

        protected override void OnOffline() {
            base.OnOffline();
            Clear();
        }

        public void Clear() {
            if (this.InvokeRequired) {
                this.Invoke(new Callback(Clear));
            } else {
                rootTreeView.Nodes.Clear();
            }
        }

        public override void Init() {
            base.Init();

            ImageList rootTreeViewIcons = new ImageList();
            rootTreeViewIcons.ColorDepth = ColorDepth.Depth32Bit;
            IconHandler iconHandler = IconHandler.Instance;
            rootTreeViewIcons.Images.Add(iconHandler.GetFolderIconFromSystem());
            rootTreeView.ImageList = rootTreeViewIcons;
        }

        private void rootTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
            WiredTreeNode node = (WiredTreeNode)rootTreeView.SelectedNode;
            if (node != null && SelectedFolderChanged != null) {
                SelectedFolderChanged(node.ModelNode);
            }
        }
    }

    public class WiredTreeNode : TreeNode {
        protected delegate void Func();

        public INode ModelNode { get; set; }

        public WiredTreeNode(INode modelNode) {
            ModelNode = modelNode;
            Text = modelNode.Name;
            PopulateFolder();
            modelNode.Updated += OnUpdated;
        }

        public void OnUpdated(INode modelNode) {
            PopulateFolder();
        }

        private void PopulateFolder() {
            if (ModelNode is Folder) {

                Func del = delegate {
                    Folder folder = ModelNode as Folder;
                    this.Nodes.Clear();

                    foreach (INode child in folder.Children)
                        if (child is Folder)
                            this.Nodes.Add(new WiredTreeNode(child));
                };

                //The TreeView is null when the current node has not been added to a TreeView
                if (this.TreeView != null) {
                    this.TreeView.Invoke(del); //Thread safe required when the node is added to a TreeView
                } else {
                    del(); //When the node is not added to a TreeView we don't need to be thread safe
                }
            }
        }
    }
}
