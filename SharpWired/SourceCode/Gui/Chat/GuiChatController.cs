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
        private LogicManager logicManager;

        /// <summary>
        /// Change the topic
        /// </summary>
        /// <param name="newTopic"></param>
        public void ChangeTopic(String newTopic) {
            logicManager.ChatHandler.ChangeTopic(newTopic);
        }

        /// <summary>
        /// Sends a message to the public chat
        /// </summary>
        /// <param name="message"></param>
        public void SendChatMessage(String message) {
            logicManager.ChatHandler.SendChatMessage(message);

            /* Removed quick hacks below when I rebuilt the chat. 
               Kept until the new implementation is done.

            if (message != null && message.Length > 0)
            {
                message = message.TrimStart(); //Trim all whitespaces in beginning of string
                //TODO: Make a real message handler if we want to support sending messages "the IRC-way"
                if (0 == string.Compare("/msg", 0, message, 0, 4, true))
                {
                    GuiPrivateMessageItem pmi = new GuiPrivateMessageItem(logicManager, message);
                    //logicManager.PrivateMessagesHandler.Commands.Msg(pmi.ToUser.UserId, pmi.Message);
                    logicManager.PrivateMessagesHandler.Msg(pmi.ToUser, pmi.Message);
                }
                else if (0 == string.Compare("/lag", 0, message, 0, 4, true))
                {
                    // TODO: This only works if we have been connected more than 10 sec (the 
                    // first ping must already be sent to the server. See the HeartBeatTimer).
                    // Since we probably will do a remake how this works GUI wise it's ok for now
                    if (logicManager.ConnectionManager.CurrentLag != null)
                    {
                        FormatAndWriteHTMLForCurrentLag((TimeSpan)logicManager.ConnectionManager.CurrentLag);
                    }
                    else
                    {
                        //TODO: We might want to tell the user that lag wasn't possible to measure
                    }
                }
                else
                {
                    logicManager.ChatHandler.SendChatMessage(message);
                }
            }
            */
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logicManager"></param>
        /// <param name="chatControl"></param>
        /// <param name="userListControl"></param>
        public GuiChatController(LogicManager logicManager, 
            ChatControl chatControl, UserListControl userListControl) {

            this.logicManager = logicManager;

            userListControl.Init(logicManager);
            chatControl.Init(this, 1); //Set id to 1 since this is public chat.

            //attach listeners in gui classes
            logicManager.ChatHandler.ChatModel.ChatMessageReceivedEvent += 
                new SharpWired.Model.Chat.ChatModel.ChatMessageReceivedDelegate(chatControl.OnChatMessageArrived);

            logicManager.ChatHandler.ChatModel.ChatTopicChangedEvent += 
                new SharpWired.Model.Chat.ChatModel.ChatTopicChangedDelegate(chatControl.OnChatTopicChanged);

            logicManager.ErrorHandler.LoginToServerFailedEvent += 
                new SharpWired.Model.Errors.ErrorHandler.LoginToServerFailedDelegate(chatControl.OnErrorEvent);

            logicManager.PrivateMessagesHandler.PrivateMessageModel.ReceivedPrivateMessageEvent +=
                new SharpWired.Model.PrivateMessages.PrivateMessageModel.ReceivedPrivateMessageDelegate(chatControl.OnPrivateMessageReceived);

            logicManager.ConnectionManager.Messages.ClientInformationEvent += chatControl.OnUserInformation;
        }
    }
}
