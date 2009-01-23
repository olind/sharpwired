using System;
using System.Collections.Generic;
using SharpWired.Model.Files;

namespace SharpWired.Model.Transfers {
    public class Transfers : ModelBase {
        private readonly List<ITransfer> transfers = new List<ITransfer>();

        public List<ITransfer> AllTransfers { get { return transfers; } }

        public delegate void TransferDelegate(ITransfer t);

        public event TransferDelegate TransferAdded;
        public event TransferDelegate TransferRemoved;

        public ITransfer Add(INode node, string target) {
            return Add(node, target, 0);
        }

        public ITransfer Add(INode node, string target, Int64 offset) {
            ITransfer transfer = null;

            if (node is INode) {
                transfer = new FileTransfer(node, target, offset);
            } else if (node is Folder) {
                transfer = new FolderTransfer((Folder) node, target);
            }

            if (transfer != null) {
                transfers.Add(transfer);

                if (TransferAdded != null) {
                    TransferAdded(transfer);
                }
            }
            return transfer;
        }

        public void Remove(ITransfer transfer) {
            throw new NotImplementedException();
        }
    }
}