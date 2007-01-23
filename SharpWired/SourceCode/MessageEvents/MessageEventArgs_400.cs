#region Information and licence agreements
/**
 * MessageEventArgs_400.cs 
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
    public class MessageEventArgs_400 : MessageEventArgs_Path
    {
        private int offset;
        private string hash;

        public int Offset
        {
            get
            {
                return offset;
            }
        }

        public string Hash
        {
            get
            {
                return hash;
            }
        }

        public MessageEventArgs_400(int messageId, string messageName, string path, int offset, string hash)
            : base(messageId, messageName, path)
        {
            this.offset = offset;
            this.hash = hash;
        }
    }
}
