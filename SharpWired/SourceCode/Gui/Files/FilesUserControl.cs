#region Information and licence agreements
/**
 * FileUserControl.cs 
 * Created by Ola Lindberg and Peter Holmdahl, 2007-05-10
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model;
using SharpWired.Model.Files;
using System.Collections;
using SharpWired.Gui.Resources;
using SharpWired.Gui.Resources.Icons;
using SharpWired.Connection.Transfers;

namespace SharpWired.Gui.Files
{
    /// <summary>
    /// Holds referenses to and inits the FileUserControl and the FileTreeControl.
    /// </summary>
    public partial class FilesUserControl : UserControl
    {
        #region Variables
        private LogicManager logicManager;
        private int mSuspendCount = 0; //Counter for knowing if the cursor is suspended or not.
        #endregion

        #region Properties
        /// <summary>
        /// Gets true if the cursor is suspended. False otherwise.
        /// </summary>
        public bool Suspended
        {
            get { return mSuspendCount > 0; }
        }
        #endregion

        #region General GUI methods
        ///<summary>
        /// Suspends the Control, and sets the Cursor to a waitcursor, if it isn't already.
        ///</summary>
        public void Suspend()
        {
            // When calling this method, the state is always Suspended.
            // But we only set the cursor if we aren't already suspended.
            // So, if counter is 0, we are the first to call this method, so we set the cursor.
            // But, we also need to increase the counter, and therefore add one.
            // Since the '++' comes after the field name, the increse will be done after
            // the logical check for == 0.
            if (mSuspendCount++ == 0)
                Cursor = Cursors.WaitCursor;
        }

        /// <summary>
        /// Unsuspends the cursor.
        /// </summary>
        /// <returns>True if the cursor isn't suspended.</returns>
        public bool UnSuspend()
        {
            // Decrease the count (the -- is before the field name, so its decreased before the == 0).
            // And if we're done to zero again, we're no longer suspended and we set the cursor back.
            if (--mSuspendCount == 0)
                Cursor = Cursors.Default;
            return mSuspendCount == 0;
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Inits this class and it's subclasses
        /// </summary>
        /// <param name="logicManager"></param>
        public void Init(LogicManager logicManager)
        {
            this.logicManager = logicManager;
            fileTreeControl.Init(this, logicManager);
            fileDetailsControl.Init(fileTreeControl);
        }

        /// <summary>
        /// Constructor - Empty
        /// </summary>
        public FilesUserControl()
        {
            InitializeComponent();
        }
        #endregion
    }
}
