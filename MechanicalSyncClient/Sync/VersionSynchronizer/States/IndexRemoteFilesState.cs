using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.States
{
    public class IndexRemoteFilesState : VersionSynchronizerState
    {
        public override async Task RunAsync()
        {
            // get metadata from server, i.e., a list of all the files on this version and its metadata
            var request = new GetFileMetadataRequest()
            {
                VersionId = Synchronizer.Version.RemoteVersion.Id,
            };
            var response = await Synchronizer.ServiceClient.GetFileMetadataAsync(request);

            // index each remote file to compare with local
            Synchronizer.RemoteFileIndex.Clear();
            foreach (FileMetadata metadata in response.FileMetadata)
            {
                Synchronizer.RemoteFileIndex.Add(metadata.RelativeFilePath, metadata);
            }
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Indexing remote files...";
        }
    }
}
