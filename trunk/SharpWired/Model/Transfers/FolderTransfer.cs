using System;
using SharpWired.Model.Files;

namespace SharpWired.Model.Transfers {
    public class FolderTransfer : ModelBase, ITransfer {
        public string Destination { get; private set; }
        public INode Source { get; private set; }
        public Status Status { get; private set; }

        public TimeSpan? EstimatedTimeLeft { get { throw new NotImplementedException(); } }

        public double Progress { get { throw new NotImplementedException(); } }

        public long Size { get { throw new NotImplementedException(); } }

        public long Received { get { throw new NotImplementedException(); } }

        public long Speed { get { throw new NotImplementedException(); } }

        public event TransferDoneDelegate TransferDone;

        public FolderTransfer(IFolder node, string destination) {
            Source = (INode)node;
            Destination = destination;
            Status = Status.Idle;
        }

        public void Start() {
            //TODO: Get all file listings from source

            System.IO.Directory.CreateDirectory(Destination);
            if (TransferDone != null) {
                TransferDone();
            }

            //TODO: Create all files and folder transfers
        }

        public void Pause() {
            throw new NotImplementedException();
        }

        public void Cancel() {
            throw new NotImplementedException();
        }
    }
}