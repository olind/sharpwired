using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model.Files;
using System.Diagnostics;

namespace SharpWired.Gui.Files {
    public class WiredTreeNode : TreeNode {
        protected delegate void Func();

        public INode ModelNode { get; set; }

        public WiredTreeNode(INode modelNode) {
            ModelNode = modelNode;
            Text = modelNode.Name;
            Name = modelNode.FullPath; // Used only for searching the tree.
            PopulateFolder();
            modelNode.Updated += OnUpdated;
        }

        public void OnUpdated(INode modelNode) {
            PopulateFolder();
        }

        private void PopulateFolder() {
            if (ModelNode is Folder) {
                Func del = delegate {
                    Folder folder = ModelNode as Folder;

                    Debug.WriteLine("GUI:Tree -> Redrawing: " + folder.FullPath);

                    this.Nodes.Clear();

                    foreach (INode child in folder.Children)
                        if (child is Folder)
                            this.Nodes.Add(new WiredTreeNode(child));
                };

                //The TreeView is null when the current node has not been added to a TreeView
                if (this.TreeView != null) {
                    this.TreeView.Invoke(del); //Thread safe required when the node is added to a TreeView
                } else {
                    del(); //When the node is not added to a TreeView we don't need to be thread safe
                }
            }
        }
    }
}
