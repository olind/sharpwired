using System;
using System.Collections.Generic;
using System.Text;
using SharpWired.Connection.Transfers;
using SharpWired.Model;

namespace SharpWired.Controller {
    public class SharpWiredController {
        //TODO: Add regions
        private ChatController              chatController;
        private UserController              userController;
        private NewsController              newsController;
        private FileListingController       fileListingController;
        private FileTransferController      fileTransferController;
        private GroupController             groupController;
        private PrivateMessageController    privateMessagesController;

        private SharpWiredModel             model;

        public FileTransferController FileTransferController { get { return fileTransferController; } }
        public FileListingController FileListingController { get { return fileListingController; } }
        public ChatController ChatController { get { return chatController; } }
        public UserController UserController { get { return userController; } }
        
        public SharpWiredController(SharpWiredModel model) {
            model.LoggedIn += OnLoggedIn;
            this.model = model;
        }

        void OnLoggedIn(Server server) {
            server.Offline += OnOffline;

            chatController                  = new ChatController(model);
            userController                  = new UserController(model);
            groupController                 = new GroupController(model);
            newsController                  = new NewsController(model);
            fileListingController           = new FileListingController(model);
            fileTransferController          = new FileTransferController(model);
            privateMessagesController       = new PrivateMessageController(model);
        }

        void OnOffline() {
            chatController                  = null;
            userController                  = null;
            groupController                 = null;
            newsController                  = null;
            fileListingController           = null;
            fileTransferController          = null;
            privateMessagesController       = null;
        }
    }
}