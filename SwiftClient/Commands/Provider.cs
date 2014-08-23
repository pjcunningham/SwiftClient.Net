using net.openstack.Core.Domain;
using net.openstack.Providers.Rackspace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SwiftClient.Commands {
    public class Provider {

        private readonly string fUser;
        private readonly string fKey;
        private readonly string fRegion;

        public Provider(string user, string key, string region) {
            fUser = user;
            fKey = key;
            fRegion = region;
        }

        private static void WriteErrorMessage(string message) {
            var savedColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = savedColor;
        }

        private void ProgressCallback(long value) {
            Console.WriteLine("Progress : {0}", value);
        }

        private CloudIdentity CreateIdentity(string username, string apikey) {

            var result = new CloudIdentity() {
                Username = username,
                APIKey = apikey
            };

            var provider = new CloudIdentityProvider();
            var ua = provider.Authenticate(result);
            return result;
        }

        private void UploadObject(CloudIdentity cloudIdentity, string container, string objectname, string filename) {
            var provider = new CloudFilesProvider(cloudIdentity);
            provider.CreateObjectFromFile(container: container, filePath: filename, objectName: objectname, progressUpdated: ProgressCallback);
        }

        private void DeleteObject(CloudIdentity cloudIdentity, string container, string objectname) {
            var provider = new CloudFilesProvider(cloudIdentity);
            provider.DeleteObject(container: container, objectName: objectname, region: fRegion);
        }

        private void DeleteContainer(CloudIdentity cloudIdentity, string container) {
            var provider = new CloudFilesProvider(cloudIdentity);
            provider.DeleteContainer(container: container, deleteObjects: true, region: fRegion);
        }

        private IEnumerable<Container> ListContainers(CloudIdentity cloudIdentity, int? limit = null, string region = null, bool useInternalUrl = false) {
            var provider = new CloudFilesProvider(cloudIdentity);

            Container lastContainer = null;

            do {
                string marker = lastContainer != null ? lastContainer.Name : null;
                IEnumerable<Container> containerObjects = provider.ListContainers(limit, marker, null, region, useInternalUrl, cloudIdentity);
                lastContainer = null;
                foreach (Container containerObject in containerObjects) {
                    lastContainer = containerObject;
                    yield return containerObject;
                }
            } while (lastContainer != null);

        }

        private IEnumerable<ContainerObject> ListContainer(CloudIdentity cloudIdentity, string container, int? limit = null, string prefix = null, string region = null, bool useInternalUrl = false) {
            var provider = new CloudFilesProvider(cloudIdentity);

            ContainerObject lastContainerObject = null;

            do {
                string marker = lastContainerObject != null ? lastContainerObject.Name : null;
                IEnumerable<ContainerObject> containerObjects = provider.ListObjects(container, limit, marker, null, prefix, region, useInternalUrl, cloudIdentity);
                lastContainerObject = null;
                foreach (ContainerObject containerObject in containerObjects) {
                    lastContainerObject = containerObject;
                    yield return containerObject;
                }
            } while (lastContainerObject != null);

        }

        public int ListContainers() {

            try {
                var ci = CreateIdentity(fUser, fKey);

                foreach (Container container in ListContainers(ci, region: fRegion))
                    Console.WriteLine(container.Name);
                return 0;
            } catch (Exception ex) {
                WriteErrorMessage(ex.Message);
                return -1;
            }

        }

        public int ListContainer(string container, string prefix) {

            try {
                var ci = CreateIdentity(fUser, fKey);
                foreach (ContainerObject containerObject in ListContainer(ci, container, prefix: prefix, region: fRegion))
                    Console.WriteLine(containerObject.Name);
                return 0;
            } catch (Exception ex) {
                WriteErrorMessage(ex.Message);
                return -1;
            }

        }

        public int UploadObject(string container, string objectname, string filename) {

            try {
                var ci = CreateIdentity(fUser, fKey);
                UploadObject(ci, container, objectname, filename);
                return 0;
            } catch (Exception ex) {
                WriteErrorMessage(ex.Message);
                return -1;
            }

        }

        public int DeleteObjectsFromList(string container, string filename) {

            try {
                var ci = CreateIdentity(fUser, fKey);
                var provider = new CloudFilesProvider(ci);
                foreach (var chunk in File.ReadLines(filename).ChunkData(8192)) {
                    provider.DeleteObjects(container: container, objects: chunk, region: fRegion);
                }
                return 0;
            } catch (Exception ex) {
                WriteErrorMessage(ex.Message);
                return -1;
            }

        }

        public int DeleteObject(string container, string filename) {

            try {
                var ci = CreateIdentity(fUser, fKey);
                DeleteObject(ci, container, filename);
                return 0;
            } catch (Exception ex) {
                WriteErrorMessage(ex.Message);
                return -1;
            }

        }

        public int DeleteContainer(string container) {

            try {
                var ci = CreateIdentity(fUser, fKey);
                DeleteContainer(ci, container);
                return 0;
            } catch (Exception ex) {
                WriteErrorMessage(ex.Message);
                return -1;
            }

        }

    }
}
