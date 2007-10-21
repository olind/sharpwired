#region Information and licence agreements
/*
 * Privileges.cs 
 * Created by Ola Lindberg, 2006-10-14
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
using SharpWired;

namespace SharpWired.Model.Users
{
    /// <summary>
    /// This class represents the privileges a user has on a Wired server.
    /// 
    /// TODO: This object should be able to have predefined values based on what group it belongs to.
    /// </summary>
    public class Privileges
    {
        string userName;
        bool getUserInfo;
        bool broadcast;
        bool postNews;
        bool clearNews;
        bool download;
        bool upload;
        bool uploadAnywhere;
        bool createFolders;
        bool alterFiles;
        bool deleteFiles;
        bool viewDropboxes;
        bool createAccounts;
        bool editAccounts;
        bool deleteAccounts;
        bool elevatePrivileges;
        bool kickUsers;
        bool banUsers;
        bool cannotBeKicked;
        int downloadSpeed;
        int uploadSpeed;
        int downloadLimit;
        int uploadLimit;
        bool changeTopic;

        /// <summary>
        /// Get the user id for this privileges mask (object).
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to get user information or not?
        /// </summary>
        public bool GetUserInfo
        {
            get
            {
                return getUserInfo;
            }
            set
            {
                getUserInfo = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to send broadcast messages or not?
        /// </summary>
        public bool Broadcast
        {
            get
            {
                return broadcast;
            }
            set 
            { 
                broadcast = value; 
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to post news or not?
        /// </summary>
        public bool PostNews
        {
            get
            {
                return postNews;
            }
            set
            {
                postNews = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to clear news or not?
        /// </summary>
        public bool ClearNews
        {
            get
            {
                return clearNews;
            }
            set { clearNews = value; }
        }

        /// <summary>
        /// Get or set whether this user is allowed to download or not?
        /// </summary>
        public bool Download
        {
            get
            {
                return download;
            }
            set
            {
                download = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to upload or not?
        /// </summary>
        public bool Upload
        {
            get
            {
                return upload;
            }
            set
            {
                upload = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to upload anywhere or not?
        /// </summary>
        public bool UploadAnywhere
        {
            get
            {
                return uploadAnywhere;
            }
            set
            {
                uploadAnywhere = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to create folders or not?
        /// </summary>
        public bool CreateFolders
        {
            get
            {
                return createFolders;
            }
            set
            {
                createFolders = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to alter files or not?
        /// </summary>
        public bool AlterFiles
        {
            get
            {
                return alterFiles;
            }
            set
            {
                alterFiles = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to delete files or not?
        /// </summary>
        public bool DeleteFiles
        {
            get
            {
                return deleteFiles;
            }
            set 
            { 
                deleteFiles = value; 
            }
        }

        /// <summary>
        /// Get or set whether this is user allowed to view dropboxe or not?
        /// </summary>
        public bool ViewDropboxes
        {
            get
            {
                return viewDropboxes;
            }
            set 
            { 
                viewDropboxes = value; 
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to create accounts or not?
        /// </summary>
        public bool CreateAccounts
        {
            get
            {
                return createAccounts;
            }
            set 
            { 
                createAccounts = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to edit accounts or not?
        /// </summary>
        public bool EditAccounts
        {
            get
            {
                return editAccounts;
            }
            set 
            { 
                editAccounts = value; 
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to delete accounts or not?
        /// </summary>
        public bool DeleteAccounts
        {
            get
            {
                return deleteAccounts;
            }
            set
            {
                deleteAccounts = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to elevate privileges or not?
        /// </summary>
        public bool ElevatePrivileges
        {
            get
            {
                return elevatePrivileges;
            }
            set
            {
                elevatePrivileges = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to kick users or not?
        /// </summary>
        public bool KickUsers
        {
            get
            {
                return kickUsers;
            }
            set
            {
                kickUsers = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to ban users or not?
        /// </summary>
        public bool BanUsers
        {
            get
            {
                return banUsers;
            }
            set
            {
                banUsers = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to be kicked or not.
        /// </summary>
        public bool CannotBeKicked
        {
            get
            {
                return cannotBeKicked;
            }
            set
            {
                cannotBeKicked = value;
            }
        }

        /// <summary>
        /// Get or set the download speed for this user.
        /// </summary>
        public int DownloadSpeed
        {
            get
            {
                return downloadSpeed;
            }
            set
            {
                downloadSpeed = value;
            }
        }

        /// <summary>
        /// Get or set the upload speed for this user.
        /// </summary>
        public int UploadSpeed
        {
            get
            {
                return UploadSpeed;
            }
            set
            {
                uploadSpeed = value;
            }
        }

        /// <summary>
        /// Get or set whether the download limit for this user.
        /// </summary>
        public int DownloadLimit
        {
            get
            {
                return downloadLimit;
            }
            set
            {
                downloadLimit = value;
            }
        }

        /// <summary>
        /// Get or set whether the upload limit for this user.
        /// </summary>
        public int UploadLimit
        {
            get
            {
                return uploadLimit;
            }
            set
            {
                uploadLimit = value;
            }
        }

        /// <summary>
        /// Get or set whether this user is allowed to change chat topic or not?
        /// </summary>
        public bool ChangeTopic
        {
            get
            {
                return changeTopic;
            }
            set
            {
                changeTopic = value;
            }
        }

        /// <summary>
        /// This privileges object is converted to a privileges string 
        /// compatible with the Wired protocol.
        /// </summary>
        /// <returns></returns>
        public string convertToWiredPrivilegesMask()
        {
            string wiredPrivilegesMask;

            wiredPrivilegesMask = 
                GetUserInfo + Utility.FS +
                Broadcast + Utility.FS +
                PostNews + Utility.FS +
                ClearNews + Utility.FS +
                Download + Utility.FS +
                Upload + Utility.FS +
                UploadAnywhere + Utility.FS +
                CreateFolders + Utility.FS +
                AlterFiles + Utility.FS +
                DeleteFiles + Utility.FS +
                ViewDropboxes + Utility.FS +
                CreateAccounts + Utility.FS +
                EditAccounts + Utility.FS +
                DeleteAccounts + Utility.FS +
                ElevatePrivileges + Utility.FS +
                KickUsers + Utility.FS +
                BanUsers + Utility.FS +
                CannotBeKicked + Utility.FS +
                DownloadSpeed + Utility.FS +
                UploadSpeed + Utility.FS +
                DownloadLimit + Utility.FS +
                UploadLimit + Utility.FS +
                ChangeTopic;

            return wiredPrivilegesMask;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userName">This privileges are for the user vith this name</param>
        /// <param name="privilegesString">The string from Wired that contains the privileges. Wired protocol 1.1.</param>
        public Privileges(string userName, string privilegesString) : this (privilegesString)
        {
            this.userName = userName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="privilegesString">The string from Wired that contains the privileges. Wired protocol 1.1.</param>
        public Privileges(string privilegesString)
        {
            string[] privilegesStringSplitted = Utility.SplitWiredString(privilegesString);
            GetUserInfo = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[0]));
            Broadcast = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[1]));
            PostNews = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[2]));
            ClearNews = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[3]));
            Download = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[4]));
            Upload = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[5]));
            uploadAnywhere = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[6]));
            CreateFolders = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[7]));
            AlterFiles = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[8]));
            DeleteFiles = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[9]));
            ViewDropboxes = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[10]));
            createAccounts = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[11]));
            EditAccounts = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[12]));
            DeleteAccounts = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[13]));
            ElevatePrivileges = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[14]));
            KickUsers = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[15]));
            BanUsers = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[16]));
            CannotBeKicked = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[17]));
            DownloadSpeed = int.Parse(privilegesStringSplitted[18]);
            UploadSpeed = int.Parse(privilegesStringSplitted[19]);
            DownloadLimit = int.Parse(privilegesStringSplitted[20]);
            UploadLimit = int.Parse(privilegesStringSplitted[21]);
            ChangeTopic = Utility.ConvertIntToBool(int.Parse(privilegesStringSplitted[22]));
        }
    }
}
