﻿using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.EventHandlers
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

        public async Task HandleAsync(FileSyncEvent fileSyncEvent)
        {
            if (fileSyncEvent is null)
            {
                throw new ArgumentNullException(nameof(fileSyncEvent));
            }

            if (fileSyncEvent.EventType != FileSyncEventType.Created)
            {
                if (NextHandler != null)
                    await NextHandler.HandleAsync(fileSyncEvent);
                return;
            }

            var fileViewer = sourceState.Synchronizer.UI.FileViewer;
            try
            {
                // no need to handle directory creation, only files
                if (Directory.Exists(fileSyncEvent.FullPath))
                    return;

                // display the new file icon in the viewer
                fileViewer.AddCreatedFile(fileSyncEvent.FullPath);

                await Task.Delay(50); // avoid overloading the server

                // no need to handle this event if the next will overwrite it
                if (NextEventOverwritesThis(fileSyncEvent))
                    return;

                await Task.Factory.StartNew(async () =>
                {
                    await client.UploadFileAsync(new UploadFileRequest
                    {
                        LocalFilePath = fileSyncEvent.FullPath,
                        RelativeFilePath = fileSyncEvent.RelativePath.Replace(Path.DirectorySeparatorChar, '/'),
                        ProjectId = fileSyncEvent.LocalProject.RemoteProjectId
                    });
                }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);

                fileViewer.SetSyncedStatusToFile(fileSyncEvent.FullPath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to handle file created event: {ex.Message}", ex);
            }
        }

        public Task HandleAsync(FileSyncEvent fileSyncEvent, int retryLimit)
        {
            throw new NotImplementedException();
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
