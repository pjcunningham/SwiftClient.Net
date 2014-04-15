using ManyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftClient.Command.Post
{
    public class PostCommand : ConsoleCommand {

        public PostCommand() {
            IsCommand("post", "Updates meta information for the account, container, or object; creates containers if not present.");
        }

        public override int Run(string[] remainingArguments) {


            return 0;
        }
    }
}
