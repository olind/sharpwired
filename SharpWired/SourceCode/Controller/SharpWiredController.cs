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
        private FileTransferHandler         fileTransferHandler; //TODO: Rename to controller
        private GroupController             groupController;
        private ErrorController             errorController;
        private PrivateMessageController    privateMessagesController;

        private SharpWiredModel                  model;

        public FileTransferHandler FileTransferHandler { get { return fileTransferHandler; } }
        public FileListingController FileListingController { get { return fileListingController; } }
        public ChatController ChatController { get { return chatController; } }
        
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
            fileTransferHandler             = new FileTransferHandler(model);
            errorController                 = new ErrorController(model);
            privateMessagesController       = new PrivateMessageController(model);
        }

        void OnOffline() {
            chatController                  = null;
            userController                  = null;
            groupController                 = null;
            newsController                  = null;
            fileListingController           = null;
            fileTransferHandler             = null;
            errorController                 = null;
            privateMessagesController       = null;
        }
    }
}