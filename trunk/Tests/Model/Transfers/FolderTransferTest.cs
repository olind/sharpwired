using Moq;
using NUnit.Framework;
using SharpWired.Model.Files;
using SharpWired.Model.Transfers;

namespace SharpWired.Tests.Model.Transfers {
	[TestFixture]
	public class FolderTransferTest {
		[Test, Ignore]
		public void ShouldGetFolderSize_test1() {
			var mocks = new MockFactory(MockBehavior.Strict);
			var folder = mocks.Create<IFolder>();
			folder.Setup(x => x.Count).Returns(1);

			var transfer = new FolderTransfer(null, folder.Object, "");
			var size = transfer.Size;

			mocks.VerifyAll();
			Assert.That(size, Is.EqualTo(1));
		}

		[Test, Ignore]
		public void ShouldGetFolderSize_test2() {
			var node = new Mock<IFolder>();
			node.Setup(x => x.Count).Returns(1);

			var transfer = new FolderTransfer(null, node.Object, "");
			var size = transfer.Size;

			Assert.That(size, Is.EqualTo(1));
		}
	}
}