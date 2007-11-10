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
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using SharpWired.Utils;

namespace SharpWired.Gui.Resources.Icons
{
    /// <summary>
    /// Abstracts reading icon files from disk.
    /// </summary>
    internal class IconHandler
	{
		#region Singelton
		private static IconHandler sInstance = new IconHandler();

		/// <summary>
		/// Get the singelton instance.
		/// </summary>
		public static IconHandler Instane
		{
			get { return sInstance; }
		}
		#endregion


		#region Constructor
		/// <summary>
		/// Constructs and sets iconFilePath. Read standard icons.
		/// </summary>
		public IconHandler()
		{
			// I think Application.StartupPath is a bit better really. The curetn
			// dir can change independent of where the .exe file is.
			// use Path.Combine to combine paths. That way we don't have to know
			// which char to use between the parts of the path.
			iconFilePath = Path.Combine(Application.StartupPath, "GUI");
			iconFilePath = Path.Combine(iconFilePath, "Icons");

			ReadStandardIcons();
		}

		/// <summary>
		/// Constructs and sets file source path to given path.
		/// </summary>
		/// <param name="pIconFilePath">The path where the icons reciedes.</param>
		public IconHandler(string pIconFilePath)
		{
			iconFilePath = pIconFilePath;
		}
		#endregion
		
		#region Variables
		// This is a collection of the icons and their namings.
		private Dictionary<string, Image> mIcons = new Dictionary<string, Image>();

		private Image folderClosed;
		private string iconFilePath;
        private Image file;
        private Image userImage;
        #endregion

		#region Properties
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

        /// <summary>
        /// Gets the user image
        /// </summary>
        public Image UserImage
        {
            get { return userImage; }
        }

		/// <summary>
		/// Get the icon with this name.
		/// </summary>
		/// <param name="name">The name of the icon (not the filename!)</param>
		/// <returns>The icon or null.</returns>
		public Image this[string name]
		{
			get { return LoadAndStoreIcon(name); }
		}

		/// <summary>
		/// Get the icon with the given name, or try to load it from the given
		/// filename.
		/// </summary>
		/// <param name="name">The name of the icon.</param>
		/// <param name="fileName">The filename to load image from.</param>
		/// <returns>The icon or null.</returns>
		public Image this[string name, string fileName]
		{
			get { return LoadAndStoreIcon(name, fileName); }
		}

		/// <summary>
		/// Get the icon with the given name, or try to load it from the given
		/// filename.
		/// </summary>
		/// <param name="nameAndFilePair">The pair of name and filename.</param>
		/// <returns>The icon or null.</returns>
		public Image this[Pair<string, string> nameAndFilePair]
		{
			get { return LoadAndStoreIcon(nameAndFilePair); }
		}
		#endregion

		#region Methods
		#region Loading File and Storing Image
		/// <summary>
		/// Tries to find icon in the dictionary. If it doesn't exist
		/// or is null, try to load it from the filename.
		/// </summary>
		/// <param name="nameAndFilePairs">The name and filename pair for the icon.</param>
		/// <returns>The icon or null.</returns>
		private Image LoadAndStoreIcon(Pair<string, string> nameAndFilePairs)
		{
			return LoadAndStoreIcon(nameAndFilePairs.Key, nameAndFilePairs.Value);
		}

		/// <summary>
		/// Tries to find the name in the dictionary with icons.
		/// If the key doesn't exist, tries to find a corresponding filename
		/// in IconList.Icons. Otherwise returns null.
		/// </summary>
		/// <param name="name">The name of the icon (not the filename!)</param>
		/// <returns>The icon, or null.</returns>
		private Image LoadAndStoreIcon(string name)
		{
			Image im = null;
			bool gotIt = mIcons.TryGetValue(name, out im);
			if (im != null)
				return im;

			Pair<string, string> pair = null;
			gotIt = IconList.Icons.TryGetValue(name, out pair);
			if (gotIt && pair != null)
				return LoadAndStoreIcon(pair);
			return null;
		}

		/// <summary>
		/// Tries to find icon in the dictionary. If it doesn't exist
		/// or is null, try to load it from the filename.
		/// </summary>
		/// <param name="name">The name of the icon, not the filename!</param>
		/// <param name="fileName">The filename to load image from.</param>
		/// <returns></returns>
		private Image LoadAndStoreIcon(string name, string fileName)
		{
			Image im = null;
			bool gotIt = mIcons.TryGetValue(name, out im);
			if (im != null)
				return im;

			if (!string.IsNullOrEmpty(fileName))
			{
				Image image = CreateHiQualityIconImage(fileName);
				if(image != null)
					// Using property add overwrites previous values.
					mIcons[name] = image;
				return image;
			}
			return null;
		}

		#endregion

		/// <summary>
        /// Creates a hi quality image from the given fileName. Note the file must be located in the IconFilePath.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>If succesful the imager is returned. Otherwise null is returned.</returns>
        private Image CreateHiQualityIconImage(string fileName)
        {
            try
            {
				string file = Path.Combine(iconFilePath, fileName);
                return Image.FromFile(file);
            }
            catch (Exception e)
            {
				// TODO: Add to log instead.
				Console.Error.WriteLine("Error loading image from file '"
					+ fileName + "'. The created path was '"
					+ file + "'.\n" + e.ToString());
                return null;
            }
        }

		/// <summary>
		/// Try load image from file.
		/// </summary>
		/// <param name="fileName">Filename.</param>
		/// <returns>Image or null.</returns>
		public Image IconFromFile(string fileName)
		{
			return CreateHiQualityIconImage(fileName);
		}

		/// <summary>
		/// Reads the most common icons from file.
		/// </summary>
		private void ReadStandardIcons()
		{
			folderClosed = CreateHiQualityIconImage("folderClosed.png");
			file = CreateHiQualityIconImage("file.png");
            userImage = CreateHiQualityIconImage("userImage.png");
		}

        #endregion
    }
}