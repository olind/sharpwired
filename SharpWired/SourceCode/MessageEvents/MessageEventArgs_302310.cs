#region Information and licence agreements
/*
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
    /// <summary>
    /// MessageEventArgs for:
    /// * Client Join
    /// * User List
    /// </summary>
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

        /// <summary>
        /// Gets if this client is idle
        /// </summary>
        public bool Idle
        {
            get
            {
                return idle;
            }
        }

        /// <summary>
        /// Get if this client is admin
        /// </summary>
        public bool Admin
        {
            get
            {
                return admin;
            }
        }

        /// <summary>
        /// Get the icon for this client
        /// </summary>
        public int Icon
        {
            get
            {
                return icon;
            }
        }

        /// <summary>
        /// Get the nick for this client
        /// </summary>
        public string Nick
        {
            get
            {
                return nick;
            }
        }

        /// <summary>
        /// Get the login for this client
        /// </summary>
        public string Login
        {
            get
            {
                return login;
            }
        }

        /// <summary>
        /// Get the ip for this client
        /// </summary>
        public IPAddress Ip
        {
            get
            {
                return ip;
            }
        }

        /// <summary>
        /// Get the host for this client
        /// </summary>
        public string Host
        {
            get
            {
                return host;
            }
        }

        /// <summary>
        /// Get the status for this client
        /// </summary>
        public string Status
        {
            get
            {
                return status;
            }
        }

        /// <summary>
        /// Get the image for this client
        /// </summary>
        public Bitmap Image
        {
            get
            {
                return image;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageId">The id for this message</param>
        /// <param name="messageName">The name for this message</param>
        /// <param name="chatId">The chat id to where this user joined</param>
        /// <param name="userId">The user id for this user</param>
        /// <param name="idle">Is this user idle?</param>
        /// <param name="admin">Is this user admin?</param>
        /// <param name="icon">The icon for this user</param>
        /// <param name="nick">The nick for this user</param>
        /// <param name="login">The login for this user</param>
        /// <param name="ip">The ip foro this user</param>
        /// <param name="host">The host for this user</param>
        /// <param name="status">The status for this user</param>
        /// <param name="image">The image for this user</param>
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
