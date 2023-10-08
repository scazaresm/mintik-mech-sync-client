using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync
{
    public class ProjectSyncChecker
    {
        private readonly LocalProject localProject;
        private readonly IMechSyncServiceClient client;
        private readonly IChecksumCalculator checksumCalculator;
        private Dictionary<string, FileMetadata> localFileIndex;
        private Dictionary<string, FileMetadata> remoteFileIndex;

        public ProjectSyncChecker(LocalProject localProject, IMechSyncServiceClient client, IChecksumCalculator checksumCalculator)
        {
            this.localProject = localProject ?? throw new ArgumentNullException(nameof(localProject));
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.checksumCalculator = checksumCalculator ?? throw new ArgumentNullException(nameof(checksumCalculator));
        }

        public async Task<ProjectSyncCheckResult> CheckAsync()
        {
            if (!Directory.Exists(localProject.LocalDirectory))
            {
                throw new DirectoryNotFoundException($"The directory does not exist: {localProject.LocalDirectory}");
            }
            await IndexRemoteFilesAsync();
            await IndexLocalFilesAsync();

            var result = new ProjectSyncCheckResult();

            foreach (KeyValuePair<string, FileMetadata> localFile in localFileIndex)
            {
                if (remoteFileIndex.ContainsKey(localFile.Key))
                    if (remoteFileIndex[localFile.Key].FileChecksum == localFile.Value.FileChecksum)
                        // Synced: file exists in both local and remote, and checksum is equals
                        result.SyncedFiles.Add(remoteFileIndex[localFile.Key]);
                    else
                        // Unsynced: file exists in both local and remote but checksum is different
                        result.ChangedFiles.Add(remoteFileIndex[localFile.Key]);
                else
                    // Created: file exists in local but not in remote
                    result.CreatedFiles.Add(localFile.Value);
            }

            // check for deleted files
            IEnumerable<string> existingInRemoteButNotInLocal = remoteFileIndex.Keys.Except(localFileIndex.Keys);
            foreach (string deletedFileKey in existingInRemoteButNotInLocal)
                result.DeletedFiles.Add(remoteFileIndex[deletedFileKey]);

            return result;
        }

        private async Task IndexRemoteFilesAsync()
        {
            var metadataResponse = await client.GetFileMetadataAsync(new GetFileMetadataRequest()
            {
                VersionId = localProject.RemoteVersionId
            });

            if (metadataResponse.FileMetadata is null)
                return;

            foreach (FileMetadata metadata in metadataResponse.FileMetadata)
                if (remoteFileIndex.ContainsKey(metadata.RelativeFilePath))
                    remoteFileIndex[metadata.RelativeFilePath] = metadata;
                else
                    remoteFileIndex.Add(metadata.RelativeFilePath, metadata);
        }

        private async Task IndexLocalFilesAsync()
        {
            string[] allLocalFiles = Directory.GetFiles(localProject.LocalDirectory, "*.*", SearchOption.AllDirectories);

            foreach (string fullFilePath in allLocalFiles)
            {
                string fileName = Path.GetFileName(fullFilePath);
                string relativeFilePath = fullFilePath.Replace(localProject.LocalDirectory + Path.DirectorySeparatorChar, "");
                relativeFilePath = relativeFilePath.Replace(Path.DirectorySeparatorChar, '/');

                // ommit lock files
                if (fileName.StartsWith("~$"))
                    continue;

                FileInfo fileInfo = new FileInfo(fullFilePath);
                var fileMetadata = new FileMetadata()
                {
                    FileChecksum = await checksumCalculator.CalculateChecksumAsync(fullFilePath),
                    RelativeFilePath = relativeFilePath,
                    FileSize = fileInfo.Length
                };

                if (localFileIndex.ContainsKey(relativeFilePath))
                    localFileIndex[relativeFilePath] = fileMetadata;
                else
                    localFileIndex.Add(relativeFilePath, fileMetadata);
            }
        }

    }
}
