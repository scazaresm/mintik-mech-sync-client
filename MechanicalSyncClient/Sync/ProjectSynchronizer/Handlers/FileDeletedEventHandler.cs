using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.Handlers
{
    public class FileDeletedEventHandler : IFileSyncEventHandler
    {
        private readonly IMechSyncServiceClient client;

        public IFileSyncEventHandler NextHandler { get; set; }

        public FileDeletedEventHandler(IMechSyncServiceClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task HandleAsync(FileChangeEvent fileSyncEvent)
        {
            if (fileSyncEvent.EventType != FileChangeEventType.Deleted)
            {
                if (NextHandler != null)
                    await NextHandler.HandleAsync(fileSyncEvent);
                return;
            }
            try
            {
                await client.DeleteFileAsync(new DeleteFileRequest
                {
                    RelativeFilePath = fileSyncEvent.RelativePath.Replace('\\', '/'),
                    ProjectId = fileSyncEvent.LocalProject.RemoteId
                });
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
