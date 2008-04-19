#region Information and licence agreements
/*
 * GuiChatController.cs
 * Created by Ola Lindberg, 2008-03-05
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
using SharpWired.Model;

namespace SharpWired.Gui.Chat {

    /// <summary>
    /// Creates chat windows.
    /// Attaches listeners from model to chat window.
    /// Provides methods for sending to model layer.
    /// </summary>
    public class GuiChatController {
        #region Fields
        LogicManager logicManager;
        ChatControl chatControl;
        UserListControl userListControl;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logicManager"></param>
        /// <param name="chatControl"></param>
        /// <param name="userListControl"></param>
        public GuiChatController(LogicManager logicManager,
            ChatControl chatControl, UserListControl userListControl) {
            this.logicManager = logicManager;
            this.chatControl = chatControl;
            this.userListControl = userListControl;

            chatControl.Init(this, 1); //Set id to 1 since this is public chat.

            logicManager.LoggedIn += OnLoggedIn;
            logicManager.LoggedOut += OnLoggedOut;
            //This is done here since userListControl doesn't have access to logicManager
            logicManager.LoggedIn += userListControl.OnLoggedIn;
            logicManager.LoggedOut += userListControl.OnLoggedOut;
        }
        #endregion

        /// <summary>
        /// Change the topic
        /// </summary>
        /// <param name="newTopic"></param>
        public void ChangeTopic(String newTopic) {
            logicManager.ChatController.ChangeTopic(newTopic);
        }

        /// <summary>
        /// Sends a message to the public chat
        /// </summary>
        /// <param name="message"></param>
        public void SendChatMessage(String message) {
            logicManager.ChatController.SendChatMessage(message);
        }

        public void OnLoggedIn() {
            userListControl.Init(logicManager);

            //attach listeners in gui classes
            logicManager.Server.PublicChat.ChatMessageReceivedEvent += chatControl.OnChatMessageArrived;
            logicManager.Server.PublicChat.ChatTopicChangedEvent += chatControl.OnChatTopicChanged;
            logicManager.ErrorController.LoginToServerFailedEvent += chatControl.OnErrorEvent;
            logicManager.PrivateMessagesController.PrivateMessageModel.ReceivedPrivateMessageEvent += chatControl.OnPrivateMessageReceived;
            logicManager.ConnectionManager.Messages.ClientInformationEvent += chatControl.OnUserInformation;
        }

        public void OnLoggedOut() {
            logicManager.Server.PublicChat.ChatMessageReceivedEvent -= chatControl.OnChatMessageArrived;
            logicManager.Server.PublicChat.ChatTopicChangedEvent -= chatControl.OnChatTopicChanged;
            logicManager.ErrorController.LoginToServerFailedEvent -= chatControl.OnErrorEvent;
            logicManager.PrivateMessagesController.PrivateMessageModel.ReceivedPrivateMessageEvent -= chatControl.OnPrivateMessageReceived;
            logicManager.ConnectionManager.Messages.ClientInformationEvent -= chatControl.OnUserInformation;
        }
    }
}
