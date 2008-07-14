#region Information and licence agreements
/*
 * SharpWiredGuiBase.cs 
 * Created by Ola Lindberg, 2008-07-09
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
using System.Text;
using System.Windows.Forms;
using SharpWired.Model;
using SharpWired.Controller;
using System.Diagnostics;

namespace SharpWired.Gui {
    /// <summary>
    /// Base class for SharpWired GUI files. Sets references to 
    /// shared resources (e.g. model and controller) when inited.
    /// </summary>
    public class SharpWiredGuiBase : UserControl { //I wanted to make this class abstract but then can't the visual studio designer handle it...

        protected SharpWiredController controller;
        protected SharpWiredModel model;

        private String browserHeader;
        private String browserFooter;
        private StringBuilder browserBody = new StringBuilder();
        private int altItemCounter = 0;

        private delegate void ToggleWindowsFormsControlCallback(Control control);
        private delegate void AppendHTMLToWebBrowserCallback(WebBrowser browser, GuiMessageItem guiMessage);
        private delegate void ResetWebBrowserCallback(WebBrowser browser);

        public virtual void Init(SharpWiredModel model, SharpWiredController controller) {
            this.model = model;
            this.controller = controller;

            model.Connected += OnConnected;
        }

        private void OnConnected(Server s) {
            s.Offline += OnOffline;
            s.Online += OnOnline;
        }

        protected virtual void OnOnline() { }
        protected virtual void OnOffline() { }

        //TODO: Move the HTML specifics to a class that extends 
        //SharpWiredGuiBase. Like the files class FilesGuiBase and 
        //have Chat + News extend that instead.
        private string AltItemBeginningHtml {
            get {
                if (altItemCounter % 2 == 0) {
                    altItemCounter++;
                    return "<div class=\"standard\">";
                }
                altItemCounter++;
                return "<div class=\"alternative\">";
            }
        }
        
        protected void ToggleWindowsFormControl(Control control) {
            if (this.InvokeRequired) {
                ToggleWindowsFormsControlCallback callback 
                    = new ToggleWindowsFormsControlCallback(ToggleWindowsFormControl);
                this.Invoke(callback, new object[] { control });
            } else {
                control.Enabled = !control.Enabled;
            }
        }

        protected void AppendHTMLToWebBrowser(WebBrowser browser, GuiMessageItem guiMessage) {
            if (this.InvokeRequired) {
                AppendHTMLToWebBrowserCallback c = new AppendHTMLToWebBrowserCallback(AppendHTMLToWebBrowser);
                this.Invoke(c, new object[] { browser, guiMessage });
            } else {
                browserBody.Append(this.AltItemBeginningHtml);
                browserBody.Append(guiMessage.GeneratedHTML);
                browserBody.Append("</div>");

                browser.DocumentText = browserHeader + browserBody + browserFooter;
            }
        }

        protected void ResetWebBrowser(WebBrowser browser) {
            if (this.InvokeRequired) {
                ResetWebBrowserCallback c = new ResetWebBrowserCallback(ResetWebBrowser);
                this.Invoke(c, new object[] { browser });
            } else {
                String chatStyleSheet = "<link href=\"" + GuiUtil.CSSFilePath + "\\GUI\\SharpWiredStyleSheet.css\" rel=\"stylesheet\" type=\"text/css\" />";
                String chatJavaScript = "<script>function pageDown () { if (window.scrollBy) window.scrollBy(0, window.innerHeight ? window.innerHeight : document.body.clientHeight); }</script>";

                browserHeader = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
                    "<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\" lang=\"en\">" +
                    "<head><title>SharpWired</title>" +
                        chatJavaScript +
                        chatStyleSheet +
                    "</head><body onload=\"pageDown(); return false;\">";

                browserFooter = "</body></html>";

                browser.DocumentText = browserHeader + browserFooter;
                browserBody.Length = 0;
            }
        }
    }
}
