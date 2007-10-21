#region Information and licence agreements
/*
 * Server.cs
 * Created by Ola Lindberg, 2006-07-10
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
	/// Represents a Server with adress and port.
	/// </summary>
    [Serializable]
    public class Server
	{

		#region Properties
		private int serverPort;
		/// <summary>
		/// Get/Set the Port.
		/// </summary>
        public int ServerPort
        {
            get { return serverPort; }
            set { serverPort = value; }
        }

        private string machineName;
		/// <summary>
		/// Get/Set the server machine name.
		/// </summary>
		public string MachineName
        {
            get { return machineName; }
            set { machineName = value; }
        }

        private string serverName;
		/// <summary>
		/// Get/Set the server name; domain or IP.
		/// </summary>
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }
		#endregion


		#region Constructors
		/// <summary>
        /// Constructs.
        /// </summary>
        /// <param name="serverPort">The port to use.</param>
        /// <param name="machineName">The servers computer name.</param>
        /// <param name="serverName">The domain name or IP adress.</param>
        public Server(int serverPort, string machineName, string serverName)
        {
            ServerPort = serverPort;
            MachineName = machineName;
            ServerName = serverName;
        }

		/// <summary>
		/// Parameterless constructor for de-serialization.
		/// </summary>
		private Server()
		{
		}
		#endregion


		#region Overrides
		/// <summary>
		/// Compares the server name, the machine name and the port using '=='.
		/// </summary>
		/// <param name="obj">The object to compare with.</param>
		/// <returns>T/F.</returns>
		public override bool Equals(object obj)
		{
			Server s = obj as Server;
			if (s == null)
				return false;

			return s.machineName == this.machineName
				&& s.serverName == this.serverName
				&& s.serverPort == this.serverPort;
		}

		/// <summary>
		/// Returns a string representing this Server.
		/// </summary>
		/// <returns>[MachineName]-[ServerName]:[Port]</returns>
		public override string ToString()
		{
			return machineName + " - " + serverName + " : " + serverPort;
		}

		/// <summary>
		/// Return base.GetHashCode().
		/// </summary>
		/// <returns>A hash code.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}