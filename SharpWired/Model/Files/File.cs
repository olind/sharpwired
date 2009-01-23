using System;

namespace SharpWired.Model.Files {
    public class File : ANode, IFile {
        public long Size { get; private set; }

        public File(string path, DateTime created, DateTime modified, long size)
            : base(path, created, modified) {
            Size = size;
        }

        public override event UpdatedDelegate Updated;

        public override void Reload() {
            //FIXME: Run wired command STAT
            throw new NotImplementedException();
        }
    }
}