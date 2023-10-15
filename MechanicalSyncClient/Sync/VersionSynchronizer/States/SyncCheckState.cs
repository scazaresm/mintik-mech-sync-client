using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.States
{
    public class SyncCheckState : VersionSynchronizerState
    {
        public override async Task RunAsync()
        {
            try
            {
                var localFileIndex = Synchronizer.LocalFileIndex;
                var remoteFileIndex = Synchronizer.RemoteFileIndex;
                var summary = new SyncCheckSummary();

                foreach (KeyValuePair<string, FileMetadata> localFile in localFileIndex)
                {
                    if (remoteFileIndex.ContainsKey(localFile.Key))
                        if (remoteFileIndex[localFile.Key].FileChecksum == localFile.Value.FileChecksum)
                            // Synced: file exists in both local and remote, and checksum is equals
                            summary.SyncedFiles.Add(remoteFileIndex[localFile.Key]);
                        else
                            // Unsynced: file exists in both local and remote but checksum is different
                            summary.ChangedFiles.Add(remoteFileIndex[localFile.Key]);
                    else
                        // Created: file exists in local but not in remote
                        summary.CreatedFiles.Add(localFile.Value);
                }

                // check for deleted files
                IEnumerable<string> existingInRemoteButNotInLocal = remoteFileIndex.Keys.Except(localFileIndex.Keys);
                foreach (string deletedFileKey in existingInRemoteButNotInLocal)
                {
                    summary.DeletedFiles.Add(remoteFileIndex[deletedFileKey]);
                }

                EnqueueCreatedFiles(summary.CreatedFiles);
                EnqueueChangedFiles(summary.ChangedFiles);
                EnqueueDeletedFiles(summary.DeletedFiles);

                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Checking sync...";
            ui.SynchronizerToolStrip.Enabled = false;
        }

        private void EnqueueCreatedFiles(List<FileMetadata> createdFiles)
        {
            foreach (var file in createdFiles)
            {
                Synchronizer.ChangeMonitor.EnqueueEvent(new FileSyncEvent
                {
                    EventType = FileSyncEventType.Created,
                    Version = Synchronizer.Version,
                    FullPath = Path.Combine(Synchronizer.Version.LocalDirectory, file.RelativeFilePath),
                    RelativePath = file.RelativeFilePath,
                    RaiseDateTime = DateTime.Now,
                    EventState = FileSyncEventState.Queued
                });
            }
        }

        private void EnqueueChangedFiles(List<FileMetadata> changedFiles)
        {
            foreach (var file in changedFiles)
            {
                Synchronizer.ChangeMonitor.EnqueueEvent(new FileSyncEvent
                {
                    EventType = FileSyncEventType.Changed,
                    Version = Synchronizer.Version,
                    FullPath = Path.Combine(Synchronizer.Version.LocalDirectory, file.RelativeFilePath),
                    RelativePath = file.RelativeFilePath,
                    RaiseDateTime = DateTime.Now,
                    EventState = FileSyncEventState.Queued
                });
            }
        }

        private void EnqueueDeletedFiles(List<FileMetadata> deletedFiles)
        {
            foreach (var file in deletedFiles)
            {
                Synchronizer.ChangeMonitor.EnqueueEvent(new FileSyncEvent
                {
                    EventType = FileSyncEventType.Deleted,
                    Version = Synchronizer.Version,
                    FullPath = Path.Combine(Synchronizer.Version.LocalDirectory, file.RelativeFilePath),
                    RelativePath = file.RelativeFilePath,
                    RaiseDateTime = DateTime.Now,
                    EventState = FileSyncEventState.Queued
                });
            }
        }
    }
}
