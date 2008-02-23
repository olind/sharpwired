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
            this.sendChatButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chatWebBrowser = new System.Windows.Forms.WebBrowser();
            this.sendChatRichTextBox = new System.Windows.Forms.RichTextBox();
            this.userListControl = new SharpWired.Gui.Chat.UserListControl();
            this.chatWrapperPanel = new System.Windows.Forms.Panel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.chatWrapperPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendChatButton
            // 
            this.sendChatButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.sendChatButton.Location = new System.Drawing.Point(348, 0);
            this.sendChatButton.Name = "sendChatButton";
            this.sendChatButton.Size = new System.Drawing.Size(52, 37);
            this.sendChatButton.TabIndex = 6;
            this.sendChatButton.Text = "Send";
            this.sendChatButton.UseVisualStyleBackColor = true;
            this.sendChatButton.Click += new System.EventHandler(this.sendChatButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.userListControl);
            this.splitContainer1.Size = new System.Drawing.Size(517, 353);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chatWrapperPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.sendChatRichTextBox);
            this.splitContainer2.Panel2.Controls.Add(this.sendChatButton);
            this.splitContainer2.Size = new System.Drawing.Size(400, 353);
            this.splitContainer2.SplitterDistance = 312;
            this.splitContainer2.TabIndex = 8;
            // 
            // chatWebBrowser
            // 
            this.chatWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.chatWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.chatWebBrowser.Name = "chatWebBrowser";
            this.chatWebBrowser.Size = new System.Drawing.Size(396, 308);
            this.chatWebBrowser.TabIndex = 0;
            // 
            // sendChatRichTextBox
            // 
            this.sendChatRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sendChatRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.sendChatRichTextBox.Name = "sendChatRichTextBox";
            this.sendChatRichTextBox.Size = new System.Drawing.Size(348, 37);
            this.sendChatRichTextBox.TabIndex = 7;
            this.sendChatRichTextBox.Text = "";
            this.sendChatRichTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendChatRichTextBox_KeyDown);
            // 
            // userListControl
            // 
            this.userListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userListControl.Location = new System.Drawing.Point(0, 0);
            this.userListControl.Name = "userListControl";
            this.userListControl.Size = new System.Drawing.Size(113, 353);
            this.userListControl.TabIndex = 0;
            // 
            // chatWrapperPanel
            // 
            this.chatWrapperPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chatWrapperPanel.Controls.Add(this.chatWebBrowser);
            this.chatWrapperPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatWrapperPanel.Location = new System.Drawing.Point(0, 0);
            this.chatWrapperPanel.Name = "chatWrapperPanel";
            this.chatWrapperPanel.Size = new System.Drawing.Size(400, 312);
            this.chatWrapperPanel.TabIndex = 1;
            // 
            // ChatUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ChatUserControl";
            this.Size = new System.Drawing.Size(517, 353);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.chatWrapperPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button sendChatButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UserListControl userListControl;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox sendChatRichTextBox;
        private System.Windows.Forms.WebBrowser chatWebBrowser;
        private System.Windows.Forms.Panel chatWrapperPanel;
    }
}
