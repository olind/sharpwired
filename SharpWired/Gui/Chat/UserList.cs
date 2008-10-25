#region Information and licence agreements
/*
 * UserListControl.cs
 * Created by Ola Lindberg, 2006-11-20
 * Refactored by Ola and Adam, 2008-03-07
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
using SharpWired.Model;
using System.Collections;
using SharpWired.Model.Users;
using SharpWired.Controller;

namespace SharpWired.Gui.Chat {
    /// <summary>
    /// The gui class for the user list
    /// </summary>
    public partial class UserList : SharpWiredGuiBase {

        #region Fields
        SharpWired.Model.Users.UserList userList;
        #endregion

        #region Events
        delegate void RemoveUserCallback(User removeUser);
        delegate void AddUserCallback(User newUser);
        delegate void RedrawUserListCallback(List<User> userList);
        #endregion

        #region Event listeners
        void AddUser(User user) {
            if (this.InvokeRequired) {
                AddUserCallback ucb = new AddUserCallback(AddUser);
                this.Invoke(ucb, new object[] { user });
            } else {
                ListView.ListViewItemCollection items = this.userListView.Items;
                if (!items.ContainsKey(user.UserId.ToString())) {
                    user.Updated += UpdateUser;

                    if (user.Image != null)
                        userListView.LargeImageList.Images.Add(user.UserId.ToString(), user.Image);

                    WiredListViewItem item = new WiredListViewItem(user,
                        new string[] { user.Nick, user.Status }, user.UserId.ToString());
                    items.Add(item);
                }
            }
        }

        void UpdateUser(User user) {
            if (this.InvokeRequired) {
                AddUserCallback ucb = new AddUserCallback(UpdateUser);
                this.Invoke(ucb, new object[] { user });
            } else {
                WiredListViewItem u = FindUserById(user);
                if (u != null) {
                    if (user.Image != null)
                        userListView.LargeImageList.Images.Add(user.UserId.ToString(), user.Image);

                    u.Text = user.Nick;
                    u.SubItems[1].Text = user.Status;
                }
            }
        }

        void RemoveUser(User user) {
            if (this.InvokeRequired) {
                AddUserCallback ucb = new AddUserCallback(RemoveUser);
                this.Invoke(ucb, new object[] { user });
            } else {
                user.Updated -= UpdateUser;
                WiredListViewItem u = FindUserById(user);
                if (u != null)
                    this.userListView.Items.Remove(u);
            }
        }

        private WiredListViewItem FindUserById(User user) {
            ListView.ListViewItemCollection items = this.userListView.Items;
            WiredListViewItem u = null;
            foreach (WiredListViewItem wli in items)
                if (wli.UserItem.UserId == user.UserId)
                    u = wli;

            return u;
        }

        protected override void OnOnline() {
            this.userList = Model.Server.PublicChat.Users;

            userList.ClientJoined += AddUser;
            userList.ClientLeft += RemoveUser;
        }

        protected override void OnOffline() {
            userList.ClientJoined -= AddUser;
            userList.ClientLeft -= RemoveUser;
            userListView.Clear();
        }
        #endregion

        #region Initialization of UserListControl
        /// <summary>
        /// Constructor
        /// </summary>
        public UserList() {
            InitializeComponent();
            this.userListView.LargeImageList = new ImageList();
            this.userListView.LargeImageList.ImageSize = new Size(32, 32);
            this.userListView.ContextMenu = new ContextMenu();

            Size s = new Size(130, 34);
            this.userListView.TileSize = s;
        }

        #endregion

        private void OnClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                ContextMenu cm = this.userListView.ContextMenu;
                cm.MenuItems.Clear();
                ListView.SelectedListViewItemCollection users = this.userListView.SelectedItems;
                if (users.Count > 0) {
                    cm.MenuItems.Add("Information", OnInformationClick);
                    cm.MenuItems.Add("-");
                    cm.MenuItems.Add("Private Chat").Enabled = false;
                    cm.MenuItems.Add("Private Message").Enabled = false;
                    cm.MenuItems.Add("-");
                    cm.MenuItems.Add("Kick").Enabled = false;
                    cm.MenuItems.Add("Ban").Enabled = false;
                    cm.MenuItems.Add("Ignore").Enabled = false;
                    cm.MenuItems.Add("-");
                }
                cm.MenuItems.Add("Select All").Enabled = false;
                cm.MenuItems.Add("Broadcast").Enabled = false;
            }
        }

        private void OnInformationClick(object sender, EventArgs e) {
            ListView.SelectedListViewItemCollection userItems = this.userListView.SelectedItems;
            foreach (WiredListViewItem li in userItems) {
                Controller.UserController.GetUserInfo(li.UserItem);
            }
        }
    }
}
