#region Information and licence agreements
/**
 * NewsObject.cs 
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

namespace SharpWired.Model.News
{
    public class NewsObject
    {
        private string nick;
        private DateTime postTime;
        private string post;

        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }
        
        public DateTime PostTime
        {
            get { return postTime; }
            set { postTime = value; }
        }
        

        public string Post
        {
            get { return post; }
            set { post = value; }
        }

        #region Initialization

        public NewsObject(MessageEventArgs_320322 messageEventArgs)
        {
            this.nick = messageEventArgs.Nick;
            this.postTime = messageEventArgs.PostTime;
            this.post = messageEventArgs.Post;
        }

        #endregion
    }
}
