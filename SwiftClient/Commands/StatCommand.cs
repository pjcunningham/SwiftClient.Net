using ManyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftClient.Commands {
    public class StatCommand : SwiftClientCommand {

        public StatCommand()
            : base() {
            IsCommand("stat", "Displays information for the account, container, or object.");
        }

        public override int Execute() {
            if (VersionOption) {
                Console.WriteLine("Version Option is on");
            }

            if (VerboseOption) {
                Console.WriteLine("Verbose is on");
            }


            if (DebugOption) {
                Console.WriteLine("Debug Option is on");
            }

            if (QuietOption) {
                Console.WriteLine("Quiet Option is on");
            }

            return 0;
        }
    }
}
