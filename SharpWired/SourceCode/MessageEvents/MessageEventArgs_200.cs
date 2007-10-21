#region Information and licence agreements
/*
 * MessageEventArgs_200.cs 
 * Created by Ola Lindberg, 2006-09-28
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

namespace SharpWired.MessageEvents
{
    public class MessageEventArgs_200 : MessageEventArgs
    {
        private string appVersion; // TODO change type of this
        private string protocolVersion; // TODO Change type of this
        private string serverName;
        private string serverDescription;
        private DateTime startTime;
        private int filesCount;
        private int filesSize;

        public string AppVersion
        {
            get
            {
                return appVersion;
            }
        }

        public string ProtocolVersion
        {
            get
            {
                return protocolVersion;
            }
        }

        public string ServerName
        {
            get
            {
                return serverName;
            }
        }

        public string ServerDescription
        {
            get
            {
                return serverDescription;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
        }

        public int FilesCount
        {
            get
            {
                return filesCount;
            }
        }

        public int FilesSize
        {
            get
            {
                return filesSize;
            }
        }

        public MessageEventArgs_200(int messageId, string messageName, string appVersion, string protocolVersion, 
            string serverName, string serverDescription, DateTime startTime, int filesCount, int filesSize)
            : base(messageId, messageName)
        {
            this.appVersion = appVersion;
            this.protocolVersion = protocolVersion;
            this.serverName = serverName;
            this.serverDescription = serverDescription;
            this.startTime = startTime;
            this.filesCount = filesCount;
            this.filesSize = filesSize;
        }
    }
}
