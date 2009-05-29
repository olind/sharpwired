using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using SharpWired.Model.Files;

namespace SharpWired.Model.Transfers {
    public class FolderTransfer : ModelBase, ITransfer {
        public string Destination { get; private set; }
        public INode Source { get; private set; }
        
        public Status Status { 
        	get {
        		if(SubTransfers.Count == 0){
        			//TODO: Should return done when we are sure no subfolders should exist for this?
        		} else {
        			//TODO: Should return aggregated status of all subtransfers
        		}
        		return Status.Pending;
        	} 
        	set {
        		
        	}
        }

        public TimeSpan? EstimatedTimeLeft { get { throw new NotImplementedException(); } }

        public double Progress { get; private set; }

        public long Size { 
        	get {
        		if(((IFolder)Source).Count == 0){
        			return 0; 	
        		} else {
        			throw new NotImplementedException();
        		}
        	} 
        }

        public long Received { get { throw new NotImplementedException(); } }

        public long Speed { get { throw new NotImplementedException(); } }

        private Transfers Transfers { get; set; }
        private List<ITransfer> SubTransfers = new List<ITransfer>();

        public event TransferDoneDelegate TransferDone;

        private void OnNodeUpdated(INode node) {
        	if(!node.Equals(Source)){
        		throw new ArgumentException("Received updated callback for wrong node");
        	}

        	Status = Status.Active;
            Source.Updated -= OnNodeUpdated;
            
            foreach(var child in ((IFolder)node).Children) {
            	ITransfer t = Transfers.CreateTransfer(child, Path.Combine(Destination, child.Name), 0);
            	SubTransfers.Add(t);
            }
            
            Download();
        }
        
        public FolderTransfer(Transfers transfers, IFolder node, string destination) {
        	Transfers = transfers;
        	
            Source = (INode)node;
            Destination = destination;
            Status = Status.Idle;
            Progress = 0.0;
        }

        public void Start() {
        	Status = Status.Pending;
        	Source.Updated += OnNodeUpdated;
            ConnectionManager.Commands.List(Source.FullPath);
        }
        
        private void Download() {
            System.IO.Directory.CreateDirectory(Destination);
            
            foreach(var t in SubTransfers){
            	t.Start();
            }
            
            
            Progress = 1.0;
           	Status = Status.Done;

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