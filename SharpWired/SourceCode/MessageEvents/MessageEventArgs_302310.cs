#region Information and licence agreements
/**
 * MessageEventArgs_302310.cs 
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
using System.Net;
using System.Drawing;

namespace SharpWired.MessageEvents
{
    public class MessageEventArgs_302310 : MessageEventArgs_303331332
    {
        private bool idle;
        private bool admin;
        private int icon;
        private string nick;
        private string login;
        private IPAddress ip;
        private string host;
        private string status;
        private Bitmap image;

        public bool Idle
        {
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

        public string Login
        {
            get
            {
                return login;
            }
        }

        public IPAddress Ip
        {
            get
            {
                return ip;
            }
        }

        public string Host
        {
            get
            {
                return host;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
        }

        public Bitmap Image
        {
            get
            {
                return image;
            }
        }

        public MessageEventArgs_302310(int messageId, string messageName, int chatId, int userId,
            bool idle, bool admin, int icon, string nick, string login, IPAddress ip, string host, string status, Bitmap image)
            : base(messageId, messageName, chatId, userId)
        {
            this.idle = idle;
            this.admin = admin;
            this.icon = icon;
            this.nick = nick;
            this.login = login;
            this.ip = ip;
            this.host = host;
            this.status = status;
            this.image = image;
        }
    }
}
