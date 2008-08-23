using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection.Transfers.Entries;
using SharpWired.Connection.Transfers;

namespace SharpWired.Model.Transfers {

    public class Transfers {

        List<Transfer> transfers = new List<Transfer>();

        public Transfers(FileTransferHandler fileTransferHandler) {
            fileTransferHandler.DownloadAdded += OnDownloadAdded;
        }

        public List<Transfer> AllTransfers {
            get { return  transfers; }
        }

        public delegate void TransferAddedDelegate(Transfer t);
        public event TransferAddedDelegate TransferAdded;

        void OnDownloadAdded(DownloadEntry d) {
            Transfer t = new Transfer(d);
            transfers.Add(t);
            if (TransferAdded != null)
                TransferAdded(t);
        }
    }
}
