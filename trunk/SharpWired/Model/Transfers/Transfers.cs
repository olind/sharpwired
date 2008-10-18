using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection.Transfers.Entries;
using SharpWired.Connection.Transfers;
using SharpWired.Model.Files;
using SharpWired.Connection;
using SharpWired.MessageEvents;

namespace SharpWired.Model.Transfers {
    public class Transfers {
        private List<Transfer> transfers = new List<Transfer>();
        private Commands Commands { get; set; } // TODO: Move to ModelBase!
        private Messages Messages { get; set; } // TODO: Move to ModelBase!

        public List<Transfer> AllTransfers { get { return transfers; } }
        public delegate void TransferDelegate(Transfer t);
        public event TransferDelegate TransferAdded;
        public event TransferDelegate TransferRemoved;

        public Transfers(ConnectionManager connectionManager) {
            this.Commands = connectionManager.Commands;
            this.Messages = connectionManager.Messages;

            this.Messages.TransferReadyEvent += OnTransferReady;
        }

        public Transfer Add(FileSystemEntry node, string target) {
            return Add(node, target, 0);
        }

        public Transfer Add(FileSystemEntry node, string target, Int64 offset) {
            Transfer transfer = new Transfer(Commands, node, target, offset);
            transfers.Add(transfer);

            if(TransferAdded != null)
                TransferAdded(transfer);

            return transfer;
        }

        public void Request(Transfer transfer) {
            transfer.Request();
        }

        public void Pause(Transfer transfer) {
            throw new NotImplementedException();
        }

        public void Remove(Transfer transfer) {
            throw new NotImplementedException();
        }

        private void OnTransferReady(MessageEventArgs_400 args) {
            Transfer transfer = transfers.Find(t => t.Source.Path == args.Path);
            if(transfer != null)
                transfer.Start(args.Hash);
        }
    }
}
