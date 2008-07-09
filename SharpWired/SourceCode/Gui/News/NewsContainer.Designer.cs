#region Information and licence agreements
/*
 * NewsUserControl.Designer.cs
 * Created by Ola Lindberg, 2006-12-10
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

namespace SharpWired.Gui.News
{
    /// <summary>
    /// GUI class for news view
    /// </summary>
    partial class NewsContainer
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
            this.newsWebBrowser = new System.Windows.Forms.WebBrowser();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.newsWrapperPanel = new System.Windows.Forms.Panel();
            this.postNewsTextBox = new System.Windows.Forms.TextBox();
            this.postNewsButton = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.newsWrapperPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // newsWebBrowser
            // 
            this.newsWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newsWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.newsWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.newsWebBrowser.Name = "newsWebBrowser";
            this.newsWebBrowser.Size = new System.Drawing.Size(459, 268);
            this.newsWebBrowser.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.newsWrapperPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.postNewsTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.postNewsButton);
            this.splitContainer1.Size = new System.Drawing.Size(467, 335);
            this.splitContainer1.SplitterDistance = 273;
            this.splitContainer1.TabIndex = 1;
            // 
            // newsWrapperPanel
            // 
            this.newsWrapperPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newsWrapperPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newsWrapperPanel.Controls.Add(this.newsWebBrowser);
            this.newsWrapperPanel.Location = new System.Drawing.Point(3, 3);
            this.newsWrapperPanel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.newsWrapperPanel.Name = "newsWrapperPanel";
            this.newsWrapperPanel.Size = new System.Drawing.Size(461, 270);
            this.newsWrapperPanel.TabIndex = 1;
            // 
            // postNewsTextBox
            // 
            this.postNewsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.postNewsTextBox.Location = new System.Drawing.Point(3, 3);
            this.postNewsTextBox.Multiline = true;
            this.postNewsTextBox.Name = "postNewsTextBox";
            this.postNewsTextBox.Size = new System.Drawing.Size(391, 52);
            this.postNewsTextBox.TabIndex = 2;
            // 
            // postNewsButton
            // 
            this.postNewsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.postNewsButton.Location = new System.Drawing.Point(400, 2);
            this.postNewsButton.Name = "postNewsButton";
            this.postNewsButton.Size = new System.Drawing.Size(64, 54);
            this.postNewsButton.TabIndex = 1;
            this.postNewsButton.Text = "Post";
            this.postNewsButton.UseVisualStyleBackColor = true;
            this.postNewsButton.Click += new System.EventHandler(this.postNewsButton_Click);
            // 
            // NewsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "NewsUserControl";
            this.Size = new System.Drawing.Size(467, 335);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.newsWrapperPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser newsWebBrowser;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button postNewsButton;
        private System.Windows.Forms.Panel newsWrapperPanel;
        private System.Windows.Forms.TextBox postNewsTextBox;
    }
}
