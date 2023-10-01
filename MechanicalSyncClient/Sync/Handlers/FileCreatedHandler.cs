using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Database.Domain;
using System;
using System.Threading.Tasks;


namespace MechanicalSyncApp.Sync.Handlers
{
    public class FileCreatedHandler : IFileSyncEventHandler
    {
        private readonly IMechSyncServiceClient client;

        public IFileSyncEventHandler NextHandler { get; set; }

        public FileCreatedHandler(IMechSyncServiceClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task HandleAsync(FileSyncEvent fileSyncEvent)
        {
            if (fileSyncEvent.EventType != FileSyncEventType.Created)
            {
                //await NextHandler?.HandleAsync(fileSyncEvent);
                return;
            }
            try
            {
                await client.UploadProjectFileAsync(new UploadFileRequest
                {
                    LocalFilePath = fileSyncEvent.FullPath,
                    RelativeFilePath = fileSyncEvent.RelativePath.Replace('\\', '/'),
                    ProjectId = fileSyncEvent.LocalProject.RemoteId
                });
            } 
            catch(Exception ex)
            {
                throw new Exception($"Failed to handle file created event: {ex.Message}", ex);
            }
        }

        public Task HandleAsync(FileSyncEvent fileSyncEvent, int retryLimit)
        {
            throw new NotImplementedException();
        }
    }
}
