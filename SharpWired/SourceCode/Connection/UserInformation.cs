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
    [Serializable]
    public class UserInformation
    {
		/// <summary>
		/// Parameterless constructor for serialization.
		/// </summary>
		private UserInformation()
		{
		}

        public UserInformation(string nick, string userName, string password)
        {
            this.Nick = nick;
            this.UserName = userName;
            this.Password = password;
        }

        private string nick;

        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }

        private string user;

        public string UserName
        {
            get { return user; }
            set { user = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

		/// <summary>
		/// Compares the user name and nick using '=='. The password isn't compared.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (!(obj is UserInformation))
				return false;
			else
			{
				UserInformation u = obj as UserInformation;
				return u.nick == this.nick
					&& u.user == this.user;
			}
		}
    }
}
