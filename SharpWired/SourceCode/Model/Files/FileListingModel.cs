#region Information and licence agreements
/**
 * FileListingModel.cs 
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

namespace SharpWired.Model.Files
{
    public class FileListingModel
    {
        #region Variables
        private LogicManager logicManager;
        private FolderNode rootNode;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the root node for this file three
        /// </summary>
        public FolderNode RootNode
        {
            get { return rootNode; }
        }
        #endregion

        /// <summary>
        /// Call this method when the file listing is done
        /// </summary>
        /// <param name="superRootNode"></param>
        public void FileListingDone(FolderNode superRootNode)
        {
            OnFileListingDoneEvent(superRootNode);
        }

        #region Delegates, events, event raiser methods

        /// <summary>
        /// Raised when the file listing are done.
        /// </summary>
        /// <param name="superRootNode">The root node from where file listing started.</param>
        private void OnFileListingDoneEvent(FolderNode superRootNode)
        {
            if (FileListingDoneEvent != null)
                FileListingDoneEvent(superRootNode);
        }

        public event FileListingDoneDelegate FileListingDoneEvent;
        public delegate void FileListingDoneDelegate(FolderNode superRootNode);
        #endregion

        #region Initialization
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logicManager"></param>
        public FileListingModel(LogicManager logicManager)
        {
            this.logicManager = logicManager;
            rootNode = new FolderNode();
            rootNode.InitRootNode();
        }
        #endregion
    }
}
