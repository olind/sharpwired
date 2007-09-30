#region Information and licence agreements
/**
 * FileDetailsControl.cs
 * Created by Ola Lindberg and Peter Holmdahl, 2007-09-29
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
using SharpWired.Gui.Resources.Icons;

namespace SharpWired.Gui.Files
{
    /// <summary>
    /// Represents a detail view of the currently selected nodes
    /// </summary>
    public partial class FileDetailsControl : UserControl
    {
        private FileTreeControl fileTreeControl;

        #region Listeners from FileTree
        /// <summary>
        /// When this is received we subscribe to an event that triggers when the files has been updated from the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fileTreeControl_FolderSelectedEvent(object sender, WiredTreeNodeArgs e)
        {
            FolderNode fn = (FolderNode)e.Node.ModelNode;
            fn.FolderNodeUpdatedEvent += new FolderNode.FolderNodeUpdated(fn_FolderNodeUpdatedEvent);
        }

        void fn_FolderNodeUpdatedEvent(FolderNode updatedNode)
        {
            //TODO: Unsubscribe from listening
            UpdateListView(updatedNode);            
        }

        private void UpdateListView(FolderNode updatedNode)
        {
            if (this.InvokeRequired)
            {
                UpdateListViewCallback updateListViewCallback = new UpdateListViewCallback(UpdateListView);
                this.Invoke(updateListViewCallback, new object[] { updatedNode });
            }
            else
            {
                listView1.Items.Clear();
                List<FileSystemEntry> children = (updatedNode as FolderNode).Children;
                foreach (FileSystemEntry child in children)
                {
                    WiredListNode wln = new WiredListNode(child);
                    wln.ImageIndex = wln.IconIndex;
                    wln.StateImageIndex = wln.IconIndex;
                    this.listView1.Items.Add(wln);
                }
            }
        }
        delegate void UpdateListViewCallback(FolderNode updatedNode);
        #endregion

        #region Initialization
        /// <summary>
        /// Initiates this control
        /// </summary>
        /// <param name="fileTreeControl"></param>
        public void Init(FileTreeControl fileTreeControl)
        {
            this.fileTreeControl = fileTreeControl;
            fileTreeControl.FolderSelectedEvent += new EventHandler<WiredTreeNodeArgs>(fileTreeControl_FolderSelectedEvent);

            ImageList fileViewIcons = new ImageList();
            fileViewIcons.ColorDepth = ColorDepth.Depth32Bit;
            IconHandler iconHandler = new IconHandler();

            try
            {
                fileViewIcons.Images.Add(iconHandler.FolderClosed);
                fileViewIcons.Images.Add(iconHandler.File);
            }
            catch (Exception e)
            {
                Console.WriteLine("FileUserControl.cs | Failed to add images for rootTreView. Exception: " + e); //TODO: Throw exception
            }

            listView1.SmallImageList = fileViewIcons;
            listView1.LargeImageList = fileViewIcons;
            listView1.View = View.LargeIcon;          
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FileDetailsControl()
        {
            InitializeComponent();
        }
        #endregion
    }
}
