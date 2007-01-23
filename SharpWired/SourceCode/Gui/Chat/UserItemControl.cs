#region Information and licence agreements
/**
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

        public bool Admin
        {
            set { admin = value; }
        }

        public string Host
        {
            set { host = value; }
        }

        public int Icon
        {
            set { icon = value; }
        }

        public bool Idle
        {
            set { idle = value; }
        }

        public Bitmap Image
        {
            set { 
                image = value;
                //TODO: Set to some default image if null, or set it to the icon instead
                UpdatePictureBox(image, imagePictureBox);
            }
        }

        public IPAddress Ip
        {
            set { ip = value; }
        }

        public string Login
        {
            set { login = value; }
        }

        public string Nick
        {
            set { 
                nick = value;
                //this.nickLabel.Text = nick;
                UpdateTextLabel(nick, this.nickLabel);
            }
        }

        public string Status
        {
            set { 
                status = value;
                UpdateTextLabel(status, this.statusLabel);
            }
        }

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
        /// <param name="updatedString">The string to write to gui</param>
        /// <param name="labelToUpdate">The label to where the string should be written</param>
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
