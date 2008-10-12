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
        private SWByte Offset { get; set; }
        private Commands Commands { get; set; }
        private SWByte LastBytesReceived { get; set; }

        public string Destination { get; set; }
        public FileSystemEntry Source { get; set; }
        public Status Status { get; set; }

        /// <summary>
        /// Gets the time left in seconds
        /// </summary>
        public TimeSpan? EstimatedTimeLeft {
            get {
                if (Speed.B <= 0)
                    return null;
                return TimeSpan.FromSeconds((Size.B - Received.B) / Speed.B); 
            }
        }
        public double Progress {
            get {
                if(Received.B == 0)
                    return 0;
                
                return (double)Received.B / (double)Size.B;
            }
        }
        public SWByte Size {
            get { return new SWByte(((FileNode)Source).Size); }
        }
        public SWByte Received {
            get {
                if (DownloadEntry.Socket != null)
                    return new SWByte(DownloadEntry.Socket.BytesTransferred);
                else
                    return new SWByte();
            }
        }

        /// <summary>
        /// Gets the speed in bytes / second
        /// </summary>
        public SWByte Speed { get; private set; }

        public Transfer(Commands commands, FileSystemEntry node, string destination, Int64 offset) {
            this.Source = node;
            this.Destination = destination;
            this.Status = Status.Idle;
            this.Commands = commands;
            this.Offset = new SWByte(0);
            this.Speed = new SWByte(0);
        }

        public void Request() {
            Status = Status.Pending;
            Commands.Get(Source.Path, Offset.B);
        }

        private void OnCompleted() { }

        internal void Start(string hash) {
            Status = Status.Active;
            LastBytesReceived = new SWByte();

            DownloadEntry =
                new DownloadEntry(ConnectionManager.CurrentBookmark.Transfer,
                                  (FileNode)Source, Destination, hash, Offset.B);

            DownloadEntry.Socket.Interval += OnInterval;
        }

        private void OnInterval(){
            Speed.B = Received.B - LastBytesReceived.B;
            LastBytesReceived = Received;
        }
    }
}
