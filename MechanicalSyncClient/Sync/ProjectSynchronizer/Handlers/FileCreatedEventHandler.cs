using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.Handlers
{
    public class FileCreatedEventHandler : IFileSyncEventHandler
    {
        private readonly IMechSyncServiceClient client;
        private readonly ProjectSynchronizerState sourceState;

        public IFileSyncEventHandler NextHandler { get; set; }

        public FileCreatedEventHandler(IMechSyncServiceClient client, ProjectSynchronizerState sourceState)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.sourceState = sourceState ?? throw new ArgumentNullException(nameof(sourceState));
        }

        public async Task HandleAsync(FileChangeEvent fileSyncEvent)
        {
            if (fileSyncEvent.EventType != FileChangeEventType.Created)
            {
                if (NextHandler != null)
                    await NextHandler.HandleAsync(fileSyncEvent);
                return;
            }

            var fileBrowser = sourceState.Synchronizer.UI.FileViewer;
            try
            {
                fileBrowser.AddCreatedFile(fileSyncEvent.FullPath); 
                
                if (NextEventOverwritesThis(fileSyncEvent))
                    return;

                await Task.Delay(50); // avoid overloading the server
                Task uploadTask = new Task(new Action(() =>
                    client.UploadFileAsync(new UploadFileRequest
                    {
                        LocalFilePath = fileSyncEvent.FullPath,
                        RelativeFilePath = fileSyncEvent.RelativePath.Replace(Path.DirectorySeparatorChar, '/'),
                        ProjectId = fileSyncEvent.LocalProject.RemoteId
                    })
                ));
                uploadTask.Start();
                while(!uploadTask.IsCompleted)
                    Application.DoEvents();
                uploadTask.GetAwaiter().GetResult();
                fileBrowser.SetSyncedStatusToFile(fileSyncEvent.FullPath);
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

        private bool NextEventOverwritesThis(FileChangeEvent thisEvent)
        {
            var nextEvent = sourceState.Synchronizer.ChangeMonitor.PeekNextEvent();
            return nextEvent != null &&
                (nextEvent.EventType == FileChangeEventType.Changed || nextEvent.EventType == FileChangeEventType.Deleted) &&
                nextEvent.FullPath == thisEvent.FullPath;
        }
    }
}
