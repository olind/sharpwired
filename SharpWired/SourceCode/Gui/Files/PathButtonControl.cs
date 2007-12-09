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
    public partial class PathButtonControl : UserControl
    {
        private GuiFilesController guiFilesController;

        /// <summary>
        /// Call this method when selected folder node has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="selectedNode"></param>
        public void OnFolderNodeChanged(object sender, WiredNodeArgs selectedNode)
        {
            PopulatePathButtons(selectedNode.Node);
        }

        /// <summary>
        /// Replace old path buttons with new!
        /// </summary>
        /// <param name="node"></param>
        private void PopulatePathButtons(FileSystemEntry node)
        {
            this.flowLayoutPanel1.Controls.Clear();

            for (int i = 0; i<node.PathArray.Length; i++)
            {
                Button b = new Button();
                if (node.PathArray[i] != "")
                {
                    b.Text = node.PathArray[i];
                }
                else
                {
                    b.Text = "/";
                }
                
                //TODO: Make buttons clickable

                this.flowLayoutPanel1.Controls.Add(b);
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
                b.Text = "/";

                this.flowLayoutPanel1.Controls.Clear();
                this.flowLayoutPanel1.Controls.Add(b);
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
        public PathButtonControl()
        {
            InitializeComponent();
        }
        #endregion
    }
}
