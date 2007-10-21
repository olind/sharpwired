#region Information and licence agreements
/*
 * NewsModel.cs 
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

namespace SharpWired.Model.News
{
    /// <summary>
    /// Represents all the news posted on the server
    /// </summary>
    public class NewsModel
    {
        private LogicManager logicManager;
        private List<NewsObject> newsListObjects;

        /// <summary>
        /// Add a new newspost to this model
        /// </summary>
        /// <param name="newPost"></param>
        public void AddNewsPost(NewsObject newPost)
        {
            this.newsListObjects.Add(newPost);
            OnNewsPostedEvent(newPost);
        }

        /// <summary>
        /// Replaces the current list of news with the one provided.
        /// </summary>
        /// <param name="allNewsObjects"></param>
        public void ReplaceNewsList(List<NewsObject> allNewsObjects)
        {
            this.newsListObjects = allNewsObjects;
            OnNewsListReplacedEvent(newsListObjects);
        }

        #region Delegates, events and raiser methods

        public delegate void NewsPostedDelegate(NewsObject newPost);
        public delegate void NewsListReplacedDelegate(List<NewsObject> newsList);

        public event NewsPostedDelegate NewsPostedEvent;
        public event NewsListReplacedDelegate NewsListReplacedEvent;

        private void OnNewsPostedEvent(NewsObject newPost)
        {
            if (NewsPostedEvent != null)
                NewsPostedEvent(newPost);
        }

        /// <summary>
        /// Handler for adding the new post to the model
        /// </summary>
        /// <param name="newsList"></param>
        private void OnNewsListReplacedEvent(List<NewsObject> newsList)
        {
            if (NewsListReplacedEvent != null)
                NewsListReplacedEvent(newsList);
        }

        #endregion

        #region Initialization

        public NewsModel(LogicManager logicManager)
        {
            this.logicManager = logicManager;
            newsListObjects = new List<NewsObject>();
        }

        #endregion
    }
}
