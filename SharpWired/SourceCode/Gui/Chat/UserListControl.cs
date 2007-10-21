#region Information and licence agreements
/*
 * UserListControl.cs
 * Created by Ola Lindberg, 2006-11-20
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

namespace SharpWired.Gui.Chat
{
    /// <summary>
    /// The gui class for the user list
    /// </summary>
    public partial class UserListControl : UserControl
    {
        #region Variables

        private LogicManager logicManager;

        #endregion

        #region Logic for UserListControl

        void RemoveUser(object sender, UserItem removeUser)
        {
            if (this.InvokeRequired)
            {
                RemoveUserCallback ucb = new RemoveUserCallback(RemoveUser);
                this.Invoke(ucb, new object[] { sender, removeUser });
            }
            else
            {
                // TODO Remove the user instead of redrawing the list
                this.RedrawUserList(null, logicManager.UserHandler.UserModel.UserList);
            }
        }
        delegate void RemoveUserCallback(object sender, UserItem removeUser);

        void AddUser(object sender, UserItem newUser)
        {
            if (this.InvokeRequired)
            {
                AddUserCallback ucb = new AddUserCallback(AddUser);
                this.Invoke(ucb, new object[] { sender, newUser });
            }
            else
            {
                UserItemControl newUserItemControl = new UserItemControl();
                newUserItemControl.Init(newUser, logicManager);

                userListFlowLayoutPanel.Controls.Add(newUserItemControl);
            }
        }
        delegate void AddUserCallback(object sender, UserItem newUser);

        /// <summary>
        /// Draw or redraw the user list.
        /// </summary>
        void RedrawUserList(object sender, List<UserItem> userList)
        {
            if (this.InvokeRequired)
            {
                RedrawUserListCallback ucb = new RedrawUserListCallback(RedrawUserList);
                this.Invoke(ucb, new object[] { sender, userList });
            }
            else
            {
                this.userListFlowLayoutPanel.Controls.Clear();
                this.userListFlowLayoutPanel.Refresh();
                
                foreach (UserItem u in userList)
                {
                    AddUser(sender, u);
                }
            }
        }
        delegate void RedrawUserListCallback(object sender, List<UserItem> userList);

        #endregion

        #region Listener methods: From model

        void Users_ClientLeftEvent(object sender, UserItem leftUser)
        {
            this.RemoveUser(sender, leftUser);
        }

        void Users_ClientJoinEvent(object sender, UserItem newUser)
        {
            this.AddUser(sender, newUser);
        }

        void Users_UserListUpdatedEvent(object sender, List<UserItem> userList)
        {
            this.RedrawUserList(sender, userList);
        }

        #endregion

        #region Initialization of UserListControl

        /// <summary>
        /// Init this component, set up the listeners etc
        /// </summary>
        /// <param name="logicManager"></param>
        public void Init(LogicManager logicManager)
        {
            this.logicManager = logicManager;
            logicManager.UserHandler.UserModel.UserListUpdatedEvent += new UserModel.UserListUpdatedDelegate(Users_UserListUpdatedEvent);

            logicManager.UserHandler.UserModel.ClientJoinEvent += new UserModel.ClientJoinDelegate(Users_ClientJoinEvent);
            logicManager.UserHandler.UserModel.ClientLeftEvent += new UserModel.ClientLeftDelegate(Users_ClientLeftEvent);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public UserListControl()
        {
            InitializeComponent();
        }

        #endregion
    }
}
