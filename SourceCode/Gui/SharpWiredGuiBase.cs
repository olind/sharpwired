#region Information and licence agreements
/*
 * SharpWiredGuiBase.cs 
 * Created by Ola Lindberg, 2008-07-09
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
using SharpWired.Model;
using SharpWired.Controller;
using System.Diagnostics;

namespace SharpWired.Gui {
    /// <summary>
    /// Base class for SharpWired GUI files. Sets references to 
    /// shared resources (e.g. model and controller) when inited.
    /// </summary>
    public class SharpWiredGuiBase : UserControl { //I wanted to make this class abstract but then can'transfer the visual studio designer handle it...

        protected SharpWiredController controller;
        protected SharpWiredModel model;

        private delegate void ToggleWindowsFormsControlCallback(Control control);
        protected delegate void UpdateDelegate();

        public virtual void Init(SharpWiredModel model, SharpWiredController controller) {
            this.model = model;
            this.controller = controller;

            model.Connected += OnConnected;
        }

        private void OnConnected(Server s) {
            s.Offline += OnOffline;
            s.Online += OnOnline;
        }

        protected virtual void OnOnline() { }
        protected virtual void OnOffline() { }

        protected void ToggleWindowsFormControl(Control control) {
            if (this.InvokeRequired) {
                ToggleWindowsFormsControlCallback callback 
                    = new ToggleWindowsFormsControlCallback(ToggleWindowsFormControl);
                this.Invoke(callback, new object[] { control });
            } else {
                control.Enabled = !control.Enabled;
            }
        }

        protected void UpdateControl(UpdateDelegate update) {
            if (this.InvokeRequired)
                this.Invoke(update, new object[] { });
            else
                update();
        }
    }
}
