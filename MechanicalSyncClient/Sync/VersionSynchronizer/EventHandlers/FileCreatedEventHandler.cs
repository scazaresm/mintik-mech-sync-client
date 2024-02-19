using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.EventHandlers
{
    public class FileCreatedEventHandler : IFileSyncEventHandler
    {
        private const string ONGOING_FOLDER = "Ongoing";

        private readonly IMechSyncServiceClient client;
        private readonly VersionSynchronizerState sourceState;

        public IFileSyncEventHandler NextHandler { get; set; }

        public FileCreatedEventHandler(IMechSyncServiceClient client, VersionSynchronizerState sourceState)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.sourceState = sourceState ?? throw new ArgumentNullException(nameof(sourceState));
        }

        public async Task HandleAsync(FileSyncEvent fileSyncEvent)
        {
            if (fileSyncEvent is null)
                throw new ArgumentNullException(nameof(fileSyncEvent));

            // this handler is exclusive for file created events
            if (fileSyncEvent.EventType != FileSyncEventType.Created)
            {
                // delegate responsibility to the next handler in the chain
                if (NextHandler != null)
                    await NextHandler.HandleAsync(fileSyncEvent);
                return;
            }

            var synchronizer = sourceState.Synchronizer;
            var fileViewer = synchronizer.UI.LocalFileViewer;
            try
            {
                // no need to handle directory creation, only files
                if (Directory.Exists(fileSyncEvent.FullPath))
                    return;  
                
                // display the new file icon in the viewer
                fileViewer.AddCreatedFile(fileSyncEvent.FullPath);

                // no need to handle this event if the next will overwrite it
                if (NextEventOverwritesThis(fileSyncEvent))
                    return;  
                
                // no need to handle event if file no longer exists
                if (!File.Exists(fileSyncEvent.FullPath))
                    return;

                await client.UploadFileAsync(new UploadFileRequest
                {
                    LocalFilePath = fileSyncEvent.FullPath,
                    VersionId = fileSyncEvent.Version.RemoteVersion.Id,
                    VersionFolder = ONGOING_FOLDER,
                    RelativeEquipmentPath = fileSyncEvent.Version.RemoteProject.RelativeEquipmentPath,
                    RelativeFilePath = fileSyncEvent.RelativeFilePath.Replace(Path.DirectorySeparatorChar, '/')
                });

                if (synchronizer.ChangeMonitor.IsMonitoring())
                {
                    synchronizer.OnlineWorkSummary.AddCreatedFile(new FileMetadata()
                    {
                        RelativeFilePath = fileSyncEvent.RelativeFilePath
                    });
                    fileViewer.SetSyncedStatusToFile(fileSyncEvent.FullPath);
                }
                else
                    fileViewer.SetOfflineStatusToFile(fileSyncEvent.FullPath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to handle file created event: {ex.Message}", ex);
            }
        }

        private bool NextEventOverwritesThis(FileSyncEvent thisEvent)
        {
            var nextEvent = sourceState.Synchronizer.ChangeMonitor.PeekNextEvent();
            return nextEvent != null &&
                (nextEvent.EventType == FileSyncEventType.Changed || nextEvent.EventType == FileSyncEventType.Deleted) &&
                nextEvent.FullPath == thisEvent.FullPath;
        }
    }
}
