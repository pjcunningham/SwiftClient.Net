using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftClient.Commands
{
    public class DeleteFromFileCommand : SwiftClientCommand {

        public DeleteFromFileCommand() {
            IsCommand("deletefromfile", "Delete objects listed in a file from within a container.");
            this.HasRequiredOption("container=", "Name of container to delete the object from.", s => ContainerOption = s);
            this.HasRequiredOption("filename=", "Name of file containing the objects to delete.", s => FilenameOption = s);
        }

        public string ContainerOption { get; private set; }
        public string FilenameOption { get; private set; }

        public override int Execute() {

            return this.Provider.DeleteObjectsFromList(ContainerOption, FilenameOption);
        }
    }
}
