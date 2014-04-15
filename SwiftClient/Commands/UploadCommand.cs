using ManyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftClient.Command.Upload {

    public class UploadCommand : ConsoleCommand {

        public UploadCommand() {
            IsCommand("upload", "Uploads files or directories to the given container.");
        }

        public override int Run(string[] remainingArguments) {


            return 0;
        }
    }
}
