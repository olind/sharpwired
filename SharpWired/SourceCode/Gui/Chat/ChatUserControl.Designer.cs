#region Information and licence agreements
/*
 * ChatUserControl.Designer.cs
 * Created by Ola Lindberg, 2006-10-12
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

namespace SharpWired.Gui.Chat
{
    partial class ChatUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatSplitContainer = new System.Windows.Forms.SplitContainer();
            this.chatControl = new SharpWired.Gui.Chat.ChatControl();
            this.userListControl = new SharpWired.Gui.Chat.UserListControl();
            this.chatSplitContainer.Panel1.SuspendLayout();
            this.chatSplitContainer.Panel2.SuspendLayout();
            this.chatSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatSplitContainer
            // 
            this.chatSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.chatSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.chatSplitContainer.Name = "chatSplitContainer";
            // 
            // chatSplitContainer.Panel1
            // 
            this.chatSplitContainer.Panel1.Controls.Add(this.chatControl);
            // 
            // chatSplitContainer.Panel2
            // 
            this.chatSplitContainer.Panel2.Controls.Add(this.userListControl);
            this.chatSplitContainer.Size = new System.Drawing.Size(517, 353);
            this.chatSplitContainer.SplitterDistance = 384;
            this.chatSplitContainer.TabIndex = 8;
            // 
            // chatControl
            // 
            this.chatControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatControl.Location = new System.Drawing.Point(0, 0);
            this.chatControl.Name = "chatControl";
            this.chatControl.Size = new System.Drawing.Size(384, 353);
            this.chatControl.TabIndex = 0;
            // 
            // userListControl
            // 
            this.userListControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.userListControl.Location = new System.Drawing.Point(2, 0);
            this.userListControl.Name = "userListControl";
            this.userListControl.Size = new System.Drawing.Size(127, 353);
            this.userListControl.TabIndex = 0;
            // 
            // ChatUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chatSplitContainer);
            this.Name = "ChatUserControl";
            this.Size = new System.Drawing.Size(517, 353);
            this.chatSplitContainer.Panel1.ResumeLayout(false);
            this.chatSplitContainer.Panel2.ResumeLayout(false);
            this.chatSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer chatSplitContainer;
        private UserListControl userListControl;
        private ChatControl chatControl;
    }
}
