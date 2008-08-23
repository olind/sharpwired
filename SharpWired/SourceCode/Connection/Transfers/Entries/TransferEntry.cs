#region Information and licence agreements
/*
 * TransferEntry.cs
 * Created by Peter Holmdahl, 2007-11-03
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
using SharpWired.Connection;
using System.Diagnostics;
using System.IO;
using SharpWired.Connection.Sockets;
using SharpWired.Model.Files;

namespace SharpWired.Connection.Transfers.Entries {
    /// <summary>
    /// And entry in the Transfer Queue. Consists of a from location, a to location and a server.
    /// </summary>
    public class TransferEntry {
        protected ConnectionManager connectionManager;
        Int64 offset;
        string id;

        protected TransferEntry(ConnectionManager connectionManager) {
            this.connectionManager = connectionManager;
        }

        public Int64 Offset {
            get { return offset; }
            set { offset = value; }
        }

        public string Id {
            get { return id; }
            set { id = value; }
        }
    }
}