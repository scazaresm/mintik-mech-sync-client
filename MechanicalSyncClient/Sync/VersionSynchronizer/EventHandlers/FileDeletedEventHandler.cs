using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
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
        private const string ONGOING_FOLDER = "Ongoing";

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
                throw new ArgumentNullException(nameof(fileSyncEvent));

            // this handler is exclusive for file deleted events
            if (fileSyncEvent.EventType != FileSyncEventType.Deleted)
            {
                // delegate responsibility to the next handler in the chain
                if (NextHandler != null)
                    await NextHandler.HandleAsync(fileSyncEvent);
                return;
            }

            var synchronizer = sourceState.Synchronizer;
            try
            {
                var targetFiles = await DetermineTargetFilesAsync(fileSyncEvent);

                SetSyncingStatusInViewer(targetFiles);

                // if RelativeFilePath is a directory, all its contents will be removed in server
                await client.DeleteFileAsync(new DeleteFileRequest
                {
                    VersionId = fileSyncEvent.Version.RemoteVersion.Id,
                    VersionFolder = ONGOING_FOLDER,
                    RelativeEquipmentPath = fileSyncEvent.Version.RemoteProject.RelativeEquipmentPath,
                    RelativeFilePath = fileSyncEvent.RelativeFilePath.Replace(Path.DirectorySeparatorChar, '/')
                });
                RemoveDeletedFilesInViewer(targetFiles);

                if (synchronizer.ChangeMonitor.IsMonitoring())
                {
                    foreach(var file in targetFiles)
                        synchronizer.OnlineWorkSummary.AddDeletedFile(file);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to handle file deleted event: {ex.Message}", ex);
            }
        }

        private void SetSyncingStatusInViewer(List<FileMetadata> targetFiles)
        {
            var synchronizer = sourceState.Synchronizer;
            var fileViewer = synchronizer.UI.LocalFileViewer;

            foreach (var target in targetFiles)
            {
                var remoteRelativePath = target.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar);
                var localFilePath = Path.Combine(synchronizer.Version.LocalDirectory, remoteRelativePath);
                fileViewer.SetSyncingStatusToFile(localFilePath);
            }
        }

        private void RemoveDeletedFilesInViewer(List<FileMetadata> targetFiles)
        {
            var synchronizer = sourceState.Synchronizer;
            var fileViewer = synchronizer.UI.LocalFileViewer;

            foreach (var target in targetFiles)
            {
                var remoteRelativePath = target.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar);
                var localFilePath = Path.Combine(synchronizer.Version.LocalDirectory, remoteRelativePath);
                fileViewer.RemoveDeletedFile(localFilePath);
            }
        }

        private async Task<List<FileMetadata>> DetermineTargetFilesAsync(FileSyncEvent fileSyncEvent)
        {
            var synchronizer = sourceState.Synchronizer;
            var target = fileSyncEvent.FullPath;
            var targetIsDirectory = !Path.HasExtension(target);
            var targetFiles = new List<FileMetadata>();

            if (targetIsDirectory)
            {
                // get server file metadata for this version
                var versionId = synchronizer.Version.RemoteVersion.Id;
                var allFileMetadata = await synchronizer.SyncServiceClient.GetFileMetadataAsync(versionId, null);

                // determine which files are inside the target directory
                foreach (var fileMetadata in allFileMetadata)
                {
                    // paths in metadata are linux-based (server-side paths), make it Windows-based
                    var remoteRelativePath = fileMetadata.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar);

                    bool isFileInTargetDirectory = remoteRelativePath.StartsWith(fileSyncEvent.RelativeFilePath);
                    if (isFileInTargetDirectory)
                        targetFiles.Add(fileMetadata);
                }
            }
            else
            {
                targetFiles.Add(new FileMetadata()
                {
                    FullFilePath = fileSyncEvent.FullPath,
                    RelativeFilePath = fileSyncEvent.RelativeFilePath,
                });
            }
            return targetFiles;
        }

    }
}
