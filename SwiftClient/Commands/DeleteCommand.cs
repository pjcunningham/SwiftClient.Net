using ManyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftClient.Command.Delete
{
    public class DeleteCommand : ConsoleCommand {

        public DeleteCommand() {
            IsCommand("delete", "Delete a container or objects within a container.");
        }

        public override int Run(string[] remainingArguments) {


            return 0;
        }
    }
}
