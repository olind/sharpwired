#region Information and licence agreements
/*
 * NewsHandler.cs 
 * Created by Ola Lindberg, 2006-12-09
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

namespace SharpWired.Model.News
{
    /// <summary>
    /// Model representation of the news
    /// </summary>
    public class NewsHandler : HandlerBase
    {
        private NewsModel newsModel;
        /// <summary>
        /// Used to stack all news objects until we receive a NewsDoneEvent
        /// </summary>
        private List<NewsObject> newsListObjects;

        #region Properties

        /// <summary>
        /// Get the news model
        /// </summary>
        public NewsModel NewsModel
        {
            get { return newsModel; }
        }

        #endregion

        #region Sending to commands layer

        /// <summary>
        /// Send a post message to the server
        /// </summary>
        /// <param name="newsMessage"></param>
        public void PostNewsMessage(string newsMessage)
        {
            this.LogicManager.ConnectionManager.Commands.Post(newsMessage);
        }

        /// <summary>
        /// Refresh the news from the server
        /// </summary>
        public void ReloadNewsFromServer()
        {
            this.LogicManager.ConnectionManager.Commands.News();
        }

        #endregion

        #region Listeners from messages layer

        void Messages_NewsPostedEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_320322 messageEventArgs)
        {
            // Asyncronomysly when a new post is posted
            NewsObject n = new NewsObject(messageEventArgs);
            newsModel.AddNewsPost(n);
        }

        void Messages_NewsEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_320322 messageEventArgs)
        {
            // Run when we issue the NEWS command
            NewsObject n = new NewsObject(messageEventArgs);
            newsListObjects.Add(n);
        }

        void Messages_NewsDoneEvent(object sender, SharpWired.MessageEvents.MessageEventArgs_Messages messageEventArgs)
        {
            newsModel.ReplaceNewsList(newsListObjects);
            newsListObjects.Clear();          
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Init this handler
        /// </summary>
        /// <param name="connectionManager">The connection manager</param>
        public override void Init(ConnectionManager connectionManager)
        {
            base.Init(connectionManager);

            connectionManager.Messages.NewsDoneEvent += new Messages.NewsDoneEventHandler(Messages_NewsDoneEvent);
            connectionManager.Messages.NewsEvent += new Messages.NewsEventHandler(Messages_NewsEvent);
            connectionManager.Messages.NewsPostedEvent += new Messages.NewsPostedEventHandler(Messages_NewsPostedEvent);

            this.ReloadNewsFromServer();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logicManager">The logic manager</param>
        public NewsHandler(LogicManager logicManager): base(logicManager)
        {
            newsModel = new NewsModel(logicManager);
            newsListObjects = new List<NewsObject>();
        }

        #endregion
    }
}
