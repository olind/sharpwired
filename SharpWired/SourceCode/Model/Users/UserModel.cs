#region Information and licence agreements
/**
 * UserModel.cs 
 * Created by Ola Lindberg, 2006-12-03
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

namespace SharpWired.Model.Users
{
    public class UserModel
    {
        private LogicManager logicManager;
        private List<UserItem> userList;

        #region Properties

        /// <summary>
        /// Get the list of users connected to this list of users.
        /// </summary>
        public List<UserItem> UserList
        {
            get
            {
                return userList;
            }
        }

        #endregion

        #region Methods: User list manipulation

        /// <summary>
        /// Add the given user to this list of users
        /// </summary>
        /// <param name="u"></param>
        public void AddUser(MessageEventArgs_302310 messageEventArgs)
        {
            if (!UserExists(messageEventArgs.UserId))
            {
                UserItem newUser = new UserItem(messageEventArgs);
                this.userList.Add(newUser);
                this.OnClientJoinEvent(newUser);
            }
        }

        /// <summary>
        /// Remove the given user from this list of users
        /// </summary>
        /// <param name="u"></param>
        public void RemoveUser(MessageEventArgs_303331332 messageEventArgs)
        {
            if (UserExists(messageEventArgs.UserId))
            {
                UserItem leftUser = GetUser(messageEventArgs.UserId);
                this.userList.Remove(leftUser);
                this.OnClientLeavEvent(leftUser);
            }
        }

        /// <summary>
        /// Refresh the user list.
        /// </summary>
        public void RefreshUserList()
        {
            OnUserListUpdatedEvent();
        }


        public void StatusChanged(MessageEventArgs_304 messageEventArgs)
        {
            UserItem u = GetUser(messageEventArgs.UserId);

            u.UserId = messageEventArgs.UserId;
            u.Idle = messageEventArgs.Idle;
            u.Admin = messageEventArgs.Admin;
            u.Icon = messageEventArgs.Icon;
            u.Nick = messageEventArgs.Nick;
            u.Status = messageEventArgs.Status;
        }

        #endregion


        /// <summary>
        /// Gets the user with the given user name
        /// </summary>
        /// <param name="userId">The UserId for the user</param>
        /// <returns>The user with the given user name, null if no user is found</returns>
        public UserItem GetUser(int userId)
        {
            foreach (UserItem u in userList)
            {
                if (userId == u.UserId)
                    return u;
            }
            return null;
        }

        /// <summary>
        /// Finds out if the user with the given UserId exists
        /// </summary>
        /// <param name="userId">The UserId for the user</param>
        /// <returns>True if the user exists, false otherwise</returns>
        private bool UserExists(int userId)
        {
            bool userExists = false;
            foreach (UserItem user in userList)
            {
                if (userId == user.UserId)
                    userExists = true;
            }
            return userExists;
        }

        #region Delegates, Events and Raisers for the events

        // Delegates
        public delegate void ClientJoinDelegate(object sender, UserItem newUser);
        public delegate void ClientLeftDelegate(object sender, UserItem leftUser);
        public delegate void UserListUpdatedDelegate(object sender, List<UserItem> userList);

        // Events
        public event ClientJoinDelegate ClientJoinEvent;
        public event ClientLeftDelegate ClientLeftEvent;
        public event UserListUpdatedDelegate UserListUpdatedEvent;

        // Raiser methods for events
        private void OnClientJoinEvent(UserItem newUser)
        {
            if (ClientJoinEvent != null)
                ClientJoinEvent(this, newUser);
        }

        private void OnClientLeavEvent(UserItem leftUser)
        {
            if (ClientLeftEvent != null)
                ClientLeftEvent(this, leftUser);
        }

        private void OnUserListUpdatedEvent()
        {
            if (UserListUpdatedEvent != null)
                UserListUpdatedEvent(this, userList);
        }
        
        #endregion 

        #region Initialization

        public UserModel(LogicManager logicManager)
        {
            this.logicManager = logicManager;
            this.userList = new List<UserItem>();
        }

        #endregion
    }
}