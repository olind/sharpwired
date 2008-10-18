using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

using SharpWired.Model;

namespace SharpWired.Tests.Model {
    [TestFixture]
    public class SWByteTest : AssertionHelper {

        [Test]
        public void CreateByteObject() {
            Expect(new SWByte(), Is.TypeOf(typeof(SWByte)));
        }

        [Test]
        public void GetDefaultValue() {
            var swByte = new SWByte();
            Expect(swByte.B, Is.EqualTo(0));
        }

        [Test]
        public void ConstructWithValue() {
            var swByte = new SWByte(123);
            Expect(swByte.B, Is.EqualTo(123));
        }

        [Test]
        public void SetValue() {
            var swByte = new SWByte();
            swByte.B = 123;
            Expect(swByte.B, Is.EqualTo(123));
        }

        [Test]
        public void CompareToInt() {
            var myByte = new SWByte();
            Expect(myByte.CompareTo((int)0), Is.EqualTo(0));
            Expect(myByte.CompareTo(int.MaxValue), Is.EqualTo(-1));
            Expect(myByte.CompareTo(int.MinValue), Is.EqualTo(1));
        }

        [Test]
        public void CompareToInt16() {
            var myByte = new SWByte();
            Expect(myByte.CompareTo((Int16)0), Is.EqualTo(0));
            Expect(myByte.CompareTo(Int16.MaxValue), Is.EqualTo(-1));
            Expect(myByte.CompareTo(Int16.MinValue), Is.EqualTo(1));
        }

        [Test]
        public void CompareToInt32() {
            var myByte = new SWByte();
            Expect(myByte.CompareTo((Int32)0), Is.EqualTo(0));
            Expect(myByte.CompareTo(Int32.MaxValue), Is.EqualTo(-1));
            Expect(myByte.CompareTo(Int32.MinValue), Is.EqualTo(1));
        }

        [Test]
        public void CompareToInt64() {
            var myByte = new SWByte();
            Expect(myByte.CompareTo((Int64)0), Is.EqualTo(0));
            Expect(myByte.CompareTo(Int64.MaxValue), Is.EqualTo(-1));
            Expect(myByte.CompareTo(Int64.MinValue), Is.EqualTo(1));
        }

        [Test]
        public void CompareToLong() {
            var myByte = new SWByte();
            Expect(myByte.CompareTo((long)0), Is.EqualTo(0));
            Expect(myByte.CompareTo(long.MaxValue), Is.EqualTo(-1));
            Expect(myByte.CompareTo(long.MinValue), Is.EqualTo(1));
        }

        [Test]
        public void GetGiB() {
            var myByte = new SWByte();
            Expect(myByte.GiB, Is.EqualTo(0.0));
            myByte.B = 1024 * 1024 * 1024 * 1;
            Expect(myByte.GiB, Is.EqualTo(1.0));
            myByte.B = 67108864;
            Expect(myByte.GiB, Is.EqualTo(0.0625));
        }

        [Test]
        public void GetMiB() {
            var myByte = new SWByte();
            Expect(myByte.MiB, Is.EqualTo(0.0));
            myByte.B = 1024 * 1024 * 1;
            Expect(myByte.MiB, Is.EqualTo(1.0));
            myByte.B = 65536;
            Expect(myByte.MiB, Is.EqualTo(0.0625));
        }

        [Test]
        public void GetKiB() {
            var myByte = new SWByte();
            Expect(myByte.KiB, Is.EqualTo(0.0));
            myByte.B = 1024 * 1;
            Expect(myByte.KiB, Is.EqualTo(1.0));
            myByte.B = 65536;
            Expect(myByte.KiB, Is.EqualTo(64.0));
        }

        [Test]
        public void GetB() {
            var myByte = new SWByte();
            Expect(myByte.B, Is.EqualTo(0.0));
            myByte.B = 1024 * 1;
            Expect(myByte.B, Is.EqualTo(1024));
            myByte.B = 65536;
            Expect(myByte.B, Is.EqualTo(65536));
        }

        [Test]
        public void FormatGiB() {
            var myByte = new SWByte();
            Expect(myByte.ToString("GiB"), Is.EqualTo("0.0 GiB"));
            myByte.B = 1024 * 1024 * 1024 * 1;
            Expect(myByte.ToString("GiB"), Is.EqualTo("1.0 GiB"));
            myByte.B = 67108864;
            Expect(myByte.ToString("GiB"), Is.EqualTo("0.1 GiB"));
        }

        [Test]
        public void FormatMiB() {
            var myByte = new SWByte();
            Expect(myByte.ToString("MiB"), Is.EqualTo("0.0 MiB"));
            myByte.B = 1024 * 1024 * 1;
            Expect(myByte.ToString("MiB"), Is.EqualTo("1.0 MiB"));
            myByte.B = 65536;
            Expect(myByte.ToString("MiB"), Is.EqualTo("0.1 MiB"));
        }

        [Test]
        public void FormatKiB() {
            var myByte = new SWByte();
            Expect(myByte.ToString("KiB"), Is.EqualTo("0 KiB"));
            myByte.B = 511;
            Expect(myByte.ToString("KiB"), Is.EqualTo("0 KiB"));
            myByte.B = 1024 * 1;
            Expect(myByte.ToString("KiB"), Is.EqualTo("1 KiB"));
            myByte.B = 65536;
            Expect(myByte.ToString("KiB"), Is.EqualTo("64 KiB"));
        }

        [Test]
        public void FormatB() {
            var myByte = new SWByte();
            Expect(myByte.ToString("B"), Is.EqualTo("0 B"));
            myByte.B = 1;
            Expect(myByte.ToString("B"), Is.EqualTo("1 B"));
        }

        [Test]
        public void HumanReadableZeroByte() {
            var myByte = new SWByte();
            Expect(myByte.ToString(), Is.EqualTo("0 B"));
        }

        [Test]
        public void HumanReadable() {
            var myByte = new SWByte(1024 * 1024 * 1024 * 1);
            Expect(myByte.ToString(), Is.EqualTo("1.0 GiB"));
            myByte.B = 1024 * 1024 * 1;
            Expect(myByte.ToString(), Is.EqualTo("1.0 MiB"));
            myByte.B = 1024 * 1;
            Expect(myByte.ToString(), Is.EqualTo("1 KiB"));
            myByte.B = 1;
            Expect(myByte.ToString(), Is.EqualTo("1 B"));
        }

        [Test]
        public void Equality() {
            var x = new SWByte(0);
            var y = new SWByte(0);
            var z = new SWByte(0);

            // x.Equals(x) returns true.
            Expect(x.Equals(x), Is.True);

            // x. Equals (y) returns the same value as y. Equals (x).
            Expect(x.Equals(y), Is.EqualTo(y.Equals(x)));

            // if (x. Equals (y) && y. Equals (z)) returns true, then x. Equals (z) returns true.
            Expect(x.Equals(z) == true, Is.EqualTo(x.Equals(y) && y.Equals(z) == true));

            // Successive invocations of x. Equals (y) return the same value as long as the objects referenced by x and y are not modified.
            Expect(x.Equals(y), Is.True);
            Expect(x.Equals(y), Is.True);

            // x. Equals (null) returns false (for non-nullable value types only. For more information, see Nullable Types (C# Programming Guide).) 
            Expect(x.Equals(null), Is.False);
        }
    }
}
