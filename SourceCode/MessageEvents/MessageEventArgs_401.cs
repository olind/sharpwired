#region Information and licence agreements
/*
 * MessageEventArgs_401.cs 
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
    /// MessageEventArgs for Transfer Queued (401)
    /// </summary>
    public class MessageEventArgs_401 : MessageEventArgs_Path
    {
        private int position;

        /// <summary>
        /// The position in the queue
        /// </summary>
        public int Position
        {
            get
            {
                return position;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageId">The id for this message</param>
        /// <param name="messageName">The name for this message</param>
        /// <param name="destination">The destination for thi file</param>
        /// <param name="position">The position in the queue</param>
        public MessageEventArgs_401(int messageId, string messageName, string path, int position)
            : base(messageId, messageName, path)
        {
            this.position = position;
        }
    }
}
