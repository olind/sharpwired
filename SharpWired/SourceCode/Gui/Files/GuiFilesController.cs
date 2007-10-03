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
        /// TODO: It might be more logical to have a ChangeSelectedFolder(object sender, FolderNode selectedFolder). Not really sure.
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
            else
            {
                throw new Exception("Dealing with file nodes are not implemented"); //TODO: Deal with file nodes if neccessary
            }
        }

        public event EventHandler<WiredNodeArgs> SelectedFolderNodeChangedEvent;

        /// <summary>
        /// Constructor - Inits the other GUI-classes
        /// </summary>
        /// <param name="logicManager"></param>
        /// <param name="fileTreeControl"></param>
        /// <param name="fileDetailsControl"></param>
        public GuiFilesController(LogicManager logicManager, 
            FileTreeControl fileTreeControl, FileDetailsControl fileDetailsControl)
        {
            //TODO: It would be nice if the inits for this nodes wouldnt be done here..
            //It feels more logic if the GuiFilesController was responsible for creating 
            //the FilesUserControll and not the other way around
            this.logicManager = logicManager;
            fileTreeControl.Init(logicManager, this);
            fileDetailsControl.Init(this);
        }
    }
}
