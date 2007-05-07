#region Information and licence agreements
/**
 * FolderNode.cs 
 * Created by Ola Lindberg, 2007-05-01
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
using System.Collections;

namespace SharpWired.Model.Files
{
    /// <summary>
    /// Representation of a "Wired Folder"
    /// </summary>
    public class FolderNode : FileSystemEntry
    {
        #region Variables
        private List<FileSystemEntry> children = new List<FileSystemEntry>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the list with this nodes childrens.
        /// </summary>
        public List<FileSystemEntry> Children
        {
            get { return children; }
        }

        /// <summary>
        /// Gets the FolderNodes that are childrens of this node.
        /// </summary>
        public override IEnumerator<FolderNode> FolderNodes
        {
            get
            {
                foreach (FileSystemEntry entry in this.Children)
                    if (entry is FolderNode)
                        yield return ((FolderNode)entry);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finds out if the given child is a child of this FolderNode.
        /// </summary>
        /// <param name="child">The child (FileNode or FolderNode) to look up parents for.</param>
        /// <returns>True if the given child is a child of this FolderNode. False otherwise.</returns>
        public bool HasChild(FileSystemEntry child)
        {

            foreach (FileSystemEntry parent in Children)
            {
                if (parent.Path == null && child.ParentPath == "/")
                {
                    // The children is located as a child of the rootNode
                    return true;
                }
                else if (parent.Path == child.Path)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Finds out if the given child is a child or grandchild of this FolderNode.
        /// </summary>
        /// <param name="grandChild">The child (FileNode or FolderNode) to look up parents for.</param>
        /// <returns>True if the given child is a child or grandchild of this FolderNode. False otherwise.</returns>
        public bool HasChildOrGrandchild(FileSystemEntry child)
        {
            if (HasChild(child))
            {
                return true;
            }

            // Find if the child is children to any FolderNodes found below in the tree
            foreach (FolderNode parent in Children)
            {
                if (parent.HasChildOrGrandchild(child))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageEventArgs">The server message that this node shold represent</param>
        public FolderNode(MessageEventArgs_410420 messageEventArgs)
            : base(messageEventArgs)
        {
        }

        /// <summary>
        /// Constructor - Empty
        /// </summary>
        public FolderNode()
        {
        }
        #endregion
    }
}