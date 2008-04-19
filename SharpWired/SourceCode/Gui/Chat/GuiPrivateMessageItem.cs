/*
 * PrivateMessageItem.cs 
 * Created by Ola Lindberg, 2007-01-06
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
using SharpWired.Model.Users;
using SharpWired.Model;

namespace SharpWired.Gui.Chat
{
    /// <summary>
    /// Parses and sets data for a private message.
    /// </summary>
    public class GuiPrivateMessageItem
    {
        private User toUser = null;
        private string message = "";

        /// <summary>
        /// Gets the user who should receive the message
        /// </summary>
        public User ToUser
        {
            get { return toUser; }
        }

        /// <summary>
        /// Gets the private message
        /// </summary>
        public string Message
        {
            get { return message; }
        }
	
        /// <summary>
        /// Constructor - Parses the message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logicManager"></param>
        public GuiPrivateMessageItem(LogicManager logicManager, string message)
        {
            //Searching for users with string written as "/msg "nick name" message (space separated)
            int f = message.IndexOf("\"", 4);
            int l = message.IndexOf("\"", 6);

            if (f == -1 || l == -1)
            {
                //If we didn't find any we search for user with string written as 
                //  "/msg nickName message" (no space)
                f = message.IndexOf(" ", 3);
                l = message.IndexOf(" ", 5);
            }

            //TODO: Error handling
            string searchedNick = message.Substring(f + 1, (l - f - 1));
            this.toUser = logicManager.Server.PublicChat.Users.GetUserByNick(searchedNick);
            this.message = message.Substring(l + 1).Trim();
        }
    }
}
