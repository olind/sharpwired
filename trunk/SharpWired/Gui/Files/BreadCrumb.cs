#region Information and licence agreements
/*
 * BreadCrumbControl.cs
 * Created by Ola Lindberg, 2007-11-09
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
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SharpWired.Controller;
using SharpWired.Model;
using SharpWired.Model.Files;
using SharpWired.Gui.Resources.Icons;

namespace SharpWired.Gui.Files {
    /// <summary>
    /// The destination to the selected folder represented as browsable buttons
    /// </summary>
    public partial class BreadCrumb : SharpWiredGuiBase {

        #region Constructors
        public BreadCrumb() {
            InitializeComponent();
        }
        #endregion

        delegate void AddButtonsToFlowLayoutCallback(Button b);
        delegate void ClearFlowLayoutCallback();

        public delegate void SelectFolderNodeChangeDelegate(INode node);
        public event SelectFolderNodeChangeDelegate SelectedFolderChanged;
        
        public void OnSelectedFolderNodeChanged(Folder folder) { 
            PopulatePathButtons(folder);
        }

        private void button_MouseUp(object sender, MouseEventArgs e) {
            if (SelectedFolderChanged != null) {
                INode n = (INode)((Button)sender).Tag;
                SelectedFolderChanged(n);
            }
        }

        private void PopulatePathButtons(Folder node) {
            ClearFlowLayout();

            string[] pathArray = node.Path.Split('/');

            for (int i = 0; i < pathArray.Length; i++) {
                Button b = new Button();
                if (pathArray[i] != "") {
                    b.Text = pathArray[i];
                } else {
                    IconHandler iconHandler = IconHandler.Instance;
                    b.Image = iconHandler.GoHome;
                }

                b.MouseUp += new MouseEventHandler(button_MouseUp);
                //b.Tag = Model.Server.FileListingModel.GetNode(CombineFilePath(pathArray, i));
                //todo: file
                b.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                b.AutoSize = true;
                b.Padding = new Padding(2);
                b.Margin = new Padding(0, 0, 5, 0);
                AddButtonsToFlowLayout(b);
            }
        }

        private void AddButtonsToFlowLayout(Button b) {
            if (this.InvokeRequired) {
                AddButtonsToFlowLayoutCallback callback = new AddButtonsToFlowLayoutCallback(AddButtonsToFlowLayout);
                this.Invoke(callback, new object[] { b });
            } else {
                this.breadCrumbsFlowLayoutPanel.Controls.Add(b);
            }
        }

        private void ClearFlowLayout() {
            if (this.InvokeRequired) {
                ClearFlowLayoutCallback callback = new ClearFlowLayoutCallback(ClearFlowLayout);
                this.Invoke(callback, new object[] { });
            } else {
                this.breadCrumbsFlowLayoutPanel.Controls.Clear();
            }
        }
        
        /// <summary>
        /// Combines the entries in the given pathArray to a valid server destination uptil the given dept
        /// </summary>
        /// <param name="pathArray">Array with entries representing a destination</param>
        /// <param name="dept">The dept to where we want the returned string destination to be limited</param>
        /// <returns></returns>
        private string CombineFilePath(String[] pathArray, int dept) {
            StringBuilder sb = new StringBuilder();
            if (dept > 0 && pathArray.Length > 0) {
                for (int i = 0; i <= dept; i++) {
                    sb.Append(pathArray[i]);
                    if (i != dept)
                        sb.Append(SharpWired.Utility.PATH_SEPARATOR);
                }
                return sb.ToString();
            } else {
                return SharpWired.Utility.PATH_SEPARATOR;
            }
        }
    }
}
