#region Information and licence agreements
/**
 * BookmarkManagerGUI.Designer.cs
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

namespace SharpWired.Gui
{
	partial class BookmarkManagerGUI
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
			this.bookmarkList = new System.Windows.Forms.ListView();
			this.deleteButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.bookmarkEntryControl1 = new SharpWired.Gui.BookmarkEntryControl();
			this.SuspendLayout();
			// 
			// bookmarkList
			// 
			this.bookmarkList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.bookmarkList.Location = new System.Drawing.Point(3, 3);
			this.bookmarkList.MultiSelect = false;
			this.bookmarkList.Name = "bookmarkList";
			this.bookmarkList.Size = new System.Drawing.Size(325, 118);
			this.bookmarkList.TabIndex = 1;
			this.bookmarkList.UseCompatibleStateImageBehavior = false;
			this.bookmarkList.View = System.Windows.Forms.View.List;
			this.bookmarkList.SelectedIndexChanged += new System.EventHandler(this.bookmarkList_SelectedIndexChanged);
			// 
			// deleteButton
			// 
			this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.deleteButton.Enabled = false;
			this.deleteButton.Location = new System.Drawing.Point(3, 356);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(53, 23);
			this.deleteButton.TabIndex = 2;
			this.deleteButton.Text = "Delete";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// addButton
			// 
			this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.addButton.Location = new System.Drawing.Point(216, 356);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(53, 23);
			this.addButton.TabIndex = 3;
			this.addButton.Text = "Add";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.applyButton.Location = new System.Drawing.Point(275, 356);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(53, 23);
			this.applyButton.TabIndex = 4;
			this.applyButton.Text = "Apply";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// bookmarkEntryControl1
			// 
			this.bookmarkEntryControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.bookmarkEntryControl1.Location = new System.Drawing.Point(3, 127);
			this.bookmarkEntryControl1.Name = "bookmarkEntryControl1";
			this.bookmarkEntryControl1.Size = new System.Drawing.Size(325, 223);
			this.bookmarkEntryControl1.TabIndex = 0;
			this.bookmarkEntryControl1.ValueChanged += new SharpWired.Gui.BookmarkEntryControl.ValueChangedDelegate(this.bookmarkEntryControl1_ValueChanged);
			// 
			// BookmarkManagerGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.bookmarkList);
			this.Controls.Add(this.bookmarkEntryControl1);
			this.Name = "BookmarkManagerGUI";
			this.Size = new System.Drawing.Size(331, 385);
			this.Load += new System.EventHandler(this.BookmarkManagerGUI_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private BookmarkEntryControl bookmarkEntryControl1;
		private System.Windows.Forms.ListView bookmarkList;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button applyButton;



	}
}
