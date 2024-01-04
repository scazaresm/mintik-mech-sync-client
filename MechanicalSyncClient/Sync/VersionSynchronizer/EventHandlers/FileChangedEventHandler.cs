using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.EventHandlers
{
    public class FileChangedEventHandler : IFileSyncEventHandler
    {
        private const string ONGOING_FOLDER = "Ongoing";

        private readonly IMechSyncServiceClient client;
        private readonly VersionSynchronizerState sourceState;

        public IFileSyncEventHandler NextHandler { get; set; }

        public FileChangedEventHandler(IMechSyncServiceClient client, VersionSynchronizerState sourceState)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.sourceState = sourceState ?? throw new ArgumentNullException(nameof(sourceState));
        }

        public async Task HandleAsync(FileSyncEvent fileSyncEvent)
        {
            if (fileSyncEvent is null)
                throw new ArgumentNullException(nameof(fileSyncEvent));

            // this handler is exclusive for file change events
            if (fileSyncEvent.EventType != FileSyncEventType.Changed)
            {
                // delegate responsibility to the next handler in the chain
                if (NextHandler != null)
                    await NextHandler.HandleAsync(fileSyncEvent);
                return;
            }

            var synchronizer = sourceState.Synchronizer;
            var fileViewer = synchronizer.UI.FileViewer;
            try
            {
                // no need to handle directory change events, only files
                if (Directory.Exists(fileSyncEvent.FullPath))
                    return;

                if (synchronizer.ChangeMonitor.IsMonitoring())
                    fileViewer.SetSyncingStatusToFile(fileSyncEvent.FullPath);

                await client.UploadFileAsync(new UploadFileRequest
                {
                    LocalFilePath = fileSyncEvent.FullPath,
                    VersionId = fileSyncEvent.Version.RemoteVersion.Id,
                    VersionFolder = ONGOING_FOLDER,
                    RelativeEquipmentPath = fileSyncEvent.Version.RemoteProject.RelativeEquipmentPath,
                    RelativeFilePath = fileSyncEvent.RelativeFilePath.Replace(Path.DirectorySeparatorChar, '/')
                });

                if(synchronizer.ChangeMonitor.IsMonitoring())
                    fileViewer.SetSyncedStatusToFile(fileSyncEvent.FullPath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to handle file changed event: {ex.Message}", ex);
            }
        }

    }
}
