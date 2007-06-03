#region Information and licence agreements
/**
 * FileUserControl.cs 
 * Created by Ola Lindberg, 2007-05-10
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
        private LogicManager logicManager;
        string output = ""; // Keeps the output through itterations

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
            PopulateTreeView(rootTreeView, null, superRootNode, 0);
        }


        #region TreeNode

        /// <summary>
        /// Populates the tree view. NOTE! This implementation does not currently work.
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="parentNode"></param>
        /// <param name="modelNodes"></param>
        /// <param name="folderCount"></param>
        private void PopulateTreeView(TreeView tree, WiredTreeNode parentNode, FolderNode modelNodes, int folderCount)
        {
            if (parentNode == null) // Nodes should be added to the root of the treeview
            {
                foreach (FileSystemEntry fse in modelNodes.Children)
                {
                    WiredTreeNode newNode = new WiredTreeNode(fse);
                    AddTreeNodeToTreeView(tree, newNode); // runs: tree.Nodes.Add(newNode);

                    folderCount++;

                    if (fse.HasChildren() && fse is FolderNode)
                    {
                        ModelNodeToTreeNode(tree, newNode, ((FolderNode)fse), folderCount); //fse has children, add those children to the tree
                    }
                }
            }
            else
            {
                Console.WriteLine("ParentNode: '" + parentNode.ModelNode.Name + "' ParentNode.ParentPath: " + parentNode.ModelNode.ParentPath + " Count: " + (folderCount-1));
                Console.WriteLine("ModelNode: '" + modelNodes.Name + "' ModelNode.ParentPath: " + modelNodes.ParentPath + " Count: " + (folderCount-1));
                    
                folderCount++; //FIXME: I'm having a hard time finding out where in the tree the new nodes should be added.. Have no good approach for this.
                WiredTreeNode newNode = new WiredTreeNode(modelNodes);
                AddTreeNodeToTreeNode(tree, parentNode, newNode, folderCount-1);
            }
        }

        #endregion
        #region GUI-thread-safe callback methods

        /// <summary>
        /// Add the given node to the given parent node in the given tree
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="parentNode"></param>
        /// <param name="node"></param>
        private void AddTreeNodeToTreeNode(TreeView tree, WiredTreeNode parentNode, WiredTreeNode node, int folderIndex)
        {
            if (this.InvokeRequired)
            {
                AddTreeNodeToTreeNodeCallback addTreeNodeToTreeNodeCallback = new AddTreeNodeToTreeNodeCallback(AddTreeNodeToTreeNode);
                this.Invoke(addTreeNodeToTreeNodeCallback, new object[] { tree, parentNode, node, folderIndex });
            }
            else
            {
                tree.Nodes[folderIndex].Nodes.Add(node);
            }
        }
        delegate void AddTreeNodeToTreeNodeCallback(TreeView tree, WiredTreeNode parentNode, WiredTreeNode node, int folderIndex);

        /// <summary>
        /// Add the given node to the given tree. Creates a GUI thread safe callback.
        /// </summary>
        /// <param name="tree">The tree where the given node should be added to.</param>
        /// <param name="node">The node to add to the tree.</param>
        private void AddTreeNodeToTreeView(TreeView tree, WiredTreeNode node) 
        {
            if (this.InvokeRequired)
            {
                AddTreeNodeToTreeViewCallback addTreeNodeToTreeViewCallback = new AddTreeNodeToTreeViewCallback(AddTreeNodeToTreeView);
                this.Invoke(addTreeNodeToTreeViewCallback, new object[] { tree,node });
            }
            else
            {
                
                tree.Nodes.Add(node);
            }
        }
        delegate void AddTreeNodeToTreeViewCallback(TreeView tree, WiredTreeNode node);

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
        }

        public FilesUserControl()
        {
            InitializeComponent();
        }
        #endregion
    }
}
