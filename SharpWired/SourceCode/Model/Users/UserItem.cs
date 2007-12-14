#region Information and licence agreements
/*
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

        private Privileges userPrivileges;
        private Group group;

        #endregion

        #region Properties: User details

        /// <summary>
        /// Get or set if this user is admin
        /// </summary>
        public bool Admin
        {
            get { return admin; }
            set { admin = value; }
        }

        /// <summary>
        /// Get or set the host for this user
        /// </summary>
        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        /// <summary>
        /// Get or set the icon for this user
        /// </summary>
        public int Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        /// <summary>
        /// Get or set the idle status for this user
        /// </summary>
        public bool Idle
        {
            get { return idle; }
            set { idle = value; }
        }

        /// <summary>
        /// Get or set the image for this user
        /// </summary> 
        public Bitmap Image
        {
            get { return image; }
            set { 
                image = value;
                OnImageChandegEvent(image);
            }
        }

        /// <summary>
        /// Get or set ip for this user
        /// </summary>
        public IPAddress Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        /// <summary>
        /// Get or set the login for this user
        /// </summary>
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        /// <summary>
        /// Get or set the nick for this user
        /// </summary> 
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

        /// <summary>
        /// Get or set the status for this user
        /// </summary> 
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

        /// <summary>
        /// Get or set the user id for this user
        /// </summary>
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        //TODO: We can now get the user privileges and the group privileges
        //      but instead it would be nice to be able to get the values
        //      for if a user can do the requested action or not (based on group 
        //      AND user privileges).
        //      The group privileges overrides the user privileges.

        /// <summary>
        /// Get or set the privileges for this user
        /// </summary>
        public Privileges UserPrivileges
        {
            get { return userPrivileges; }
            set { userPrivileges = value; }
        }

        /// <summary>
        /// Get or set the group for this user
        /// </summary> 
        public Group Group
        {
            get { return group; }
            set { group = value; }
        }

        #endregion

        #region Delegates, events and event raiser methods

        // Delegates

        /// <summary>
        /// Delegate for StatusChangedEvent
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="newStatus">The new status</param>
        public delegate void StatsChangedDelegate(object sender, string newStatus);
        /// <summary>
        /// Delegate for NickChangedEvent
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="newNick">The new nick</param>
        public delegate void NickChangedDelegate(object sender, string newNick);
        /// <summary>
        /// Delegate for ImageChangedEvent
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="newImage">The new image</param>
        public delegate void ImageChangedDelegate(object sender, Bitmap newImage);

        // Events
        /// <summary>
        /// The event for status change
        /// </summary>
        public event StatsChangedDelegate StatusChangedEvent;
        /// <summary>
        /// The event for nick changed
        /// </summary>
        public event NickChangedDelegate NickChangedEvent;
        /// <summary>
        /// The event for image changed
        /// </summary>
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
