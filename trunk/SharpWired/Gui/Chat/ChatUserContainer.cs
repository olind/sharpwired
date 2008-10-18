#region Information and licence agreements
/*
 * ChatUserControl.cs
 * Created by Ola Lindberg, 2006-10-12
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model;
using SharpWired.Model.Users;
using SharpWired.Model.Messaging;
using SharpWired.Controller;

namespace SharpWired.Gui.Chat {
    /// <summary>
    /// User control for the chat
    /// </summary>
    public partial class ChatUserContainer : SharpWiredGuiBase {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="controller"></param>
        public override void Init(SharpWiredModel model, SharpWiredController controller) {
            base.Init(model, controller);
            chat.Init(model, controller);
            userList.Init(model, controller);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ChatUserContainer() {
            InitializeComponent();
        }
    }
}
