using ManyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftClient.Commands {

    public class DownloadCommand : SwiftClientCommand {

        public DownloadCommand() {
            IsCommand("download", "Download objects from containers.");
        }

        public override int Execute() {

            return 0;
        }
    }
}
