using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model;
using SharpWired.Model.Files;

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
        }

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
