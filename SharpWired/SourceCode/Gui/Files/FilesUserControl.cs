#region Information and licence agreements
/**
 * FileUserControl.cs 
 * Created by Ola Lindberg and Peter Holmdahl, 2007-05-10
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
using SharpWired.Model.Files;
using System.Collections;

namespace SharpWired.Gui.Files
{
    /// <summary>
    /// A test class for printing file listing to screen
    /// TODO: Replace this by the real file tree
    /// </summary>
    public partial class FilesUserControl : UserControl
    {
        #region Variables
        private LogicManager logicManager;
        string output = ""; // Keeps the output through itterations for temporary file listing. Can be removed once the textbox for file listing is obsoleted
        private string iconFilePath;
        #endregion

        #region Properties

        /// <summary>
        /// Get or set the file path for the CSS-file
        /// </summary>
        private string IconFilePath
        {
            get
            {
                if (iconFilePath == null)
                    return System.Environment.CurrentDirectory + "\\GUI\\Icons\\"; //TODO: The path to the CSS-file should probably be set in some other way
                return iconFilePath;
            }
            set
            {
                iconFilePath = value;
            }
        }

        #endregion

        #region Temp

        private void button1_Click(object sender, EventArgs e)
        {
            WriteTextToTexBox(textBox1, "Reloading file listing");
            logicManager.FileListingHandler.ReloadFileList(textBox2.Text);
        }

        void FileListingModel_FileListingDoneEvent(FolderNode superRootNode)
        {
            WriteTextToTexBox(textBox1, "");
            output = "";
            WriteTextToTexBox(textBox1, GetFileTreeOutput(superRootNode));

            ClearTreeView(rootTreeView);
			PopulateFileTree(rootTreeView, superRootNode);
        }

        #endregion 

        #region TreeNode
        /// <summary>
		/// Populates the filetree from the given super root.
		/// </summary>
		/// <remarks>Uses callback if necessary.</remarks>
		/// <param name="rootTreeView">The TreeView to populate.</param>
		/// <param name="superRootNode">The root node from the model to populate from.</param>
		private void PopulateFileTree(TreeView rootTreeView, FolderNode superRootNode)
		{
			if (InvokeRequired)
			{
				PopulateFileTreeCallBack callback = new PopulateFileTreeCallBack(PopulateFileTree);
				Invoke(callback, new object[] { rootTreeView, superRootNode });
				return;
			}

			if (rootTreeView.Nodes.Count > 0)
				rootTreeView.Nodes.Clear();

			// Just to put a name on the root in the filetree. alternatively,
			// the tree can skip the server root node, and have several nodes
			// at "level 0" in the tree.
            if (string.IsNullOrEmpty(superRootNode.Name))
                superRootNode.Name = "Server";
			rootTreeView.Nodes.Add(MakeFileNode(superRootNode));

			// Expand all nodes make the test easy and nice.
			rootTreeView.ExpandAll();
		}
        delegate void PopulateFileTreeCallBack(TreeView treeView, FolderNode rootNode);

		/// <summary>
		/// Takes a FileSystemEntry and build a subtree from that,
		/// returnung the WiredTreeNode that represent the FSE given.
		/// </summary>
		/// <param name="fileSystemEntry">The FSE to build a WiredNode from (a subtree).</param>
		/// <returns>The WiredNode that represents the given FSE.</returns>
		private WiredTreeNode MakeFileNode(FileSystemEntry fileSystemEntry)
		{
			if (fileSystemEntry != null)
			{
				WiredTreeNode node = new WiredTreeNode(fileSystemEntry);
                node.ImageIndex = node.IconIndex;
				if (fileSystemEntry is FolderNode
					&& fileSystemEntry.HasChildren())
				{
					List<FileSystemEntry> children = (fileSystemEntry as FolderNode).Children;
					foreach (FileSystemEntry child in children)
					{
						node.Nodes.Add(MakeFileNode(child));
					}
				}
				return node;
			}
			return new WiredTreeNode("The given node was null, but I didn't feel like trowing an exception!");
		}
        
        /// <summary>
        /// Clears the given tree view.
        /// </summary>
        /// <param name="tree"></param>
        private void ClearTreeView(TreeView tree)
        {
            if (this.InvokeRequired)
            {
                ClearTreeViewCallback clearTreeViewCallback = new ClearTreeViewCallback(ClearTreeView);
                this.Invoke(clearTreeViewCallback, new object[] { tree });
            }
            else
            {
                tree.Nodes.Clear();
            }
        }
        delegate void ClearTreeViewCallback(TreeView tree);

        #endregion

        #region TextBox

        /// <summary>
        /// Generates a string with all folders
        /// </summary>
        /// <param name="node"></param>
        private string GetFileTreeOutput(FolderNode node)
        {
            foreach (FileSystemEntry fn in node.Children)
            {
                if (fn is FolderNode)
                {
                    if (((FolderNode)fn).Children != null)
                    {
                        output += "FolderNode: " + fn.Path + System.Environment.NewLine;
                        GetFileTreeOutput((FolderNode)fn);
                    }
                    else
                    {
                        Console.Write("FolderNode " + fn.Path + " has no childrens");
                    }
                }
                else if (fn is FileNode)
                {
                    output += "  FileNode: " + fn.Path + System.Environment.NewLine;
                }
            }
            return output;
        }

        private void WriteTextToTexBox(TextBox textBoxToPopulate, string textToPopulate)
        {
            if (this.InvokeRequired)
            {
                WriteTextToTextBoxCallback writeTextToTextBoxCallback = new WriteTextToTextBoxCallback(WriteTextToTexBox);
                this.Invoke(writeTextToTextBoxCallback, new object[] { textBoxToPopulate, textToPopulate });
            }
            else
            {
                textBox1.Text = textToPopulate;
            }
        }
        delegate void WriteTextToTextBoxCallback(TextBox textBoxToPopulate, string textToPopulate);

        #endregion

        #region Initialization
        public void Init(LogicManager logicManager)
        {
            this.logicManager = logicManager;
            logicManager.FileListingHandler.FileListingModel.FileListingDoneEvent += new FileListingModel.FileListingDoneDelegate(FileListingModel_FileListingDoneEvent);

            //TODO: Add upload + drop box icons ass well
            ImageList rootTreeViewIcons = new ImageList();
            Image folder = (Image)Bitmap.FromFile(IconFilePath + "folder.png");
            Image file = (Image)Bitmap.FromFile(IconFilePath + "text-x-generic.png");
            rootTreeViewIcons.Images.Add(folder);
            rootTreeViewIcons.Images.Add(file);
            rootTreeView.ImageList = rootTreeViewIcons;
        }

        public FilesUserControl()
        {
            InitializeComponent();
        }
        #endregion
    }
}
