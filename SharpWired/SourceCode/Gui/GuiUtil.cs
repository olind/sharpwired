using System;
using System.Collections.Generic;
using System.Text;

namespace SharpWired.Gui {
    /// <summary>
    /// Util class for GUI
    /// </summary>
    public static class GuiUtil {
        
        /// <summary>
        /// Request or set the file destination for the CSS-file
        /// </summary>
        public static string CSSFilePath {
            //TODO: The destination to the CSS-file should be set in some other way
            get { return System.Environment.CurrentDirectory; }
        }
    }
}
