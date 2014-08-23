using ManyConsole;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SwiftClient.Commands {
    public abstract class SwiftClientCommand : ConsoleCommand {

        public SwiftClientCommand() {
            this.HasOption("version", "Version flag option", b => VersionOption = true);
            this.HasOption("verbose", "Verbose flag option", b => VerboseOption = true);
            this.HasOption("debug", "Debug flag option", b => DebugOption = true);
            this.HasOption("quiet", "Quiet flag option", b => QuietOption = true);
            this.HasOption("output=", "Send output to file", s => OutputOption = s);

            this.HasRequiredOption("user=", "<username>", s => UserOption = s);
            this.HasRequiredOption("key=", "<api_key>", s => KeyOption = s);
            this.HasRequiredOption("region=", "<region>", s => RegionOption = s);

            this.SkipsCommandSummaryBeforeRunning();

        }


        public Provider Provider { get; private set; }

        public bool VersionOption { get; private set; }
        public bool VerboseOption { get; private set; }
        public bool DebugOption { get; private set; }
        public bool QuietOption { get; private set; }
        public string OutputOption { get; set; }

        public string UserOption { get; set; }
        public string KeyOption { get; set; }
        public string RegionOption { get; set; }

        public abstract int Execute();

        public override int Run(string[] remainingArguments) {

            Provider = new Provider(UserOption, KeyOption, RegionOption);

            if (string.IsNullOrEmpty(OutputOption)) {

                return Execute();

            } else {
                
                try {
                
                    using (var stream = new FileStream(OutputOption, FileMode.OpenOrCreate, FileAccess.Write)) {
                        using(var writer = new StreamWriter(stream)) {
                            Console.SetOut(writer);
                            return Execute();
                        }
                    }

                } catch (Exception e) {
                    
                    Console.WriteLine("Cannot open {0} for writing.", OutputOption);
                    Console.WriteLine(e.Message);
                    return -1;

                }
            }
        }

  
    }
}
