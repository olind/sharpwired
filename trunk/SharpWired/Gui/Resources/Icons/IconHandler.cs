#region Information and licence agreements

/*
 * IconHandler.cs 
 * Created by Ola Lindberg, 2007-06-25
 * 
 * SharpWired - a Wired client.
 * See: http://www.zankasoftware.com/wired/ for more infromation about Wired
 * 
 * Copyright (C) Ola Lindberg (http://olalindberg.com)
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301 USA
 */

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SharpWired.Utils;

namespace SharpWired.Gui.Resources.Icons {
    /// <summary>Abstracts reading icon files from disk.</summary>
    internal class IconHandler {

        private static readonly IconHandler sInstance = new IconHandler();
        /// <summary>Request the singelton instance.</summary>
        public static IconHandler Instance { get { return sInstance; } }

        // This is a collection of the icons and their namings.
        private readonly Dictionary<string, Image> mIcons = new Dictionary<string, Image>();

        private readonly string iconFilePath;
        
        private string IconFilePath { get { return iconFilePath; } }

        /// <summary>Request the icon with this name.</summary>
        /// <param name="name">The name of the icon (not the filename!)</param>
        /// <returns>The icon or null.</returns>
        public Image this[string name] { get { return LoadAndStoreIcon(name); } }

        /// <summary>
        /// Request the icon with the given name, or try to load it from the given
        /// filename.
        /// </summary>
        /// <param name="name">The name of the icon.</param>
        /// <param name="fileName">The filename to load image from.</param>
        /// <returns>The icon or null.</returns>
        public Image this[string name, string fileName] { get { return LoadAndStoreIcon(name, fileName); } }

        /// <summary>
        /// Request the icon with the given name, or try to load it from the given
        /// filename.
        /// </summary>
        /// <param name="nameAndFilePair">The pair of name and filename.</param>
        /// <returns>The icon or null.</returns>
        public Image this[Pair<string, string> nameAndFilePair] { get { return LoadAndStoreIcon(nameAndFilePair); } }

        public List<Image> GetAllIcons() { 
            var iconList = new List<Image>();
            foreach (var ic in IconList.Icons) {
                var img = LoadAndStoreIcon(new Pair<string, string>(ic.Key, ic.Value.Value));
                iconList.Add(img);
            }
            return iconList;
        }

        /// <summary>Constructs and sets iconFilePath. Read standard icons.</summary>
        public IconHandler() {
            iconFilePath = Path.Combine(Application.StartupPath, "GUI");
            iconFilePath = Path.Combine(iconFilePath, "Icons");
        }

        /// <summary>Constructs and sets file source destination to given destination.</summary>
        /// <param name="pIconFilePath">The destination where the icons reciedes.</param>
        public IconHandler(string pIconFilePath) {
            iconFilePath = pIconFilePath;
        }

        /// <summary>Tries to find icon in the dictionary. If it doesn'transfer exist or is null, try to load it from the filename.</summary>
        /// <param name="nameAndFilePairs">The name and filename pair for the icon.</param>
        /// <returns>The icon or null.</returns>
        private Image LoadAndStoreIcon(Pair<string, string> nameAndFilePairs) {
            return LoadAndStoreIcon(nameAndFilePairs.Key, nameAndFilePairs.Value);
        }

        /// <summary>
        /// Tries to find the name in the dictionary with icons.
        /// If the key doesn'transfer exist, tries to find a corresponding filename
        /// in IconList.Icons. Otherwise returns null.
        /// </summary>
        /// <param name="name">The name of the icon (not the filename!)</param>
        /// <returns>The icon, or null.</returns>
        private Image LoadAndStoreIcon(string name) {
            Image im = null;
            var gotIt = mIcons.TryGetValue(name, out im);
            if (im != null) {
                return im;
            }

            Pair<string, string> pair = null;
            gotIt = IconList.Icons.TryGetValue(name, out pair);
            if (gotIt && pair != null) {
                return LoadAndStoreIcon(pair);
            }
            return null;
        }

        /// <summary>
        /// Tries to find icon in the dictionary. If it doesn'transfer exist
        /// or is null, try to load it from the filename.
        /// </summary>
        /// <param name="name">The name of the icon, not the filename!</param>
        /// <param name="fileName">The filename to load image from.</param>
        /// <returns></returns>
        private Image LoadAndStoreIcon(string name, string fileName) {
            Image im = null;
            var gotIt = mIcons.TryGetValue(name, out im);
            if (im != null) {
                return im;
            }

            if (!string.IsNullOrEmpty(fileName)) {
                var image = CreateHiQualityIconImage(fileName);
                if (image != null) {
                    // Using property add overwrites previous values.
                    mIcons[name] = image;
                }
                return image;
            }
            return null;
        }

        /// <summary>Creates a hi quality image from the given fileName. Note the file must be located in the IconFilePath.</summary>
        /// <param name="fileName"></param>
        /// <returns>If succesful the imager is returned. Otherwise null is returned.</returns>
        private Image CreateHiQualityIconImage(string fileName) {
            try {
                var file = Path.Combine(iconFilePath, fileName);
                return Image.FromFile(file);
            } catch (Exception e) {
                // TODO: Add to log instead.
                Debug.WriteLine("Error loading image from file '" + fileName + "'.\n" + e);
                return null;
            }
        }

        /// <summary>Try load image from file.</summary>
        /// <param name="fileName">Filename.</param>
        /// <returns>Image or null.</returns>
        public Image IconFromFile(string fileName) {
            return CreateHiQualityIconImage(fileName);
        }

