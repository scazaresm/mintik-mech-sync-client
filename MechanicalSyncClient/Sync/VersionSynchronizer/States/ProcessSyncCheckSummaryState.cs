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
            EnqueueFileSyncEvents(summary.CreatedFiles.Values.ToList(), FileSyncEventType.Created);
            EnqueueFileSyncEvents(summary.ChangedFiles.Values.ToList(), FileSyncEventType.Changed);
            EnqueueFileSyncEvents(summary.DeletedFiles.Values.ToList(), FileSyncEventType.Deleted);
            await Task.Delay(500);
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Processing sync check summary...";
        }

        private void EnqueueFileSyncEvents(List<FileMetadata> targetFiles, FileSyncEventType eventType)
        {
            foreach (var file in targetFiles)
            {
                string fullPath = Path.Combine(
                    Synchronizer.Version.LocalDirectory, 
                    file.RelativeFilePath
                ).Replace('/', Path.DirectorySeparatorChar);

                Synchronizer.ChangeMonitor.EnqueueEvent(new FileSyncEvent
                {
                    EventType = eventType,
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
