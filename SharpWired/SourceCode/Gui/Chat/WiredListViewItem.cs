using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model.Users;

namespace SharpWired.Gui.Chat {
    class WiredListViewItem : ListViewItem {
        private User userItem;

        public User UserItem {
            get { return userItem; }
            set { userItem = value; }
        }

        public WiredListViewItem(User user, string nick, string imageIndex) 
            : base(nick, imageIndex) {
            this.UserItem = user;
        }

        public WiredListViewItem(User user, string[] subItems, string imageKey)
            : base( subItems, imageKey) { 
            this.UserItem = user;
        }
    }
}
