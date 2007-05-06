#region Information and licence agreements
/**
 * BookmarkEntryControl.Designer.cs
 * Created by Peter Holmdal, 2006-12-03
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

namespace SharpWired.Gui.Bookmarks
{
	partial class BookmarkEntryControl
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.passwordBox = new System.Windows.Forms.MaskedTextBox();
			this.portUpDown = new System.Windows.Forms.NumericUpDown();
			this.serverNameBox = new System.Windows.Forms.TextBox();
			this.bookmarkEntryToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.userBox = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.userNameBox = new System.Windows.Forms.TextBox();
			this.nickBox = new System.Windows.Forms.TextBox();
			this.serverBox = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.machineNameBox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.portUpDown)).BeginInit();
			this.userBox.SuspendLayout();
			this.serverBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server Name";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(53, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Nick";
			// 
			// passwordBox
			// 
			this.passwordBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.passwordBox.Location = new System.Drawing.Point(88, 77);
			this.passwordBox.Name = "passwordBox";
			this.passwordBox.Size = new System.Drawing.Size(256, 20);
			this.passwordBox.TabIndex = 3;
			this.passwordBox.TextChanged += new System.EventHandler(this.serverNameBox_TextChanged);
			// 
			// portUpDown
			// 
			this.portUpDown.Location = new System.Drawing.Point(88, 78);
			this.portUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.portUpDown.Name = "portUpDown";
			this.portUpDown.Size = new System.Drawing.Size(69, 20);
			this.portUpDown.TabIndex = 4;
			this.portUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.portUpDown.ValueChanged += new System.EventHandler(this.portUpDown_ValueChanged);
			// 
			// serverNameBox
			// 
			this.serverNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.serverNameBox.Location = new System.Drawing.Point(88, 26);
			this.serverNameBox.Name = "serverNameBox";
			this.serverNameBox.Size = new System.Drawing.Size(256, 20);
			this.serverNameBox.TabIndex = 6;
			this.serverNameBox.TextChanged += new System.EventHandler(this.serverNameBox_TextChanged);
			// 
			// userBox
			// 
			this.userBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.userBox.Controls.Add(this.passwordBox);
			this.userBox.Controls.Add(this.label7);
			this.userBox.Controls.Add(this.label6);
			this.userBox.Controls.Add(this.label2);
			this.userBox.Controls.Add(this.userNameBox);
			this.userBox.Controls.Add(this.nickBox);
			this.userBox.Location = new System.Drawing.Point(-2, 110);
			this.userBox.Name = "userBox";
			this.userBox.Size = new System.Drawing.Size(350, 112);
			this.userBox.TabIndex = 7;
			this.userBox.TabStop = false;
			this.userBox.Text = "User Settings";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(29, 80);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(53, 13);
			this.label7.TabIndex = 1;
			this.label7.Text = "Password";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(27, 55);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55, 13);
			this.label6.TabIndex = 1;
			this.label6.Text = "Username";
			// 
			// userNameBox
			// 
			this.userNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.userNameBox.Location = new System.Drawing.Point(88, 52);
			this.userNameBox.Name = "userNameBox";
			this.userNameBox.Size = new System.Drawing.Size(256, 20);
			this.userNameBox.TabIndex = 6;
			this.userNameBox.TextChanged += new System.EventHandler(this.serverNameBox_TextChanged);
			// 
			// nickBox
			// 
			this.nickBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.nickBox.Location = new System.Drawing.Point(88, 26);
			this.nickBox.Name = "nickBox";
			this.nickBox.Size = new System.Drawing.Size(256, 20);
			this.nickBox.TabIndex = 6;
			this.nickBox.TextChanged += new System.EventHandler(this.serverNameBox_TextChanged);
			// 
			// serverBox
			// 
			this.serverBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.serverBox.Controls.Add(this.label5);
			this.serverBox.Controls.Add(this.label4);
			this.serverBox.Controls.Add(this.label1);
			this.serverBox.Controls.Add(this.machineNameBox);
			this.serverBox.Controls.Add(this.portUpDown);
			this.serverBox.Controls.Add(this.serverNameBox);
			this.serverBox.Location = new System.Drawing.Point(-2, 3);
			this.serverBox.Name = "serverBox";
			this.serverBox.Size = new System.Drawing.Size(350, 116);
			this.serverBox.TabIndex = 0;
			this.serverBox.TabStop = false;
			this.serverBox.Text = "Server Settings";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(56, 80);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(26, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Port";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 55);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Machine Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// machineNameBox
			// 
			this.machineNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.machineNameBox.Location = new System.Drawing.Point(88, 52);
			this.machineNameBox.Name = "machineNameBox";
			this.machineNameBox.Size = new System.Drawing.Size(256, 20);
			this.machineNameBox.TabIndex = 6;
			this.machineNameBox.TextChanged += new System.EventHandler(this.serverNameBox_TextChanged);
			// 
			// BookmarkEntryControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.userBox);
			this.Controls.Add(this.serverBox);
			this.Name = "BookmarkEntryControl";
			this.Size = new System.Drawing.Size(346, 223);
			((System.ComponentModel.ISupportInitialize)(this.portUpDown)).EndInit();
			this.userBox.ResumeLayout(false);
			this.userBox.PerformLayout();
			this.serverBox.ResumeLayout(false);
			this.serverBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.MaskedTextBox passwordBox;
		private System.Windows.Forms.NumericUpDown portUpDown;
		private System.Windows.Forms.TextBox serverNameBox;
		private System.Windows.Forms.ToolTip bookmarkEntryToolTip;
		private System.Windows.Forms.GroupBox userBox;
		private System.Windows.Forms.GroupBox serverBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox machineNameBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox nickBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox userNameBox;
	}
}
