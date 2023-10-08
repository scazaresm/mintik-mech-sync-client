using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync
{
    public class LocalProjectAnalyzer
    {
        private readonly LocalProject localProject;
        private readonly IChecksumCalculator checksumCalculator;
        private Dictionary<string, FileMetadata> localFileIndex;
        private Dictionary<string, FileMetadata> remoteFileIndex;

        private object lockObject = new object();

        public LocalProjectAnalyzer(LocalProject localProject, IChecksumCalculator checksymCalculator)
        {
            this.localProject = localProject ?? throw new ArgumentNullException(nameof(localProject));
            this.checksumCalculator = checksymCalculator ?? throw new ArgumentNullException(nameof(checksymCalculator));
            localFileIndex = new Dictionary<string, FileMetadata>();
            remoteFileIndex = new Dictionary<string, FileMetadata>();
        }

        public async Task<SyncCheckSummary> CompareAgainstRemoteAsync(List<FileMetadata> remoteFiles)
        {
            if (remoteFiles is null)
            {
                throw new ArgumentNullException(nameof(remoteFiles));
            }

            if (!Directory.Exists(localProject.LocalDirectory))
            {
                throw new DirectoryNotFoundException($"The directory does not exist: {localProject.LocalDirectory}");
            }
            IndexRemoteFiles(remoteFiles);
            await IndexLocalFilesAsync();

            var response = new SyncCheckSummary();

            foreach (KeyValuePair<string, FileMetadata> localFile in localFileIndex)
            {
                if (remoteFileIndex.ContainsKey(localFile.Key))
                    if (remoteFileIndex[localFile.Key].FileChecksum == localFile.Value.FileChecksum)
                        // Synced: file exists in both local and remote, and checksum is equals
                        response.SyncedFiles.Add(remoteFileIndex[localFile.Key]);
                    else
                        // Unsynced: file exists in both local and remote but checksum is different
                        response.ChangedFiles.Add(remoteFileIndex[localFile.Key]);
                else
                    // Created: file exists in local but not in remote
                    response.CreatedFiles.Add(localFile.Value);
            }

            // check for deleted files
            IEnumerable<string> existingInRemoteButNotInLocal = remoteFileIndex.Keys.Except(localFileIndex.Keys);
            foreach (string deletedFileKey in existingInRemoteButNotInLocal)
            {
                response.DeletedFiles.Add(remoteFileIndex[deletedFileKey]);
            }
            return response;
        }

        private void IndexRemoteFiles(List<FileMetadata> remoteFiles)
        {
            foreach (FileMetadata metadata in remoteFiles)
            {
                if (remoteFileIndex.ContainsKey(metadata.RelativeFilePath))
                    remoteFileIndex[metadata.RelativeFilePath] = metadata;
                else
                    remoteFileIndex.Add(metadata.RelativeFilePath, metadata);
            }
        }

        private async Task IndexLocalFilesAsync()
        {
            string[] allLocalFiles = Directory.GetFiles(localProject.LocalDirectory, "*.*", SearchOption.AllDirectories);

            foreach (string fullFilePath in allLocalFiles)
            {
                string fileName = Path.GetFileName(fullFilePath);
                string relativeFilePath = fullFilePath.Replace(localProject.LocalDirectory + Path.DirectorySeparatorChar, "");
                relativeFilePath = relativeFilePath.Replace(Path.DirectorySeparatorChar, '/');

                // ommit lock files and duplicates
                if (fileName.StartsWith("~$"))
                    continue;

                var fileChecksum = await checksumCalculator.CalculateChecksumAsync(fullFilePath);
                var metadata = new FileMetadata()
                {
                    FileChecksum = fileChecksum,
                    RelativeFilePath = relativeFilePath
                };

                if (localFileIndex.ContainsKey(relativeFilePath))
                    localFileIndex[relativeFilePath] = metadata;
                else
                    localFileIndex.Add(relativeFilePath, metadata);
            }
        }

    }
}
