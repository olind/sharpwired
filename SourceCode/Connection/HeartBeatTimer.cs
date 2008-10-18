#region Information and licence agreements
/*
 * HeartBeatTimer.cs
 * Created by Ola Lindberg, 2008-01-26
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
using System.Threading;

namespace SharpWired.Connection
{
    /// <summary>
    /// Timer that sends regular PING commands to the Wired server
    /// </summary>
    class HeartBeatTimer
    {
        private ConnectionManager connectionManager;
        private Timer timer;
        private HeartBeatHandler heartBeatHandler;

        /// <summary>
        /// Starts the timer for this connection
        /// </summary>
        public void StartTimer()
        {
            TimerCallback tc = new TimerCallback(heartBeatHandler.DoPing);
            timer = new Timer(tc);

            // Waits 10 seconds before starting 
            // the pings. Then every minute (same
            // time difference as Wired Client)
            int waitBeforeStarting = 10000; // TODO: Move to configuration
            int waitBetwenPings = 60000;    //       Move to configuration
            timer.Change(waitBeforeStarting, waitBetwenPings);
        }

        /// <summary>
        /// Stops the timer for this connection
        /// </summary>
        public void StopTimer()
        {
            timer.Dispose();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionManager">The commands object assosiated 
        /// with the connection to send PINGS to</param>
        public HeartBeatTimer(ConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
            this.heartBeatHandler = new HeartBeatHandler(connectionManager);
        }
    }

    /// <summary>
    /// The TimerCallback object receiving the message sent from the timer
    /// </summary>
    class HeartBeatHandler
    {
        ConnectionManager connectionManager;
        private DateTime lastPing;

        /// <summary>
        /// Gets the time when the last PING command was sent
        /// </summary>
        public DateTime LastSentPing
        {
            get { return lastPing; }
        }

        /// <summary>
        /// Does a ping to the server
        /// </summary>
        public void DoPing(Object stateInfo)
        {
            connectionManager.Commands.Ping(this);
            lastPing = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionManager">The connection manager for
        /// the connection to bing</param>
        public HeartBeatHandler(ConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }
    }
}
