using ManyConsole;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace SwiftClient.Command.Download
{
    [Export(typeof(ConsoleCommand))]
    public class DownloadCommand : ConsoleCommand {

        public DownloadCommand() {
            IsCommand("download", "Download objects from containers.");
        }

        public override int Run(string[] remainingArguments) {


            return 0;
        }
    }
}
