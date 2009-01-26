#region Information and licence agreements

/*
 * FolderNode.cs 
 * Created by Ola Lindberg, 2007-05-01
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
using System.Diagnostics;
using SharpWired.MessageEvents;

namespace SharpWired.Model.Files {
    /// <summary>Representation of a "Wired Folder"</summary>
    public class Folder : ANode, IFolder {
        public long Count { get; private set; }
        public NodeChildren Children { get; private set; }

        public bool HasChildren { get { return Count > 0; } }

        public Folder(string path, DateTime created, DateTime modified, long count)
            : base(path, created, modified) {
            Count = count;

            Children = new NodeChildren(this);
        }

        public override event UpdatedDelegate Updated;

        public override void Reload() {
            ConnectionManager.Commands.List(FullPath);
        }

        public void Reload(int depth) {
            //run wired command LIST
            //set depth som variabel
            //säg till alla barn att lista sig med depth - 1
            throw new NotImplementedException();
        }

        public virtual INode Get(string path) {
            if (FullPath == path) {
                return this;
            }

            foreach (var child in Children) {
                if (child.FullPath == path) {
                    return child;
                } else if (child is Folder) {
                    var found = ((Folder) child).Get(path);
                    if (found != null) {
                        return found;
                    }
                }
            }

            return null;
        }

        public void AddChildren(List<MessageEventArgs_410420> list) {
            Children.Clear();
            foreach (var message in list) {
                ANode newNode = null;
                if (message.FileType == FileType.FILE) {
                    newNode = new File(message.FullPath, message.Created, message.Modified, message.Size);
                } else if (message.FileType == FileType.FOLDER) {
                    newNode = new Folder(message.FullPath, message.Created, message.Modified, message.Size);
                } else if (message.FileType == FileType.UPLOADS) {
                    //FIXME: Why does this never get called?
                    throw new NotImplementedException();
                } else if (message.FileType == FileType.DROPBOX) {
                    //FIXME: Create DropBox
                    newNode = new Folder(message.FullPath, message.Created, message.Modified, message.Size);
                    Debug.WriteLine("MODEL:Folder -> AddChildren. Adding DropBox as Folder.");
                }
                Children.Add(newNode);
            }

            if (Updated != null) {
                Updated(this);
            }
        }
    }
}