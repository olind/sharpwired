#region Information and licence agreements
/*
 * GroupHandler.cs 
 * Created by Ola Lindberg, 2007-12-14
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

namespace SharpWired.Model.Users
{
    /// <summary>
    /// Handles groups
    /// </summary>
    class GroupHandler
    {
        private LogicManager logicManager;
        private List<Group> groups = new List<Group>();


        /// <summary>
        /// Gets the group with the given name
        /// </summary>
        /// <param name="name">The name for the searched group</param>
        /// <returns>The searched group. If not found null is returned.</returns>
        public Group GetGroup(string name)
        {
            foreach (Group g in groups)
            {
                if (g.Name == name)
                {
                    return g;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds out if the given group exists or not.
        /// NOTE! This method only finds groups that has been loaded from server.
        /// </summary>
        /// <param name="name">The name of the searched group</param>
        /// <returns>True if the given group exists, false otherwise</returns>
        public bool GroupExists(string name)
        {
            if (GetGroup(name) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds the group with the given name and privileges to the list of groups
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="p">The privileges for this group</param>
        /// <returns>True if the group was added. False otherwise.</returns>
        public bool AddGroup(string name, Privileges p)
        {
            if (!GroupExists(name))
            {
                groups.Add(new Group(name, p));
                return true;
            }
            return false;
        }

        #region Listener handler methods
        void Messages_GroupSpecificationEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_601 messageEventArgs)
        {
            if (GroupExists(messageEventArgs.Name))
            {
                //Update existing group
                Group g = GetGroup(messageEventArgs.Name);
                g.Privileges.UpdatePrivileges(messageEventArgs.Privileges);
            }
            else
            {
                //Create new group
                AddGroup(messageEventArgs.Name, messageEventArgs.Privileges);
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logicManager"></param>
        public GroupHandler(LogicManager logicManager)
        {
            this.logicManager = logicManager;

            logicManager.ConnectionManager.Messages.GroupSpecificationEvent += new SharpWired.Connection.Messages.GroupSpecificationEventHandler(Messages_GroupSpecificationEvent);
        }
    }
}
