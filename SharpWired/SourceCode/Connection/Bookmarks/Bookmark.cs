#region Information and licence agreements
/**
 * Bookmark.cs
 * Created by Peter Holmdal, 2006-12-03
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

namespace SharpWired.Connection.Bookmarks
{
    [Serializable]
    public class Bookmark
    {
        private UserInformation  userInformation;
        public UserInformation  UserInformation
        {
            get { return userInformation; }
            set { userInformation = value; }
        }

        private Server server;
        public Server Server
        {
            get { return server; }
            set { server = value; }
        }
	
        public Bookmark(Server server, UserInformation userInformation)
        {
            this.Server = server;
            this.UserInformation = userInformation;
        }

		/// <summary>
		/// Parameterless constructor for de-serialization.
		/// </summary>
		private Bookmark()
		{
		}

		/// <summary>
		/// Compares the objects using .Equals() fo Server and UserInformation.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Bookmark))
				return false;
			else
			{
				Bookmark b = obj as Bookmark;
				return b.server.Equals( this.server )
					&& b.userInformation.Equals( this.userInformation );
			}
		}

		public string ToShortString()
		{
			return this.UserInformation.UserName
							+ " @ " + this.Server.ServerName
							+ ":" + this.Server.ServerPort.ToString();
		}
    }
}
