using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection.Transfers.Entries;

namespace SharpWired.Connection.Transfers {
    public class TransferException : Exception {

        public TransferEntry Entry { get; set; }

        public TransferException(string message, TransferEntry entry)
            : base(message) {
            Entry = entry;
        }
    }
}
