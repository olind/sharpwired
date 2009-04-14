using System;
using SharpWired.Model.Files;
using System.Threading;

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

        private void OnNodeUpdated(INode node) {
            Source.Updated -= OnNodeUpdated;
            Download();
        }

        public FolderTransfer(IFolder node, string destination) {
            Source = (INode)node;
            Destination = destination;
            Status = Status.Idle;
        }

        public void Start() {
        	Source.Updated += OnNodeUpdated;
            ConnectionManager.Commands.List(Source.FullPath);
        }
        
        private void Download() {
            System.IO.Directory.CreateDirectory(Destination);
            
            //Thread.Sleep(1000);
            
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