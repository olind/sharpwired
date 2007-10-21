#region Information and licence agreements
/*
 * UserItemControl.cs
 * Created by Ola Lindberg, 2006-10-12
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using SharpWired.MessageEvents;
using SharpWired.Model.Users;
using SharpWired.Model;

namespace SharpWired.Gui.Chat
{
    /// <summary>
    /// GUI that represents one user
    /// </summary>
    public partial class UserItemControl : UserControl
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

        private LogicManager logicManager;

        #endregion

        #region Properties

        /// <summary>
        /// Set if this user is admin
        /// </summary>
        public bool Admin
        {
            set { admin = value; }
        }

        /// <summary>
        /// Set the hostname for this user
        /// </summary>
        public string Host
        {
            set { host = value; }
        }

        /// <summary>
        /// Set the icon for this user
        /// </summary>
        public int Icon
        {
            set { icon = value; }
        }

        /// <summary>
        /// Set if this user is idle
        /// </summary>
        public bool Idle
        {
            set { idle = value; }
        }

        /// <summary>
        /// Set the image for this user
        /// </summary>
        public Bitmap Image
        {
            set { 
                image = value;
                //TODO: Set to some default image if null, or set it to the icon instead
                UpdatePictureBox(image, imagePictureBox);
            }
        }

        /// <summary>
        /// Set the ip address for this user
        /// </summary>
        public IPAddress Ip
        {
            set { ip = value; }
        }

        /// <summary>
        /// Set the login for this user
        /// </summary>
        public string Login
        {
            set { login = value; }
        }

        /// <summary>
        /// Set the nick for this user
        /// </summary>
        public string Nick
        {
            set { 
                nick = value;
                //this.nickLabel.Text = nick;
                UpdateTextLabel(nick, this.nickLabel);
            }
        }

        /// <summary>
        /// Set the status for this user
        /// </summary>
        public string Status
        {
            set { 
                status = value;
                UpdateTextLabel(status, this.statusLabel);
            }
        }

        /// <summary>
        /// Set the user id for this user
        /// </summary>
        public int UserId
        {
            set { userId = value; }
        }

        #endregion

        #region Private methods: Update GUI

        /// <summary>
        /// Updates the text label provided. This handles the callbacks needed to write to the GUI
        /// </summary>
        /// <param name="updatedString">The string to write to gui</param>
        /// <param name="labelToUpdate">The label to where the string should be written</param>
        private void UpdateTextLabel(string updatedString, Label labelToUpdate)
        {
            if(labelToUpdate.InvokeRequired){
                UpdateTextLabelCallback updateTextLabelCallback = new UpdateTextLabelCallback(UpdateTextLabel);
                this.Invoke(updateTextLabelCallback, new object[] { updatedString, labelToUpdate });
            } else {
                labelToUpdate.Text = updatedString;
            }
        }
        delegate void UpdateTextLabelCallback(string updatedString, Label labelToUpdate);

        /// <summary>
        /// Updates the image box label provided. This handles the callbacks needed to write to the GUI
        /// </summary>
		/// <param name="updatedImage">TODO!</param>
		/// <param name="pictureBoxToUpdate">TODO!</param>
        private void UpdatePictureBox(Bitmap updatedImage, PictureBox pictureBoxToUpdate)
        {
            if (pictureBoxToUpdate.InvokeRequired)
            {
                UpdatePictureBoxCallback updatePictureBoxCallback = new UpdatePictureBoxCallback(UpdatePictureBox);
                this.Invoke(updatePictureBoxCallback, new object[] { updatedImage, pictureBoxToUpdate });
            }
            else
            {
                pictureBoxToUpdate.BackgroundImage = updatedImage;
            }
        }
        delegate void UpdatePictureBoxCallback(Bitmap updatedImage, PictureBox pictureBoxToUpdate);

        #endregion

        #region listeners from UserItem

        void newUser_StatusChangedEvent(object sender, string newStatus)
        {
            this.Status = newStatus;
        }

        void newUser_NickChangedEvent(object sender, string newNick)
        {
            this.Nick = newNick;
        }

        void newUser_ImageChangedEvent(object sender, Bitmap newImage)
        {
            this.Image = newImage;
        }

        #endregion

        #region Listeners: From GUI

        private void UserItemControl_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Initialization
        /// <summary>
        /// Init this control
        /// </summary>
        /// <param name="newUser">The new user</param>
        /// <param name="logicManager">The logic manager for this connection</param>
        public void Init(UserItem newUser, LogicManager logicManager)
        {
            this.Admin = newUser.Admin;
            this.Host = newUser.Host;
            this.Icon = newUser.Icon;
            this.Idle = newUser.Idle;
            this.Image = newUser.Image;
            this.Ip = newUser.Ip;
            this.Login = newUser.Login;
            this.Nick = newUser.Nick;
            this.Status = newUser.Status;
            this.UserId = newUser.UserId;

            this.logicManager = logicManager;
            newUser.StatusChangedEvent += new UserItem.StatsChangedDelegate(newUser_StatusChangedEvent);
            newUser.NickChangedEvent += new UserItem.NickChangedDelegate(newUser_NickChangedEvent);
            newUser.ImageChangedEvent += new UserItem.ImageChangedDelegate(newUser_ImageChangedEvent);

        }

        /// <summary>
        /// Constructor
        /// </summary>
        public UserItemControl()
        {
            InitializeComponent();
        }

        #endregion
    }
}
