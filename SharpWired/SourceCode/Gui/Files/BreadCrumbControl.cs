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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model.Files;

namespace SharpWired.Gui.Files
{
    /// <summary>
    /// The path to the selected folder represented as browsable buttons
    /// </summary>
    public partial class BreadCrumbControl : UserControl
    {
        private GuiFilesController guiFilesController;

        //TODO: Adjust the length of the button to the length of the folder it represent

        /// <summary>
        /// Call this method when selected folder node has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="selectedNode"></param>
        public void OnFolderNodeChanged(object sender, WiredNodeArgs selectedNode)
        {
            //TODO: This event is triggered once for each level down in the three. ie. one for /f1 two for /f1/f11 and three for /f1/f11/f111
            PopulatePathButtons(selectedNode.Node);
        }

        /// <summary>
        /// Replace old path buttons with new!
        /// </summary>
        /// <param name="node"></param>
        private void PopulatePathButtons(FileSystemEntry node)
        {
            ClearFlowLayout();

            for (int i = 0; i<node.PathArray.Length; i++)
            {
                Button b = new Button();
                if (node.PathArray[i] != "")
                {
                    b.Text = node.PathArray[i];
                    b.Tag = CombineFilePath(node.PathArray, i);
                    b.MouseUp += new MouseEventHandler(button_MouseUp);
                }
                else
                {
                    b.Text = SharpWired.Utility.PATH_SEPARATOR;
                    b.Tag = SharpWired.Utility.PATH_SEPARATOR;
                    b.MouseUp += new MouseEventHandler(button_MouseUp);
                }

                AddButtonsToFlowLayout(b);
            }
        }

        /// <summary>
        /// Combines the entries in the given pathArray to a valid server path uptil the given dept
        /// </summary>
        /// <param name="pathArray">Array with entries representing a path</param>
        /// <param name="dept">The dept to where we want the returned string path to be limited</param>
        /// <returns></returns>
        private string CombineFilePath(String[] pathArray, int dept)
        {
            StringBuilder sb = new StringBuilder();
            if (dept > 0 && pathArray.Length > 0)
            {
                for (int i = 0; i <= dept; i++)
                {
                    sb.Append(pathArray[i]);
                    if (i != dept)
                    {
                        sb.Append(SharpWired.Utility.PATH_SEPARATOR);
                    }
                }
                return sb.ToString();
            }
            else
            {
                return SharpWired.Utility.PATH_SEPARATOR;
            }
        }


        #region Init
        /// <summary>
        /// Update the listview with the given list. Use this method to init the view once the 
        /// root node has been loaded from server.
        /// </summary>
        public void OnRootNodeInitialized(List<FileSystemEntry> nodes)
        {
            //FIXME: Unsubscribe from root node listening. 
            //The listener is connected in GuiFilesController 
            //and I have no idea for how to unsubscribe.
            //We should only redraw the root node the first time it's updated
            if (doRootNode)
            {
                Button b = new Button();
                b.Text = SharpWired.Utility.PATH_SEPARATOR;

                ClearFlowLayout(); //TODO: Must be thread safe
                AddButtonsToFlowLayout(b);
                doRootNode = false;
            }
        }
        bool doRootNode = true;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="guiFilesController"></param>
        public void Init(GuiFilesController guiFilesController)
        {
            this.guiFilesController = guiFilesController;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BreadCrumbControl()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// The mouse was clicked in this control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            guiFilesController.ChangeSelectedNode(this, ((Button)sender).Tag.ToString());
        }

        #region Thread safe flowlayout manipulation
        /// <summary>
        /// Thread safe add a button to the flow layout
        /// </summary>
        /// <param name="b"></param>
        private void AddButtonsToFlowLayout(Button b)
        {
            if (this.InvokeRequired)
            {
                AddButtonsToFlowLayoutCallback callback = new AddButtonsToFlowLayoutCallback(AddButtonsToFlowLayout);
                this.Invoke(callback, new object[] { b });
            }
            else
            {
                this.flowLayoutPanel1.Controls.Add(b);
            }
        }
        delegate void AddButtonsToFlowLayoutCallback(Button b);

        /// <summary>
        /// Thread safe clearing of the flow layout
        /// </summary>
        private void ClearFlowLayout()
        {
            if (this.InvokeRequired)
            {
                ClearFlowLayoutCallback callback = new ClearFlowLayoutCallback(ClearFlowLayout);
                this.Invoke(callback, new object[] { });
            }
            else
            {
                this.flowLayoutPanel1.Controls.Clear();
            }
        }
        delegate void ClearFlowLayoutCallback();

        #endregion
    }
}
