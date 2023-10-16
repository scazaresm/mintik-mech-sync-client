using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.EventHandlers
{
    public class FileDeletedEventHandler : IFileSyncEventHandler
    {
        private readonly IMechSyncServiceClient client;
        private readonly VersionSynchronizerState sourceState;

        public IFileSyncEventHandler NextHandler { get; set; }

        public FileDeletedEventHandler(IMechSyncServiceClient client, VersionSynchronizerState sourceState)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.sourceState = sourceState ?? throw new ArgumentNullException(nameof(sourceState));
        }

        public async Task HandleAsync(FileSyncEvent fileSyncEvent)
        {
            if (fileSyncEvent is null)
            {
                throw new ArgumentNullException(nameof(fileSyncEvent));
            }

            if (fileSyncEvent.EventType != FileSyncEventType.Deleted)
            {
                if (NextHandler != null)
                    await NextHandler.HandleAsync(fileSyncEvent);
                return;
            }

            var synchronizer = sourceState.Synchronizer;
            var fileViewer = synchronizer.UI.FileViewer;
            try
            {
                var targetFiles = await DetermineTargetFilesAsync(fileSyncEvent);

                foreach (var target in targetFiles)
                    fileViewer.SetSyncingStatusToFile(target);

                await Task.Delay(10); // avoid overloading the server

                // if RelativeFilePath is a directory, all its contents will be removed in server
                await client.DeleteFileAsync(new DeleteFileRequest
                {
                    RelativeFilePath = fileSyncEvent.RelativeFilePath.Replace(Path.DirectorySeparatorChar, '/'),
                    ProjectId = fileSyncEvent.Version.RemoteProject.Id
                });

                foreach(var target in targetFiles)
                    fileViewer.RemoveDeletedFile(target);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to handle file deleted event: {ex.Message}", ex);
            }
        }

        public Task HandleAsync(FileSyncEvent fileSyncEvent, int retryLimit)
        {
            throw new NotImplementedException();
        }

        private async Task<List<string>> DetermineTargetFilesAsync(FileSyncEvent fileSyncEvent)
        {
            var synchronizer = sourceState.Synchronizer;
            var targetIsDirectory = !Path.HasExtension(fileSyncEvent.FullPath);
            var targetFiles = new List<string>();

            if (targetIsDirectory)
            {
                // get server file metadata for this version
                var request = new GetFileMetadataRequest()
                {
                    VersionId = synchronizer.Version.RemoteVersion.Id
                };
                var fileMetadataResponse = await synchronizer.ServiceClient.GetFileMetadataAsync(request);

                // determine which files are inside the target directory
                foreach (var fileMetadata in fileMetadataResponse.FileMetadata)
                {
                    // paths in metadata are linux-based (server-side paths), make it Windows-based
                    var remoteRelativePath = fileMetadata.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar);

                    bool isFileInTargetDirectory = remoteRelativePath.StartsWith(fileSyncEvent.RelativeFilePath);
                    if (isFileInTargetDirectory)
                    {
                        var localFilePath = Path.Combine(synchronizer.Version.LocalDirectory, remoteRelativePath);
                        targetFiles.Add(localFilePath);
                    }
                }
            }
            else
            {
                targetFiles.Add(fileSyncEvent.FullPath);
            }
            return targetFiles;
        }

    }
}
