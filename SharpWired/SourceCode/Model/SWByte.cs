using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections;

namespace SharpWired.Model {
    public class SWByte : IComparable {

        public Int64 B { get; set; }
        public double GiB { get { return (double)B / (double)(1024 * 1024 * 1024); } }
        public double MiB { get { return (double)B / (double)(1024 * 1024); } }
        public double KiB { get { return (double)B / (double)1024; } }

        public SWByte() { }

        public SWByte(long value) {
            this.B = value;
        }

        public int CompareTo(object obj) {
            if (obj is int)
                return B.CompareTo((int)obj);
            else if (obj is short)
                return B.CompareTo((short)obj);
            else if (obj is long)
                return B.CompareTo((long)obj);

            return -1;
        }

        /// <summary>
        /// Gets the string in human readable format.
        /// </summary>
        /// <returns></returns>
        public string ToString() {
            return ToString("h");
        }

        /// <param name="format">Availble formats are: MiB, KiB, B, GiB and h (human readable) </param>
        /// <returns></returns>
        public string ToString(string format) {
            //TODO: Make format global for whole application?
            NumberFormatInfo nfi = new CultureInfo("en-US").NumberFormat;

            switch (format) {
                case "h":
                    return HumanReadable(B);
                case "GiB":
                    return GiB.ToString("N1", nfi) + " GiB";
                case "MiB":
                    return MiB.ToString("N1",nfi) + " MiB";
                case "KiB":
                    return KiB.ToString("N0", nfi) + " KiB";
                case "B":
                    return B.ToString() + " B"; 
            }

            return null;
        }

        private string HumanReadable(Int64 bytes) {
            if (bytes >= 1024 * 1024 * 1024)
                return ToString("GiB");
            else if (bytes >= 1024 * 1024)
                return ToString("MiB");
            else if (bytes >= 1024)
                return ToString("KiB");

            return ToString("B");
        }

        public override bool Equals(object obj) {
            if (obj is SWByte)
                return B.Equals(((SWByte)obj).B);
            else if (obj is long)
                return ((long)obj).Equals(B);

            return false;
        }
    }
}
