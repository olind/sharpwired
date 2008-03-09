using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model.Users;

namespace SharpWired.Gui.Chat {
    class WiredListViewItem : ListViewItem {
        private UserItem userItem;

        public UserItem UserItem {
            get { return userItem; }
            set { userItem = value; }
        }

        public WiredListViewItem(UserItem user, string[] subItems, string imageKey)
            : base(subItems, imageKey) { 
            this.UserItem = user;
        }
    }
}
