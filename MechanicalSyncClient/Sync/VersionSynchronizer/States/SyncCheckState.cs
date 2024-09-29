using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.States
{
    public class SyncCheckState : VersionSynchronizerState
    {
        private readonly ILogger logger;

        public SyncCheckSummary Summary { get; private set; }
        public bool RethrowException { get; set; } = false;

        public bool SkipDeletedFilesCheck { get; set; }

        public SyncCheckState(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task RunAsync()
        {
            logger.Debug($"Starting SyncCheckState: versionId = {Synchronizer.Version.RemoteVersion.Id}, versionLocalDirectory = {Synchronizer.Version.LocalDirectory}");
         
            try
            {
                var localFileIndex = Synchronizer.LocalFileIndex;
                var remoteFileIndex = Synchronizer.RemoteFileIndex;
                Summary = new SyncCheckSummary();

                foreach (KeyValuePair<string, FileMetadata> localFile in localFileIndex)
                {
                    if (remoteFileIndex.ContainsKey(localFile.Key))
                        if (remoteFileIndex[localFile.Key].FileChecksum == localFile.Value.FileChecksum)
                        {
                            // Synced: file exists in both local and remote, and checksum is equals
                            var syncedFile = remoteFileIndex[localFile.Key];
                            Summary.AddSyncedFile(syncedFile);
                            logger.Debug($"\t{syncedFile.RelativeFilePath} = Synced -> {syncedFile.FileChecksum}");
                        }
                        else
                        {
                            // Unsynced: file exists in both local and remote but checksum is different
                            var changedFile = remoteFileIndex[localFile.Key];
                            Summary.AddChangedFile(changedFile);
                            logger.Debug($"\t{changedFile.RelativeFilePath} = Changed -> {changedFile.FileChecksum}");
                        }
                    else
                    {
                        // Created: file exists in local but not in remote
                        var createdFile = localFile.Value;
                        Summary.AddCreatedFile(createdFile);
                        logger.Debug($"\t{createdFile.RelativeFilePath} = Created -> {createdFile.FileChecksum}");
                    }
                }

                if (!SkipDeletedFilesCheck)
                { 
                    // check for deleted files
                    IEnumerable<string> existingInRemoteButNotInLocal = remoteFileIndex.Keys.Except(localFileIndex.Keys);
                    foreach (string deletedFileKey in existingInRemoteButNotInLocal)
                    {
                        Summary.AddDeletedFile(remoteFileIndex[deletedFileKey]);
                        logger.Debug($"\t{remoteFileIndex[deletedFileKey].RelativeFilePath} = Deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                Summary.ExceptionObject = ex;
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "";
                logger.Error($"Could not complete SyncCheckState: {ex.GetType()} {ex} {innerExceptionMessage}");

                if (RethrowException) throw ex;
            }
            logger.Debug("SyncCheckState complete.");
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Checking sync...";
            ui.SynchronizerToolStrip.Enabled = false;
        }
    }
}
