#region Information and licence agreements
/*
 * UserHandler.cs 
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
using System.Collections;
using SharpWired.MessageEvents;
using SharpWired.Connection;

namespace SharpWired.Model.Users
{
    /// <summary>
    /// This class represents the users connected to the chat. If this chat is the public chat, 
    /// it represents the users connected to the server, if it is a private chat it represents
    /// the users available in that chat.
    /// </summary>
    public class UserHandler : HandlerBase
    {

        #region Variables

        private UserModel userModel;

        #endregion

        #region Properties

        /// <summary>
        /// Get the user model
        /// </summary>
        public UserModel UserModel
        {
            get
            {
                return userModel;
            }
        }

        #endregion

        #region Listeners: From Connection layer

        void Messages_ClientJoinEvent(object sender, global::SharpWired.MessageEvents.MessageEventArgs_302310 messageEventArgs)
        {
            userModel.AddUser(messageEventArgs);
        }

        void Messages_ClientLeaveEvent(object sender, global::SharpWired.MessageEvents.MessageEventArgs_303331332 messageEventArgs)
        {
            userModel.RemoveUser(messageEventArgs);
        }

        void Messages_UserListEvent(object sender, global::SharpWired.MessageEvents.MessageEventArgs_302310 messageEventArgs)
        {
            userModel.AddUser(messageEventArgs);
        }

        void Messages_UserListDoneEvent(object sender, global::SharpWired.MessageEvents.MessageEventArgs_311330 messageEventArgs)
        {
            userModel.RefreshUserList(); // Tell model that the user listing is done and it's time to update
        }

        void Messages_UserSpecificationEvent(object sender, MessageEventArgs_600 messageEventArgs)
        {
            //TODO: This retreives account information and not online user information
            //      Implement when we are building the server account information handler
        }

        void Messages_PrivilegesSpecificationEvent(object sender, MessageEventArgs_602 messageEventArgs)
        {
            UserItem u = userModel.GetUser(messageEventArgs.Privileges.UserName);
            if (u != null)
            {
                u.UserPrivileges.UpdatePrivileges(messageEventArgs.Privileges);
            }
        }

        void Messages_StatusChangeEvent(object sender, MessageEventArgs_304 messageEventArgs)
        {
            userModel.StatusChanged(messageEventArgs);
        }

        void Messages_ClientInformationEvent(object sender, MessageEventArgs_308 messageEventArgs)
        {
            UserItem u = userModel.GetUser(messageEventArgs.UserId);
            u.UserId = messageEventArgs.UserId;
            u.Idle = messageEventArgs.Idle;
            u.Admin = messageEventArgs.Admin;
            u.Icon = messageEventArgs.Icon;
            u.Nick = messageEventArgs.Nick;
            u.Login = messageEventArgs.Login;
            u.Ip = messageEventArgs.Ip;
            u.Host = messageEventArgs.Host;
            
            //TODO These are not implemented in the connection layer yet
            //u.ClientInformation
            //u.CipherName
            //u.CipherBits
            //u.LoginTime
            //u.IdleTime
            //u.Downloads
            //u.Uploads
            u.Status = messageEventArgs.Status;
            u.Image = messageEventArgs.Image;
            //u.Transfer
            //u.Path
            //u.Transferred
            //u.Size
            //u.Speed
        }

        void Messages_ClientImageChangedEvent(object sender, MessageEventArgs_340 messageEventArgs)
        {
            userModel.GetUser(messageEventArgs.UserId).Image = messageEventArgs.Image;
        }


        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        public UserHandler(LogicManager logicManager) : base(logicManager)
        {
            this.userModel = new UserModel(logicManager);

            LogicManager.ConnectionManager.Messages.ClientJoinEvent += new global::SharpWired.Connection.Messages.ClientJoinEventHandler(Messages_ClientJoinEvent);
            LogicManager.ConnectionManager.Messages.ClientLeaveEvent += new global::SharpWired.Connection.Messages.ClientLeaveEventHandler(Messages_ClientLeaveEvent);
            LogicManager.ConnectionManager.Messages.UserListEvent += new global::SharpWired.Connection.Messages.UserListEventHandler(Messages_UserListEvent);
            LogicManager.ConnectionManager.Messages.UserListDoneEvent += new global::SharpWired.Connection.Messages.UserListDoneEventHandler(Messages_UserListDoneEvent);

            LogicManager.ConnectionManager.Messages.ClientImageChangedEvent += new Messages.ClientImageChangedEventHandler(Messages_ClientImageChangedEvent);
            LogicManager.ConnectionManager.Messages.ClientInformationEvent += new Messages.ClientInformationEventHandler(Messages_ClientInformationEvent);
            LogicManager.ConnectionManager.Messages.StatusChangeEvent += new Messages.StatusChangeEventHandler(Messages_StatusChangeEvent);
            
            LogicManager.ConnectionManager.Messages.UserSpecificationEvent += new Messages.UserSpecificationEventHandler(Messages_UserSpecificationEvent);
            logicManager.ConnectionManager.Messages.PrivilegesSpecificationEvent += new Messages.PrivilegesSpecificationEventHandler(Messages_PrivilegesSpecificationEvent);
        }

        #endregion
    }
}
