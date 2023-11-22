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
        public SyncCheckSummary Summary { get; private set; }

        public override async Task RunAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    var localFileIndex = Synchronizer.LocalFileIndex;
                    var remoteFileIndex = Synchronizer.RemoteFileIndex;
                    Summary = new SyncCheckSummary();

                    foreach (KeyValuePair<string, FileMetadata> localFile in localFileIndex)
                    {
                        if (remoteFileIndex.ContainsKey(localFile.Key))
                            if (remoteFileIndex[localFile.Key].FileChecksum == localFile.Value.FileChecksum)
                                // Synced: file exists in both local and remote, and checksum is equals
                                Summary.SyncedFiles.Add(remoteFileIndex[localFile.Key]);
                            else
                                // Unsynced: file exists in both local and remote but checksum is different
                                Summary.ChangedFiles.Add(remoteFileIndex[localFile.Key]);
                        else
                            // Created: file exists in local but not in remote
                            Summary.CreatedFiles.Add(localFile.Value);
                    }

                    // check for deleted files
                    IEnumerable<string> existingInRemoteButNotInLocal = remoteFileIndex.Keys.Except(localFileIndex.Keys);
                    foreach (string deletedFileKey in existingInRemoteButNotInLocal)
                    {
                        Summary.DeletedFiles.Add(remoteFileIndex[deletedFileKey]);
                    }
                });
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
    }
}
