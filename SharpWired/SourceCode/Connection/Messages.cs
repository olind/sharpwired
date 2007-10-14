/**
 * Messages.cs 
 * Created by Ola Lindberg, 2006-06-29
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

using System;
using System.Collections.Generic;
using System.Text;
using SharpWired;
using SharpWired.Connection;
using System.Net;
using System.Drawing;
using SharpWired.MessageEvents;
using SharpWired.Connection.Sockets;
using SharpWired.Model.Users;

namespace SharpWired.Connection
{
    /// <summary>
    /// Handles all the messages in the Wired 1.1 protocol and error messages from the network.
    /// See http://www.zankasoftware.com/wired/ for more information regarding the Wired protocol.
    ///
    /// Authors:	Ola Lindberg (d02ola@ituniv.se)
    ///				Peter Thorin (it3thpe@ituniv.se)
    /// 
    /// NOTE: This class has derived from the Socio Project. See http://socio.sf.net/
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// The socket for this connection
        /// </summary>
        private SecureSocket socket;
        /// <summary>
        /// TODO: This delegate is obsolete and should be used for debuging only.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="message"></param>
        public delegate void MessageReceived(object sender, EventArgs e, string message);

        ///
        /// All server messages follows
        ///

        /// 200
        public delegate void ServerInformationEventHandler(object sender, MessageEventArgs_200 messageEventArgs);
        /// 201
        public delegate void LoginSucceededEventHandler(object sender, MessageEventArgs_201 messageEventArgs);
        /// 202
        public delegate void PingReplyEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 203
        public delegate void ServerBannerEventHandler(object sender, MessageEventArgs_203 messageEventArgs);
        /// 300
        public delegate void ChatEventHandler(object sender, MessageEventArgs_300301 messageEventArgs);
        /// 301
        public delegate void ActionChatEventHandler(object sender, MessageEventArgs_300301 messageEventArgs);
        /// 302
        public delegate void ClientJoinEventHandler(object sender, MessageEventArgs_302310 messageEventArgs);
        /// 303
        public delegate void ClientLeaveEventHandler(object sender, MessageEventArgs_303331332 messageEventArgs);
        /// 304
        public delegate void StatusChangeEventHandler(object sender, MessageEventArgs_304 messageEventArgs);
        /// 305
        public delegate void PrivateMessageEventHandler(object sender, MessageEventArgs_305309 messageEventArgs);
        /// 306
        public delegate void ClientKickedEventHandler(object sender, MessageEventArgs_306307 messageEventArgs);
        /// 307
        public delegate void ClientBannedEventHandler(object sender, MessageEventArgs_306307 messageEventArgs);
        /// 308
        public delegate void ClientInformationEventHandler(object sender, MessageEventArgs_308 messageEventArgs);
        /// 309
        public delegate void BroadcastMessageEventHandler(object sender, MessageEventArgs_305309 messageEventArgs);
        /// 310
        public delegate void UserListEventHandler(object sender, MessageEventArgs_302310 messageEventArgs);
        /// 311
        public delegate void UserListDoneEventHandler(object sender, MessageEventArgs_311330 messageEventArgs);
        /// 320
        public delegate void NewsEventHandler(object sender, MessageEventArgs_320322 messageEventArgs);
        /// 321
        public delegate void NewsDoneEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 322
        public delegate void NewsPostedEventHandler(object sender, MessageEventArgs_320322 messageEventArgs);
        /// 330
        public delegate void PrivateChatCreatedEventHandler(object sender, MessageEventArgs_311330 messageEventArgs);
        /// 331e
        public delegate void PrivateChatInvitationEventHandler(object sender, MessageEventArgs_303331332 messageEventArgs);
        /// 332
        public delegate void PrivateChatDeclinedEventHandler(object sender, MessageEventArgs_303331332 messageEventArgs);
        /// 340
        public delegate void ClientImageChangedEventHandler(object sender, MessageEventArgs_340 messageEventArgs);
        /// 341
        public delegate void ChatTopicEventHandler(object sender, MessageEventArgs_341 messageEventArgs);
        /// 400
        public delegate void TransferReadyEventHandler(object sender, MessageEventArgs_400 messageEventArgs);
        /// 401
        public delegate void TransferQueuedEventHandler(object sender, MessageEventArgs_401 messageEventArgs);
        /// 402
        public delegate void FileInformationEventHandler(object sender, MessageEventArgs_402 messageEventArgs);
        /// 410
        public delegate void FileListingEventHandler(object sender, MessageEventArgs_410420 messageEventArgs);
        /// 411
        public delegate void FileListingDoneEventHandler(object sender, MessageEventArgs_411 messageEventArgs);
        /// 420
        public delegate void SearchListingEventHandler(object sender, MessageEventArgs_410420 messageEventArgs);
        /// 421
        public delegate void SearchListingDoneEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 500
        public delegate void CommandFailedEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 501
        public delegate void CommandNotRecognizedEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 502
        public delegate void CommandNotImplementedEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 503
        public delegate void SyntaxErrorEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 510
        public delegate void LoginFailedEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 511
        public delegate void BannedEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 512
        public delegate void ClientNotFoundEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 513
        public delegate void AccountNotFoundEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 514
        public delegate void AccountExistsEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 515
        public delegate void CannotBeDisconnectedEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 516
        public delegate void PermissionDeniedEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 520
        public delegate void FileOrDirectoryNotFoundEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 521
        public delegate void FileOrDirectoryExistsEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 522
        public delegate void ChecksumMismatchEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 523
        public delegate void QueueLimitExceededEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 600
        public delegate void UserSpecificationEventHandler(object sender, MessageEventArgs_600 messageEventArgs);
        /// 601
        public delegate void GroupSpecificationEventHandler(object sender, MessageEventArgs_601 messageEventArgs);
        /// 602
        public delegate void PrivilegesSpecificationEventHandler(object sender, MessageEventArgs_602 messageEventArgs);
        /// 610
        public delegate void UserListingEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 611
        public delegate void UserListingDoneEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 620
        public delegate void GroupListingEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 621
        public delegate void GroupListingDoneEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);

        /// Tracker specific
        /// TODO: We should have specifict events for these tracker specific
        /// 710 TrackerCategoryListingEvent	
        public delegate void TrackerCategoryListingEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 711 TrackerCategoryListingDoneEvent
        public delegate void TrackerCategoryListingDoneEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 720 TrackerServerListingEvent
        public delegate void TrackerServerListingEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);
        /// 721 TrackerServerListingDoneEvent
        public delegate void TrackerServerListingDoneEventHandler(object sender, MessageEventArgs_Messages messageEventArgs);

        /// <summary>
        /// Basic information about the server
        /// </summary>
        public event ServerInformationEventHandler ServerInformationEvent;

        /// <summary>
        /// Login succeded
        /// </summary>
        public event LoginSucceededEventHandler LoginSucceededEvent;
        public event PingReplyEventHandler PingReplyEvent;
        public event ServerBannerEventHandler ServerBannerEvent;
        /// <summary>
        /// Event to be notified when a chat message is received
        /// </summary>
        public event ChatEventHandler ChatEvent;
        /// <summary>
        /// Event to be notified when an action chat message is received
        /// </summary>
        public event ActionChatEventHandler ActionChatEvent;
        public event ClientJoinEventHandler ClientJoinEvent;
        public event ClientLeaveEventHandler ClientLeaveEvent;
        public event StatusChangeEventHandler StatusChangeEvent;
        public event PrivateMessageEventHandler PrivateMessageEvent;
        public event ClientKickedEventHandler ClientKickedEvent;
        public event ClientBannedEventHandler ClientBannedEvent;
        public event ClientInformationEventHandler ClientInformationEvent;
        public event BroadcastMessageEventHandler BroadcastMessageEvent;
        public event UserListEventHandler UserListEvent;
        public event UserListDoneEventHandler UserListDoneEvent;
        public event NewsEventHandler NewsEvent;
        public event NewsDoneEventHandler NewsDoneEvent;
        public event NewsPostedEventHandler NewsPostedEvent;
        public event PrivateChatCreatedEventHandler PrivateChatCreatedEvent;
        public event PrivateChatInvitationEventHandler PrivateChatInvitationEvent;
        public event PrivateChatDeclinedEventHandler PrivateChatDeclinedEvent;
        public event ClientImageChangedEventHandler ClientImageChangedEvent;
        public event ChatTopicEventHandler ChatTopicEvent;
        public event TransferReadyEventHandler TransferReadyEvent;
        public event TransferQueuedEventHandler TransferQueuedEvent;
        public event FileInformationEventHandler FileInformationEvent;
        public event FileListingEventHandler FileListingEvent;
        public event FileListingDoneEventHandler FileListingDoneEvent;
        public event SearchListingEventHandler SearchListingEvent;
        public event SearchListingDoneEventHandler SearchListingDoneEvent;
        public event CommandFailedEventHandler CommandFailedEvent;
        public event CommandNotRecognizedEventHandler CommandNotRecognizedEvent;
        public event CommandNotImplementedEventHandler CommandNotImplementedEvent;
        public event SyntaxErrorEventHandler SyntaxErrorEvent;
        public event LoginFailedEventHandler LoginFailedEvent;
        public event BannedEventHandler BannedEvent;
        public event ClientNotFoundEventHandler ClientNotFoundEvent;
        public event AccountNotFoundEventHandler AccountNotFoundEvent;
        public event AccountExistsEventHandler AccountExistsEvent;
        public event CannotBeDisconnectedEventHandler CannotBeDisconnectedEvent;
        public event PermissionDeniedEventHandler PermissionDeniedEvent;
        public event FileOrDirectoryNotFoundEventHandler FileOrDirectoryNotFoundEvent;
        public event FileOrDirectoryExistsEventHandler FileOrDirectoryExistsEvent;
        public event ChecksumMismatchEventHandler ChecksumMismatchEvent;
        public event QueueLimitExceededEventHandler QueueLimitExceededEvent;
        public event UserSpecificationEventHandler UserSpecificationEvent;
        public event GroupSpecificationEventHandler GroupSpecificationEvent;
        public event PrivilegesSpecificationEventHandler PrivilegesSpecificationEvent;
        public event UserListingEventHandler UserListingEvent;
        public event UserListingDoneEventHandler UserListingDoneEvent;
        public event GroupListingEventHandler GroupListingEvent;
        public event GroupListingDoneEventHandler GroupListingDoneEvent;

        /// Tracker specific events (doesn't exist in the protocol specification)
        public event TrackerCategoryListingEventHandler TrackerCategoryListingEvent;
        public event TrackerCategoryListingDoneEventHandler TrackerCategoryListingDoneEvent;
        public event TrackerServerListingEventHandler TrackerServerListingEvent;
        public event TrackerServerListingDoneEventHandler TrackerServerListingDoneEvent;

        #region create events and raise

        /// 
        /// Here follows the Raisers of events
        ///

        // 200
        private void OnServerInformationEvent(object sender, int messageId, string messageName, string message)
        {
            if (ServerInformationEvent != null)
            {
                // Parse the server information event
                string[] words = SplitMessage(message);

                string appVersion = words[0];
                string protocolVersion = "1,1"; // TODO: Find a better way to represent Protocol Version
                string serverName = words[2];
                string serverDescription = words[3];
                DateTime startTime;
                DateTime.TryParse(words[4], out startTime);

                int filesCount = Convert.ToInt16(words[5]);
                int filesSize = Convert.ToInt16(words[6]);

                MessageEventArgs_200 m = new MessageEventArgs_200(messageId, messageName, appVersion, protocolVersion, serverName, serverDescription, startTime, filesCount, filesSize);

                ServerInformationEvent(this, m);
            }
        }

        // 201
        private void OnLoginSucceededEvent(object sender, int messageId, string messageName, string message)
        {
            if (LoginSucceededEvent != null)
            {
                int userId = Int16.Parse(message);
                MessageEventArgs_201 m = new MessageEventArgs_201(messageId, messageName, userId);
                LoginSucceededEvent(this, m);
            }
        }

        // 202
        private void OnPingReplyEvent(object sender, int messageId, string messageName, string message)
        {
            if (PingReplyEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                PingReplyEvent(this, m);
            }
        }

        // 203
        private void OnServerBannerEvent(object sender, int messageId, string messageName, string message)
        {
            if (ServerBannerEvent != null)
            {
                Bitmap serverBanner = new Bitmap(Base64StringToBitmap(message));
                MessageEventArgs_203 m = new MessageEventArgs_203(messageId, messageName, serverBanner);
                ServerBannerEvent(this, m);
            }
        }

        // 300
        private void OnChatEvent(object sender, int messageId, string messageName, string message)
        {
            if (ChatEvent != null)
            {
                string[] words = SplitMessage(message);
                int chatId = int.Parse(words[0]);
                int fromUserId = int.Parse(words[1]);
                string parsedMessage = words[2];

                MessageEventArgs_300301 m = new MessageEventArgs_300301(messageId, messageName, chatId, fromUserId, parsedMessage);
                ChatEvent(this, m);
            }
        }

        // 301
        private void OnActionChatEvent(object sender, int messageId, string messageName, string message)
        {
            if (ActionChatEvent != null)
            {
                string[] words = SplitMessage(message);
                int chatId = int.Parse(words[0]);
                int fromUserId = int.Parse(words[1]);
                string parsedMessage = words[2];

                MessageEventArgs_300301 m = new MessageEventArgs_300301(messageId, messageName, chatId, fromUserId, parsedMessage);

                ActionChatEvent(this, m);
            }
        }

        // 302 
        private void OnClientJoinEvent(object sender, int messageId, string messageName, string message)
        {
            if (ClientJoinEvent != null)
            {

                string[] words = SplitMessage(message);

                int chatId = int.Parse(words[0]);
                int userId = int.Parse(words[1]);
                bool idle = Utility.ConvertIntToBool(int.Parse(words[2]));
                bool admin = Utility.ConvertIntToBool(int.Parse(words[3]));
                int icon = int.Parse(words[4]);
                string nick = words[5];
                string login = words[6];
                IPAddress ip = IPAddress.Parse(words[7]);
                string host = words[8];
                string status = words[9];
                Bitmap image = Base64StringToBitmap(words[10]);

                MessageEventArgs_302310 m = new MessageEventArgs_302310(messageId, messageName, chatId, userId, idle, admin, icon, nick, login, ip, host, status, image);

                ClientJoinEvent(this, m);
            }
        }

        // 303
        private void OnClientLeaveEvent(object sender, int messageId, string messageName, string message)
        {
            if (ClientLeaveEvent != null)
            {
                string[] words = SplitMessage(message);
                int chatId = int.Parse(words[0]);
                int userId = int.Parse(words[1]);

                MessageEventArgs_303331332 m = new MessageEventArgs_303331332(messageId, messageName, chatId, userId);

                ClientLeaveEvent(this, m);
            }
        }

        // 304
        private void OnStatusChangeEvent(object sender, int messageId, string messageName, string message)
        {
            if (StatusChangeEvent != null)
            {
                string[] words = SplitMessage(message);

                // If we thing we want to set the variable to -1 instead 
                // of catching exception when something fails we can do it like this.
                // int userId;
                // if(!(int.TryParse(words[0], out userId)))
                //     userId = -1;

                int userId = int.Parse(words[0]);
                bool idle = Utility.ConvertIntToBool(int.Parse(words[1]));
                bool admin = Utility.ConvertIntToBool(int.Parse(words[2]));
                int icon = int.Parse(words[3]);
                string nick = words[4];
                string status = words[5];

                MessageEventArgs_304 m = new MessageEventArgs_304(messageId, messageName, userId, idle, admin, icon, nick, status);

                StatusChangeEvent(this, m);
            }
        }

        // 305
        private void OnPrivateMessageEvent(object sender, int messageId, string messageName, string message)
        {
            if (PrivateMessageEvent != null)
            {
                string[] words = SplitMessage(message);

                int userId = int.Parse(words[0]);
                string parsedMessage = words[1];

                MessageEventArgs_305309 m = new MessageEventArgs_305309(messageId, messageName, userId, parsedMessage);

                PrivateMessageEvent(this, m);
            }
        }

        // 306
        private void OnClientKickedEvent(object sender, int messageId, string messageName, string message)
        {
            if (ClientKickedEvent != null)
            {
                string[] words = SplitMessage(message);

                int victimId = int.Parse(words[0]);
                int killerId = int.Parse(words[1]);
                string parsedMessage = words[2];

                MessageEventArgs_306307 m = new MessageEventArgs_306307(messageId, messageName, parsedMessage, victimId, killerId);

                ClientKickedEvent(this, m);
            }
        }

        // 307
        private void OnClientBannedEvent(object sender, int messageId, string messageName, string message)
        {
            if (ClientBannedEvent != null)
            {
                string[] words = SplitMessage(message);

                int victimId = int.Parse(words[0]);
                int killerId = int.Parse(words[1]);
                string parsedMessage = words[3];

                MessageEventArgs_306307 m = new MessageEventArgs_306307(messageId, messageName, parsedMessage, victimId, killerId);

                ClientBannedEvent(this, m);
            }
        }

        // 308
        private void OnClientInformationEvent(object sender, int messageId, string messageName, string message)
        {
            if (ClientInformationEvent != null)
            {
                string[] w = SplitMessage(message);
                int userId = int.Parse(w[0]);
                bool idle = Utility.ConvertIntToBool(int.Parse(w[1]));
                bool admin = Utility.ConvertIntToBool(int.Parse(w[2]));
                int icon = int.Parse(w[3]);
                string nick = w[4];
                string login = w[5];
                IPAddress ip = IPAddress.Parse(w[6]);
                string host = w[7];
                string clientVersion = w[8]; // TODO: This type is not optimal
                string cipherName = w[9]; // TODO: This type is not optimal
                int cipherBits = int.Parse(w[10]);
                DateTime loginTime = DateTime.Parse(w[11]);
                DateTime idleTime = DateTime.Parse(w[12]);
                string downloads = w[13]; // TODO: This is not optimal type
                string uploads = w[14]; // TODO: This is not optimal type
                string status = w[15];
                Bitmap image = Base64StringToBitmap(w[16]);
                string transfer = w[16];
                string path = w[17];
                int transferred = int.Parse(w[18]);
                int size = int.Parse(w[19]);
                int speed = int.Parse(w[20]);

                MessageEventArgs_308 m = new MessageEventArgs_308(messageId, messageName, userId, image, idle, admin, icon, nick, login, status, ip, host, clientVersion, cipherName, cipherBits, loginTime, idleTime, downloads, uploads, transfer, path, transferred, size, speed);

                ClientInformationEvent(this, m);
            }
        }

        // 309
        private void OnBroadcastMessageEvent(object sender, int messageId, string messageName, string message)
        {
            if (BroadcastMessageEvent != null)
            {
                string[] w = SplitMessage(message);
                int userId = int.Parse(w[1]);
                string parsedMessage = w[2];

                MessageEventArgs_305309 m = new MessageEventArgs_305309(messageId, messageName, userId, parsedMessage);

                BroadcastMessageEvent(this, m);
            }
        }

        // 310
        private void OnUserListEvent(object sender, int messageId, string messageName, string message)
        {
            if (UserListEvent != null)
            {
                string[] s = SplitMessage(message);
                int chatId = Convert.ToInt16(s[0]);
                int userId = Convert.ToInt16(s[1]);
                bool idle = Convert.ToBoolean(Convert.ToInt16(s[2]));
                bool admin = Convert.ToBoolean(Convert.ToInt16(s[3]));
                int icon = Convert.ToInt16(s[4]);
                string nick = s[5];
                string login = s[6];
                IPAddress ip = IPAddress.Parse(s[7]);
                string host = s[8];
                string status = s[9];
                Bitmap image = Base64StringToBitmap(s[10]);

                MessageEventArgs_302310 m = new MessageEventArgs_302310(messageId, messageName, chatId, userId, idle, admin, icon, nick, login, ip, host, status, image);

                UserListEvent(this, m);
            }
        }

        // 311
        private void OnUserListDoneEvent(object sender, int messageId, string messageName, string message)
        {
            if (UserListDoneEvent != null)
            {
                int chatId = Convert.ToInt16(message);
                MessageEventArgs_311330 m = new MessageEventArgs_311330(messageId, messageName, chatId);
                UserListDoneEvent(this, m);
            }
        }

        // 320
        private void OnNewsEvent(object sender, int messageId, string messageName, string message)
        {
            if (NewsEvent != null)
            {
                string[] w = SplitMessage(message);
                string nick = w[0];
                DateTime postTime = DateTime.Parse(w[1]);
                string post = w[2];

                MessageEventArgs_320322 m = new MessageEventArgs_320322(messageId, messageName, nick, postTime, post);

                NewsEvent(this, m);
            }
        }

        // 321
        private void OnNewsDoneEvent(object sender, int messageId, string messageName, string message)
        {
            if (NewsDoneEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                NewsDoneEvent(this, m);
            }
        }

        // 322
        private void OnNewsPostedEvent(object sender, int messageId, string messageName, string message)
        {
            if (NewsPostedEvent != null)
            {
                string[] w = SplitMessage(message);
                string nick = w[0];
                DateTime postTime = DateTime.Parse(w[1]);
                string post = w[2];

                MessageEventArgs_320322 m = new MessageEventArgs_320322(messageId, messageName, nick, postTime, post);

                NewsPostedEvent(this, m);
            }
        }

        // 330
        private void OnPrivateChatCreatedEvent(object sender, int messageId, string messageName, string message)
        {
            if (PrivateChatCreatedEvent != null)
            {
                int chatId = int.Parse(message);
                MessageEventArgs_311330 m = new MessageEventArgs_311330(messageId, messageName, chatId);
                PrivateChatCreatedEvent(this, m);
            }
        }

        // 331
        private void OnPrivateChatInvitationEvent(object sender, int messageId, string messageName, string message)
        {
            if (PrivateChatInvitationEvent != null)
            {
                string[] w = SplitMessage(message);
                int chatId = int.Parse(w[0]);
                int userId = int.Parse(w[1]);
                MessageEventArgs_303331332 m = new MessageEventArgs_303331332(messageId, messageName, chatId, userId);
                PrivateChatInvitationEvent(this, m);
            }
        }

        // 332
        private void OnPrivateChatDeclinedEvent(object sender, int messageId, string messageName, string message)
        {
            if (PrivateChatDeclinedEvent != null)
            {

                string[] w = SplitMessage(message);
                int chatId = int.Parse(w[0]);
                int userId = int.Parse(w[1]);
                MessageEventArgs_303331332 m = new MessageEventArgs_303331332(messageId, messageName, chatId, userId);
                PrivateChatDeclinedEvent(this, m);
            }
        }

        // 340
        private void OnClientImageChangedEvent(object sender, int messageId, string messageName, string message)
        {
            if (ClientImageChangedEvent != null)
            {
                string[] w = SplitMessage(message);
                int userId = int.Parse(w[0]);
                Bitmap image = Base64StringToBitmap(w[1]);
                MessageEventArgs_340 m = new MessageEventArgs_340(messageId, messageName, userId, image);
                ClientImageChangedEvent(this, m);
            }
        }

        // 341
        private void OnChatTopicEvent(object sender, int messageId, string messageName, string message)
        {
            if (ChatTopicEvent != null)
            {
                string[] words = SplitMessage(message);
                int chatId = Convert.ToInt16(words[0]);
                string nick = words[1];
                string login = words[2];
                IPAddress ip = IPAddress.Parse(words[3]);
                DateTime time = DateTime.Parse(words[4]);
                string topic = words[5];

                MessageEventArgs_341 m = new MessageEventArgs_341(messageId, messageName, chatId, nick, login, ip, time, topic);

                ChatTopicEvent(this, m);
            }
        }

        // 400
        private void OnTransferReadyEvent(object sender, int messageId, string messageName, string message)
        {
            if (TransferReadyEvent != null)
            {
                string[] w = SplitMessage(message);
                string path = w[0];
                int offset = int.Parse(w[1]);
                string hash = w[2];
                MessageEventArgs_400 m = new MessageEventArgs_400(messageId, messageName, path, offset, hash);
                TransferReadyEvent(this, m);
            }
        }

        // 401
        private void OnTransferQueuedEvent(object sender, int messageId, string messageName, string message)
        {
            if (TransferQueuedEvent != null)
            {
                string[] w = SplitMessage(message);
                string path = w[0];
                int position = int.Parse(w[1]);
                MessageEventArgs_401 m = new MessageEventArgs_401(messageId, messageName, path, position);
                TransferQueuedEvent(this, m);
            }
        }

        // 402
        private void OnFileInformationEvent(object sender, int messageId, string messageName, string message)
        {
            if (FileInformationEvent != null)
            {
                string[] w = SplitMessage(message);
                string path = w[0];
                string fileType = w[1]; // TODO: This is not an optimal type
                int size = int.Parse(w[2]);
                DateTime created = DateTime.Parse(w[3]);
                DateTime modified = DateTime.Parse(w[4]);
                string checksum = w[5];
                string comment = w[6];
                MessageEventArgs_402 m = new MessageEventArgs_402(messageId, messageName, path, fileType, size, created, modified, checksum, comment);
                FileInformationEvent(this, m);
            }
        }

        // 410
        private void OnFileListingEvent(object sender, int messageId, string messageName, string message)
        {
            if (FileListingEvent != null)
            {
                string[] w = SplitMessage(message);
                string path = w[0];
                string fileType = w[1]; // TODO: this type is not optimal
                long size = long.Parse(w[2]); //FIXME: Must be a long
                DateTime created = DateTime.Parse(w[3]);
                DateTime modified = DateTime.Parse(w[4]);
                MessageEventArgs_410420 m = new MessageEventArgs_410420(messageId, messageName, path, fileType, size, created, modified);
                FileListingEvent(this, m);
            }
        }

        // 411
        private void OnFileListingDoneEvent(object sender, int messageId, string messageName, string message)
        {
            if (FileListingDoneEvent != null)
            {
                string[] w = SplitMessage(message);
                string path = w[0];
                long free = long.Parse(w[1]);
                MessageEventArgs_411 m = new MessageEventArgs_411(messageId, messageName, path, free);
                FileListingDoneEvent(this, m);
            }
        }

        // 420
        private void OnSearchListingEvent(object sender, int messageId, string messageName, string message)
        {
            if (SearchListingEvent != null)
            {
                string[] w = SplitMessage(message);
                string path = w[0];
                string fileType = w[1]; // TODO: this type is not optimal
                int size = int.Parse(w[2]);
                DateTime created = DateTime.Parse(w[3]);
                DateTime modified = DateTime.Parse(w[4]);

                MessageEventArgs_410420 m = new MessageEventArgs_410420(messageId, messageName, path, fileType, size, created, modified);

                SearchListingEvent(this, m);
            }
        }

        // 421
        private void OnSearchListingDoneEvent(object sender, int messageId, string messageName, string message)
        {
            if (SearchListingDoneEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, messageName);
                SearchListingDoneEvent(this, m);
            }
        }

        // 500
        private void OnCommandFailedEvent(object sender, int messageId, string messageName, string message)
        {
            if (CommandFailedEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                CommandFailedEvent(this, m);
            }
        }

        // 501
        private void OnCommandNotRecognizedEvent(object sender, int messageId, string messageName, string message)
        {
            if (CommandNotRecognizedEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                CommandNotRecognizedEvent(this, m);
            }
        }

        // 502
        private void OnCommandNotImplementedEvent(object sender, int messageId, string messageName, string message)
        {
            if (CommandNotImplementedEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                CommandNotImplementedEvent(this, m);
            }
        }

        // 503
        private void OnSyntaxErrorEvent(object sender, int messageId, string messageName, string message)
        {
            if (SyntaxErrorEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                SyntaxErrorEvent(this, m);
            }
        }

        // 510
        private void OnLoginFailedEvent(object sender, int messageId, string messageName, string message)
        {
            if (LoginFailedEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                LoginFailedEvent(this, m);
            }
        }

        // 511
        private void OnBannedEvent(object sender, int messageId, string messageName, string message)
        {
            if (BannedEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                BannedEvent(this, m);
            }
        }

        // 512
        private void OnClientNotFoundEvent(object sender, int messageId, string messageName, string message)
        {
            if (ClientNotFoundEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                ClientNotFoundEvent(this, m);
            }
        }

        // 513
        private void OnAccountNotFoundEvent(object sender, int messageId, string messageName, string message)
        {
            if (AccountNotFoundEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                AccountNotFoundEvent(this, m);
            }
        }

        // 514
        private void OnAccountExistsEvent(object sender, int messageId, string messageName, string message)
        {
            if (AccountExistsEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                AccountExistsEvent(this, m);
            }
        }

        // 515
        private void OnCannotBeDisconnectedEvent(object sender, int messageId, string messageName, string message)
        {
            if (CannotBeDisconnectedEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                CannotBeDisconnectedEvent(this, m);
            }
        }

        // 516
        private void OnPermissionDeniedEvent(object sender, int messageId, string messageName, string message)
        {
            if (PermissionDeniedEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                PermissionDeniedEvent(this, m);
            }
        }

        // 520
        private void OnFileOrDirectoryNotFoundEvent(object sender, int messageId, string messageName, string message)
        {
            if (FileOrDirectoryNotFoundEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                FileOrDirectoryNotFoundEvent(this, m);
            }
        }

        // 521
        private void OnFileOrDirectoryExistsEvent(object sender, int messageId, string messageName, string message)
        {
            if (FileOrDirectoryExistsEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                FileOrDirectoryExistsEvent(this, m);
            }
        }

        // 522
        private void OnChecksumMismatchEvent(object sender, int messageId, string messageName, string message)
        {
            if (ChecksumMismatchEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                ChecksumMismatchEvent(this, m);
            }
        }

        // 523
        private void OnQueueLimitExceededEvent(object sender, int messageId, string messageName, string message)
        {
            if (QueueLimitExceededEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                QueueLimitExceededEvent(this, m);
            }
        }

        // 600
        private void OnUserSpecificationEvent(object sender, int messageId, string messageName, string message)
        {
            if (UserSpecificationEvent != null)
            {
                string[] w = SplitMessage(message);
                string name = w[0];
                string password = w[1];
                string group = w[2];
                //string privileges = w[3]; // TODO: This is not optimal type
                Privileges p = new Privileges(name, w[3]);

                MessageEventArgs_600 m = new MessageEventArgs_600(messageId, messageName, p, name, password, group);

                UserSpecificationEvent(this, m);
            }
        }

        // 601
        private void OnGroupSpecificationEvent(object sender, int messageId, string messageName, string message)
        {
            if (GroupSpecificationEvent != null)
            {
                string[] w = SplitMessage(message);
                string name = w[0];
                Privileges p = new Privileges (name, w[1]); // TODO: this is not optimal type, instead we should create some smarter for privileges

                MessageEventArgs_601 m = new MessageEventArgs_601(messageId, messageName, p, name);

                GroupSpecificationEvent(this, m);
            }
        }

        // 602
        private void OnPrivilegesSpecificationEvent(object sender, int messageId, string messageName, string message)
        {
            if (PrivilegesSpecificationEvent != null)
            {
                // string privileges = message; // TODO: This is not optimal type
                Privileges p = new Privileges("", message); // CheckThis
                MessageEventArgs_602 m = new MessageEventArgs_602(messageId, messageName, p);
                PrivilegesSpecificationEvent(this, m);
            }
        }

        // 610
        private void OnUserListingEvent(object sender, int messageId, string messageName, string message)
        {
            if (UserListingEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                UserListingEvent(this, m);
            }
        }

        // 611
        private void OnUserListingDoneEvent(object sender, int messageId, string messageName, string message)
        {
            if (UserListingDoneEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                UserListingDoneEvent(this, m);
            }
        }

        // 620
        private void OnGroupListingEvent(object sender, int messageId, string messageName, string message)
        {
            if (GroupListingEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                GroupListingEvent(this, m);
            }
        }

        // 621
        private void OnGroupListingDoneEvent(object sender, int messageId, string messageName, string message)
        {
            if (GroupListingDoneEvent != null)
            {
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                GroupListingDoneEvent(this, m);
            }
        }

        // 7xx
        private void OnTrackerCategoryListingEvent(object sender, int messageId, string messageName, string message)
        {
            if (TrackerCategoryListingEvent != null)
            {
                // TODO: Implement the tracker as well.
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                TrackerCategoryListingEvent(this, m);
            }
        }

        // 7xx
        private void OnTrackerCategoryListingDoneEvent(object sender, int messageId, string messageName, string message)
        {
            if (TrackerCategoryListingDoneEvent != null)
            {
                // TODO: Implement the tracker as well.
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                TrackerCategoryListingDoneEvent(this, m);
            }
        }

        // 7xx
        private void OnTrackerServerListingEvent(object sender, int messageId, string messageName, string message)
        {
            if (TrackerServerListingEvent != null)
            {
                // TODO: Implement the tracker as well.
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                TrackerServerListingEvent(this, m);
            }
        }

        // 7xx
        private void OnTrackerServerListingDoneEvent(object sender, int messageId, string messageName, string message)
        {
            if (TrackerServerListingDoneEvent != null)
            {
                // TODO: Implement the tracker as well.
                MessageEventArgs_Messages m = new MessageEventArgs_Messages(messageId, messageName, message);
                TrackerServerListingDoneEvent(this, m);
            }
        }

#endregion

        #region Parse message and create appropreate messageArgs

        /// <summary>
        /// Parses the messages
        /// </summary>
        /// <param name="msg">The message from the server</param>
        private void ParseMessage(string msg)
        {
            // Get the message identifier and the message data
            int msgId = Convert.ToInt32(msg.Substring(0, 3));
            string argument = msg.Substring(4);

            // Switch on the message identifier and call the proper event
            System.Diagnostics.Debug.WriteLine("Starting switch on msgId: " + msgId + " with argument: " + argument);
            switch (msgId)
            {
                case 200:
                    OnServerInformationEvent(this, msgId, "Server Information", argument);
                    break;
                case 201:
                    OnLoginSucceededEvent(this, msgId, "Login Succeeded", argument);
                    break;
                case 202:
                    OnPingReplyEvent(this, msgId, "Ping Reply", argument);
                    break;
                case 203:
                    OnServerBannerEvent(this, msgId, "Server Banner", argument);
                    break;
                case 300:
                    OnChatEvent(this, msgId, "Chat", argument);
                    break;
                case 301:
                    OnActionChatEvent(this, msgId, "Action Chat", argument);
                    break;
                case 302:
                    OnClientJoinEvent(this, msgId, "Client Join", argument);
                    break;
                case 303:
                    OnClientLeaveEvent(this, msgId, "Client Leave", argument);
                    break;
                case 304:
                    OnStatusChangeEvent(this, msgId, "Status Change", argument);
                    break;
                case 305:
                    OnPrivateMessageEvent(this, msgId, "Private Message", argument);
                    break;
                case 306:
                    OnClientKickedEvent(this, msgId, "Client Kicked", argument);
                    break;
                case 307:
                    OnClientBannedEvent(this, msgId, "Client Banned", argument);
                    break;
                case 308:
                    OnClientInformationEvent(this, msgId, "Client Information", argument);
                    break;
                case 309:
                    OnBroadcastMessageEvent(this, msgId, "Broadcast Message", argument);
                    break;
                case 310:
                    OnUserListEvent(this, msgId, "User List", argument);
                    break;
                case 311:
                    OnUserListDoneEvent(this, msgId, "User List Done", argument);
                    break;
                case 320:
                    OnNewsEvent(this, msgId, "News", argument);
                    break;
                case 321:
                    OnNewsDoneEvent(this, msgId, "News Done", argument);
                    break;
                case 322:
                    OnNewsPostedEvent(this, msgId, "News Posted", argument);
                    break;
                case 330:
                    OnPrivateChatCreatedEvent(this, msgId, "Private Chat Created", argument);
                    break;
                case 331:
                    OnPrivateChatInvitationEvent(this, msgId, "Private Chat Invitation", argument);
                    break;
                case 332:
                    OnPrivateChatDeclinedEvent(this, msgId, "Private Chat Declined", argument);
                    break;
                case 340:
                    OnClientImageChangedEvent(this, msgId, "Client Image Change", argument);
                    break;
                case 341:
                    OnChatTopicEvent(this, msgId, "Chat Topic", argument);
                    break;
                case 400:
                    OnTransferReadyEvent(this, msgId, "Transfer Ready", argument);
                    break;
                case 401:
                    OnTransferQueuedEvent(this, msgId, "Transfer Queued", argument);
                    break;
                case 402:
                    OnFileInformationEvent(this, msgId, "File Information", argument);
                    break;
                case 410:
                    OnFileListingEvent(this, msgId, "File Listing", argument);
                    break;
                case 411:
                    OnFileListingDoneEvent(this, msgId, "File Listing Done", argument);
                    break;
                case 420:
                    OnSearchListingEvent(this, msgId, "Search Listing", argument);
                    break;
                case 421:
                    OnSearchListingDoneEvent(this, msgId, "Search Listing Done", argument);
                    break;
                case 500:
                    OnCommandFailedEvent(this, msgId, "Command Failed", argument);
                    break;
                case 501:
                    OnCommandNotRecognizedEvent(this, msgId, "Command Not Recognized", argument);
                    break;
                case 502:
                    OnCommandNotImplementedEvent(this, msgId, "Command Not Implemented", argument);
                    break;
                case 503:
                    OnSyntaxErrorEvent(this, msgId, "Syntax Error", argument);
                    break;
                case 510:
                    OnLoginFailedEvent(this, msgId, "Login Failed", argument);
                    break;
                case 511:
                    OnBannedEvent(this, msgId, "Banned", argument);
                    break;
                case 512:
                    OnClientNotFoundEvent(this, msgId, "Client Not Found", argument);
                    break;
                case 513:
                    OnAccountNotFoundEvent(this, msgId, "Account Not Found", argument);
                    break;
                case 514:
                    OnAccountExistsEvent(this, msgId, "Account Exists", argument);
                    break;
                case 515:
                    OnCannotBeDisconnectedEvent(this, msgId, "Cannot Be Disconnected", argument);
                    break;
                case 516:
                    OnPermissionDeniedEvent(this, msgId, "Permission Denied", argument);
                    break;
                case 520:
                    OnFileOrDirectoryNotFoundEvent(this, msgId, "File or Directory Not Found", argument);
                    break;
                case 521:
                    OnFileOrDirectoryExistsEvent(this, msgId, "File or Directory Exists", argument);
                    break;
                case 522:
                    OnChecksumMismatchEvent(this, msgId, "Checksum Mismatch", argument);
                    break;
                case 523:
                    OnQueueLimitExceededEvent(this, msgId, "Queue Limit Exceeded", argument);
                    break;
                case 600:
                    OnUserSpecificationEvent(this, msgId, "User Specification", argument);
                    break;
                case 601:
                    OnGroupSpecificationEvent(this, msgId, "Group Specification", argument);
                    break;
                case 602:
                    OnPrivilegesSpecificationEvent(this, msgId, "Privileges Specification", argument);
                    break;
                case 610:
                    OnUserListingEvent(this, msgId, "User Listing", argument);
                    break;
                case 611:
                    OnUserListingDoneEvent(this, msgId, "User Listing Done", argument);
                    break;
                case 620:
                    OnGroupListingEvent(this, msgId, "Group Listing", argument);
                    break;
                case 621:
                    OnGroupListingDoneEvent(this, msgId, "Group Listing Done", argument);
                    break;
                case 710:
                    OnTrackerCategoryListingEvent(this, msgId, "Tracker Category Listing", argument);
                    break;
                case 711:
                    OnTrackerCategoryListingDoneEvent(this, msgId, "Tracker Category Listing Done", argument);
                    break;
                case 720:
                    OnTrackerServerListingEvent(this, msgId, "Tracker Server Listing", argument);
                    break;
                case 721:
                    OnTrackerServerListingDoneEvent(this, msgId, "Tracker Server Listing Done", argument);
                    break;
                default:
                    Console.WriteLine("Unhandled message id {0}", msgId); // TODO: Exception instead!
                    break;
            }
        }

        #endregion

        /// <summary>
        /// Handles incoming messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="message"></param>
        private void socket_MessageReceived(object sender, EventArgs e, string message)
        {
			try
			{
				ParseMessage(message);
			}
			catch (FormatException formatExp)
			{
				Console.Error.WriteLine("Error trying to parse the message "
				+ "recieved on socket!\nReason\n" + formatExp.ToString());
			}
        }

        /// <summary>
        /// Splits the string by the Utility.FS
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string[] SplitMessage(string message)
        {
            // Parse the server information event
            char[] delimiterChars = { Convert.ToChar(Utility.FS) };
            return message.Split(delimiterChars);
        }

        /// <summary>
        /// Converts a Base64 (as a string) to an Bitmap.
        /// Tip from David McCarter, see: http://www.vsdntips.com/Tips/VS.NET/Csharp/76.aspx
        /// </summary>
        /// <param name="imageText">The image represented as a Base64 String</param>
        /// <returns>An Bitmap with the image</returns>
        private Bitmap Base64StringToBitmap(string imageText)
        {
            Bitmap image = null;
            if (imageText.Length > 0)
            {
                /**
                This could be used to remove all (if any) \r\n and spaces 
                System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image,Image.Length);
                sbText.Replace("\r\n", String.Empty);
                sbText.Replace(" ", String.Empty);
                */
                Byte[] bitmapData = new Byte[imageText.Length];
                bitmapData = Convert.FromBase64String(imageText);
                System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
                image = new Bitmap((Bitmap)Image.FromStream(streamBitmap));
            }
            return image;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="socket">The secure socket that this messages will listen to</param>
        public Messages(SecureSocket socket)
        {
            this.socket = socket;
            // Listen to events from the socket.
            socket.MessageReceived += new SecureSocket.MessageReceivedHandler(socket_MessageReceived);
        }
    }
}