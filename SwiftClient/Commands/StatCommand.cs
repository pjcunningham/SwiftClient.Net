using ManyConsole;
using SwiftClient.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftClient.Command.Stat {
    public class StatCommand : SwiftClientCommand {

        public StatCommand()
            : base() {
            IsCommand("stat", "Displays information for the account, container, or object.");
        }

        public override int Run(string[] remainingArguments) {

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
