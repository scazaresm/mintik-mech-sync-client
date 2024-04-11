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
        private readonly ILogger logger;

        public IndexRemoteFilesState(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task RunAsync()
        {
            logger.Debug("Starting IndexRemoteFilesState...");

            var versionId = Synchronizer.Version.RemoteVersion.Id;

            logger.Debug($"\tGetting all file metadata for version {versionId}");
            var allFileMetadata = await Synchronizer.SyncServiceClient.GetFileMetadataAsync(versionId, null);

            logger.Debug($"\tIndexing all remote files to compare with local...");
            Synchronizer.RemoteFileIndex.Clear();
            foreach (FileMetadata metadata in allFileMetadata)
            {
                logger.Debug($"\tIndexing {metadata.RelativeFilePath}");
                Synchronizer.RemoteFileIndex.TryAdd(metadata.RelativeFilePath, metadata);
            }

            logger.Debug("Completed IndexRemoteFilesState.");
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Indexing remote files...";
            ui.SynchronizerToolStrip.Enabled = false;
        }
    }
}
