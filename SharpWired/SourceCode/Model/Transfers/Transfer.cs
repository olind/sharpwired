using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection.Transfers.Entries;
using System.Windows.Forms;
using SharpWired.Model.Files;
using SharpWired.Connection;
using System.Diagnostics;

namespace SharpWired.Model.Transfers {

    public enum Status { Pending, Idle, Active }

    public class Transfer : ModelBase {
        private DownloadEntry DownloadEntry { get; set; }
        private Int64 Offset { get; set; }
        private Commands Commands { get; set; }

        public string Destination { get; set; }
        public FileSystemEntry Source { get; set; }
        public Status Status { get; set; }
        public TimeSpan EstimatedTimeLeft {
            get { return new TimeSpan(); }
        }
        public double Progress {
            get {
                if(DownloadEntry.BytesReceived == 0)
                    return 0;
                
                return (double)DownloadEntry.BytesReceived / (double)Size;;
            }
        }
        public Int64 Size {
            get { return ((FileNode)Source).Size; }
        }
        public Int64 Speed {
            get { return -1; }
        }

        public Transfer(Commands commands, FileSystemEntry node, string destination, Int64 offset) {
            this.Source = node;
            this.Destination = destination;
            this.Status = Status.Idle;
            this.Commands = commands;
        }

        public void Request() {
            Status = Status.Pending;
            Commands.Get(Source.Path, Offset);
        }

        private void OnCompleted() {
            
        }

        internal void Start(string hash) {
            Status = Status.Active;
            DownloadEntry =
                new DownloadEntry(ConnectionManager.CurrentBookmark.Transfer,
                                  (FileNode)Source, Destination, hash, Offset);
        }
    }
}
