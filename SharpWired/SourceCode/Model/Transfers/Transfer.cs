using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection.Transfers.Entries;
using System.Windows.Forms;
using SharpWired.Model.Files;

namespace SharpWired.Model.Transfers {
    public class Transfer {

        private DownloadEntry downloadEntry;

        public Transfer(DownloadEntry downloadEntry) {
            downloadEntry.Completed += OnCompleted;
            this.downloadEntry = downloadEntry;
        }

        public TimeSpan EstimatedTimeLeft {
            get { return new TimeSpan(); }
        }

        public Int64 Progress {
            get { return downloadEntry.BytesReceived; }
        }

        public Int64 Size {
            get { return -1; }
        }

        public Int64 Speed {
            get { return -1; }
        }

        public FileSystemEntry ServerFilePath {
            get { return downloadEntry.Source; }
        }

        public string LocalFilePath {
            get { return downloadEntry.Destination; }
        }

        private void OnCompleted() {
            
        }
    }
}
