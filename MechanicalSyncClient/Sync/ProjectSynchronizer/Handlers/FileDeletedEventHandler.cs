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

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.Handlers
{
    public class FileDeletedEventHandler : IFileSyncEventHandler
    {
        private readonly IMechSyncServiceClient client;
        private readonly ProjectSynchronizerState sourceState;

        public IFileSyncEventHandler NextHandler { get; set; }

        public FileDeletedEventHandler(IMechSyncServiceClient client, ProjectSynchronizerState sourceState)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.sourceState = sourceState ?? throw new ArgumentNullException(nameof(sourceState));
        }

        public async Task HandleAsync(FileChangeEvent fileSyncEvent)
        {
            if (fileSyncEvent.EventType != FileChangeEventType.Deleted)
            {
                if (NextHandler != null)
                    await NextHandler.HandleAsync(fileSyncEvent);
                return;
            }

            var fileBrowser = sourceState.Synchronizer.UI.FileViewer;
            try
            {
                fileBrowser.SetSyncingStatusToFile(fileSyncEvent.FullPath);
                await Task.Delay(50); // avoid overloading server
                await client.DeleteFileAsync(new DeleteFileRequest
                {
                    RelativeFilePath = fileSyncEvent.RelativePath.Replace(Path.DirectorySeparatorChar, '/'),
                    ProjectId = fileSyncEvent.LocalProject.RemoteId
                });
                fileBrowser.RemoveDeletedFile(fileSyncEvent.FullPath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to handle file deleted event: {ex.Message}", ex);
            }
        }

        public Task HandleAsync(FileChangeEvent fileSyncEvent, int retryLimit)
        {
            throw new NotImplementedException();
        }
    }
}
