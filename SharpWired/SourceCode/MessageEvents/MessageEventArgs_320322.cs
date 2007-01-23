#region Information and licence agreements
/**
 * MessageEventArgs_320322.cs 
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
    public class MessageEventArgs_320322 : MessageEventArgs
    {
        private string nick;
        private DateTime postTime;
        private string post;

        public string Nick
        {
            get
            {
                return nick;
            }
        }

        public DateTime PostTime
        {
            get
            {
                return postTime;
            }
        }

        public string Post
        {
            get
            {
                return post;
            }
        }

        public MessageEventArgs_320322(int messageId, string messageName, string nick, DateTime postTime, string post)
            : base(messageId, messageName)
        {
            this.nick = nick;
            this.postTime = postTime;
            this.post = post;
        }
    }
}
