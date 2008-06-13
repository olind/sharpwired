#region Information and licence agreements
/*
 * FileListingController.cs 
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
using SharpWired.Model.Files;
using SharpWired.Model;

namespace SharpWired.Controller
{
    /// <summary>
    /// Handles the local model for all file interactions
    /// </summary>
    public class FileListingController : ControllerBase {

        #region Constructor
        public FileListingController(SharpWiredModel model) : base(model) {
            //ReloadFileList();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Requests a reload of the filelisting from root node on this server.
        /// </summary>
        public void ReloadFileList() {
            ReloadFileList(""); //Reloads from the root directory
        }

        /// <summary>
        /// Requests a reload of the filelisting on this server on the given node. 
        /// Note! Does only reload direct children to this node and not grand childrens etc.
        /// </summary>
        /// <param name="node">The node where reloading should be requested.</param>
        public void ReloadFileList(FolderNode node) {
            ReloadFileList(node.Path);
        }

        /// <summary>
        /// Requests a reload of the filelisting on this server on the given path.
        /// If this node doesn't have any childrens we assume it hasn't been loaded from server and 
        /// requests a reload. NOTE! If the node is not found in the tree we do a reload from the server root.
        /// </summary>
        /// <param name="path">The path node where reloading should be requested.</param>
        private void ReloadFileList(string path) {
            FileListingModel flm = this.server.FileListingModel;
            FileSystemEntry reloadNode = flm.GetNode(path, flm.RootNode);
             if(reloadNode != null && reloadNode is FolderNode) {
                if (((FolderNode)reloadNode).HasChildren())
                    (reloadNode as FolderNode).DoneUpdating();
                else
                    this.model.ConnectionManager.Commands.List(path);
             } else if (reloadNode == null) {
                 // To load the initial file list
                 this.model.ConnectionManager.Commands.List("/");
             }
        }
        #endregion
    }
}
