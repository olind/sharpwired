#region Information and licence agreements
/**
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
    [Serializable]
    public class Server
    {
        private int serverPort;

        public int ServerPort
        {
            get { return serverPort; }
            set { serverPort = value; }
        }

        private string machineName;

        public string MachineName
        {
            get { return machineName; }
            set { machineName = value; }
        }

        private string serverName;

        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }
	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverPort"></param>
        /// <param name="machineNamen"></param>
        /// <param name="serverName"></param>
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

		/// <summary>
		/// Compares the server name, the machine name and the port using '=='.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Server))
				return false;
			else
			{
				Server s = obj as Server;
				return s.machineName == this.machineName
					&& s.serverName == this.serverName
					&& s.serverPort == this.serverPort;
			}
		}
    }
}
