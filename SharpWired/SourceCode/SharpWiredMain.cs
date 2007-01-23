#region Information and licence agreements
/**
 * SharpWiredMain.cs 
 * Created by Ola Lindberg, 2006-07-23
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
using System.Windows.Forms;

using SharpWired.Connection;
using SharpWired;
using SharpWired.Connection.Bookmarks;

namespace SharpWired
{
    class SharpWiredMain
    {
        private Gui.SharpWiredForm publicChat;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

			// We run the public chat window
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			SharpWiredMain sharpWired = new SharpWiredMain();
			sharpWired.publicChat = new global::SharpWired.Gui.SharpWiredForm();
			Application.Run(sharpWired.publicChat);
        }
    }
}
