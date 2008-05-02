#region Information and licence agreements
/*
 * NewsPost.cs 
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
using SharpWired.MessageEvents;

namespace SharpWired.Model.News {
    /// <summary>
    /// Represents one news message
    /// </summary>
    public class NewsPost /*: IComparable */ {

        #region Fields
        private string nick;
        private DateTime postTime;
        private string post;
        #endregion

        #region Constructor
        /// <summary>
        /// Construtcor
        /// </summary>
        /// <param name="messageEventArgs"></param>
        public NewsPost(MessageEventArgs_320322 messageEventArgs) {
            this.nick = messageEventArgs.Nick;
            this.postTime = messageEventArgs.PostTime;
            this.post = messageEventArgs.Post;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the nick for the user that wrote this message
        /// </summary>
        public string Nick {
            get { return nick; }
        }

        /// <summary>
        /// Gets the date when this message was posted
        /// </summary>
        public DateTime PostTime {
            get { return postTime; }
        }

        /// <summary>
        /// Get the news post body
        /// </summary>
        public string Post {
            get { return post; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Compare this object with the given
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj) {
            NewsPost np = obj as NewsPost;
            string thisPost = this.postTime + this.nick + this.post;
            string comparePost = np.postTime + np.nick + np.post;
            return thisPost.CompareTo(comparePost);
        }
        #endregion
    }
}
