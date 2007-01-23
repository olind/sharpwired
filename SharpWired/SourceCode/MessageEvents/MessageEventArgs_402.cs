#region Information and licence agreements
/**
 * MessageEventArgs_402.cs 
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
    public class MessageEventArgs_402 : MessageEventArgs_410420
    {
        private string checksum;
        private string comment;

        public string Checksum
        {
            get
            {
                return checksum;
            }
        }

        public string Comment
        {
            get { 
                return comment; 
            }
        }

        /// <summary>
        /// Constructon
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageName"></param>
        /// <param name="path"></param>
        /// <param name="fileType"></param>
        /// <param name="size"></param>
        /// <param name="created"></param>
        /// <param name="modified"></param>
        /// <param name="checksum"></param>
        /// <param name="comment"></param>
        public MessageEventArgs_402(int messageId, string messageName, string path,
            string fileType, int size, DateTime created, DateTime modified, string checksum, string comment)
            : base(messageId, messageName, path, fileType, size, created, modified)
        {
            this.checksum = checksum;
            this.comment = comment;
        }
    }
}
