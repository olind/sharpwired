#region Information and licence agreements
/*
 * MessageEventArgs_304.cs 
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
    public class MessageEventArgs_304 : MessageEventArgs_201
    {
        private bool idle;
        private bool admin;
        private int icon;
        private string nick;
        private string status;

        public bool Idle {
            get
            {
                return idle;
            }
        }

        public bool Admin
        {
            get
            {
                return admin;
            }
        }

        public int Icon
        {
            get
            {
                return icon;
            }
        }

        public string Nick
        {
            get
            {
                return nick;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageName"></param>
        /// <param name="userId"></param>
        /// <param name="idle"></param>
        /// <param name="admin"></param>
        /// <param name="icon"></param>
        /// <param name="nick"></param>
        /// <param name="status"></param>
        public MessageEventArgs_304(int messageId, string messageName, int userId, bool idle, bool admin, int icon, string nick, string status)
            : base(messageId, messageName, userId)
        {
            this.idle = idle;
            this.admin = admin;
            this.icon = icon;
            this.nick = nick;
            this.status = status;
        }
    }
}
