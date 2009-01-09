using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpWired.Model.Files {

    public delegate void UpdatedDelegate(INode node);

    public interface INode : IComparable {

        /* Based on this blog post:
         * http://dvanderboom.wordpress.com/2008/03/15/treet-implementing-a-non-binary-tree-in-c/
         */

        INode Parent { get; set; }
        INode Root { get; }
        int Depth { get; }

        string Name { get; }
        string Path { get; }
        DateTime Created { get; }
        DateTime Modified { get; }

        event UpdatedDelegate Updated;

        void Reload();
    }
}
