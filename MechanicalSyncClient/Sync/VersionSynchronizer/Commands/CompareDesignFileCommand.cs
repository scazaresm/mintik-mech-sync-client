﻿using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    internal class CompareDesignFileCommand : IVersionSynchronizerCommandAsync
    {
        public bool OnlineWorkSummaryMode { get; set; } = false;

        private readonly FileMetadata localFileMetadata;
        private readonly string remoteAction;
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer { get; set; }


        public CompareDesignFileCommand(
            IVersionSynchronizer synchronizer, 
            FileMetadata localFileMetadata,
            string remoteAction,
            ILogger logger
            )
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.localFileMetadata = localFileMetadata ?? throw new ArgumentNullException(nameof(localFileMetadata));
            this.remoteAction = remoteAction ?? throw new ArgumentNullException(nameof(remoteAction));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("Starting CompareDesignFileCommand...");
            string localFilePath;
            string tempRemoteFilePath = null;

            var localVersionDirectory = OnlineWorkSummaryMode
                ? Synchronizer.SnapshotDirectory        // if we're in online work summary mode and file was updated, then pick files from temp snapshot copy
                : Synchronizer.Version.LocalDirectory;  // otherwise pick files from local copy in workspace directory

            var leftFileTitlePrefix = OnlineWorkSummaryMode ? "Before:" : "Local:";
            var rightFileTitlePrefix = OnlineWorkSummaryMode ? "After:" : "Remote:";

            try
            {
                if (NeedToShowLeftHandFile())
                    localFilePath = Path.Combine(
                        localVersionDirectory,
                        localFileMetadata.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar)
                    );
                else
                    localFilePath = null;

                if (OnlineWorkSummaryMode && localFilePath != null && !File.Exists(localFilePath))
                {
                    MessageBox.Show("The file you are trying to compare could not be found in your system, " +
                        "perhaps was it created and deleted during the same online working session.", 
                        "File not found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                    return;
                }

                if (NeedToShowRightHandFile())
                    tempRemoteFilePath = await DownloadRemoteFileAsync(localFileMetadata);
                else
                    tempRemoteFilePath = null;

                // open form to compare both files
                var compareForm = new DesignFileComparatorForm(localFilePath, tempRemoteFilePath);
                compareForm.Initialize(
                    $"{leftFileTitlePrefix} {Path.GetFileName(localFileMetadata.RelativeFilePath)}",
                    $"{rightFileTitlePrefix} {Path.GetFileName(localFileMetadata.RelativeFilePath)}"
                );
                compareForm.ShowDialog();
            }
            catch (COMException)
            {
                var errorMessage = "Failed to connect to eDrawings software. Please make sure you have it installed on your computer and set the correct EDRAWINGS_VIEWER_CLSID value in the config file.";
                logger.Error(errorMessage);
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to show file comparator: {ex}";
                logger.Error(errorMessage);
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (tempRemoteFilePath != null && File.Exists(tempRemoteFilePath)) {
                    logger.Debug($"Cleaning up temporary file {tempRemoteFilePath}...");
                    File.Delete(tempRemoteFilePath);
                }
                else
                    logger.Debug($"Tried to clean up temporary file {tempRemoteFilePath} but it was not found.");
            }
            logger.Debug("Completed CompareDesignFileCommand.");
        }

        private bool NeedToShowRightHandFile()
        {
            List<string> allowedRemoteActions;

            if (OnlineWorkSummaryMode)
                allowedRemoteActions = new List<string>()
                {
                    SyncCheckSummaryForm.RESTORE_LOCAL_ACTION_LABEL,
                    SyncCheckSummaryForm.ADD_REMOTE_ACTION_LABEL,
                    SyncCheckSummaryForm.UPDATE_REMOTE_ACTION_LABEL,
                };
            else
                allowedRemoteActions = new List<string>()
                {
                    SyncCheckSummaryForm.RESTORE_LOCAL_ACTION_LABEL,
                    SyncCheckSummaryForm.DELETE_REMOTE_ACTION_LABEL,
                    SyncCheckSummaryForm.UPDATE_REMOTE_ACTION_LABEL,
                };

            return allowedRemoteActions.Contains(remoteAction);
        }

        private bool NeedToShowLeftHandFile()
        {
            List<string> allowedRemoteActions;

            if (OnlineWorkSummaryMode)
                allowedRemoteActions = new List<string>()
                {
                    SyncCheckSummaryForm.DELETE_REMOTE_ACTION_LABEL,
                    SyncCheckSummaryForm.UPDATE_REMOTE_ACTION_LABEL,
                };
            else
                allowedRemoteActions = new List<string>()
                {
                    SyncCheckSummaryForm.ADD_REMOTE_ACTION_LABEL,
                    SyncCheckSummaryForm.UPDATE_REMOTE_ACTION_LABEL,
                };

            return allowedRemoteActions.Contains(remoteAction);
        }

        private async Task<string> DownloadRemoteFileAsync(FileMetadata localFileMetadata)
        {
            var tempFile = PathUtils.GetTempFileWithExtension(Path.GetExtension(localFileMetadata.RelativeFilePath));
            logger.Debug($"Created temporary file name: {tempFile}");
            logger.Debug("Downloading file from server to temporary file path...");
            await Synchronizer.SyncServiceClient.DownloadFileAsync(new DownloadFileRequest()
            {
                LocalFilename = tempFile,
                RelativeEquipmentPath = Synchronizer.Version.RemoteProject.RelativeEquipmentPath,
                RelativeFilePath = localFileMetadata.RelativeFilePath.Replace(Path.DirectorySeparatorChar, '/'),
                VersionFolder = "Ongoing",
            });
            logger.Debug("Successfully downloaded remote file to local temporary directory.");
            return tempFile;
        }
    }
}
