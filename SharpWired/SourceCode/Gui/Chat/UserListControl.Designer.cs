#region Information and licence agreements
/**
 * UserListControl.Designer.cs
 * Created by Ola Lindberg, 2006-11-20
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

using System.Windows.Forms;
namespace SharpWired.Gui.Chat
{
    partial class UserListControl :UserControl
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
            this.userListFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // userListFlowLayoutPanel
            // 
            this.userListFlowLayoutPanel.BackColor = System.Drawing.SystemColors.Window;
            this.userListFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userListFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userListFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.userListFlowLayoutPanel.Name = "userListFlowLayoutPanel";
            this.userListFlowLayoutPanel.Size = new System.Drawing.Size(285, 329);
            this.userListFlowLayoutPanel.TabIndex = 0;
            // 
            // UserListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.userListFlowLayoutPanel);
            this.Name = "UserListControl";
            this.Size = new System.Drawing.Size(285, 329);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel userListFlowLayoutPanel;
    }
}
