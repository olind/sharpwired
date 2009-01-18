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
    public partial class BreadCrumb : SharpWiredGuiBase, IFilesView {

        delegate void AddButtonsToFlowLayoutCallback(Button b);
        delegate void ClearFlowLayoutCallback();

        public event NodeSelectedDelegate NodeSelected;

        public BreadCrumb() {
            InitializeComponent();
        }

        public void SetCurrentNode(INode node) {
            if (node is Folder)
                PopulatePathButtons(node as Folder);
        }

        void PopulatePathButtons(Folder node) {
            ClearFlowLayout();
            
            List<string> path;

            if(node.FullPath == "/") {
                path = new List<string>();
                path.Add("");
            } else {
                path = new List<string>(node.FullPath.Split('/'));
            }

            foreach (string folder in path) {
                Button b = new Button();
                if (folder != "") {
                    b.Text = folder;
                } else {
                    IconHandler iconHandler = IconHandler.Instance;
                    b.Image = iconHandler.GoHome;
                }

                b.MouseUp += new MouseEventHandler(OnMouseUp);
                //b.Tag = Model.Server.FileListingModel.GetNode(CombineFilePath(path, i));
                //todo: file
                b.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                b.AutoSize = true;
                b.Padding = new Padding(2);
                b.Margin = new Padding(0, 0, 5, 0);
                AddButtonsToFlowLayout(b);
            }
        }

        void AddButtonsToFlowLayout(Button b) {
            if (this.InvokeRequired) {
                AddButtonsToFlowLayoutCallback callback = new AddButtonsToFlowLayoutCallback(AddButtonsToFlowLayout);
                this.Invoke(callback, new object[] { b });
            } else {
                this.breadCrumbsFlowLayoutPanel.Controls.Add(b);
            }
        }

        void ClearFlowLayout() {
            if (this.InvokeRequired) {
                ClearFlowLayoutCallback callback = new ClearFlowLayoutCallback(ClearFlowLayout);
                this.Invoke(callback, new object[] { });
            } else {
                this.breadCrumbsFlowLayoutPanel.Controls.Clear();
            }
        }
        
        string CombineFilePath(String[] pathArray, int dept) {
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

        void OnMouseUp(object sender, MouseEventArgs e) {
            if(NodeSelected != null) {
                INode n = (INode)((Button)sender).Tag;
                NodeSelected(n);
            }
        }
    }
}
