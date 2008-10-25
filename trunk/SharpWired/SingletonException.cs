using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpWired {
    public class SingletonException : Exception {
        public SingletonException(string message) : base(message) { }
    }
}
