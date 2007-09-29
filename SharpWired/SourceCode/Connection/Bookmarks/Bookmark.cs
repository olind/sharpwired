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
	/// <summary>
	/// This class is a Bookmark to a server. It consist of server info together with a login.
	/// </summary>
    [Serializable]
    public class Bookmark
	{
		#region Properties
		private UserInformation  userInformation;
		/// <summary>
		/// Get/Set the login to the server.
		/// </summary>
        public UserInformation  UserInformation
        {
            get { return userInformation; }
            set { userInformation = value; }
        }

        private Server server;
		/// <summary>
		/// Get/Set the Server info.
		/// </summary>
        public Server Server
        {
            get { return server; }
            set { server = value; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Constrcut.
		/// </summary>
		/// <param name="server">The server.</param>
		/// <param name="userInformation">The login.</param>
		public Bookmark(Server server, UserInformation userInformation)
        {
            this.Server = server;
            this.UserInformation = userInformation;
        }

		/// <summary>
		/// Parameterless constructor for de-serialization. For Xml.
		/// </summary>
		private Bookmark()
		{
		}
		#endregion

		#region
		/// <summary>
		/// Compares the objects using .Equals() fo Server and UserInformation.
		/// </summary>
		/// <param name="obj">The object to compare with.</param>
		/// <returns>T/F.</returns>
		public override bool Equals(object obj)
		{
			Bookmark b = obj as Bookmark;
			if (b == null)
				return false;
			return b.server.Equals(this.server)
				&& b.userInformation.Equals(this.userInformation);
		}

		/// <summary>
		/// Returns a short representation of this Bookmark.
		/// </summary>
		/// <returns>[UserName]@[ServerName]:[Port]</returns>
		public string ToShortString()
		{
			return this.UserInformation.UserName
							+ " @ " + this.Server.ServerName
							+ ":" + this.Server.ServerPort.ToString();
		}

		/// <summary>
		/// A string for this Bookmark.
		/// </summary>
		/// <returns>user @ server.</returns>
		public override string ToString()
		{
			return userInformation + " @ " + server;
		}

		/// <summary>
		/// Base.GetHashCode().
		/// </summary>
		/// <returns>Code de la Hash.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
    }
}
