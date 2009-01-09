using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpWired.Model.Files {
    public class NodeChildren : List<INode> {
        INode Parent { get; set; }

        public NodeChildren(INode parent) {
            Parent = parent;
        }

        public INode Add(INode node) {
            base.Add(node);
            node.Parent = Parent;
            return node;
        }

        public INode Remove(INode node) {
            node.Parent = null;
            base.Remove(node);
            return node;
        }
    }
}
