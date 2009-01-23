using System;
using SharpWired.Model.Files;

namespace SharpWired.Model.Transfers {
    public interface ITransfer {
        string Destination { get; }
        INode Source { get; }
        Status Status { get; }
        TimeSpan? EstimatedTimeLeft { get; }
        double Progress { get; }
        long Size { get; }
        long Received { get; }
        long Speed { get; }

        void Start();
        void Pause();
        void Cancel();
    }
}