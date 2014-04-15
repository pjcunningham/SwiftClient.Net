using ManyConsole;
using net.openstack.Core.Domain;
using net.openstack.Providers.Rackspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftClient.Command.Base {
    public abstract class SwiftClientCommand : ConsoleCommand {

        private static void WriteErrorMessage(string message) {
            var savedColor = Console.ForegroundColor;
            try {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
            } finally {
                Console.ForegroundColor = savedColor;
            }
        }

        private static CloudIdentity CreateIdentity(string username, string apikey) {
            
            var result = new CloudIdentity() {
                Username = username,
                APIKey = apikey
            };

            var provider = new CloudIdentityProvider();
            var ua = provider.Authenticate(result);
            return result;
        }

        private static void ListContainers(CloudIdentity cloudIdentity) {
            var provider = new CloudFilesProvider(cloudIdentity);
            var containers = provider.ListContainers();
            foreach (var container in containers) {
                Console.WriteLine(container.Name);
            }
        }

        private static void ListContainer(CloudIdentity cloudIdentity, string container, int? limit = null, string marker = null, string markerEnd = null, string prefix = null, string region = null, bool useInternalUrl = false) {
            var provider = new CloudFilesProvider(cloudIdentity);
            var containerObjects = provider.ListObjects(container, limit, marker, markerEnd, prefix, region, useInternalUrl, cloudIdentity);
            foreach (var containerObject in containerObjects) {
                Console.WriteLine(containerObject.Name);
            }
        }


        public SwiftClientCommand() {
            this.HasOption("version", "Version flag option", b => VersionOption = true);
            this.HasOption("verbose", "Verbose flag option", b => VerboseOption = true);
            this.HasOption("debug", "Debug flag option", b => DebugOption = true);
            this.HasOption("quiet", "Quiet flag option", b => QuietOption = true);

            this.HasRequiredOption("user=", "<username>", s => UserOption = s);
            this.HasRequiredOption("key=", "<api_key>", s => KeyOption = s);
        }

        public bool VersionOption { get; private set; }
        public bool VerboseOption { get; private set; }
        public bool DebugOption { get; private set; }
        public bool QuietOption { get; private set; }

        public string UserOption { get; set; }
        public string KeyOption { get; set; }

        public int ListContainers() {

            try {
                var ci = CreateIdentity(this.UserOption, this.KeyOption);
                ListContainers(ci);
                return 0;
            } catch (Exception ex) {
                WriteErrorMessage(ex.Message);
                return -1;
            }

        }


        public int ListContainer(string container, string prefix) {

            try {
                var ci = CreateIdentity(this.UserOption, this.KeyOption);
                ListContainer(ci, container, prefix:prefix);
                return 0;
            } catch (Exception ex) {
                WriteErrorMessage(ex.Message);
                return -1;
            }

        }

    }
}
