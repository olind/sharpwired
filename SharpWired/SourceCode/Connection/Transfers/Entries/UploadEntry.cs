#region Information and licence agreements
/*
 * Bookmark.cs
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
using SharpWired.Model.Files;

namespace SharpWired.Connection.Transfers.Entries
{
    /// <summary>
    /// And upload entry in the Transfer Queue.
    /// </summary>
	public class UploadEntry: TransferEntry
	{
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="server"></param>
        /// <param name="pSourceFileName"></param>
        /// <param name="pDestinationFolder"></param>
		public UploadEntry(ConnectionManager cm, string pSourceFileName, FolderNode pDestinationFolder)
			:base() {}
	}
}
