#region Information and licence agreements
/**
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
using System.Text;
using System.Drawing;

namespace SharpWired.Gui.Resources
{
    /// <summary>
    /// Abstracts reading icon files from disk.
    /// </summary>
    class IconHandler
    {
        #region Variables
        private Image folderClosed;
        private string iconFilePath = System.Environment.CurrentDirectory + "\\GUI\\Icons\\";
        private Image file;
        #endregion

        /// <summary>
        /// Get the file path for the CSS-file
        /// </summary>
        private string IconFilePath
        {
            get { return iconFilePath; }
        }

        /// <summary>
        /// Gets icon representing a closed folder.
        /// </summary>
        public Image FolderClosed
        {
            get { return folderClosed; }
        }

        /// <summary>
        /// Gets icon representing a file.
        /// </summary>
        public Image File
        {
            get { return file; }
        }

        #region Methods

        /// <summary>
        /// Creates a hi quaslity image from the given fileName. Note the file must be located in the IconFilePath.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>If succesful the imager is returned. Otherwise null is returned.</returns>
        private Image CreateHiQualityIconImage(string fileName)
        {
            try
            {
                return Image.FromFile(IconFilePath + fileName);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// Constructor. Reads icons from disk.
        /// </summary>
        public IconHandler()
        {
            folderClosed = CreateHiQualityIconImage("folderClosed.png");
            file = CreateHiQualityIconImage("file.png");
        }
    }
}
