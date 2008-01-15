#region Information and licence agreements
/*
 * SharpWiredClientInfo.cs 
 * Created by Ola Lindberg, 2008-01-15
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

namespace SharpWired
{
    /// <summary>
    /// Holds client and protocol information
    /// </summary>
    public static class SharpWiredClientInfo
    {
        // See 2.6 Version Strings in the Wired Protocol specification for more information
        private static double protocolVersion = 1.1;
        private static string clientName = "SharpWired";
        private static string architecture = "Intel X86"; //TODO: Read value
        private static string osVersion = "2002"; //TODO: Read value (version number)
        private static string osRelease = "Win32"; //TODO: Read value
        private static string libVersion = ".Net2.0"; //TODO: What should we use this for?
        private static string appVersion = clientName + "/0.1-Pre" + Utility.SP + "(" + Os + ")" + Utility.SP + "(" + libVersion + ")";

        /// <summary>
        /// Gets the Wired protocol version Sharpwired is using.
        /// </summary>
        public static double ProtocolVersion
        {
            get { return protocolVersion; }
        }

        /// <summary>
        /// Gets the name for this client.
        /// </summary>
        public static string ClientName
        {
            get { return clientName; }
        }

        /// <summary>
        /// Gets the app version for this client.
        /// </summary>
        public static string AppVersion
        {
            get { return appVersion; }
        }

        /// <summary>
        /// Get the operative system info string
        /// </summary>
        public static string Os
        {
            get {
                StringBuilder os = new StringBuilder();
                os.Append(osRelease + "; " + osVersion + "; " + architecture);
                return os.ToString(); 
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        static SharpWiredClientInfo()
        {
        }
    }
}
