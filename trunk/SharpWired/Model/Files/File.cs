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
            //run wired command STAT
            throw new NotImplementedException();
        }

        /*public override void OnFileListing(MessageEventArgs_410420 message) {
            if (message.Path == Path) {
                if (message.FileType != FileType.FILE)
                    throw new NotImplementedException("FileType changed");

                Created = message.Created;
                Modified = message.Modified;
                Size = message.Size;
            }
        }*/

        /** Old Stuff ******************************************

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="args"></param>
        public FileNode(MessageEventArgs_410420 messageEventArgs)
            : base(messageEventArgs) {
            this.size = messageEventArgs.Size;
        }
        
        */
    }
}