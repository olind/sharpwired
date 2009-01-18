using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpWired.Model.Files;

namespace SharpWired.Gui.Files {
    public delegate void NodeSelectedDelegate(INode node);

    interface IFilesView {
        event NodeSelectedDelegate NodeSelected;

        void SetCurrentNode(INode node);
    }
}
