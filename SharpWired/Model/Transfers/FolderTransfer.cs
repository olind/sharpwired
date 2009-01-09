using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpWired.Model.Files;

namespace SharpWired.Model.Transfers {
    public class FolderTransfer : ModelBase, ITransfer {

        public string Destination { get; private set; }
        public INode Source { get; private set; }
        public Status Status { get; private set; }

        public TimeSpan? EstimatedTimeLeft {
            get { throw new NotImplementedException(); }
        }

        public double Progress {
            get { throw new NotImplementedException(); }
        }

        public long Size {
            get { throw new NotImplementedException(); }
        }

        public long Received {
            get { throw new NotImplementedException(); }
        }

        public long Speed {
            get { throw new NotImplementedException(); }
        }

        public FolderTransfer(Folder node, string destination) {
            this.Source = node;
            this.Destination = destination;
            this.Status = Status.Idle;
        }

        public void Start() {
            //Get all file listings from source
            //Create folder on local disk
            //Create all files and folder transfers
        }

        public void Pause() {
            throw new NotImplementedException();
        }

        public void Cancel() {
            throw new NotImplementedException();
        }
    }
}
