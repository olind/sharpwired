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

namespace SharpWired.Gui.Chat
{
    /// <summary>
    /// User control for the chat
    /// </summary>
    public partial class ChatUserControl : UserControl
    {
        SharpWiredModel model;
        private int altItemCounter = 0;

        private string AltItemBeginningHtml {
            get {
                if (altItemCounter % 2 == 0) {
                    altItemCounter++;
                    return "<div class=\"standard\">";
                }
                altItemCounter++;
                return "<div class=\"alternative\">";
            }
        }

        #region Initialization
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model"></param>
        public void Init(SharpWiredModel model, SharpWiredController controller)
        {
            this.model = model;
            //TODO: It's a bit aquard that this gui class initializes the 
            //      controller class. This is done since we place the controls
            //      in the Visual studio designer
            GuiChatController guiChatController = new GuiChatController(model, controller, this.chatControl, this.userListControl);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ChatUserControl()
        {
            InitializeComponent();
        }
        #endregion
    }
}
