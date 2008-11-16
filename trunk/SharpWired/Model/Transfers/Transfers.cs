using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Model.Files;
using SharpWired.Connection;
using SharpWired.MessageEvents;

namespace SharpWired.Model.Transfers {
    public class Transfers : ModelBase {
        private List<ITransfer> transfers = new List<ITransfer>();

        public List<ITransfer> AllTransfers { get { return transfers; } }
        public delegate void TransferDelegate(ITransfer t);
        public event TransferDelegate TransferAdded;
        public event TransferDelegate TransferRemoved;

        public ITransfer Add(FileSystemEntry node, string target) {
            return Add(node, target, 0);
        }

        public ITransfer Add(FileSystemEntry node, string target, Int64 offset) {
            ITransfer transfer = new FileTransfer(node, target, offset);
            transfers.Add(transfer);

            if(TransferAdded != null)
                TransferAdded(transfer);

            return transfer;
        }

        public void Remove(ITransfer transfer) { throw new NotImplementedException(); }
    }
}
