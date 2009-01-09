using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpWired.Model.Files {
    public interface IFolder {
        long Count { get; }
        NodeChildren Children { get; }
        
        INode Get(string path);
    }
}
