using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftClient.Commands
{
    public class DeleteCommand : SwiftClientCommand {

        public DeleteCommand() {
            IsCommand("delete", "Delete an object within a container.");
            this.HasRequiredOption("container=", "Name of container to delete the object from.", s => ContainerOption = s);
            this.HasRequiredOption("objectname=", "Name of object to delete.", s => ObjectnameOption = s);
        }

        public string ContainerOption { get; private set; }
        public string ObjectnameOption { get; private set; }

        public override int Execute() {

            return this.Provider.DeleteObject(ContainerOption, ObjectnameOption);;
        }
    }
}
