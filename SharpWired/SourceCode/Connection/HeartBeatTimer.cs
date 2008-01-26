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
        private Nullable<TimeSpan> lag;

        /// <summary>
        /// Starts the timer for this connection
        /// </summary>
        public void StartTimer()
        {
            TimerCallback tc = new TimerCallback(heartBeatHandler.DoPing);
            timer = new Timer(tc);
            timer.Change(10000, 60000); // Waits 10 seconds before starting 
                                        // the pings. Then every minute (same
                                        // time difference as Wired Client)
        }

        /// <summary>
        /// Stops the timer for this connection
        /// </summary>
        public void StopTimer()
        {
            timer.Dispose();
            lag = null;
        }

        #region Listeners from messages
        void Messages_PingReplyEvent(object sender,
            SharpWired.MessageEvents.MessageEventArgs_Messages
            messageEventArgs)
        {
            lag = DateTime.Now.Subtract(heartBeatHandler.LastSentPing);
            Console.WriteLine("Lag: " + lag.ToString());
            //TODO: Instead of having the heart beat timer responsible for 
            //calculating the lag we should have some other class. If we
            //decide to use heartbeattimer for this we should rename it to
            //something like LagHandler instead.
            //The laghandler could then send an event every time the lag
            //value was updated.
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionManager">The commands object assosiated 
        /// with the connection to send PINGS to</param>
        public HeartBeatTimer(ConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
            this.heartBeatHandler = new HeartBeatHandler(connectionManager);
            //Listening to this event to be able to calculate the server lag
            connectionManager.Messages.PingReplyEvent += new 
                Messages.PingReplyEventHandler(Messages_PingReplyEvent);
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
            connectionManager.Commands.Ping();
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
