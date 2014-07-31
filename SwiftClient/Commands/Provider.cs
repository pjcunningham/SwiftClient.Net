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

        public Provider(string user, string key) {
            fUser = user;
            fKey = key;
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

        private void ListContainers(CloudIdentity cloudIdentity) {
            var provider = new CloudFilesProvider(cloudIdentity);
            var containers = provider.ListContainers();
            foreach (var container in containers) {
                Console.WriteLine(container.Name);
            }
        }

        private void UploadObject(CloudIdentity cloudIdentity, string container, string objectname, string filename) {
            var provider = new CloudFilesProvider(cloudIdentity);
            provider.CreateObjectFromFile(container: container, filePath: filename, objectName: objectname, progressUpdated: ProgressCallback);
        }

        private void DeleteObject(CloudIdentity cloudIdentity, string container, string objectname) {
            var provider = new CloudFilesProvider(cloudIdentity);
            provider.DeleteObject(container: container, objectName: objectname);
        }

        private void DeleteObjectsFromList(CloudIdentity cloudIdentity, string container, string filename) {
            var provider = new CloudFilesProvider(cloudIdentity);

            foreach (string line in File.ReadLines(filename)) {
                provider.DeleteObject(container: container, objectName: line);
            }
            
        }

        private void DeleteContainer(CloudIdentity cloudIdentity, string container) {
            var provider = new CloudFilesProvider(cloudIdentity);
            provider.DeleteContainer(container: container, deleteObjects: true);
        }

        private void ListContainer(CloudIdentity cloudIdentity, string container, int? limit = null, string marker = null, string markerEnd = null, string prefix = null, string region = null, bool useInternalUrl = false) {
            var provider = new CloudFilesProvider(cloudIdentity);
            var containerObjects = provider.ListObjects(container, limit, marker, markerEnd, prefix, region, useInternalUrl, cloudIdentity);
            foreach (var containerObject in containerObjects) {
                Console.WriteLine(containerObject.Name);
            }
        }

        public int ListContainers() {

            try {
                var ci = CreateIdentity(fUser, fKey);
                ListContainers(ci);
                return 0;
            } catch (Exception ex) {
                WriteErrorMessage(ex.Message);
                return -1;
            }

        }

        public int ListContainer(string container, string prefix) {

            try {
                var ci = CreateIdentity(fUser, fKey);
                ListContainer(ci, container, prefix: prefix);
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
                DeleteObjectsFromList(ci, container, filename);
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
