/*
 * Commands.cs 
 * Created by Ola Lindberg, 2006-06-27
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
using System.Drawing;

using SharpWired.Connection;
using SharpWired.Connection.Sockets;

namespace SharpWired.Connection
{
    /// <summary>
    /// This class handels all commands that are sent to the server. It provides 
    /// methods (that can be used from the GUI) and creates strings that are compatible
    /// with the Wired 1.1 protocol.
    /// </summary>
    public class Commands
    {
        /// <summary>
        /// The socket this commands uses
        /// </summary>
        private SecureSocket socket;

        /// <summary>
        /// Constructor
        /// </summary>
        public Commands(SecureSocket socket)
        {
            this.socket = socket;
        }

        /// <summary>
        /// Send a message string to the server. 
        /// Note, the end EOT should not be included in this string.
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {
            socket.SendMessage(message);
        }

        /// <summary>
        /// Connects the client to the server.
        /// </summary>
        public void InitConnection(UserInformation userInformation)
        {
            if (socket != null)
            {
                SendMessage("HELLO");
                Nick(userInformation .Nick);
                User(userInformation .UserName);
                Pass(userInformation .Password);
                Who(1);
            }
        }

        /// <summary>
        /// Bans the user.
        /// </summary>
        /// <param name="id">The ID for the user to be banned</param>
        /// <param name="message">The message to send to the user that is banned</param>
        public void Ban (int id, string message)
        {
            if (socket != null)
                socket.SendMessage("BAN" + Utility.SP + id + Utility.FS + message);
        }

        /// <summary>
        /// Requests the server banner.
        /// </summary>
        public void Banner()
        {
            if (socket != null)
                socket.SendMessage("BANNER");
        }

        /// <summary>
        /// Send a broadcast message.
        /// </summary>
        /// <param name="message">The message to broadcast</param>
        public void Broadcast(string message)
        {
            if (socket != null)
                socket.SendMessage("BROADCAST" + Utility.SP + message);
        }

        /// <summary>
        /// Empty the news.
        /// </summary>
        public void Clearnews()
        {
            if (socket != null)
                socket.SendMessage("CLEARNEWS");
        }

        /// <summary>
        /// Send the client information.
        /// </summary>
        public void Client()
        {
            if (socket != null)
            {
                //TODO: Fix appVersion
                String appVersion = "SharpWired Development Branch";
                socket.SendMessage("CLIENT" + Utility.SP + appVersion);
            }
        }

        /// <summary>
        /// Set the comment for a file or folder
        /// </summary>
        /// <param name="path">The path to the file or folder to set the comment to</param>
        /// <param name="comment">The comment</param>
        public void Comment(string path, string comment)
        {
            if (socket != null)
                socket.SendMessage("COMMENT" + Utility.SP + path + Utility.FS + comment);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="name">The user name for the new user</param>
        /// <param name="password">The password for the new user</param>
        public void CreateUser (string name, string password)
        {
            if (socket != null)
            {
                //TODO: Group and privileges
                string group = "";
                string privileges = "";
                socket.SendMessage("CREATEUSER" + Utility.SP + name
                    + Utility.FS + password + Utility.FS + group + Utility.FS + privileges);
            }
        }

        /// <summary>
        /// Create a new group
        /// </summary>
        /// <param name="name">The name of the new group</param>
        public void CreateGroup(string name)
        {
            if (socket != null)
            {
                //TODO: Privileges
                string privileges = "";
                socket.SendMessage("CREATEGROUP" + Utility.SP + name + Utility.FS + privileges);
            }
        }

        /// <summary>
        /// Decline a chat invitation
        /// </summary>
        /// <param name="id">The id for the chat to decline</param>
        public void Decline(int id) 
        {
            if (socket != null)
                socket.SendMessage("DECLINE" + Utility.SP + id);
        }

        /// <summary>
        /// Delete a file or folder
        /// </summary>
        /// <param name="path">The path to the file or folder to delete</param>
        public void Delete(string path)
        {
            if (socket != null)
                socket.SendMessage("DELETE" + Utility.SP + path);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="name">The user name for the user to delete</param>
        public void DeleteUser(string name)
        {
            if (socket != null)
                socket.SendMessage("DELETEUSER" + Utility.SP + name);
        }

        /// <summary>
        /// Delete a group
        /// </summary>
        /// <param name="group">The name of the group to delete</param>
        public void DeleteGroup(string group)
        {
            if (socket != null)
                socket.SendMessage("DELETEGROUP" + Utility.SP + group);
        }

        /// <summary>
        /// Modify a user
        /// </summary>
        /// <param name="name">The user name for the user to modify</param>
        /// <param name="password">The new password for the user</param>
        /// <param name="group">The new group for the user</param>
        /// <param name="privileges">The new privileges for the user</param>
        public void EditUser(string name, string password, string group, string privileges)
        {
            if (socket != null)
            {
                //TODO: Privileges
                socket.SendMessage("EDITUSER" + Utility.SP + name + Utility.FS
                    + password + Utility.FS + group + Utility.FS + privileges);
            }
        }

        /// <summary>
        /// Modify a group
        /// </summary>
        /// <param name="name">The name of the group to modify</param>
        /// <param name="privileges">The new privileges for the group</param>
        public void EditGroup(string name, string privileges)
        {
            if (socket != null)
            {
                //TODO Privileges
                socket.SendMessage("EDITGROUP" + Utility.SP + name + Utility.FS + privileges);
            }
        }

        /// <summary>
        /// Create a new folder
        /// </summary>
        /// <param name="path">The path for the new folder</param>
        public void Folder(string path)
        {
            if (socket != null)
                socket.SendMessage("FOLDER" + Utility.SP + path);
        }

        /// <summary>
        /// Request a download of a file
        /// </summary>
        /// <param name="path">The path for the file to request</param>
        /// <param name="offset">The offset for the file FIXME: Is an int sufficient?</param>
        public void Get(string path, int offset)
        {
            if (socket != null)
            {
                //TODO Offset?
                socket.SendMessage("GET" + Utility.SP + path + Utility.FS + offset);
            }
        }

        /// <summary>
        /// Request a listing of all group accounts on the server
        /// </summary>
        public void Groups()
        {
            if (socket != null)
                socket.SendMessage("GROUPS");
        }

        /// <summary>
        /// Start a conversation with a server.
        /// </summary>
        /// <param name="serverPort">The port for the server to use for this connection</param>
        /// <param name="machineName">The host running the server application</param>
        /// <param name="serverName">The machine name for the server, must match the machine name in the server certificate</param>
        public void Hello(string machineName, int serverPort, string serverName)
        {
            if (socket != null)
                socket.SendMessage("HELLO");
        }

        /// <summary>
        /// Change the icon
        /// </summary>
        /// <param name="icon">The icon number to use</param>
        /// <param name="image">The custom icon image</param>
        public void Icon(int icon, Image image)
        {
            if (socket != null)
            {
                //TODO Convert the image to Base64
                String b64Image = "";
                socket.SendMessage("ICON" + Utility.SP + icon + Utility.FS + b64Image);
                
                // TODO: Convert bitmap to byte array
                //Convert.ToBase64String()
            }
        }

        /// <summary>
        /// Request client information
        /// </summary>
        /// <param name="userId">The ID for the user client (user) to request information for</param>
        public void Info(int userId)
        {
            if (socket != null)
                socket.SendMessage("INFO" + Utility.SP + userId);
        }

        /// <summary>
        /// Invite a user to a chat
        /// </summary>
        /// <param name="userId">The ID for the user to invite</param>
        /// <param name="chatId">The ID for the chat to invite the user to</param>
        public void Invite(int userId, int chatId)
        {
            if (socket != null)
                socket.SendMessage("INVITE" + Utility.SP + userId + Utility.FS + chatId);
        }

        /// <summary>
        /// Join a chat
        /// </summary>
        /// <param name="chatId">The ID for the chat to join.</param>
        public void Join(int chatId)
        {
            if (socket != null)
                socket.SendMessage("JOIN" + Utility.SP + chatId);
        }

        /// <summary>
        /// Kick a user
        /// </summary>
        /// <param name="userId">The user ID for the user to kick</param>
        /// <param name="message">The message to send to the user to kick</param>
        public void Kick(int userId, string message)
        {
            if (socket != null)
                socket.SendMessage("KICK" + Utility.SP + userId + Utility.FS + message);
        }

        /// <summary>
        /// Leave a chat
        /// </summary>
        /// <param name="chatId">The chat ID for the chat to leave</param>
        public void Leave(int chatId)
        {
            if (socket != null)
                socket.SendMessage("LEAVE" + Utility.SP + chatId);
        }

        /// <summary>
        /// List the file content
        /// </summary>
        /// <param name="path">The path to where file content should be listed</param>
        public void List(string path)
        {
            if (socket != null)
                socket.SendMessage("LIST" + Utility.SP + path);
        }

        /// <summary>
        /// Send action to chat
        /// </summary>
        /// <param name="id">The ID for the chat to send to</param>
        /// <param name="message">The action message</param>
        public void Me(int id, string message)
        {
            if (socket != null)
                socket.SendMessage("ME" + Utility.SP + id + Utility.FS + message);
        }

        /// <summary>
        /// Move a file or folder
        /// </summary>
        /// <param name="from">The current path to the file or folder</param>
        /// <param name="to">The new path to the file or folder</param>
        public void Move(string from, string to)
        {
            if (socket != null)
                socket.SendMessage("MOVE" + Utility.SP + from + Utility.FS + to);
        }

        /// <summary>
        /// Send a private message to a user
        /// </summary>
        /// <param name="id">The ID for the user to receive the message</param>
        /// <param name="message">The message</param>
        public void Msg(int id, string message)
        {
            if (socket != null)
                socket.SendMessage("MSG" + Utility.SP + id + Utility.FS + message);
        }

        /// <summary>
        /// Request the news
        /// </summary>
        public void News()
        {
            if(socket != null)
                socket.SendMessage("NEWS");
        }

        /// <summary>
        /// Change the nic
        /// </summary>
        /// <param name="nick">The new nick name</param>
        public void Nick(string nick)
        {
            if (socket != null)
                socket.SendMessage("NICK" + Utility.SP + nick);
        }

        /// <summary>
        /// Send the password
        /// </summary>
        /// <param name="password">The password to be sent</param>
        public void Pass(string password)
        {
			if (socket != null)
				// socket.SendMessage("PASS" + Utility.SP + Utility.HashPassword(password));
				// Password is already hashed!
				socket.SendMessage("PASS" + Utility.SP + password);
        }

        /// <summary>
        /// Ping the server
        /// </summary>
        public void Ping()
        {
            if (socket != null)
                socket.SendMessage("PING");
        }

        /// <summary>
        /// Post a message to the news
        /// </summary>
        /// <param name="message">The message to be posted</param>
        public void Post(string message)
        {
            if (socket != null)
                socket.SendMessage("POST" + Utility.SP + message);
        }

        /// <summary>
        /// Create a private chat on the server
        /// </summary>
        public void PrivateChat()
        {
            if (socket != null)
                socket.SendMessage("PRIVCHAT");
        }

        /// <summary>
        /// Request the current privileges mask
        /// </summary>
        public void Privileges()
        {
            if (socket != null)
                socket.SendMessage("PRIVILEGES");
        }

        /// <summary>
        /// Request an upload of a file
        /// </summary>
        /// <param name="path">The path (on the server) to the file to be uploaded</param>
        /// <param name="size">The size of the file to be uploaded</param>
        /// <param name="checksum">The checksum</param>
        public void Put(string path, int size, string checksum)
        {
            //TODO: Should we have another mechanism for checksum?
            if (socket != null)
            {
                socket.SendMessage("PUT" + Utility.SP + path + Utility.FS +
                    size + Utility.FS + checksum);
            }
        }

        /// <summary>
        /// Get account specification for a user
        /// </summary>
        /// <param name="name">The name of the user to request information on</param>
        public void ReadUser(string name)
        {
            if (socket != null)
                socket.SendMessage("READUSER" + Utility.SP + name);
        }

        /// <summary>
        /// Get the account specification for a group
        /// </summary>
        /// <param name="name">The group to request specification for</param>
        public void ReadGroup(string name)
        {
            if (socket != null)
                socket.SendMessage("READGROUP" + Utility.SP + name);
        }

        /// <summary>
        /// Send a message to the public chat. Note, only redirects to the other say method.
        /// </summary>
        /// <param name="message">The message</param>
        public void Say(string message)
        {
            Say(1, message);
        }

        /// <summary>
        /// Send a chat message
        /// </summary>
        /// <param name="id">The ID for the chat where the message will be sent to</param>
        /// <param name="message">The message</param>
        public void Say(int id, string message)
        {
            if (socket != null)
                socket.SendMessage("SAY" + Utility.SP + id + Utility.FS + message);
        }

        /// <summary>
        /// Search for a file
        /// </summary>
        /// <param name="query">The query for what to search for</param>
        public void Search(string query)
        {
            if (socket != null)
                socket.SendMessage("SEARCH" + Utility.SP + query);
        }

        /// <summary>
        /// Get the file information for a file or folder
        /// </summary>
        /// <param name="path">The path to the file or folder</param>
        public void Stat(string path)
        {
            if (socket != null)
                socket.SendMessage("STAT" + Utility.SP + path);
        }

        /// <summary>
        /// Change the status message
        /// </summary>
        /// <param name="status">The new status message</param>
        public void Status(string status)
        {
            if (socket != null)
                socket.SendMessage("STATUS" + Utility.SP + status);
        }

        /// <summary>
        /// Set the topic to a chat
        /// </summary>
        /// <param name="chatId">The ID for the chat to change topic</param>
        /// <param name="topic">The new topic</param>
        public void Topic(int chatId, string topic)
        {
            if (socket != null)
                socket.SendMessage("TOPIC" + Utility.SP + chatId + Utility.SP + topic);
        }

        /// <summary>
        /// Initiate the transfer with the given ID.
        /// </summary>
        /// <param name="hash">The hash key for the transfer</param>
        public void Transfer(string hash)
        {
            if (socket != null)
                socket.SendMessage("TRANSFER" + Utility.SP + hash);
        }

        /// <summary>
        /// Set the type of a folder
        /// </summary>
        /// <param name="path">The path to the folder</param>
        /// <param name="folderType">The new type of the folder</param>
        public void Type(string path, int folderType)
        {
            if (socket != null)
                socket.SendMessage("TYPE" + Utility.SP + path + Utility.SP + folderType);
        }

        /// <summary>
        /// Send login name
        /// </summary>
        /// <param name="login">The login name to be sent</param>
        public void User(string login)
        {
            if (socket != null)
                socket.SendMessage("USER" + Utility.SP + login);
        }

        /// <summary>
        /// Get a listing of all user accounts on the server
        /// </summary>
        public void Users()
        {
            if (socket != null)
                socket.SendMessage("USERS");
        }

        /// <summary>
        /// Get the user list for a chat
        /// </summary>
        /// <param name="chat">The ID for the chat</param>
        public void Who(int chat)
        {
            if (socket != null)
                socket.SendMessage("WHO" + Utility.SP + chat);
        }
    }
}
