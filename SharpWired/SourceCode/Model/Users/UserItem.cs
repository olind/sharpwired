#region Information and licence agreements
/**
 * UserItem.cs 
 * Created by Ola Lindberg, 2006-10-15
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
using System.Drawing;
using System.Net;

namespace SharpWired.Model.Users
{
    /// <summary>
    /// Represents one Wired user 
    /// </summary>
    public class UserItem
    {
        #region Variables

        private bool admin;
        private string host;
        private int icon;
        private bool idle;
        private Bitmap image;
        private IPAddress ip;
        private string login;
        private string nick;
        private string status;
        private int userId;

        private Privileges privileges;
        private Group group;

        #endregion

        #region Properties: User details

        public bool Admin
        {
            get { return admin; }
            set { admin = value; }
        }

        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        public int Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public bool Idle
        {
            get { return idle; }
            set { idle = value; }
        }

        public Bitmap Image
        {
            get { return image; }
            set { 
                image = value;
                OnImageChandegEvent(image);
            }
        }

        public IPAddress Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Nick
        {
            get { return nick; }
            set {
                if (nick.CompareTo(value) != 0)
                {
                    nick = value;
                    OnNickChangedEvent(nick);
                }
            }
        }

        public string Status
        {
            get { return status; }
            set {
                if (status.CompareTo(value) != 0)
                {
                    status = value;
                    OnStatusChangedEvent(status);
                }
            }
        }

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public Privileges Privileges
        {
            get { return privileges; }
            set { privileges = value; }
        }

        public Group Group
        {
            get { return group; }
            set { group = value; }
        }

        #endregion

        #region Delegates, events and event raiser methods

        // Delegates

        public delegate void StatsChangedDelegate(object sender, string newStatus);
        public delegate void NickChangedDelegate(object sender, string newNick);
        public delegate void ImageChangedDelegate(object sender, Bitmap newImage);

        // Events
        public event StatsChangedDelegate StatusChangedEvent;
        public event NickChangedDelegate NickChangedEvent;
        public event ImageChangedDelegate ImageChangedEvent;

        // Raiser methods for events
        private void OnStatusChangedEvent(string newStatus)
        {
            if (StatusChangedEvent != null)
                StatusChangedEvent(this, newStatus);
        }

        private void OnNickChangedEvent(string newNick)
        {
            if (NickChangedEvent != null)
                NickChangedEvent(this, newNick);
        }

        private void OnImageChandegEvent(Bitmap newImage)
        {
            if (ImageChangedEvent != null)
                ImageChangedEvent(this, newImage);
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        public UserItem(MessageEventArgs_302310 messageEventArs)
        {
            this.admin = messageEventArs.Admin;
            this.host = messageEventArs.Host;
            this.icon = messageEventArs.Icon;
            this.idle = messageEventArs.Idle;
            this.image = messageEventArs.Image;
            this.ip = messageEventArs.Ip;
            this.login = messageEventArs.Login;
            this.nick = messageEventArs.Nick;
            this.status = messageEventArs.Status;
            this.userId = messageEventArs.UserId;
        }

        #endregion
    }
}
