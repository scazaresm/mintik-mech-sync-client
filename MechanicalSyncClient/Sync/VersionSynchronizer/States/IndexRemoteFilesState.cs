using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using Serilog;
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
            Log.Information("Starting IndexRemoteFilesState...");

            var versionId = Synchronizer.Version.RemoteVersion.Id;

            Log.Information($"\tGetting all file metadata for version {versionId}");
            var allFileMetadata = await Synchronizer.SyncServiceClient.GetFileMetadataAsync(versionId, null);

            Log.Information($"\tIndexing all remote files to compare with local...");
            Synchronizer.RemoteFileIndex.Clear();
            foreach (FileMetadata metadata in allFileMetadata)
            {
                Log.Information($"\tIndexing {metadata.RelativeFilePath}");
                Synchronizer.RemoteFileIndex.TryAdd(metadata.RelativeFilePath, metadata);
            }

            Log.Information("Completed IndexRemoteFilesState.");
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Indexing remote files...";
            ui.SynchronizerToolStrip.Enabled = false;
        }
    }
}
