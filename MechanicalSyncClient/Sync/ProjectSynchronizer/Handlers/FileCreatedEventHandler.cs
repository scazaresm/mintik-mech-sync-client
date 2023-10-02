using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using System;
using System.Threading.Tasks;


namespace MechanicalSyncApp.Sync.ProjectSynchronizer.Handlers
{
    public class FileCreatedEventHandler : IFileSyncEventHandler
    {
        private readonly IMechSyncServiceClient client;

        public IFileSyncEventHandler NextHandler { get; set; }

        public FileCreatedEventHandler(IMechSyncServiceClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task HandleAsync(FileChangeEvent fileSyncEvent)
        {
            if (fileSyncEvent.EventType != FileChangeEventType.Created)
            {
                if (NextHandler != null)
                    await NextHandler.HandleAsync(fileSyncEvent);
                return;
            }
            try
            {
                await client.UploadFileAsync(new UploadFileRequest
                {
                    LocalFilePath = fileSyncEvent.FullPath,
                    RelativeFilePath = fileSyncEvent.RelativePath.Replace('\\', '/'),
                    ProjectId = fileSyncEvent.LocalProject.RemoteId
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to handle file created event: {ex.Message}", ex);
            }
        }

        public Task HandleAsync(FileChangeEvent fileSyncEvent, int retryLimit)
        {
            throw new NotImplementedException();
        }
    }
}
