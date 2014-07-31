using ManyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftClient.Commands {
    public class PostCommand : SwiftClientCommand {

        public PostCommand() {
            IsCommand("post", "Updates meta information for the account, container, or object; creates containers if not present.");
        }

        public override int Execute() {
            return 0;
        }
    }
}
