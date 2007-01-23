#region Information and licence agreements
/**
 * HandlerBase.cs 
 * Created by Ola Lindberg and Peter Holmdahl, 2006-11-25
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
using SharpWired.Connection;

namespace SharpWired.Model
{
    /// <summary>
    /// The basic functionality for the handlers for all different handler objects
    /// </summary>
    public class HandlerBase
    {

        #region Variables

        private Commands commands;

        private LogicManager logicManager;

        #endregion

        #region Properties

        public LogicManager LogicManager
        {
            get { return logicManager; }
            set { logicManager = value; }
        }

        public Commands Commands
        {
            get { return commands; }
            set { commands = value; }
        }

        #endregion

        #region Initialization of the HandlerBase

        public virtual void Init(ConnectionManager connectionManager)
        {
            commands = connectionManager.Commands;
        }

        public HandlerBase(LogicManager logicManager)
        {
            this.logicManager = logicManager;
        }

        #endregion
    }
}
