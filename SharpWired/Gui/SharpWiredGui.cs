using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using SharpWired.Model;
using SharpWired.Controller;

namespace SharpWired.Gui {
    class SharpWiredGui {
        public SharpWiredGui(SharpWiredModel model, SharpWiredController sharpWiredController) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new global::SharpWired.Gui.SharpWiredForm(model, sharpWiredController));
        }
    }
}
