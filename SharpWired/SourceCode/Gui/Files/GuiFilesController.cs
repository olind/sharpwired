using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Model;
using SharpWired.Model.Files;

namespace SharpWired.Gui.Files
{
    /// <summary>
    /// A controller class for the file handling in GUI. 
    ///  * Updates the model.
    ///  * Holds a referense to the currently selected node
    /// </summary>
    public class GuiFilesController
    {
        private LogicManager logicManager;
        private FileSystemEntry selectedNode;

        /// <summary>
        /// Get the selected node.
        /// </summary>
        public FileSystemEntry SelectedNode
        {
            get { return selectedNode; }
        }

        /// <summary>
        /// Change the selected node. 
        /// When the selected node is set an event is raised.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="selectedNode"></param>
        public void ChangeSelectedNode(object sender, FileSystemEntry selectedNode)
        {
            this.selectedNode = selectedNode;

            if (selectedNode is FolderNode)
            {
                WiredNodeArgs nodeArgs = new WiredNodeArgs(selectedNode);
                if (SelectedFolderNodeChangedEvent != null)
                {
                    SelectedFolderNodeChangedEvent(sender, nodeArgs);
                }

                this.logicManager.FileListingHandler.ReloadFileList((FolderNode)selectedNode);
            }
            else if (selectedNode is FileNode)
            {
                Console.WriteLine("TODO: Dealing with file nodes are not implemented");
            }
        }

        /// <summary>
        /// Call to request a download of a node
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="node">The node where download should begin. 
        ///     All subnodes will be requested as well.
        ///     Note: All subnodes might not be loaded from model yet
        /// </param>
        public void RequestNodeDownload(object sender, FileSystemEntry node)
        {
            logicManager.FileTransferHandler.EnqueEntry(node);
        }

        /// <summary>
        /// Raised when the selected node has changed
        /// </summary>
        public event EventHandler<WiredNodeArgs> SelectedFolderNodeChangedEvent;

        /// <summary>
        /// Changes the selected node to be the root node
        /// </summary>
        /// <param name="sender"></param>
        public void ChangeSelectedNodeToRootNode(object sender)
        {
            ChangeSelectedNode(sender, this.logicManager.FileListingHandler.FileListingModel.RootNode);
        }

        /// <summary>
        /// Constructor - Inits the other GUI-classes
        /// </summary>
        /// <param name="logicManager"></param>
        /// <param name="fileTreeControl"></param>
        /// <param name="fileDetailsControl"></param>
        public GuiFilesController(LogicManager logicManager, 
            FileTreeControl fileTreeControl, FileDetailsControl fileDetailsControl, PathButtonControl pathButtonControl)
        {
            this.logicManager = logicManager;
            fileTreeControl.Init(logicManager, this);
            fileDetailsControl.Init(this);
            pathButtonControl.Init(this);
            

            // Attach listeners to other GUI files
            this.SelectedFolderNodeChangedEvent+=new EventHandler<WiredNodeArgs>(fileDetailsControl.OnFolderNodeChanged);
            this.SelectedFolderNodeChangedEvent += new EventHandler<WiredNodeArgs>(pathButtonControl.OnFolderNodeChanged);
            logicManager.FileListingHandler.FileModelUpdatedEvent += new FileListingHandler.FileModelUpdatedDelegate(fileTreeControl.OnNewNodesAdded);
            // To get the initial listing in the details view
            logicManager.FileListingHandler.FileModelUpdatedEvent+=new FileListingHandler.FileModelUpdatedDelegate(fileDetailsControl.OnRootNodeInitialized);
            logicManager.FileListingHandler.FileModelUpdatedEvent += new FileListingHandler.FileModelUpdatedDelegate(pathButtonControl.OnRootNodeInitialized);
        }
    }
}
