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
using SharpWired.Controller;

namespace SharpWired.Gui.Chat {

    /// <summary>
    /// Creates chat windows.
    /// Attaches listeners from model to chat window.
    /// Provides methods for sending to model layer.
    /// </summary>
    public class GuiChatController {
        #region Fields
        SharpWiredModel model;
        ChatControl chatControl;
        UserListControl userListControl;

        SharpWiredController controller;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="chatControl"></param>
        /// <param name="userListControl"></param>
        public GuiChatController(SharpWiredModel model, SharpWiredController controller,
            ChatControl chatControl, UserListControl userListControl) {
            this.model = model;
            this.chatControl = chatControl;
            this.userListControl = userListControl;

            this.controller = controller;

            chatControl.Init(this, 1); //Set id to 1 since this is public chat.

            model.Connected += OnLoggedIn;
        }
        #endregion

        /// <summary>
        /// Change the topic
        /// </summary>
        /// <param name="newTopic"></param>
        public void ChangeTopic(String newTopic) {
            controller.ChatController.ChangeTopic(newTopic);
        }

        /// <summary>
        /// Sends a message to the public chat
        /// </summary>
        /// <param name="message"></param>
        public void SendChatMessage(String message) {
            controller.ChatController.SendChatMessage(message);
        }

        public void OnLoggedIn(Server s) {
            model.Server.Offline += Offline;

            userListControl.Init(model);
            s.Online += OnLine;
        }

        public void OnLine() {
            model.Server.PublicChat.ChatMessageReceivedEvent += chatControl.OnChatMessageArrived;
            model.Server.PublicChat.ChatTopicChangedEvent += chatControl.OnChatTopicChanged;
        }

        public void Offline() {
            model.Server.Offline -= Offline;

            model.Server.PublicChat.ChatMessageReceivedEvent -= chatControl.OnChatMessageArrived;
            model.Server.PublicChat.ChatTopicChangedEvent -= chatControl.OnChatTopicChanged;

            // TODO: Remove dependency on model and controllers for events
            //model.ErrorController.LoginToServerFailedEvent -= chatControl.OnErrorEvent;
            //model.PrivateMessagesController.PrivateMessageModel.ReceivedPrivateMessageEvent -= chatControl.OnPrivateMessageReceived;
            //model.ConnectionManager.Messages.ClientInformationEvent -= chatControl.OnUserInformation;
        }
    }
}
