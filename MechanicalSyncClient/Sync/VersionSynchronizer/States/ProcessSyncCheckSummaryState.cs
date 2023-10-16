using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.States
{
    public class ProcessSyncCheckSummaryState : VersionSynchronizerState
    {
        private readonly SyncCheckSummary summary;

        public ProcessSyncCheckSummaryState(SyncCheckSummary summary)
        {
            this.summary = summary ?? throw new ArgumentNullException(nameof(summary));
        }

        public override async Task RunAsync()
        {
            EnqueueCreatedFiles(summary.CreatedFiles);
            EnqueueChangedFiles(summary.ChangedFiles);
            EnqueueDeletedFiles(summary.DeletedFiles);
            await Task.Delay(500);
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Processing sync check summary...";
        }

        private void EnqueueCreatedFiles(List<FileMetadata> createdFiles)
        {
            foreach (var file in createdFiles)
            {
                string fullPath = Path.Combine(
                    Synchronizer.Version.LocalDirectory, 
                    file.RelativeFilePath
                ).Replace('/', Path.DirectorySeparatorChar);

                Synchronizer.ChangeMonitor.EnqueueEvent(new FileSyncEvent
                {
                    EventType = FileSyncEventType.Created,
                    Version = Synchronizer.Version,
                    FullPath = fullPath,
                    RelativeFilePath = file.RelativeFilePath,
                    RaiseDateTime = DateTime.Now,
                    EventState = FileSyncEventState.Queued
                });
            }
        }

        private void EnqueueChangedFiles(List<FileMetadata> changedFiles)
        {
            foreach (var file in changedFiles)
            {
                string fullPath = Path.Combine(
                    Synchronizer.Version.LocalDirectory,
                    file.RelativeFilePath
                ).Replace('/', Path.DirectorySeparatorChar);

                Synchronizer.ChangeMonitor.EnqueueEvent(new FileSyncEvent
                {
                    EventType = FileSyncEventType.Changed,
                    Version = Synchronizer.Version,
                    FullPath = fullPath,
                    RelativeFilePath = file.RelativeFilePath,
                    RaiseDateTime = DateTime.Now,
                    EventState = FileSyncEventState.Queued
                });
            }
        }

        private void EnqueueDeletedFiles(List<FileMetadata> deletedFiles)
        {
            foreach (var file in deletedFiles)
            {
                string fullPath = Path.Combine(
                    Synchronizer.Version.LocalDirectory,
                    file.RelativeFilePath
                ).Replace('/', Path.DirectorySeparatorChar);

                Synchronizer.ChangeMonitor.EnqueueEvent(new FileSyncEvent
                {
                    EventType = FileSyncEventType.Deleted,
                    Version = Synchronizer.Version,
                    FullPath = fullPath,
                    RelativeFilePath = file.RelativeFilePath,
                    RaiseDateTime = DateTime.Now,
                    EventState = FileSyncEventState.Queued
                });
            }
        }
    }
}
