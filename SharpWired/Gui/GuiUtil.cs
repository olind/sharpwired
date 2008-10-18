using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

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

        public static string FormatByte(long bytes) {
            return FormatByte(bytes, "h");
        }

        public static string FormatByte(long bytes, string format) {
            //TODO: Make format global for whole application?
            NumberFormatInfo nfi = new CultureInfo("en-US").NumberFormat;

            switch (format) {
                case "h":
                    return FormatByte(bytes, HumanReadableFormat(bytes));
                case "GiB":
                    return String.Format(nfi, "{0:0.#} GiB", (double)bytes / (double)(1024 * 1024 * 1024));
                case "MiB":
                    return String.Format(nfi, "{0:0.#} MiB", (double)bytes / (double)(1024 * 1024));
                case "KiB":
                    return String.Format(nfi, "{0:0} KiB", (double)bytes / (double)1024);
                case "B":
                    return String.Format(nfi, "{0:0} B", bytes);
            }
            throw new FormatException("Unable to format bytes '" + bytes + 
                "' according to format '" + format + "'");
        }

        private static string HumanReadableFormat(long bytes) {
            if (bytes >= 1024 * 1024 * 1024)
                return "GiB";
            else if (bytes >= 1024 * 1024)
                return "MiB";
            else if (bytes >= 1024)
                return "KiB";

            return "B";
        }
    }
}
