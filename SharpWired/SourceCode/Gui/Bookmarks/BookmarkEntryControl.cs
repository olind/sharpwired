#region Information and licence agreements
/*
 * BookmarkEntryControl.cs
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpWired.Connection;
using SharpWired.Connection.Bookmarks;

namespace SharpWired.Gui.Bookmarks
{
	/// <summary>
	/// A control for showing and editing a BookmarkEntry.
	/// </summary>
	public partial class BookmarkEntryControl : UserControl
	{
		#region Constructors
		/// <summary>
		/// Inits.
		/// </summary>
		public BookmarkEntryControl()
		{
			InitializeComponent();
		}
		#endregion


		#region Get Bookmark
		/// <summary>
		/// Get a Bookmark created from the info entered into the control at present.
		/// </summary>
		/// <returns>A Bookmark.</returns>
		public Bookmark GetBookmark()
		{
			return new Bookmark(GetServer(), GetUser());
		}

		/// <summary>
		/// Get the user info. Password should be hashed!
		/// </summary>
		/// <returns></returns>
		private UserInformation GetUser()
		{
			return new UserInformation(	this.nickBox.Text.Trim(),
										this.userNameBox.Text.Trim(),
										Utility.HashPassword(this.passwordBox.Text));
		}

		/// <summary>
		/// Creates a Server from what is currently entered into the controls.
		/// </summary>
		/// <returns>A Server.</returns>
		private Server GetServer()
		{
			return new Server(	(int)this.portUpDown.Value,
								this.machineNameBox.Text.Trim(),
								this.serverNameBox.Text.Trim());
		}
		#endregion


		#region Set Bookmark.
		/// <summary>
		/// Set the info to display in the controls.
		/// </summary>
		/// <param name="bookmark">The Bookmark to set.</param>
		public void SetBookmark(Bookmark bookmark)
		{
			if (bookmark != null)
			{
				SetUser(bookmark.UserInformation);
				SetServer(bookmark.Server);
			}
			else
			{
				SetUser(null);
				SetServer(null);
			}
		}

		/// <summary>
		/// Set the user info to show.
		/// </summary>
		/// <param name="user">The UserInformation to show.</param>
		public void SetUser(UserInformation user)
		{
			this.suspendEvents = true;
			if (user == null)
			{
				this.userNameBox.Text = "";
				this.nickBox.Text = "";
				this.passwordBox.Text = "";				
			}
			else
			{
				this.userNameBox.Text = user.UserName;
				this.nickBox.Text = user.Nick;
				//this.passwordBox.Text = user.Password;
				this.passwordBox.Text = "";
			}
			this.suspendEvents = false;
		}

		/// <summary>
		/// Set the Server to display.
		/// </summary>
		/// <param name="server">The Server object to display.</param>
		public void SetServer(Server server)
		{
			this.suspendEvents = true;
			if (server == null)
			{
				this.serverNameBox.Text = "";
				this.machineNameBox.Text = "";
				this.portUpDown.Value = 0M;
			}
			else
			{
				this.serverNameBox.Text = server.ServerName;
				this.machineNameBox.Text = server.MachineName;
				this.portUpDown.Value = (decimal)server.ServerPort;
			}
			this.suspendEvents = false;
		}
		#endregion


		#region Events
		private bool suspendEvents = false;

		/// <summary>
		/// This is used for the ValueChanged event.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		public delegate void ValueChangedDelegate(object sender);
		/// <summary>
		/// Listen to this if you want to know when the Server name or port changed.
		/// </summary>
		public event ValueChangedDelegate ValueChanged;

		/// <summary>
		/// Triggers the ValueChanged event.
		/// </summary>
		protected virtual void OnValueChanged()
		{
			if (!suspendEvents)
				if (this.ValueChanged != null)
					ValueChanged(this);
		}

		private void serverNameBox_TextChanged(object sender, EventArgs e)
		{
			OnValueChanged();
		}

		private void portUpDown_ValueChanged(object sender, EventArgs e)
		{
			OnValueChanged();
		}
		#endregion
	}
}