using Moq;
using NUnit.Framework;
using SharpWired.Connection;
using SharpWired.Model.Files;
using SharpWired.Model.Transfers;
using System;

namespace SharpWired.Tests.Model.Transfers {
	
	[TestFixture]
	public class FolderTransferTest {
		
		MockFactory mocks;
		Mock<IFolder> folder;
		Mock<ICommands> commands;
		
		[SetUp]
		public void SetUp(){
			mocks = new MockFactory(MockBehavior.Strict);
			folder = mocks.Create<IFolder>();
			commands = mocks.Create<ICommands>();
			
			folder.Setup(x => x.Count).Returns(0);
			folder.Setup(x => x.FullPath).Returns("/my_server/test_folder");
			commands.Setup(x => x.List("/my_server/test_folder")).AtMostOnce();
		}
		
		[TearDown]
		public void TearDown() {
			mocks.VerifyAll();
			mocks = null;
			folder = null;
			commands = null;
		}
		
		[Test]
		public void ShouldGetCorrectEmptyFolderSize() {			
			var transfer = new FolderTransfer(commands.Object, null, folder.Object, "");
			Assert.That(transfer.Size, Is.EqualTo(0));
		}
		
		[Test]
		public void ShouldGetCorrectNonEmptyFolderSize() {
			folder.Setup(x => x.Count).Returns(1);
			var transfer = new FolderTransfer(commands.Object, null, folder.Object, "");			
			Assert.That(transfer.Size, Is.EqualTo(1));
		}
		
		[Test]
		public void ShouldDownloadEmptyFolder() {
			var cwd = Environment.CurrentDirectory;
			
			// Create FolderTransfer with destination
			var transfer = new FolderTransfer(commands.Object, null, folder.Object, cwd);
			
			Assert.That(transfer.Progress, Is.EqualTo(0.0));
			Assert.That(transfer.Status, Is.EqualTo(Status.Idle));
			
			// Start FolderTransfer
			transfer.Start();
			
			Assert.That(transfer.Progress, Is.EqualTo(0.0));
			Assert.That(transfer.Status, Is.EqualTo(Status.Pending));
			
			// TODO: Added missing Children setup. Test works. Not sure if we want some special Children expect?
			folder.Setup(x => x.Children).Returns(new NodeChildren(null));
			folder.Raise(x => x.Updated -= null, folder.Object);

			// TODO: (överkurs) Wait for TransferDone callback
			
			Assert.That(transfer.Progress, Is.EqualTo(1.0));
			Assert.That(transfer.Status, Is.EqualTo(Status.Done));
			
			// TODO: VERIFY: Folder existis on disk
			
			// TODO: CLEANUP: Remove folder on disk
		}
	}
}