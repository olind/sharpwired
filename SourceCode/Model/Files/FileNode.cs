#region Information and licence agreements
/*
 * FileNode.cs 
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

namespace SharpWired.Model.Files
{
    /// <summary>
    /// Representation of a "Wired File"
    /// </summary>
    public class FileNode : FileSystemEntry
    {
        #region Variables
        private long size;
        #endregion

        #region Properties

        /// <summary>
        /// Request the file size for this file.
        /// If this object is a folder 0 is returned.
        /// </summary>
        public long Size
        {
            get { return size; }
        }

        /// <summary>
        /// Returns null since a FileNode dont have any FolderNodes.
        /// </summary>
        public override IEnumerable<FolderNode> FolderNodes
        {
            get { return null; }
        }

        /// <summary>
        /// Finds out if this node has any chilrens. Since this is a FileNode it will never have any.
        /// </summary>
        /// <returns></returns>
        public override bool HasChildren()
        {
            return false;
        }

        #endregion

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="args"></param>
        public FileNode(MessageEventArgs_410420 messageEventArgs)
            : base(messageEventArgs)
        {
            this.size = messageEventArgs.Size;
        }
    }
}
