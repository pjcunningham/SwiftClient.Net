﻿using ManyConsole;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;

namespace SwiftClient.Commands {

    public class ListCommand : SwiftClientCommand {

        public ListCommand() {
            IsCommand("list", "Lists the containers for the account or the objects for a container.");

            this.HasRequiredOption("container=", "Name of container to list object in.", s => ContainerOption = s);

            this.HasOption("long", "Long listing format, similar to ls -l.", b => LongOption = true);
            this.HasOption("lh", "Report sizes in human readable format similar to ls -lh.", b => LhOption = true);
            this.HasOption("prefix", "Only list items beginning with the prefix.", s => PrefixOption = s);
            this.HasOption("totals", "Used with -l or --lh, only report totals", b => TotalsOption = true);
            this.HasOption("delimiter", "Roll up items with the given delimiter. For containers only.", b => DelimiterOption = true);

        }

        public bool LongOption { get; private set; }
        public bool LhOption { get; private set; }
        public string PrefixOption { get; private set; }
        public bool TotalsOption { get; private set; }
        public bool DelimiterOption { get; private set; }

        public string ContainerOption { get; private set; }

        public override int Execute() {
            if (this.ContainerOption == null) {
                return this.Provider.ListContainers();
            } else {
                return this.Provider.ListContainer(ContainerOption, PrefixOption);
            }
        }

    }
}
