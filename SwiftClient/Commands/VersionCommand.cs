using ManyConsole;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;

namespace SwiftClient.Command.Version
{
    [Export(typeof(ConsoleCommand))]
    public class VersionCommand : ConsoleCommand {

        public VersionCommand() {
            IsCommand("version");
        }

        public override int Run(string[] remainingArguments) {

            Console.WriteLine("Assembly : {0}; Version: {1}", Assembly.GetEntryAssembly().GetName().Name, Assembly.GetEntryAssembly().GetName().Version);   
         
            return 0;
        }
    }
}
