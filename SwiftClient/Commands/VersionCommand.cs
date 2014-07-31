using ManyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SwiftClient.Commands {

    public class VersionCommand : ConsoleCommand {

        public VersionCommand() {
            IsCommand("version", "Shows executable version information.");
            this.SkipsCommandSummaryBeforeRunning();
        }

        public override int Run(string[] remainingArguments) {

            Console.WriteLine("Assembly : {0}; Version: {1}", Assembly.GetEntryAssembly().GetName().Name, Assembly.GetEntryAssembly().GetName().Version);

            return 0;
        }
    }
}
