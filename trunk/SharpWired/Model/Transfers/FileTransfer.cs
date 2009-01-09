using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SharpWired.Model.Files;
using SharpWired.Connection;
using System.Diagnostics;
using SharpWired.MessageEvents;
using System.IO;
using SharpWired.Connection.Sockets;

namespace SharpWired.Model.Transfers {

    public enum Status { Pending, Idle, Active }

    public class FileTransfer : ModelBase, ITransfer {
        private long Offset { get; set; }
        private Commands Commands { get; set; }
        private long LastBytesReceived { get; set; }
        private const int SPEED_HISTORY_LENGTH = 10;
        private BinarySecureSocket Socket { get; set; }

        public string Destination { get; private set; }
        public INode Source { get; private set; }
        public Status Status { get; private set; }

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
            get { return ((SharpWired.Model.Files.File)Source).Size; }
        }
        public long Received {
            get {
                if (Socket != null)
                    return Socket.BytesTransferred;
                else
                    return new long();
            }
        }
        /// <summary>
        /// Gets the speed in bytes / second
        /// </summary>
        public long Speed { get; private set; }
        private Queue<long> SpeedHistory { get; set; }

        public FileTransfer(INode node, string destination, Int64 offset) {
            this.Source = node;
            this.Destination = destination;
            this.Status = Status.Idle;
            this.Offset = 0;
            this.Speed = 0;
            this.SpeedHistory = new Queue<long>(SPEED_HISTORY_LENGTH);
        }

        public void Start() {
            //TODO: File exists on disk? Resume?
            ConnectionManager.Messages.TransferReadyEvent += OnTransferReady;
            Status = Status.Pending;
            ConnectionManager.Commands.Get(Source.Path, Offset);
        }

        private void OnTransferReady(MessageEventArgs_400 args) {
            if (Source.Path == args.Path) {
                ConnectionManager.Messages.TransferReadyEvent -= OnTransferReady;
                Status = Status.Active;
                LastBytesReceived = 0;

                CreateSocket(args.Hash);
            }
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
            Socket.Disconnect();
            Socket = null;
        }

        public void Cancel() { throw new NotImplementedException(); }

        private void CreateSocket(string hash) {
            Debug.WriteLine("Transfer is ready! File '" + Source.Name + "', with ID '" + hash + "'.");

            // TODO: FileMode.CreateNew should be used when resume works
            FileStream fileStream = new FileStream(Destination, FileMode.Create);

            this.Socket = new BinarySecureSocket();
            this.Socket.DataReceivedDoneEvent += OnDataReceivedDone;
            this.Socket.Connect(Model.ConnectionManager.CurrentBookmark.Transfer,
                fileStream, ((SharpWired.Model.Files.File)Source).Size, Offset);

            this.Socket.SendMessage("TRANSFER" + Utility.SP + hash);

            Socket.Interval += OnInterval;
        }

        void OnDataReceivedDone() {
            if (Socket != null) {
                Socket.DataReceivedDoneEvent -= OnDataReceivedDone;
                Socket.Interval -= OnInterval;
            }
        }
    }
}
