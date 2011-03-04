#region Information and licence agreements

/*
 * IconHandler.cs 
 * Created by Peter Holmdahl, 2007-06-25
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

using System.Collections.Generic;
using SharpWired.Utils;

namespace SharpWired.Gui.Resources.Icons {
    /// <summary>A list with the icons and their filenames.</summary>
    internal class IconList {
        private static readonly SortedDictionary<string, Pair<string, string>> sIcons = new SortedDictionary<string, Pair<string, string>>();

        private static readonly Pair<string, string> userImage = new Pair<string, string>("UserImage", "userImage.png");
        private static readonly Pair<string, string> bookmark = new Pair<string, string>("Bookmark", "bookmark-new.ico");
        private static readonly Pair<string, string> file = new Pair<string, string>("File", "format-justify-left.ico");
        private static readonly Pair<string, string> folder = new Pair<string, string>("Folder", "folder.ico");
        private static readonly Pair<string, string> folderOpen = new Pair<string, string>("FolderOpen", "folder-open.ico");
        private static readonly Pair<string, string> goHome = new Pair<string, string>("GoHome", "go-home.ico");
        private static readonly Pair<string, string> mediaPlaybackPause = new Pair<string, string>("MediaPlaybackPause", "media-playback-pause.ico");
        private static readonly Pair<string, string> mediaPlaybackStart = new Pair<string, string>("MediaPlaybackStart", "media-playback-start.ico");
        private static readonly Pair<string, string> processStop = new Pair<string, string>("ProcessStop", "process-stop.ico");

        public static Pair<string, string> UserImage { get { return userImage; } }
        public static Pair<string, string> Bookmark { get { return bookmark; } }
        public static Pair<string, string> File { get { return file; } }
        public static Pair<string, string> Folder { get { return folder; } }
        public static Pair<string, string> FolderOpen { get { return folderOpen; } }
        public static Pair<string, string> GoHome { get { return goHome; } }
        public static Pair<string, string> MediaPlaybackPause { get { return mediaPlaybackPause; } }
        public static Pair<string, string> MediaPlaybackStart { get { return mediaPlaybackStart; } }
        public static Pair<string, string> ProcessStop { get { return processStop; } }

        /// <summary>Gets the list of icon pairs.</summary>
        public static SortedDictionary<string, Pair<string, string>> Icons { get { return sIcons; } }

        static IconList() {
            sIcons.Add(file.Key, file);
            sIcons.Add(folder.Key, folder);
            sIcons.Add(folderOpen.Key, folderOpen);
            sIcons.Add(userImage.Key, userImage);
            sIcons.Add(goHome.Key, goHome);
            sIcons.Add(mediaPlaybackPause.Key, mediaPlaybackPause);
            sIcons.Add(mediaPlaybackStart.Key, mediaPlaybackStart);
            sIcons.Add(processStop.Key, processStop);
        }
    }
}