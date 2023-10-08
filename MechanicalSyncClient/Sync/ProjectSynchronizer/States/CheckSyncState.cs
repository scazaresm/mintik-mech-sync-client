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

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.States
{
    public class CheckSyncState : ProjectSynchronizerState
    {
        public override async Task RunTransitionLogicAsync()
        {
            try
            {
                /*
                var request = new GetFilesMetadataRequest()
                {
                    VersionId = Synchronizer.LocalProject.RemoteVersionId,
                };
                var filesInRemote = await MechSyncServiceClient.Instance.GetFilesMetadataAsync(request);

                var analyzer = new LocalProjectAnalyzer(Synchronizer.LocalProject, new Sha256ChecksumValidator());

                var result = analyzer.CompareAgainstRemote(filesInRemote.Files);
                EnqueueCreatedFiles(result.CreatedFiles);
                EnqueueChangedFiles(result.ChangedFiles);
                EnqueueDeletedFiles(result.DeletedFiles);
                */

                Synchronizer.SetState(new MonitorFileSyncEventsState());
                _ = Synchronizer.RunTransitionLogicAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override void UpdateUI()
        {

        }

        private void EnqueueFileSyncEvent(FileMetadata file, FileSyncEventType eventType)
        {
            Synchronizer.ChangeMonitor.EnqueueEvent(new FileSyncEvent
            {
                EventType = eventType,
                LocalProject = Synchronizer.LocalProject,
                FullPath = Path.Combine(Synchronizer.LocalProject.LocalDirectory, file.RelativeFilePath),
                RelativePath = file.RelativeFilePath,
                RaiseDateTime = DateTime.Now,
                EventState = FileSyncEventState.Queued
            });
        }
    }
}
