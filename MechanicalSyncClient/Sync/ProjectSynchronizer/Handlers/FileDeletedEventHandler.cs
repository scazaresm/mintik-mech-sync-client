﻿using MechanicalSyncApp.Core;
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

        public async Task HandleAsync(FileSyncEvent fileSyncEvent)
        {
            if (fileSyncEvent.EventType != FileSyncEventType.Deleted)
            {
                if (NextHandler != null)
                    await NextHandler.HandleAsync(fileSyncEvent);
                return;
            }

            var fileViewer = sourceState.Synchronizer.UI.FileViewer;
            try
            {
                fileViewer.SetSyncingStatusToFile(fileSyncEvent.FullPath);

                await Task.Delay(10); // avoid overloading the server

                await client.DeleteFileAsync(new DeleteFileRequest
                {
                    RelativeFilePath = fileSyncEvent.RelativePath.Replace(Path.DirectorySeparatorChar, '/'),
                    ProjectId = fileSyncEvent.LocalProject.RemoteProjectId
                });

                // if we deleted a directory and its contents, we need to repopulate files in viewer
                if(!Path.HasExtension(fileSyncEvent.FullPath))
                {
                    fileViewer.PopulateFiles();
                }
                else
                {
                    fileViewer.RemoveDeletedFile(fileSyncEvent.FullPath);
                }
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

    }
}
