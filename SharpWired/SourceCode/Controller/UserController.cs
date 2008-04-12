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
using SharpWired.Model.Users;

namespace SharpWired.Controller
{
    /// <summary>
    /// This class represents the users connected to the chat. If this chat is the public chat, 
    /// it represents the users connected to the server, if it is a private chat it represents
    /// the users available in that chat.
    /// </summary>
    public class UserController : ControllerBase
    {

        #region Variables
        private UserModel userModel;
        #endregion

        #region Properties
        /// <summary>
        /// Get the user model
        /// </summary>
        public UserModel UserModel {
            get { return userModel; }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Constructor
        /// </summary>
        public UserController(LogicManager logicManager) : base(logicManager) {
            this.userModel = new UserModel();
            Messages m = logicManager.ConnectionManager.Messages;

            //Attach the events with the model listener methods
            m.StatusChangeEvent += userModel.OnStatusChangedMessage;
            m.ClientImageChangedEvent += userModel.OnClientImageChangedMessage;
            m.ClientInformationEvent += userModel.OnClientInformationMessage;
            m.ClientJoinEvent += userModel.OnClientJoinMessage;
            m.UserListEvent += userModel.OnUserListMessage;
            m.ClientLeaveEvent += userModel.OnClientLeaveMessage;
            m.ClientKickedEvent += userModel.OnClientKickedMessage;
            m.ClientBannedEvent += userModel.OnClientBannedMessage;
            m.PrivilegesSpecificationEvent += userModel.OnPrivilegesSpecificationMessage;
        }

        #endregion
    }
}
