using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI.Forms;
using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class OpenVersionCommand : VersionSynchronizerCommandAsync
    {
        private const string ONGOING_VERSION_FOLDER_NAME = "Ongoing";

        private OpenVersionProgressDialog progressDialog;

        private SyncCheckSummary SyncCheckSummary;

        public IVersionSynchronizer Synchronizer { get; }
        public Core.Services.MechSync.Models.Version RemoteVersion { get; }
        public OngoingVersion LocalVersion { get; }
        public IMechSyncServiceClient ServiceClient { get; }

        public OpenVersionCommand(VersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));

            LocalVersion = Synchronizer.Version;
            RemoteVersion = Synchronizer.Version.RemoteVersion;
            ServiceClient = Synchronizer.ServiceClient;
        }

        public async Task RunAsync()
        {
            using (var cts = new CancellationTokenSource())
            {
                if (IsNotVersionOwnerAnymore)
                    await HandleNotVersionOwnerAsync();

                if (ShallDownloadFiles)
                {
                    if (ShallRemoveExistingFolder)
                        await MoveFolderToRecycleBinAsync();

                    if (ShallAcknowledgeOwnership)
                        await AskForOwnershipAcknowledgeAsync();

                    progressDialog = new OpenVersionProgressDialog(cts);
                    progressDialog.SetOpeningLegend($"Opening {Synchronizer.Version}");
                    progressDialog.Show();

                    Directory.CreateDirectory(Synchronizer.Version.LocalDirectory);

                    try
                    {
                        await DownloadVersionFilesAsync(cts);
                    }
                    catch(OperationCanceledException)
                    {
                        await MoveFolderToRecycleBinAsync();
                        progressDialog.Close();
                        throw new OpenVersionCanceledException();
                    }

                    Synchronizer.InitializeUI();
                    Synchronizer.ChangeMonitor.Initialize();

                    progressDialog.SetStatus("Checking local copy...");
                    await CheckLocalCopyAsync();

                    if (ShallAcknowledgeOwnership)
                    {
                        progressDialog.SetStatus("Acknowledging version ownership...");
                        await AcknowledgeOwnershipAsync();
                        MessageBox.Show(
                            $"You are the new owner of this version and successfully downloaded a working copy with latest changes from previous owner." + Environment.NewLine + Environment.NewLine +
                            "Remember to sync the remote server frequently and publish this version when you are done, thanks!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    progressDialog.Close();
                }
                else
                {
                    Synchronizer.InitializeUI();
                    Synchronizer.ChangeMonitor.Initialize();
                }
            }
        }

        private async Task MoveFolderToRecycleBinAsync()
        {
            await Task.Run(() =>
                FileSystem.DeleteDirectory(LocalVersion.LocalDirectory, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin)
            );
        }

        private async Task HandleNotVersionOwnerAsync()
        {
            var nextOwnerUserId = Synchronizer.Version.RemoteVersion.NextOwner.UserId;
            var nextOwnerUserDetails = await AuthenticationServiceClient.Instance.GetUserDetailsAsync(nextOwnerUserId);

            MessageBox.Show(
                $"Cannot open this version because it was transferred to {nextOwnerUserDetails.FullName}, waiting for ownership acknowledge.",
                "Ownership transfer in progress",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            throw new NotVersionOwnerException();
        }

        private async Task CheckLocalCopyAsync()
        {
            Synchronizer.SetState(new IndexLocalFiles());
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new IndexRemoteFilesState());
            await Synchronizer.RunStepAsync();

            var syncCheckStep = new SyncCheckState();
            Synchronizer.SetState(syncCheckStep);
            await Synchronizer.RunStepAsync();
            SyncCheckSummary = syncCheckStep.Summary;

            // we expect no changes, so downloaded copy is synced with server
            if (!SyncCheckSummary.HasChanges)
            {
                Synchronizer.SetState(new IdleState());
                await Synchronizer.RunStepAsync();
                return;
            }

            // downloaded copy is not synced with server, remove corrupted files and fail
            await MoveFolderToRecycleBinAsync();

            if (progressDialog != null && progressDialog.Visible)
                progressDialog.Close();

            throw new System.Exception("Downloaded copy did not match with server, please try again.");
        }

        private async Task DownloadVersionFilesAsync(CancellationTokenSource cts)
        {
            var client = Synchronizer.ServiceClient;

            var fileMetadataResponse = await client.GetFileMetadataAsync(
                new GetFileMetadataRequest() { VersionId = RemoteVersion.Id }
            );

            var totalFiles = fileMetadataResponse.FileMetadata.Count;
            int i = 0;

            foreach (var file in fileMetadataResponse.FileMetadata)
            {
                cts.Token.ThrowIfCancellationRequested();

                var localFileName = Path.Combine(LocalVersion.LocalDirectory, file.RelativeFilePath);

                // remote file path is linux-based with forward slash, convert to windows-based
                localFileName = localFileName.Replace('/', Path.DirectorySeparatorChar);

                progressDialog.SetStatus($"Downloading {Path.GetFileName(localFileName)}...");

                var fileDirectory = Path.GetDirectoryName(localFileName);
                if (!Directory.Exists(fileDirectory))
                    Directory.CreateDirectory(fileDirectory);

                await client.DownloadFileAsync(new DownloadFileRequest()
                {
                    LocalFilename = localFileName,
                    ProjectId = RemoteVersion.ProjectId,
                    RelativeFilePath = file.RelativeFilePath,
                    VersionFolder = ONGOING_VERSION_FOLDER_NAME
                });

                i++;
                var currentProgress = totalFiles > 0 
                    ? (int)((double)i / totalFiles * 100.0) 
                    : 0;

                progressDialog.SetProgress(currentProgress);
            }
        }

        private async Task AskForOwnershipAcknowledgeAsync()
        {
            var ownerUserId = Synchronizer.Version.RemoteVersion.Owner.UserId;
            var ownerUserDetails = await AuthenticationServiceClient.Instance.GetUserDetailsAsync(ownerUserId);

            var confirmation = MessageBox.Show(
                $"This version was previously owned by {ownerUserDetails.FullName} but now was transferred to you, a copy will be downloaded to your computer so you can start working.",
                "Acknowledge version ownership",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information
            );

            if (confirmation != DialogResult.OK)
                throw new VersionOwnershipNotAcknowledgedException();
        }

        private async Task AcknowledgeOwnershipAsync()
        {
            Synchronizer.Version.RemoteVersion = await ServiceClient.AcknowledgeVersionOwnershipAsync(
                new AcknowledgeVersionOwnershipRequest() { 
                    VersionId = RemoteVersion.Id,
                    SyncChecksum = SyncCheckSummary.CalculateSyncChecksum()
                }
            );
        }

        public bool IsNotVersionOwnerAnymore
        {
            get
            {
                // logged user is current owner but there is a next owner assigned
                var userId = AuthenticationServiceClient.Instance.UserDetails.Id;
                return RemoteVersion.Owner.UserId == userId && RemoteVersion.NextOwner != null;
            }
        }

        public bool ShallAcknowledgeOwnership { 
            get 
            {
                // logged user is not current owner but is assigned as next owner
                var userId = AuthenticationServiceClient.Instance.UserDetails.Id;
                return RemoteVersion.Owner.UserId != userId && RemoteVersion.NextOwner.UserId == userId;
            }
        }    

        public bool ShallDownloadFiles { 
            get 
            {
                // directory doesn't exist or user was just assigned as owner
                return !Directory.Exists(LocalVersion.LocalDirectory) || ShallAcknowledgeOwnership;      
            } 
        }

        public bool ShallRemoveExistingFolder
        {
            get
            {
                // directory exists and is not empty
                return Directory.Exists(LocalVersion.LocalDirectory) && 
                    Directory.GetFileSystemEntries(LocalVersion.LocalDirectory).Length > 0;
            }
        }
    }
}
