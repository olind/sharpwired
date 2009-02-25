using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpWired.Model.Users;
using System.Drawing;
using NUnit.Framework.SyntaxHelpers;

namespace SharpWired.Tests.Model.Users {
    [TestFixture]
    public class NickColorTest {
        NickColor ola = new NickColor("ola");
        NickColor adam = new NickColor("adam");

        [Test]
        public void NickColor_should_not_be_black() {
            NickColor nc = new NickColor("ola");
            Color c = nc.RGB;
            Assert.That(c.A, Is.EqualTo(255));
            Assert.That(c.R, Is.Not.EqualTo(0));
            Assert.That(c.G, Is.Not.EqualTo(0));
            Assert.That(c.B, Is.Not.EqualTo(0));
        }

        [Test]
        public void Two_NickColors_should_not_be_equal() {
            Color c1 = ola.RGB;
            Color c2 = adam.RGB;
            Assert.That(c1, Is.Not.EqualTo(c2));
        }

        [Test]
        public void Same_NickColor_should_not_be_equal() {
            Color c1 = ola.RGB;
            Color c2 = adam.RGB;
            Assert.That(c1, Is.Not.EqualTo(c2));
        }

        [Test]
        public void Should_hash_two_nicks_that_should_not_be_equal() {
            Assert.That(ola.Hash, Is.Not.EqualTo(adam.Hash));
        }

        [Test]
        public void Should_get_HSL_value_from_hashed_nick() {
            Assert.Fail("TODO");
        }
    }
}
