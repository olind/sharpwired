#region Information and licence agreements
/*
 * FileDetailsControl.cs
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
using SharpWired.Model.Files;
using SharpWired.Gui.Resources.Icons;
using System.Diagnostics;
using SharpWired.Model;
using SharpWired.Controller;
using System.IO;

namespace SharpWired.Gui.Files {
    /// <summary>
    /// Represents a detail view of the currently selected nodes
    /// </summary>
    public partial class FolderListing : SharpWiredGuiBase {

        IconHandler iconHandler = IconHandler.Instance;
        FileMenu ContextMenu { get; set; }
        Folder CurrentFolder { get; set; }

        delegate void ClearCallback();

        public List<INode> SelectedItems {
            get {
                var n = new List<INode>();
                foreach (var s in detailsListView.SelectedItems)
                    n.Add(((WiredListNode)s).ModelNode);
                return n;
            }
        }

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public FolderListing() {
            InitializeComponent();
        }
        #endregion

        #region Events & Listeners
        public delegate void NodeDelegate(INode node);
        public event NodeDelegate SelectedFolderChanged;
        public event NodeDelegate RequestDownload;

        delegate void NodeListDelegate(List<INode> nodes);

        public void OnSelectedFolderNodeChanged(Folder folder) {
            if (CurrentFolder != null)
                CurrentFolder.Updated -= OnFolderUpdated;

            CurrentFolder = folder;
            CurrentFolder.Updated += OnFolderUpdated;
        }

        void OnFolderUpdated(INode node) {
            UpdateListView(((Folder)node).Children);
        }

        private void detailsListView_MouseDoubleClick(object sender, MouseEventArgs e) {
            WiredListNode node = (WiredListNode)detailsListView.GetItemAt(e.X, e.Y);
            if (node != null
                && node.ModelNode is Folder
                && SelectedFolderChanged != null) {
                SelectedFolderChanged(node.ModelNode);
            }
        }

        private void detailsListView_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (detailsListView.SelectedItems.Count == 1) {
                    INode n = ((WiredListNode)detailsListView.SelectedItems[0]).ModelNode;
                    if (n != null && SelectedFolderChanged != null)
                        SelectedFolderChanged(n);
                }
            }
        }
        #endregion

        #region Methods
        public override void Init() {
            base.Init();

            ImageList fileViewIcons = new ImageList();
            fileViewIcons.ColorDepth = ColorDepth.Depth32Bit;
            try {
                fileViewIcons.Images.Add("FOLDER", iconHandler.GetFolderIconFromSystem());
            } catch (Exception e) {
                Debug.WriteLine("FileUserControl.cs | Failed to add images for rootTreView. Exception: " + e); //TODO: Throw exception
            }

            detailsListView.SmallImageList = fileViewIcons;
            detailsListView.LargeImageList = fileViewIcons;
            detailsListView.View = View.Details;
            ContextMenu = new FileMenu(Controller, this);
        }

        /// <summary>
        /// Replace the items in this listview with the given list
        /// </summary>
        /// <param name="newNodes"></param>
        private void UpdateListView(List<INode> newNodes) {
            if (this.InvokeRequired) {
                this.Invoke(new NodeListDelegate(UpdateListView), new object[] { newNodes });
            } else {
                detailsListView.Columns.Clear();

                //detailsListView.Sorting = SortOrder.Ascending; //TODO: Sort folders before files
                detailsListView.LabelEdit = true;
                detailsListView.AllowColumnReorder = true;

                detailsListView.Columns.Add("Name", 200);
                detailsListView.Columns.Add("Size", 50, HorizontalAlignment.Right);
                detailsListView.Columns.Add("Added", 150);
                detailsListView.Columns.Add("Modified", 150);

                detailsListView.Items.Clear();

                newNodes.Sort();

                foreach (INode folder in newNodes) {
                    if (folder is Folder) {
                        AddToListView(folder, "FOLDER");
                    }
                }

                foreach (INode child in newNodes) {
                    if (child is INode) {
                        try {
                            string imageKey = Path.GetExtension(child.Name);

                            if (imageKey == "")
                                imageKey = "FILE";

                            if (!detailsListView.SmallImageList.Images.ContainsKey(imageKey))
                                detailsListView.SmallImageList.Images.Add(
                                    imageKey,
                                    iconHandler.GetFileIconFromSystem(child.Name));

                            AddToListView(child, imageKey);
                        } catch (ArgumentException e) {
                            // TODO: Do something smarter?
                            Debug.WriteLine(e.Message + ": " + child.Path);
                        }
                    }
                }
            }
        }


        private void AddToListView(INode child, string imageKey) {
            WiredListNode wln = new WiredListNode(child);
            wln.ImageIndex = detailsListView.SmallImageList.Images.IndexOfKey(imageKey);
            //wln.StateImageIndex = wln.Type;
            wln.SubItems.Add(wln.Size);
            wln.SubItems.Add(wln.Created.ToString());
            wln.SubItems.Add(wln.Modified.ToString());

            this.detailsListView.Items.Add(wln);
        }
        #endregion

        private void detailsListView_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                ContextMenu.Show(detailsListView, e.Location);
            }
        }

        private void Clear() {
            if (this.InvokeRequired) {
                ClearCallback callback = new ClearCallback(Clear);
                this.Invoke(callback);
            } else {
                detailsListView.Clear();
            }
        }
    }
}