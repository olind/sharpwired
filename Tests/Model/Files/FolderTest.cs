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
    	Folder parentFolder = new Folder("/parent", DateTime.MinValue, DateTime.MinValue, 1);
    	
    	private List<MessageEventArgs_410420> getListWithOneChildren(){
    		int messageid = -1;
    		string messageName = "test";
    		string path = "/parent/child";
    		FileType fileType = FileType.FOLDER;
    		int fileSize = 1;
    		DateTime created = DateTime.MinValue;
    		DateTime modified = DateTime.MaxValue;
    		
    		var message = new MessageEventArgs_410420(messageid, messageName, path, fileType, fileSize, created, modified);
    		List<MessageEventArgs_410420> list = new List<MessageEventArgs_410420>();
    		list.Add(message);
    		
    		return list;
    	}
    
    	[Test]
    	public void Adding_the_same_object_twice_should_return_same_objects(){
    		var childrens = getListWithOneChildren();
    		
    		parentFolder.AddChildren(childrens);
    		INode children1 = parentFolder.Children[0];
    		
    		parentFolder.AddChildren(childrens);
    		INode children2 = parentFolder.Children[0];
    		
    		Assert.That(parentFolder.Children.Count == 1, "Adding same object twice should only get one node");
    		Assert.That(children1, Is.EqualTo(children2), "Node is not the same object");
    	}
    }
}
