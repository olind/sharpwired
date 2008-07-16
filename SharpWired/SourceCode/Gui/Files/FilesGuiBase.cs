using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SharpWired.Gui.Files {
    /// <summary>
    /// Base class for files views
    /// </summary>
    public class FilesGuiBase : SharpWiredGuiBase {

        delegate void ClearTreeViewCallback(Control c);

        /// <summary>
        /// Clear the given control view. Thread safe.
        /// </summary>
        /// <param name="c">The control to clear</param>
        protected void ClearControl(Control c) {
            if (this.InvokeRequired) {
                ClearTreeViewCallback clearTreeViewCallback = new ClearTreeViewCallback(ClearControl);
                this.Invoke(clearTreeViewCallback, new object[] { c });
            } else {
                if (c is TreeView)
                    ((TreeView)c).Nodes.Clear();
                else if (c is ListView)
                    ((ListView)c).Clear();
            }
        }
    }
}
