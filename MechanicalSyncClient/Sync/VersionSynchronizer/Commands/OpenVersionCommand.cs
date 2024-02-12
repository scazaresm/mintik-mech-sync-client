using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI.Forms;
using Microsoft.VisualBasic.FileIO;
using Serilog;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class OpenVersionCommand : IVersionSynchronizerCommandAsync
    {
        private DownloadWorkingCopyDialog progressDialog;
        private SyncCheckSummary SyncCheckSummary;

        public IVersionSynchronizer Synchronizer { get; }
        public Version RemoteVersion { get; }
        public Project RemoteProject { get; }
        public LocalVersion LocalVersion { get; }
        
        public IMechSyncServiceClient SyncServiceClient { get; }

        public IAuthenticationServiceClient AuthServiceClient { get; }

        public OpenVersionCommand(VersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));

            LocalVersion = Synchronizer.Version;
            RemoteVersion = Synchronizer.Version.RemoteVersion;
            RemoteProject = Synchronizer.Version.RemoteProject;
            SyncServiceClient = Synchronizer.SyncServiceClient;
            AuthServiceClient = Synchronizer.AuthServiceClient;
        }

        public async Task RunAsync()
        {
            Log.Debug($"Starting OpenVersionCommand, versionId = {RemoteVersion.Id}");

            if (IsNotVersionOwnerAnymore)
            {
                Log.Debug(
                    $"{AuthServiceClient.LoggedUserDetails.FullName} is not version owner anymore, version ownership change is expected..."
                );
                await HandleNotVersionOwnerAsync();
            }

            if (ShallDownloadFiles)
            {
                Log.Debug($"Could not find a local copy folder for this version, will download version files from server...");

                using (var cts = new CancellationTokenSource())
                {
                    await DownloadVersionFilesAsync(cts);
                }
            }
            Synchronizer.InitializeUI();
            Synchronizer.ChangeMonitor.Initialize();

            Log.Debug("Completed OpenVersionCommand.");
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
            var nextOwnerUserDetails = await AuthServiceClient.GetUserDetailsAsync(nextOwnerUserId);

            Log.Debug($"Next owner Id is {nextOwnerUserId}, notified logged user that version cannot be open because of waiting for ownership ack from new owner.");

            MessageBox.Show(
                $"Cannot open this version because it was transferred to {nextOwnerUserDetails.FullName}, waiting for ownership acknowledge.",
                "Ownership transfer in progress",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            throw new NotVersionOwnerException();
        }

        private async Task DownloadVersionFilesAsync(CancellationTokenSource cts)
        {
            try
            {
                if (ShallAcknowledgeOwnership)
                    await AskForOwnershipAcknowledgeAsync();

                if (FolderAlreadyExistsNotEmpty)
                    await MoveFolderToRecycleBinAsync();

                // create an empty folder to put downloaded files
                Directory.CreateDirectory(Synchronizer.Version.LocalDirectory);

                // progress dialog will inform user about download progress
                progressDialog = new DownloadWorkingCopyDialog(cts);
                progressDialog.SetOpeningLegend($"Opening {Synchronizer.Version}");
                progressDialog.Show();

                // download a working copy and get the sync check summary
                var syncCheckState = await new WorkingCopyDownloader(Synchronizer)
                {
                    Dialog = progressDialog,
                    CancellationTokenSource = cts
                }.DownloadAsync();

                SyncCheckSummary = syncCheckState.Summary;

                // we expect no changes in downloaded working copy, so it is synced with server
                if (SyncCheckSummary.HasChanges)
                {
                    await MoveFolderToRecycleBinAsync();
                    progressDialog?.Close();
                    throw new Exception("Downloaded copy did not match with server, please try again.");
                }
                progressDialog?.Close();

                string successMessage;

                if (ShallAcknowledgeOwnership)
                {
                    progressDialog.SetStatus("Acknowledging version ownership...");
                    await AcknowledgeOwnershipAsync();
                    successMessage =
                        $"You are the new owner of this version and got a local copy with latest changes from previous owner." + Environment.NewLine + Environment.NewLine +
                        "Remember to sync your changes frequently, thanks!";
                } 
                else
                {
                    successMessage = "You got a local copy to start working from, please do not move or delete this folder and remember to sync your changes frequently.";
                }
                MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                // user cancelled the operation
                Directory.Delete(LocalVersion.LocalDirectory, true);
                progressDialog.Close();
                throw new OpenVersionCanceledException();
            }
            catch (Exception ex)
            {
                Directory.Delete(LocalVersion.LocalDirectory, true);
                progressDialog.Close();
                throw ex;
            }
        }

        private async Task AskForOwnershipAcknowledgeAsync()
        {
            var ownerUserId = Synchronizer.Version.RemoteVersion.Owner.UserId;
            var ownerUserDetails = await AuthServiceClient.GetUserDetailsAsync(ownerUserId);

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
            Synchronizer.Version.RemoteVersion = await SyncServiceClient.AcknowledgeVersionOwnershipAsync(
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
                var userId = AuthServiceClient.LoggedUserDetails.Id;
                return RemoteVersion.Owner.UserId == userId && RemoteVersion.NextOwner != null;
            }
        }

        public bool ShallAcknowledgeOwnership { 
            get 
            {
                // logged user is not current owner but is assigned as next owner
                var userId = AuthServiceClient.LoggedUserDetails.Id;
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

        public bool FolderAlreadyExistsNotEmpty
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
