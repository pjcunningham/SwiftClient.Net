using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftClient.Commands
{
    public class DeleteContainerCommand : SwiftClientCommand {

        public DeleteContainerCommand() {
            IsCommand("deletecontainer", "Delete a container");
            this.HasRequiredOption("container=", "Name of container to delete.", s => ContainerOption = s);
        }

        public string ContainerOption { get; private set; }

        public override int Execute() {

            return this.Provider.DeleteContainer(ContainerOption);
        }
    }
}
