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
using SharpWired.Controller;
using System.Diagnostics;

namespace SharpWired.Gui.Files {
    /// <summary>
    /// Holds referenses to and inits the other file views
    /// </summary>
    public partial class FilesContainer : SharpWiredGuiBase {
        public delegate void FolderChangedDelegate(Folder folder);
        public event FolderChangedDelegate SelectedFolderChanged;

        public FilesContainer() {
            InitializeComponent();
        }

        protected override void OnOnline() {
            tree.SelectedFolderChanged += OnSelectedFolderChanged;

            breadCrumb.SelectedFolderChanged += OnSelectedFolderChanged;
            SelectedFolderChanged += breadCrumb.OnSelectedFolderNodeChanged;

            details.SelectedFolderChanged += OnSelectedFolderChanged;
            SelectedFolderChanged += details.OnSelectedFolderNodeChanged;

            OnSelectedFolderChanged(Model.Server.FileRoot);
        }

        protected override void OnOffline() {
            tree.SelectedFolderChanged -= OnSelectedFolderChanged;
            
            breadCrumb.SelectedFolderChanged -= OnSelectedFolderChanged;
            SelectedFolderChanged -= breadCrumb.OnSelectedFolderNodeChanged;
            
            details.SelectedFolderChanged -= OnSelectedFolderChanged;
            SelectedFolderChanged -= details.OnSelectedFolderNodeChanged;
        }

        /// <summary>
        /// Inits this class and it's subclasses
        /// </summary>
        /// <param name="model"></param>
        /// <param name="controller"></param>
        public override void Init() {
            base.Init();
            tree.Init();
            breadCrumb.Init();
            details.Init();
        }

        private void OnSelectedFolderChanged(INode node) {
            if (node is Folder) {
                Folder folder = node as Folder;

                if (SelectedFolderChanged != null)
                    SelectedFolderChanged(folder);
                
                Controller.FileListingController.ReloadFileList(folder);
            }
        }
    }
}
