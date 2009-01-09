#region Information and licence agreements
/*
 * ANode.cs 
 * Created: 2007-05-01
 * Authors: Ola Lindberg
 *          Adam Lindberg
 * 
 * SharpWired - a Wired client.
 * See: http://www.zankasoftware.com/wired/ for more infromation about Wired
 * 
 * Copyright (C) SharpWired project
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
using SharpWired.MessageEvents;
using System.Diagnostics;

namespace SharpWired.Model.Files {
    public abstract class ANode : ModelBase, INode {
        public INode Parent { get; set; }
        public INode Root { get; private set; }

        public int Depth {
            get {
                if (Parent != null)
                    return Parent.Depth + 1;
                else
                    return 0;
            }
        }

        public string Name { get; private set; }
        public string Path { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        public abstract event UpdatedDelegate Updated;

        public ANode(string path, DateTime created, DateTime modified) {
            Debug.WriteLine("Adding node: " + path);

            if (path == "/")
                Name = "/";
            else
                Name = path.Substring(path.LastIndexOf('/') + 1);

            Path = path.Substring(0, path.LastIndexOf('/') + 1);

            Created = created;
            Modified = modified;
        }

        public abstract void Reload();

        public int CompareTo(object obj) {
            INode node = obj as INode;
            string thisString = "" + Name + Path + Created + Modified;
            string nodeString = "" + node.Name + node.Path + node.Created + node.Modified;

            return thisString.CompareTo(nodeString);
        }
    }
}
