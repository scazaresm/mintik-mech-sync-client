using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.States
{
    public class IndexRemoteFilesState : ProjectSynchronizerState
    {
        public override async Task RunTransitionLogicAsync()
        {
            var request = new GetFileMetadataRequest()
            {
                VersionId = Synchronizer.LocalProject.RemoteVersionId,
            };
            var response = await Synchronizer.ServiceClient.GetFileMetadataAsync(request);

            Synchronizer.RemoteFileIndex.Clear();
            foreach (FileMetadata metadata in response.FileMetadata)
            {
                Synchronizer.RemoteFileIndex.Add(metadata.RelativeFilePath, metadata);
            }

            await Task.Delay(1000);
            Synchronizer.SetState(new SyncCheckState());
            _ = Synchronizer.RunTransitionLogicAsync();
        }

        public override void UpdateUI()
        {
            Synchronizer.UI.StatusLabel.Text = "Indexing remote files...";
        }
    }
}
