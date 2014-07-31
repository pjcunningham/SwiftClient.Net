using ManyConsole;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SwiftClient {
    internal class Program {

        static int Main(string[] args) {

            try {

                Program p = new Program();

                var commands = ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
                return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);

            } catch (ReflectionTypeLoadException ex) {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions) {
                    sb.AppendLine(exSub.Message);
                    if (exSub is FileNotFoundException) {
                        FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog)) {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                Console.WriteLine(errorMessage);
                //Display or log the error based on your application.
                return 0;
            }
        }
    }
}
