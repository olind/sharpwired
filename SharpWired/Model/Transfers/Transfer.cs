using System;
using System.Linq;
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
        private long Offset { get; set; }
        private Commands Commands { get; set; }
        private long LastBytesReceived { get; set; }
        private const int SPEED_HISTORY_LENGTH = 10;

        public string Destination { get; set; }
        public FileSystemEntry Source { get; set; }
        public Status Status { get; set; }

        /// <summary>
        /// Gets the time left in seconds
        /// </summary>
        public TimeSpan? EstimatedTimeLeft {
            get {
                if (SpeedHistory.Count <= 0)
                    return null;
                return TimeSpan.FromSeconds((Size - Received) / (long)SpeedHistory.Average()); 
            }
        }
        public double Progress {
            get {
                if(Received == 0)
                    return 0;
                
                return (double)Received / (double)Size;
            }
        }
        public long Size {
            get { return ((FileNode)Source).Size; }
        }
        public long Received {
            get {
                if (DownloadEntry != null && DownloadEntry.Socket != null)
                    return DownloadEntry.Socket.BytesTransferred;
                else
                    return new long();
            }
        }
        /// <summary>
        /// Gets the speed in bytes / second
        /// </summary>
        public long Speed { get; private set; }
        private Queue<long> SpeedHistory { get; set; }

        public Transfer(Commands commands, FileSystemEntry node, string destination, Int64 offset) {
            this.Source = node;
            this.Destination = destination;
            this.Status = Status.Idle;
            this.Commands = commands;
            this.Offset = 0;
            this.Speed = 0;
            this.SpeedHistory = new Queue<long>(SPEED_HISTORY_LENGTH);
        }

        public void Request() {
            Status = Status.Pending;
            Commands.Get(Source.Path, Offset);
        }

        private void OnCompleted() { }

        internal void Start(string hash) {
            Status = Status.Active;
            LastBytesReceived = new long();

            DownloadEntry =
                new DownloadEntry(ConnectionManager.CurrentBookmark.Transfer,
                                  (FileNode)Source, Destination, hash, Offset);

            DownloadEntry.Socket.Interval += OnInterval;
        }

        private void OnInterval(){
            Speed = Received - LastBytesReceived;
            AddToSpeedHistory(Speed);
            LastBytesReceived = Received;
        }

        private void AddToSpeedHistory(long speed) {
            if (SpeedHistory.Count >= SPEED_HISTORY_LENGTH)
                SpeedHistory.Dequeue();

            SpeedHistory.Enqueue(speed);
        }

        public void Pause() {
            Status = Status.Idle;
            DownloadEntry.Stop();
            DownloadEntry = null;
        }
    }
}
