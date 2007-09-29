#region Information and licence agreements
/**
 * UserInformation.cs
 * Created by Ola Lindberg, 2006-07-22
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

namespace SharpWired.Connection
{
	/// <summary>
	/// This class represents a login on a server with username/pass/nick.
	/// </summary>
    [Serializable]
    public class UserInformation
    {
		#region Constrctors.
		/// <summary>
		/// Parameterless constructor for serialization. For XML.
		/// </summary>
		private UserInformation()
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="nick">The Nickname.</param>
		/// <param name="userName">The user name.</param>
		/// <param name="password">The Password. NOTE: ClearText or Hash?!</param>
        public UserInformation(string nick, string userName, string password)
        {
            this.Nick = nick;
            this.UserName = userName;
            this.Password = password;
		}
		#endregion

		#region Properties
		private string nick;
		/// <summary>
		/// Get/Set the NickName.
		/// </summary>
        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }

        private string user;
		/// <summary>
		/// Get/Set the UserName.
		/// </summary>
        public string UserName
        {
            get { return user; }
            set { user = value; }
        }

        private string password;
		/// <summary>
		/// Get/Set the password. The password is hashed!
		/// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
		#endregion

		#region Overrides
		/// <summary>
		/// Compares the user name and nick using '=='. The password isn't compared.
		/// </summary>
		/// <param name="obj">The object to compare to.</param>
		/// <returns>T/F.</returns>
		public override bool Equals(object obj)
		{
			UserInformation u = obj as UserInformation;
			if (u == null)
				return false;
			else
			{
				return u.nick == this.nick
					&& u.user == this.user;
			}
		}

		/// <summary>
		/// Returns a string representation.
		/// </summary>
		/// <returns>Nick: [Nickname], User: [Username].</returns>
		public override string ToString()
		{
			return "Nick: " + nick + ", User: " + user;
		}

		/// <summary>
		/// Returns base.GetHashCode().
		/// </summary>
		/// <returns>A hash Code.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}
