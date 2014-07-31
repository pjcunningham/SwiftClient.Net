using ManyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftClient.Commands {

    public class UploadCommand : SwiftClientCommand {

        public UploadCommand() {
            IsCommand("upload", "Uploads files or directories to the given container.");

            this.HasRequiredOption("container=", "Name of container to put the object in.", s => ContainerOption = s);
            this.HasRequiredOption("filename=", "Name of filename to upload.", s => FilenameOption = s);
            this.HasOption("objectname=", "Name of object.", s => ObjectNameOption = s);

        }

        public string ContainerOption { get; private set; }
        public string FilenameOption { get; private set; }
        public string ObjectNameOption { get; private set; }

        public override int Execute() {
            return this.Provider.UploadObject(ContainerOption, ObjectNameOption, FilenameOption);
        }
    }
}
