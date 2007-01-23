#region Information and licence agreements
/**
 * UserItemControl.Designer.cs
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
    partial class UserItemControl
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
            this.userItemControlPanel = new System.Windows.Forms.Panel();
            this.imagePictureBox = new System.Windows.Forms.PictureBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.nickLabel = new System.Windows.Forms.Label();
            this.userItemControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // userItemControlPanel
            // 
            this.userItemControlPanel.Controls.Add(this.imagePictureBox);
            this.userItemControlPanel.Controls.Add(this.statusLabel);
            this.userItemControlPanel.Controls.Add(this.nickLabel);
            this.userItemControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userItemControlPanel.Location = new System.Drawing.Point(0, 0);
            this.userItemControlPanel.Name = "userItemControlPanel";
            this.userItemControlPanel.Size = new System.Drawing.Size(200, 32);
            this.userItemControlPanel.TabIndex = 0;
            // 
            // imagePictureBox
            // 
            this.imagePictureBox.Location = new System.Drawing.Point(0, 0);
            this.imagePictureBox.Name = "imagePictureBox";
            this.imagePictureBox.Size = new System.Drawing.Size(32, 32);
            this.imagePictureBox.TabIndex = 3;
            this.imagePictureBox.TabStop = false;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(35, 16);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 3);
            this.statusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusLabel.Size = new System.Drawing.Size(39, 16);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "Status";
            // 
            // nickLabel
            // 
            this.nickLabel.AutoSize = true;
            this.nickLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nickLabel.Location = new System.Drawing.Point(35, 0);
            this.nickLabel.Margin = new System.Windows.Forms.Padding(0);
            this.nickLabel.Name = "nickLabel";
            this.nickLabel.Padding = new System.Windows.Forms.Padding(2, 3, 0, 0);
            this.nickLabel.Size = new System.Drawing.Size(71, 16);
            this.nickLabel.TabIndex = 1;
            this.nickLabel.Text = "Nick Name";
            // 
            // UserItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.userItemControlPanel);
            this.Name = "UserItemControl";
            this.Size = new System.Drawing.Size(200, 32);
            this.Click += new System.EventHandler(this.UserItemControl_Click);
            this.userItemControlPanel.ResumeLayout(false);
            this.userItemControlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel userItemControlPanel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label nickLabel;
        private System.Windows.Forms.PictureBox imagePictureBox;
    }
}
