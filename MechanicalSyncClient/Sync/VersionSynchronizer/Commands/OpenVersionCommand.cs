using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class OpenVersionCommand : VersionSynchronizerCommandAsync
    {
        private const string ONGOING_VERSION_FOLDER_NAME = "Ongoing";

        private readonly Label status;
        private readonly ProgressBar progress;

        public VersionSynchronizer Synchronizer { get; }
        public Version RemoteVersion { get; }
        public OngoingVersion LocalVersion { get; }
        public IMechSyncServiceClient ServiceClient { get; }


        public OpenVersionCommand(VersionSynchronizer synchronizer, Label status, ProgressBar progress)
        {
            Synchronizer = synchronizer ?? throw new System.ArgumentNullException(nameof(synchronizer));
            this.status = status ?? throw new System.ArgumentNullException(nameof(status));
            this.progress = progress ?? throw new System.ArgumentNullException(nameof(progress));

            LocalVersion = Synchronizer.Version;
            RemoteVersion = Synchronizer.Version.RemoteVersion;
            ServiceClient = Synchronizer.ServiceClient;
        }

        public async Task RunAsync()
        {
            status.Text = "Starting...";

            if (IsNotVersionOwnerAnymore)
                HandleNotVersionOwner();

            if (ShallDownloadFiles)
            {
                if (ShallAcknowledgeOwnership)
                    await AcknowledgeVersionOwnershipAsync();

                if(ShallRemoveExistingFolder)
                    await MoveExistingFolderToRecycleBinAsync();
    
                Directory.CreateDirectory(Synchronizer.Version.LocalDirectory);
                await DownloadVersionFilesAsync();

                Synchronizer.InitializeUI();
                Synchronizer.ChangeMonitor.Initialize();

                status.Text = "Checking local copy...";
                await CheckLocalCopyAsync();
            }
            else
            {
                Synchronizer.InitializeUI();
                Synchronizer.ChangeMonitor.Initialize();
            }
        }

        private void HandleNotVersionOwner()
        {
            var nextOwnerUsername = Synchronizer.Version.RemoteVersion.NextOwner.Username;
            MessageBox.Show(
                $"Cannot open this version because it was transferred to {nextOwnerUsername}, waiting for ownership acknowledge.",
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

            // we expect no changes, so downloaded copy is synced with server
            if (!syncCheckStep.Summary.HasChanges)
            {
                Synchronizer.SetState(new IdleState());
                await Synchronizer.RunStepAsync();
                return;
            }

            // if downloaded copy is not synced with server, ask for retry
            var response =
                MessageBox.Show(
                    "Local copy does not match with server after download, do you want to retry?", 
                    "Local copy mismatch", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question
                );

            // if no retry, fail the operation
            if (response != DialogResult.Yes)
                throw new System.Exception("Failed to open version, please try again.");

            // retry
            _ = RunAsync();
        }

        private async Task MoveExistingFolderToRecycleBinAsync()
        {
            status.Text = $"Moving {LocalVersion.LocalDirectory} to recycle bin...";
            await Task.Run(() => FileSystem.DeleteDirectory(
                LocalVersion.LocalDirectory, 
                UIOption.OnlyErrorDialogs,
                RecycleOption.SendToRecycleBin
            ));
            status.Text = "";
        }

        private async Task DownloadVersionFilesAsync()
        {
            var client = Synchronizer.ServiceClient;

            var fileMetadataResponse = await client.GetFileMetadataAsync(
                new GetFileMetadataRequest() { VersionId = RemoteVersion.Id }
            );

            var totalFiles = fileMetadataResponse.FileMetadata.Count;
            int i = 0;

            foreach (var file in fileMetadataResponse.FileMetadata)
            {
                var localFileName = Path.Combine(LocalVersion.LocalDirectory, file.RelativeFilePath);

                // remote file path is linux-based with forward slash, convert to windows-based
                localFileName = localFileName.Replace('/', Path.DirectorySeparatorChar);

                status.Text = $"Downloading {Path.GetFileName(localFileName)}...";

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
                progress.Value = totalFiles > 0 
                    ? (int)((double)i / totalFiles * 100.0) 
                    : 0;
            }
        }

        private async Task AcknowledgeVersionOwnershipAsync()
        {
            var ownerUsername = Synchronizer.Version.RemoteVersion.Owner.Username;

            var confirmation = MessageBox.Show(
                $"This version was previously owned by {ownerUsername} but now was transferred to you, a copy will be downloaded to your computer so you can start working.",
                "Acknowledge version ownership",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information
            );

            if (confirmation != DialogResult.OK)
                throw new VersionOwnershipNotAcknowledgedException();

            Synchronizer.Version.RemoteVersion = await ServiceClient.AcknowledgeVersionOwnershipAsync(
                new AcknowledgeVersionOwnershipRequest() { VersionId = RemoteVersion.Id }
            );
        }

        public bool IsNotVersionOwnerAnymore
        {
            get
            {
                // loged user is current owner but there is a next owner assigned
                var username = AuthenticationServiceClient.Instance.UserDetails.Username;
                return RemoteVersion.Owner.Username == username && RemoteVersion.NextOwner != null;
            }
        }

        public bool ShallAcknowledgeOwnership { 
            get 
            {
                // logged user is not current owner but is assigned as next owner
                var username = AuthenticationServiceClient.Instance.UserDetails.Username;
                return RemoteVersion.Owner.Username != username && RemoteVersion.NextOwner.Username == username;
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
