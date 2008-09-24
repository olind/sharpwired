#region Information and licence agreements
/*
 * MessageEventArgs_308.cs 
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
using System.Drawing;
using System.Net;

namespace SharpWired.MessageEvents
{
    /// <summary>
    /// MessageEventArgs for Client Information (308)
    /// </summary>
    public class MessageEventArgs_308 : MessageEventArgs_340
    {
        private bool idle;
        private bool admin;
        private int icon;
        private string nick;
        private string login;
        private string status;
        private IPAddress ip;
        private string host;
        private string clientVersion; // TODO: This should be of type AppVersion
        private string cipherName;
        private int cipherBits;
        private DateTime loginTime;
        private DateTime idleTime;
        private string downloads; // TODO: This should be of type TransferGS
        private string uploads; // TODO: This should be of type TransferGS
        private string transfer; // TODO: This should be of type PathRS
        private string path;
        private int transferred;
        private int size;
        private int speed;

        /// <summary>
        /// Request if this user is idle or not
        /// </summary>
        public bool Idle {
            get { return idle; }
        }

        /// <summary>
        /// Request if this user is admin
        /// </summary>
        public bool Admin {
            get { return admin; }
        }

        /// <summary>
        /// Request the icon for this user
        /// </summary>
        public int Icon {
            get { return icon; }
        }

        /// <summary>
        /// Request the nick for this user
        /// </summary>
        public string Nick {
            get { return nick; }
        }

        /// <summary>
        /// Request the login for this user
        /// </summary>
        public string Login {
            get { return login; }
        }

        /// <summary>
        /// Request the status for this user
        /// </summary>
        public string Status {
            get { return status; }
        }

        /// <summary>
        /// Request the IP Address for this user
        /// </summary>
        public IPAddress Ip {
            get { return ip; }
        }

        /// <summary>
        /// Request the host for this user
        /// </summary>
        public string Host {
            get { return host; }
        }

        /// <summary>
        /// Request the client version for this user
        /// </summary>
        public string ClientVersion {
            get { return clientVersion; }
        }

        /// <summary>
        /// Request the cipher name for this user
        /// </summary>
        public string CipherName {
            get { return cipherName; }
        }

        /// <summary>
        /// Request the cipher bits for this user
        /// </summary>
        public int CipherBits {
            get { return cipherBits; }
        }

        /// <summary>
        /// Request the login time for this user
        /// </summary>
        public DateTime LoginTime {
            get { return loginTime; }
        }

        /// <summary>
        /// Request the idle time for this user
        /// </summary>
        public DateTime IdleTime {
            get { return idleTime; }
        }

        /// <summary>
        /// Request the current download for this user
        /// </summary>
        public string Downloads {
            get { return downloads; }
        }

        /// <summary>
        /// Request the current upload for this user
        /// </summary>
        public string Uploads {
            get { return uploads; }
        }

        /// <summary>
        /// Request the transfer this user currently have
        /// </summary>
        public string Transfer {
            get { return transfer; }
        }

        /// <summary>
        /// Request the destination (destination transferred size speed)
        /// </summary>
        public string Path {
            get { return path; }
        }

        /// <summary>
        /// Request the ammount of transferred data
        /// </summary>
        public int Transferred {
            get { return transferred; }
        }

        /// <summary>
        /// Request the size for the current download?
        /// </summary>
        public int Size {
            get { return size; }
        }

        /// <summary>
        /// Request the speed for the current download?
        /// </summary>
        public int Speed {
            get { return speed; }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageName"></param>
        /// <param name="userId"></param>
        /// <param name="image"></param>
        /// <param name="idle"></param>
        /// <param name="admin"></param>
        /// <param name="icon"></param>
        /// <param name="nick"></param>
        /// <param name="login"></param>
        /// <param name="status"></param>
        /// <param name="ip"></param>
        /// <param name="host"></param>
        /// <param name="clientVersion">TODO: This should be of type AppVersion</param>
        /// <param name="cipherName"></param>
        /// <param name="cipherBits"></param>
        /// <param name="loginTime"></param>
        /// <param name="idleTime"></param>
        /// <param name="downloads">TODO: This should be of type TransferGS</param>
        /// <param name="uploads">TODO: This should be of type TransferGS</param>
        /// <param name="transfer">TODO: This should be of type PathRS</param>
        /// <param name="destination"></param>
        /// <param name="transferred"></param>
        /// <param name="size"></param>
        /// <param name="speed"></param>
        public MessageEventArgs_308(int messageId, string messageName, int userId,
            Bitmap image, bool idle, bool admin, int icon, string nick, string login, string status,
            IPAddress ip, string host, string clientVersion, string cipherName, int cipherBits,
            DateTime loginTime, DateTime idleTime, string downloads, string uploads,
            string transfer, string path, int transferred, int size, int speed)
            : base(messageId, messageName, userId, image)
        {
            this.idle = idle;
            this.admin = admin;
            this.icon = icon;
            this.nick = nick;
            this.login = login;
            this.status = status;
            this.ip = ip;
            this.host = host;
            this.clientVersion = clientVersion;
            this.cipherName = cipherName;
            this.cipherBits = cipherBits;
            this.loginTime = loginTime;
            this.idleTime = idleTime;
            this.downloads = downloads;
            this.uploads = uploads;
            this.transfer = transfer;
            this.path = path;
            this.transferred = transferred;
            this.size = size;
            this.speed = speed;
        }
    }
}
