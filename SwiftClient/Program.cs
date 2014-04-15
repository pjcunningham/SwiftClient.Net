using ManyConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftClient {
    internal class Program {

        static int Main(string[] args) {

            Program p = new Program();

            var commands = ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }
    }
}
