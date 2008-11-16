using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpWired.Model.Files;

namespace SharpWired.Model.Transfers {
    public interface ITransfer {
        string Destination { get; set; }
        FileSystemEntry Source { get; set; }
        Status Status { get; set; }
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
