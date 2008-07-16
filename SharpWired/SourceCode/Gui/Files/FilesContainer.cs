#region Information and licence agreements
/*
 * FileUserControl.cs 
 * Created by Ola Lindberg and Peter Holmdahl, 2007-05-10
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
using SharpWired.Model;
using SharpWired.Model.Files;
using System.Collections;
using SharpWired.Gui.Resources;
using SharpWired.Gui.Resources.Icons;
using SharpWired.Connection.Transfers;
using SharpWired.Controller;
using System.Diagnostics;

namespace SharpWired.Gui.Files {
    /// <summary>
    /// Holds referenses to and inits the other file views
    /// </summary>
    public partial class FilesContainer : FilesGuiBase {

        #region Fields
        int cursorSuspendCount = 0;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor - Empty
        /// </summary>
        public FilesContainer() {
            InitializeComponent();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets true if the cursor is suspended. False otherwise.
        /// </summary>
        public bool Suspended {
            get { return cursorSuspendCount > 0; }
        }
        #endregion

        #region Events & Listeners
        /// <summary>
        /// Raised when the selected folder node has changed
        /// </summary>
        public event EventHandler<WiredNodeArgs> SelectedFolderNodeChanged;

        protected override void OnOnline() {
            model.Server.FileListingModel.FileModelUpdatedEvent += tree.OnNewNodesAdded;
            tree.SelectFolderNodeChange += OnSelectFolderNodeChange;
            //SelectedFolderNodeChanged += tree.OnSelectedFolderNodeChanged; //TODO: tree should mark selected node as well

            breadCrumb.SelectFolderNodeChange += OnSelectFolderNodeChange;
            SelectedFolderNodeChanged += breadCrumb.OnSelectedFolderNodeChanged;

            details.SelectFolderNodeChange += OnSelectFolderNodeChange;
            SelectedFolderNodeChanged += details.OnSelectedFolderNodeChanged;
            details.RequestDownload += OnRequestDownload;

            OnSelectFolderNodeChange(model.Server.FileListingModel.RootNode);
        }

        protected override void OnOffline() {
            model.Server.FileListingModel.FileModelUpdatedEvent -= tree.OnNewNodesAdded;
            tree.SelectFolderNodeChange -= OnSelectFolderNodeChange;
            //SelectedFolderNodeChanged -= tree.OnSelectedFolderNodeChanged; //TODO: tree should mark selected node as well
            
            breadCrumb.SelectFolderNodeChange -= OnSelectFolderNodeChange;
            SelectedFolderNodeChanged -= breadCrumb.OnSelectedFolderNodeChanged;
            details.RequestDownload -= OnRequestDownload;
            
            details.SelectFolderNodeChange -= OnSelectFolderNodeChange;
            SelectedFolderNodeChanged -= details.OnSelectedFolderNodeChanged;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Inits this class and it's subclasses
        /// </summary>
        /// <param name="model"></param>
        /// <param name="controller"></param>
        public override void Init(SharpWiredModel model, SharpWiredController controller) {
            base.Init(model, controller);
            tree.Init(model, controller);
            breadCrumb.Init(model, controller);
            details.Init(model, controller);
        }

        ///<summary>
        /// Suspends the Control, and sets the Cursor to a waitcursor, if it isn't already.
        ///</summary>
        public void Suspend() {
            // When calling this method, the state is always Suspended.
            // But we only set the cursor if we aren't already suspended.
            // So, if counter is 0, we are the first to call this method, so we set the cursor.
            // But, we also need to increase the counter, and therefore add one.
            // Since the '++' comes after the field name, the increse will be done after
            // the logical check for == 0.
            if (cursorSuspendCount++ == 0)
                Cursor = Cursors.WaitCursor;
        }

        /// <summary>
        /// Unsuspends the cursor.
        /// </summary>
        /// <returns>True if the cursor isn't suspended.</returns>
        public bool UnSuspend() {
            // Decrease the count (the -- is before the field name, so its decreased before the == 0).
            // And if we're done to zero again, we're no longer suspended and we set the cursor back.
            if (--cursorSuspendCount == 0)
                Cursor = Cursors.Default;
            return cursorSuspendCount == 0;
        }

        private void OnSelectFolderNodeChange(FileSystemEntry node) {
            if (node is FolderNode) {
                if (SelectedFolderNodeChanged != null) {
                    SelectedFolderNodeChanged(null, new WiredNodeArgs(node));
                }
                controller.FileListingController.ReloadFileList((FolderNode)node);
            } else if (node is FileNode) {
                Debug.WriteLine("TODO: Dealing with file nodes are not implemented");
            }
        }

        private void OnRequestDownload(FileSystemEntry node) {
            controller.FileTransferHandler.EnqueEntry(node);
        }
        #endregion
    }
}
