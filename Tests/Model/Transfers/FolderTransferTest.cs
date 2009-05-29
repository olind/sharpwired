using SharpWired.Model.Files;
using Moq;
using NUnit.Framework;
using SharpWired.Model.Transfers;

namespace SharpWired.Tests.Model.Transfers {
	[TestFixture]
	public class FolderTransferTest {
		
		[Test, Ignore]
		public void ShouldGetFolderSize() {
			var node = new Mock<IFolder>();
			node.Setup(x => x.Count).Returns(1);
			
			FolderTransfer transfer =  new FolderTransfer(null, node.Object, "");
			var size = transfer.Size;
			
			Assert.That(size, Is.EqualTo(1));
		}
	}
}
