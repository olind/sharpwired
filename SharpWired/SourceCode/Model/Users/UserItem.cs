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
    /// Represents one user that's online to a Wired server.
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
        private int cipherBits;
        private string cipherName;
        private string clientVersion;
        private string downloads;
        private DateTime idleTime;
        private DateTime loginTime;
        private string path;
        private int size;
        private int speed;
        private string transfer;
        private int transferred;
        private string uploads;

        private Privileges privileges;
        private Group group;
        #endregion

        #region Properties: User details

        /// <summary>
        /// Get or set if this user is admin
        /// </summary>
        public bool Admin {
            get { return admin; }
        }

        /// <summary>
        /// Get or set the cipher bits
        /// </summary>
        public int CipherBits {
            get { return cipherBits; }
        }

        /// <summary>
        /// Get or set the cipher name
        /// </summary>
        public string CipherName {
            get { return cipherName; }
        }

        /// <summary>
        /// Get or set the client version
        /// </summary>
        public string ClientVersion {
            get { return clientVersion; }
        }

        /// <summary>
        /// Get or set the downloads
        /// </summary>
        public string Downloads {
            get { return Downloads; }
        }

        /// <summary>
        /// Get or set the host for this user
        /// </summary>
        public string Host {
            get { return host; }
        }

        /// <summary>
        /// Get or set the icon for this user
        /// </summary>
        public int Icon  {
            get { return icon; }
        }

        /// <summary>
        /// Get or set the idle status for this user
        /// </summary>
        public bool Idle {
            get { return idle; }
        }

        /// <summary>
        /// Get or set the idle time
        /// </summary>
        public DateTime IdleTime {
            get { return idleTime; }
        }

        /// <summary>
        /// Get or set the image for this user
        /// </summary> 
        public Bitmap Image {
            get { return image; }
        }

        /// <summary>
        /// Get or set ip for this user
        /// </summary>
        public IPAddress Ip {
            get { return ip; }
        }

        /// <summary>
        /// Get or set the login for this user
        /// </summary>
        public string Login {
            get { return login; }
        }

        /// <summary>
        /// Get or set the login time
        /// </summary>
        public DateTime LoginTime {
            get { return loginTime; }
        }

        /// <summary>
        /// Get or set the nick for this user
        /// </summary> 
        public string Nick {
            get { return nick; }
        }

        /// <summary>
        /// Get or set the path
        /// </summary>
        public string Path {
            get { return path; }
        }

        /// <summary>
        /// Get or set the size
        /// </summary>
        public int Size {
            get { return size; }
        }

        /// <summary>
        /// Get or set the speed
        /// </summary>
        public int Speed {
            get { return speed; }
        }

        /// <summary>
        /// Get or set the status for this user
        /// </summary> 
        public string Status {
            get { return status; }
        }

        /// <summary>
        /// Get or set the current transfer
        /// </summary>
        public string Transfer {
            get { return transfer; }
        }

        /// <summary>
        /// Get or set the ammount of transferred data
        /// </summary>
        public int Transferred {
            get { return transferred; }
        }

        /// <summary>
        /// Get or set the uploads
        /// </summary>
        public string Uploads {
            get { return uploads; }
        }

        /// <summary>
        /// Get or set the user id for this user
        /// </summary>
        public int UserId {
            get { return userId; }
        }

        //TODO: We can now get the user privileges and the group privileges
        //      but instead it would be nice to be able to get the values
        //      for if a user can do the requested action or not (based on group 
        //      AND user privileges).
        //      The group privileges overrides the user privileges.

        /// <summary>
        /// Get or set the privileges for this user
        /// </summary>
        public Privileges UserPrivileges {
            get { return privileges; }
            set { privileges = value; }
        }

        /// <summary>
        /// Get or set the group for this user
        /// </summary> 
        public Group Group {
            get { return group; }
            set { group = value; }
        }
        #endregion

        #region Events
        /// <summary>
        /// Delegate for update event
        /// </summary>
        /// <param name="u">The new status</param>
        public delegate void UpdatedDelegate(UserItem u);

        /// <summary>
        /// The user information for this user was updated.
        /// </summary>
        public event UpdatedDelegate Updated;
        #endregion

        #region Methods updates user information when a message is received from the server
        /// <summary>
        /// Updates this user with the information given in the message.
        /// </summary>
        /// <param name="message"></param>
        public void OnStatusChangedMessage(MessageEventArgs_304 message) {
            if (message.UserId != this.userId)
                throw new ApplicationException("The user from the given " +
                    "message ('" + message + "') did not match the current " +
                    "user ('" + this + "')");

            this.userId = message.UserId;
            this.idle = message.Idle;
            this.admin = message.Admin;
            this.icon = message.Icon;
            this.nick = message.Nick;
            this.status = message.Status;
            if (Updated != null)
                Updated(this);
        }

        /// <summary>
        /// Call this method when the client information for this user has been updated
        /// </summary>
        /// <param name="message"></param>
        public void OnClientInformationMessage(MessageEventArgs_308 message) {
            if (message.UserId != this.userId)
                throw new ApplicationException("The user from the given " +
                    "message ('" + message + "') did not match the current " +
                    "user ('" + this + "')");

            this.admin = message.Admin;
            this.cipherBits = message.CipherBits;
            this.cipherName = message.CipherName;
            this.clientVersion = message.ClientVersion;
            this.downloads = message.Downloads;
            this.host = message.Host;
            this.icon = message.Icon;
            this.idle = message.Idle;
            this.idleTime = message.IdleTime;
            this.image = message.Image;
            this.ip = message.Ip;
            this.login = message.Login;
            this.loginTime = message.LoginTime;
            this.nick = message.Nick;
            this.path = message.Path;
            this.size = message.Size;
            this.speed = message.Speed;
            this.status = message.Status;
            this.transfer = message.Transfer;
            this.transferred = message.Transferred;
            this.uploads = message.Uploads;
            this.userId = message.UserId;
            if (Updated != null)
                Updated(this);
        }

        /// <summary>
        /// Call this method when the client image for this user has been updated
        /// </summary>
        /// <param name="message"></param>
        public void OnClientImageChangedMessage(MessageEventArgs_340 message) {
            if (message.UserId != this.userId)
                throw new ApplicationException("The user from the given " +
                    "message ('" + message + "') did not match the current " +
                    "user ('" + this + "')");

            this.image = message.Image;
            if (Updated != null)
                Updated(this);
        }

        /// <summary>
        /// Call this method when the privileges for this user has been updated
        /// </summary>
        /// <param name="message"></param>
        public void OnPrivilegesSpecificationMessage(MessageEventArgs_602 message) {
            if (message.Privileges.UserName != this.login)
                throw new ApplicationException("The login from the given " +
                    "message ('" + message + "') did not match the current " +
                    "user ('" + this + "')");

            privileges = new Privileges(message.Privileges);
        }

        /// <summary>
        /// Updates the user information with the information in the given message.
        /// </summary>
        /// <param name="message"></param>
        public void UpdateUserInformation(MessageEventArgs_302310 message) {
            if (message.UserId != this.userId)
                throw new ApplicationException("The user from the given " +
                    "message ('" + message + "') did not match the current " +
                    "user ('" + this + "')");

            SetUserInformation(message);
        }
        #endregion

        private void SetUserInformation(MessageEventArgs_302310 message) {
            this.admin = message.Admin;
            this.host = message.Host;
            this.icon = message.Icon;
            this.idle = message.Idle;
            this.image = message.Image;
            this.ip = message.Ip;
            this.login = message.Login;
            this.nick = message.Nick;
            this.status = message.Status;
            this.userId = message.UserId;
        }

        #region Initialization
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The message event arg that caused the adding of this user</param>
        public UserItem(MessageEventArgs_302310 message) {
            this.SetUserInformation(message);
        }
        #endregion
    }
}
