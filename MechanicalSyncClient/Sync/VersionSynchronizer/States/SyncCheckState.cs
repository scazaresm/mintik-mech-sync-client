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
        public SyncCheckSummary Summary { get; private set; }

        public override async Task RunAsync()
        {
            Log.Information($"Starting SyncCheckState: versionId = {Synchronizer.Version.RemoteVersion.Id}, versionLocalDirectory = {Synchronizer.Version.LocalDirectory}");
         
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
                            Summary.SyncedFiles.Add(syncedFile);
                            Log.Information($"\t{syncedFile.RelativeFilePath} = Synced -> {syncedFile.FileChecksum}");
                        }
                        else
                        {
                            // Unsynced: file exists in both local and remote but checksum is different
                            var changedFile = remoteFileIndex[localFile.Key];
                            Summary.ChangedFiles.Add(changedFile);
                            Log.Information($"\t{changedFile.RelativeFilePath} = Changed -> {changedFile.FileChecksum}");
                        }
                    else
                    {
                        // Created: file exists in local but not in remote
                        var createdFile = remoteFileIndex[localFile.Key];
                        Summary.CreatedFiles.Add(localFile.Value);
                        Log.Information($"\t{createdFile.RelativeFilePath} = Created -> {createdFile.FileChecksum}");
                    }
                }

                // check for deleted files
                IEnumerable<string> existingInRemoteButNotInLocal = remoteFileIndex.Keys.Except(localFileIndex.Keys);
                foreach (string deletedFileKey in existingInRemoteButNotInLocal)
                {
                    Summary.DeletedFiles.Add(remoteFileIndex[deletedFileKey]);
                    Log.Information($"\t{remoteFileIndex[deletedFileKey].RelativeFilePath} = Deleted");
                }
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "";
                Log.Error($"Could not complete SyncCheckState: {ex.GetType()} {ex} {innerExceptionMessage}");
            }
            
            Log.Information("SyncCheckState complete.");
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Checking sync...";
            ui.SynchronizerToolStrip.Enabled = false;
        }
    }
}
