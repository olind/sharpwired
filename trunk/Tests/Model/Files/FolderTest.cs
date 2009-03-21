using NUnit.Framework.SyntaxHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpWired.MessageEvents;
using SharpWired.Model.Files;

namespace SharpWired.Tests.Model.Files {
    [TestFixture]
    public class FolderTest {
    	Folder parentFolder;
    	int messageid = -1;
		string messageName = "test";
		string path1 = "/parent/child1";
		string path2 = "/parent/child2";
		string path3 = "/parent/child3";
		FileType fileType = FileType.FOLDER;
		int fileSize = 1;
		DateTime created = DateTime.MinValue;
		DateTime modified = DateTime.MaxValue;
		
    	private List<MessageEventArgs_410420> getListWith1Children(){
    		var message = new MessageEventArgs_410420(messageid, messageName, path1, fileType, fileSize, created, modified);
    		List<MessageEventArgs_410420> list = new List<MessageEventArgs_410420>();
    		list.Add(message);    		
    		return list;
    	}
		
		[SetUp]
		public void setUp(){
			parentFolder = new Folder("/parent", DateTime.MinValue, DateTime.MinValue, 1);
		}
		
		[Test]
		public void Getting_childrens_from_parent_with_no_childs() {
			var childrens = parentFolder.Children;
			Assert.That(childrens.Count, Is.EqualTo(0));
		}
		
		[Test]
		public void Adding_2_childrens_with_the_same_path_should_only_add_1() {
			List<MessageEventArgs_410420> list = getListWith1Children();
    		var message2 = new MessageEventArgs_410420(messageid, messageName, path1, fileType, fileSize, created, modified);
    		list.Add(message2);
			parentFolder.AddChildren(list);
			var childrens = parentFolder.Children;
			Assert.That(childrens.Count, Is.EqualTo(1), "Added 2 nodes with the same path should only return 1? Not sure about this...");
		}
		
		[Test]
		public void Adding_3_childrens_with_different_paths_should_add_3() {
			List<MessageEventArgs_410420> list = getListWith1Children();
    		var message2 = new MessageEventArgs_410420(messageid, messageName, path2, fileType, fileSize, created, modified);
    		var message3 = new MessageEventArgs_410420(messageid, messageName, path3, fileType, fileSize, created, modified);
    		list.Add(message2);
    		list.Add(message3);
			parentFolder.AddChildren(list);
			var childrens = parentFolder.Children;
			Assert.That(childrens.Count, Is.EqualTo(3));
		}
    
    	[Test]
    	public void Adding_the_same_object_twice_should_return_same_objects() {
    		var childrens = getListWith1Children();
    		parentFolder.AddChildren(childrens);
    		INode children1 = parentFolder.Children[0];    		
    		parentFolder.AddChildren(childrens);
    		INode children2 = parentFolder.Children[0];    		
    		Assert.That(parentFolder.Children.Count == 1, "Adding same object twice should only get one node");
    		Assert.That(children1, Is.EqualTo(children2), "Node is not the same object");
    	}
    }
}
