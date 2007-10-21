#region Information and licence agreements
/*
 * MessageEventArgs_410420.cs 
 * Created by Ola Lindberg, 2006-09-28
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

namespace SharpWired.MessageEvents
{
    /// <summary>
    /// MessageEventArgs for:
    /// * File Listing
    /// * Search Listing
    /// This event is sent for each file. The *ListingDone events shows 
    /// when all requested files are sent from the server.
    /// </summary>
    public class MessageEventArgs_410420 : MessageEventArgs_Path
    {
        private string fileType; // TODO: This should be of other type
        private long size;
        private DateTime created;
        private DateTime modified;

        /// <summary>
        /// Get the file type for this event
        /// </summary>
        public string FileType
        {
            get
            {
                return fileType;
            }
        }

        /// <summary>
        /// The size for this file. 
        /// If this file is a folder this represents the number of items in this folder.
        /// </summary>
        public long Size
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// The date when this file was created
        /// </summary>
        public DateTime Created
        {
            get
            {
                return created;
            }
        }

        /// <summary>
        /// The date when this file was modified
        /// </summary>
        public DateTime Modified
        {
            get
            {
                return modified;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageName"></param>
        /// <param name="path"></param>
        /// <param name="fileType">TODO: This should be of other type</param>
        /// <param name="size"></param>
        /// <param name="created"></param>
        /// <param name="modified"></param>
        public MessageEventArgs_410420(int messageId, string messageName, string path, 
            string fileType, long size, DateTime created, DateTime modified)
            : base(messageId, messageName, path)
        {
            this.fileType = fileType;
            this.size = size;
            this.created = created;
            this.modified = modified;
        }
    }
}
