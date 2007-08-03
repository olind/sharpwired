using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Model.Files;
using System.Windows.Forms;

namespace SharpWired.Gui.Files
{
	/// <summary>
	/// This class keeps the parameters neede to populate the filetree from
	/// a callback.
	/// </summary>
	class TreePopulatorBag
	{
		private FolderNode rootNode;
		/// <summary>
		/// The root node from the model.
		/// </summary>
		public FolderNode RootNode
		{
			get { return rootNode; }
			set { rootNode = value; }
		}

		private TreeView treeView;

		/// <summary>
		/// The TreeView to populate.
		/// </summary>
		public TreeView TreeView
		{
			get { return treeView; }
			set { treeView = value; }
		}

		/// <summary>
		/// Creates.
		/// </summary>
		/// <param name="tree">The file tree.</param>
		/// <param name="root">The FolderNode that is the super root of the file tree model.</param>
		public TreePopulatorBag(TreeView tree, FolderNode root)
		{
			this.treeView = tree;
			this.rootNode = root;
		}
	}
}
