using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SharpWired.Gui.Files {
    /// <summary>
    /// Base class for files views
    /// </summary>
    public class FilesGuiBase : SharpWiredGuiBase {

        delegate void ClearControlCallback(Control c);

        /// <summary>
        /// Clear the given control view. Thread safe.
        /// </summary>
        /// <param name="c">The control to clear</param>
        protected void ClearControl(Control c) {
            if (this.InvokeRequired) {
                ClearControlCallback callback = new ClearControlCallback(ClearControl);
                this.Invoke(callback, new object[] { c });
            } else {
                if (c is Tree)
                    ((Tree)c).Clear();
                else if (c is ListView)
                    ((ListView)c).Clear();
            }
        }
    }
}
