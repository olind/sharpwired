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
using SharpWired.MessageEvents;

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

        FolderNode searchNode = null; //TODO: Remove once GetNode method is fixed
        /// <summary>
        /// Gets the node at the given nodePath
        /// </summary>
        /// <param name="nodePath"></param>
        /// <returns>If the node exists in the model; the node at whe given path nodePath otherwise null</returns>
        public FileSystemEntry GetNode(string requestedNodePath, FileSystemEntry traversingNode)
        {
            //FIXME: I couldnt get this method to return the correct node without using searchNode today

            if (requestedNodePath == "") // Happens sometimes. I guess it's before nodes have been loaded from server (first time)
                return null;

            if (traversingNode.Path == requestedNodePath) //If we are searching for a FileNode this will return
            {
                return traversingNode;
            }

            foreach (FolderNode childNode in traversingNode.FolderNodes)
            {
                if (childNode.Path == requestedNodePath)
                {
                    searchNode = childNode;
                }
                else 
                {
                    GetNode(requestedNodePath, childNode);
                }
            }
            return searchNode;
        }

        /// <summary>
        /// Call this method when the file listing is done
        /// </summary>
        /// <param name="superRootNode"></param>
        public void FileListingDone(MessageEventArgs_411 updatedDoneEventArgs)
        {
            if (FileListingDoneEvent != null)
            {
                FileSystemEntry searchedNode = GetNode(updatedDoneEventArgs.Path, (FileSystemEntry)rootNode);
                if (searchedNode != null)
                    searchNode.DoneUpdating();

                //TODO: This is probably useless since we only provide the path to the superRootNode. 
                //I keep it for now to not break the TreeView
                FileListingDoneEvent(this.rootNode);
            }
        }
        public event FileListingDoneDelegate FileListingDoneEvent;
        public delegate void FileListingDoneDelegate(FolderNode superRootNode);

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
