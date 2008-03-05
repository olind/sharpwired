namespace SharpWired.Gui.Chat {
    partial class ChatControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.chatWebBrowser = new System.Windows.Forms.WebBrowser();
            this.chatWrapperPanel = new System.Windows.Forms.Panel();
            this.chatSplitContainer = new System.Windows.Forms.SplitContainer();
            this.topicWrapperPanel = new System.Windows.Forms.Panel();
            this.setByLabel = new System.Windows.Forms.Label();
            this.setByHeaderLabel = new System.Windows.Forms.Label();
            this.topicHeaderLabel = new System.Windows.Forms.Label();
            this.topicTextBox = new System.Windows.Forms.TextBox();
            this.sendChatRichTextBox = new System.Windows.Forms.RichTextBox();
            this.sendChatButton = new System.Windows.Forms.Button();
            this.chatWrapperPanel.SuspendLayout();
            this.chatSplitContainer.Panel1.SuspendLayout();
            this.chatSplitContainer.Panel2.SuspendLayout();
            this.chatSplitContainer.SuspendLayout();
            this.topicWrapperPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatWebBrowser
            // 
            this.chatWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.chatWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.chatWebBrowser.Name = "chatWebBrowser";
            this.chatWebBrowser.Size = new System.Drawing.Size(429, 165);
            this.chatWebBrowser.TabIndex = 1;
            // 
            // chatWrapperPanel
            // 
            this.chatWrapperPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chatWrapperPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chatWrapperPanel.Controls.Add(this.chatWebBrowser);
            this.chatWrapperPanel.Location = new System.Drawing.Point(3, 47);
            this.chatWrapperPanel.Name = "chatWrapperPanel";
            this.chatWrapperPanel.Size = new System.Drawing.Size(433, 169);
            this.chatWrapperPanel.TabIndex = 2;
            // 
            // chatSplitContainer
            // 
            this.chatSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.chatSplitContainer.Name = "chatSplitContainer";
            this.chatSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // chatSplitContainer.Panel1
            // 
            this.chatSplitContainer.Panel1.Controls.Add(this.chatWrapperPanel);
            this.chatSplitContainer.Panel1.Controls.Add(this.topicWrapperPanel);
            // 
            // chatSplitContainer.Panel2
            // 
            this.chatSplitContainer.Panel2.Controls.Add(this.sendChatRichTextBox);
            this.chatSplitContainer.Panel2.Controls.Add(this.sendChatButton);
            this.chatSplitContainer.Size = new System.Drawing.Size(439, 258);
            this.chatSplitContainer.SplitterDistance = 218;
            this.chatSplitContainer.TabIndex = 3;
            // 
            // topicWrapperPanel
            // 
            this.topicWrapperPanel.Controls.Add(this.setByLabel);
            this.topicWrapperPanel.Controls.Add(this.setByHeaderLabel);
            this.topicWrapperPanel.Controls.Add(this.topicHeaderLabel);
            this.topicWrapperPanel.Controls.Add(this.topicTextBox);
            this.topicWrapperPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topicWrapperPanel.Location = new System.Drawing.Point(0, 0);
            this.topicWrapperPanel.Name = "topicWrapperPanel";
            this.topicWrapperPanel.Size = new System.Drawing.Size(439, 41);
            this.topicWrapperPanel.TabIndex = 3;
            // 
            // setByLabel
            // 
            this.setByLabel.AutoSize = true;
            this.setByLabel.Location = new System.Drawing.Point(49, 25);
            this.setByLabel.Name = "setByLabel";
            this.setByLabel.Size = new System.Drawing.Size(0, 13);
            this.setByLabel.TabIndex = 3;
            // 
            // setByHeaderLabel
            // 
            this.setByHeaderLabel.AutoSize = true;
            this.setByHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setByHeaderLabel.Location = new System.Drawing.Point(3, 25);
            this.setByHeaderLabel.Name = "setByHeaderLabel";
            this.setByHeaderLabel.Size = new System.Drawing.Size(48, 13);
            this.setByHeaderLabel.TabIndex = 2;
            this.setByHeaderLabel.Text = "Set By:";
            // 
            // topicHeaderLabel
            // 
            this.topicHeaderLabel.AutoSize = true;
            this.topicHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topicHeaderLabel.Location = new System.Drawing.Point(3, 6);
            this.topicHeaderLabel.Name = "topicHeaderLabel";
            this.topicHeaderLabel.Size = new System.Drawing.Size(43, 13);
            this.topicHeaderLabel.TabIndex = 1;
            this.topicHeaderLabel.Text = "Topic:";
            // 
            // topicTextBox
            // 
            this.topicTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.topicTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.topicTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.topicTextBox.Location = new System.Drawing.Point(50, 6);
            this.topicTextBox.Name = "topicTextBox";
            this.topicTextBox.ReadOnly = true;
            this.topicTextBox.Size = new System.Drawing.Size(384, 13);
            this.topicTextBox.TabIndex = 0;
            this.topicTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.topicTextBox_KeyUp);
            this.topicTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chatTopicTextBox_MouseUp);
            // 
            // sendChatRichTextBox
            // 
            this.sendChatRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sendChatRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.sendChatRichTextBox.Name = "sendChatRichTextBox";
            this.sendChatRichTextBox.Size = new System.Drawing.Size(376, 30);
            this.sendChatRichTextBox.TabIndex = 8;
            this.sendChatRichTextBox.Text = "";
            this.sendChatRichTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.sendChatRichTextBox_KeyUp);
            // 
            // sendChatButton
            // 
            this.sendChatButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sendChatButton.Location = new System.Drawing.Point(385, 2);
            this.sendChatButton.Name = "sendChatButton";
            this.sendChatButton.Size = new System.Drawing.Size(51, 31);
            this.sendChatButton.TabIndex = 9;
            this.sendChatButton.Text = "Send";
            this.sendChatButton.UseVisualStyleBackColor = true;
            this.sendChatButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sendChatButton_MouseUp);
            // 
            // ChatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chatSplitContainer);
            this.Name = "ChatControl";
            this.Size = new System.Drawing.Size(439, 258);
            this.chatWrapperPanel.ResumeLayout(false);
            this.chatSplitContainer.Panel1.ResumeLayout(false);
            this.chatSplitContainer.Panel2.ResumeLayout(false);
            this.chatSplitContainer.ResumeLayout(false);
            this.topicWrapperPanel.ResumeLayout(false);
            this.topicWrapperPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser chatWebBrowser;
        private System.Windows.Forms.Panel chatWrapperPanel;
        private System.Windows.Forms.SplitContainer chatSplitContainer;
        private System.Windows.Forms.RichTextBox sendChatRichTextBox;
        private System.Windows.Forms.Button sendChatButton;
        private System.Windows.Forms.Panel topicWrapperPanel;
        private System.Windows.Forms.Label topicHeaderLabel;
        private System.Windows.Forms.TextBox topicTextBox;
        private System.Windows.Forms.Label setByLabel;
        private System.Windows.Forms.Label setByHeaderLabel;
    }
}