        /// <summary>Reads the most common icons from file.</summary>
        //private void ReadStandardIcons() {    
        //}
        
#region OBSOLETE: Used for reading icons from windows. Doesn't work in Mono. Instead I think we should use the Mono icons throughout

        [Obsolete("Used for reading icons from windows. Doesn't work in Mono. Instead I think we should use the Mono icons throughout")]
        private const uint conFILE_ATTRIBUTE_NORMAL = 0x00000080;
        [Obsolete("Used for reading icons from windows. Doesn't work in Mono. Instead I think we should use the Mono icons throughout")]
        private const uint conFILE_ATTRIBUTE_DIRECTORY = 0x00000010;

        [Obsolete("Used for reading icons from windows. Doesn't work in Mono. Instead I think we should use the Mono icons throughout")]
        private enum EnumFileInfoFlags : uint
        {
            /// get large icon
            LARGEICON = 0x000000000,
            /// get small icon
            SMALLICON = 0x000000001,
            /// get open icon
            OPENICON = 0x000000002,
            /// get shell size icon
            SHELLICONSIZE = 0x000000004,
            /// pszPath is a pidl
            PIDL = 0x000000008,
            /// use passed dwFileAttribute
            USEFILEATTRIBUTES = 0x000000010,
            /// apply the appropriate overlays
            ADDOVERLAYS = 0x000000020,
            /// get the index of the overlay
            OVERLAYINDEX = 0x000000040,
            /// get icon
            ICON = 0x000000100,
            /// get display name
            DISPLAYNAME = 0x000000200,
            /// get type name
            TYPENAME = 0x000000400,
            /// get attributes
            ATTRIBUTES = 0x000000800,
            /// get icon location
            ICONLOCATION = 0x000001000,
            /// return exe type
            EXETYPE = 0x000002000,
            /// get system icon index
            SYSICONINDEX = 0x000004000,
            /// put a link overlay on icon
            LINKOVERLAY = 0x000008000,
            /// show icon in selected state
            SELECTED = 0x000010000,
            /// get only specified attributes
            ATTR_SPECIFIED = 0x000020000
        }

        [Obsolete("Used for reading icons from windows. Doesn't work in Mono. Instead I think we should use the Mono icons throughout")]
        [StructLayout(LayoutKind.Sequential)]
        private struct ShellFileInfo
        {
            public const int conNameSize = 80;
            public IntPtr hIcon; // note to call DestroyIcon
            public int iIndex;
            public uint dwAttributes;

            [MarshalAs(
                UnmanagedType.ByValTStr,
                SizeConst = conMAX_PATH)]
            public string szDisplayName;

            [MarshalAs(
                UnmanagedType.ByValTStr,
                SizeConst = conNameSize)]
            public string szTypeName;
        } ;

        [Obsolete("Used for reading icons from windows. Doesn't work in Mono. Instead I think we should use the Mono icons throughout")]
        private const int conMAX_PATH = 260;


        [Obsolete("Used for reading icons from windows. Doesn't work in Mono. Instead I think we should use the Mono icons throughout")]
        [DllImport("User32.dll")]
        private static extern int DestroyIcon(IntPtr hIcon);

        [Obsolete("Used for reading icons from windows. Doesn't work in Mono. Instead I think we should use the Mono icons throughout")]
        [DllImport("Shell32.dll")]
        private static extern IntPtr SHGetFileInfo(
            string pszPath,
            uint dwFileAttributes,
            ref ShellFileInfo psfi,
            uint cbFileInfo,
            uint uFlags
            );

        [Obsolete("Used for reading icons from windows. Doesn't work in Mono. Instead I think we should use the Mono icons throughout")]
        public Image GetFileIconFromSystem(string fileName) {
            return GetSystemIcon(fileName, false);
        }

        

        [Obsolete("Used for reading icons from windows. Doesn't work in Mono. Instead I think we should use the Mono icons throughout")]
        public Image GetFolderIconFromSystem() {
            return GetSystemIcon("", true);
        }

        [Obsolete("Used for reading icons from windows. Doesn't work in Mono. Instead I think we should use the Mono icons throughout")]
        private Image GetSystemIcon(string fileName, bool isFolder) {
            // Taken from:
            // http://www.dotnetjunkies.com/WebLog/malio/archive/2004/10/04/27603.aspx

            var flags =
                EnumFileInfoFlags.ICON | EnumFileInfoFlags.USEFILEATTRIBUTES | EnumFileInfoFlags.SMALLICON;

            //if (folderType == EnumFolderType.Open)
            //    flags |= EnumFileInfoFlags.OPENICON;

            // flags |= EnumFileInfoFlags.LINKOVERLAY;

            var shellFileInfo = new ShellFileInfo();
            uint fileAttribute;
            if (isFolder) {
                fileAttribute = conFILE_ATTRIBUTE_DIRECTORY;
            } else {
                fileAttribute = conFILE_ATTRIBUTE_NORMAL;
            }

            SHGetFileInfo(
                fileName,
                fileAttribute,
                ref shellFileInfo,
                (uint) Marshal.SizeOf(shellFileInfo),
                (uint) flags);

            // deep copy
            var icon =
                (Icon) Icon.FromHandle(shellFileInfo.hIcon).Clone();

            // release handle
            DestroyIcon(shellFileInfo.hIcon);

            return icon.ToBitmap();
        }
#endregion
    
    }
}