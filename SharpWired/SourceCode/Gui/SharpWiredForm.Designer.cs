#region Information and licence agreements
/**
 * SharpWiredForm.Designer.cs 
 * Created by Ola Lindberg, 2006-07-23
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

using SharpWired.Gui.Chat;
namespace SharpWired.Gui
{
    partial class SharpWiredForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookmarksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookmarkThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageBookmarksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSharpWiredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chatUserControl1 = new SharpWired.Gui.Chat.ChatUserControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.newsUserControl1 = new SharpWired.Gui.News.NewsUserControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.publicChatToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.newsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.filesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.transfersToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ExitToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_ServerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.bookmarksToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(617, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // bookmarksToolStripMenuItem
            // 
            this.bookmarksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bookmarkThisToolStripMenuItem,
            this.manageBookmarksToolStripMenuItem,
            this.toolStripSeparator2});
            this.bookmarksToolStripMenuItem.Name = "bookmarksToolStripMenuItem";
            this.bookmarksToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.bookmarksToolStripMenuItem.Text = "Bookmarks";
            this.bookmarksToolStripMenuItem.DropDownOpening += new System.EventHandler(this.bookmarksToolStripMenuItem_DropDownOpening);
            // 
            // bookmarkThisToolStripMenuItem
            // 
            this.bookmarkThisToolStripMenuItem.Name = "bookmarkThisToolStripMenuItem";
            this.bookmarkThisToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.bookmarkThisToolStripMenuItem.Text = "Bookmark This...";
            // 
            // manageBookmarksToolStripMenuItem
            // 
            this.manageBookmarksToolStripMenuItem.Name = "manageBookmarksToolStripMenuItem";
            this.manageBookmarksToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+M";
            this.manageBookmarksToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.manageBookmarksToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.manageBookmarksToolStripMenuItem.Text = "Manage Bookmarks...";
            this.manageBookmarksToolStripMenuItem.Click += new System.EventHandler(this.manageBookmarksToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutSharpWiredToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutSharpWiredToolStripMenuItem
            // 
            this.aboutSharpWiredToolStripMenuItem.Name = "aboutSharpWiredToolStripMenuItem";
            this.aboutSharpWiredToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.aboutSharpWiredToolStripMenuItem.Text = "About SharpWired";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 49);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(617, 425);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chatUserControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(609, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Public Chat";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chatUserControl1
            // 
            this.chatUserControl1.CSSFilePath = "C:\\Program\\Microsoft Visual Studio 8\\Common7\\IDE";
            this.chatUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatUserControl1.Location = new System.Drawing.Point(3, 3);
            this.chatUserControl1.Name = "chatUserControl1";
            this.chatUserControl1.Size = new System.Drawing.Size(603, 393);
            this.chatUserControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.newsUserControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(609, 399);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "News";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // newsUserControl1
            // 
            this.newsUserControl1.CSSFilePath = "C:\\Program\\Microsoft Visual Studio 8\\Common7\\IDE";
            this.newsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newsUserControl1.Location = new System.Drawing.Point(3, 3);
            this.newsUserControl1.Name = "newsUserControl1";
            this.newsUserControl1.Size = new System.Drawing.Size(603, 393);
            this.newsUserControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(609, 399);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Files";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(609, 399);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Transfers";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.publicChatToolStripButton,
            this.newsToolStripButton,
            this.filesToolStripButton,
            this.transfersToolStripButton,
            this.ExitToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(617, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";

            // 
            // publicChatToolStripButton
            // 
            this.publicChatToolStripButton.Image = global::SharpWired.Properties.Resources.internet_group_chat;
            this.publicChatToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.publicChatToolStripButton.Name = "publicChatToolStripButton";
            this.publicChatToolStripButton.Size = new System.Drawing.Size(80, 22);
            this.publicChatToolStripButton.Text = "Public Chat";
            // 
            // newsToolStripButton
            // 
            this.newsToolStripButton.Image = global::SharpWired.Properties.Resources.format_justify_left;
            this.newsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newsToolStripButton.Name = "newsToolStripButton";
            this.newsToolStripButton.Size = new System.Drawing.Size(53, 22);
            this.newsToolStripButton.Text = "News";
            // 
            // filesToolStripButton
            // 
            this.filesToolStripButton.Image = global::SharpWired.Properties.Resources.folder;
            this.filesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.filesToolStripButton.Name = "filesToolStripButton";
            this.filesToolStripButton.Size = new System.Drawing.Size(48, 22);
            this.filesToolStripButton.Text = "Files";
            // 
            // transfersToolStripButton
            // 
            this.transfersToolStripButton.Image = global::SharpWired.Properties.Resources.mail_send_receive;
            this.transfersToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.transfersToolStripButton.Name = "transfersToolStripButton";
            this.transfersToolStripButton.Size = new System.Drawing.Size(73, 22);
            this.transfersToolStripButton.Text = "Transfers";
            // 
            // ExitToolStripButton
            // 
            this.ExitToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ExitToolStripButton.Image = global::SharpWired.Properties.Resources.system_log_out;
            this.ExitToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExitToolStripButton.Name = "ExitToolStripButton";
            this.ExitToolStripButton.Size = new System.Drawing.Size(45, 22);
            this.ExitToolStripButton.Text = "Exit";
            this.ExitToolStripButton.Click += new System.EventHandler(this.ExitToolStripButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_ServerStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 474);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(617, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_ServerStatus
            // 
            this.toolStripStatusLabel_ServerStatus.Name = "toolStripStatusLabel_ServerStatus";
            this.toolStripStatusLabel_ServerStatus.Size = new System.Drawing.Size(71, 17);
            this.toolStripStatusLabel_ServerStatus.Text = "Disconnected";
            // 
            // SharpWiredForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 496);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(412, 300);
            this.Name = "SharpWiredForm";
            this.Text = "SharpWired - Chat";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookmarksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookmarkThisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageBookmarksToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSharpWiredToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private global::SharpWired.Gui.Chat.ChatUserControl chatUserControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton publicChatToolStripButton;
        private System.Windows.Forms.ToolStripButton newsToolStripButton;
        private System.Windows.Forms.ToolStripButton filesToolStripButton;
        private System.Windows.Forms.ToolStripButton transfersToolStripButton;
        private System.Windows.Forms.ToolStripButton ExitToolStripButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ServerStatus;
        private SharpWired.Gui.News.NewsUserControl newsUserControl1;
    }
}

