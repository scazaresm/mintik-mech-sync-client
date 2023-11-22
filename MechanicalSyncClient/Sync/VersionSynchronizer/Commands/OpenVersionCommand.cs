using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
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
    public class VersionFolderAlreadyExistsException : System.Exception
    {
        public VersionFolderAlreadyExistsException()
        {
        }

        public VersionFolderAlreadyExistsException(string message)
            : base(message)
        {
        }

        public VersionFolderAlreadyExistsException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }

    public class OpenVersionCommand : VersionSynchronizerCommandAsync
    {
        private const string ONGOING_VERSION_FOLDER_NAME = "Ongoing";

        private readonly Label status;
        private readonly ProgressBar progress;

        public VersionSynchronizer Synchronizer { get; }
        public Version RemoteVersion { get; }
        public OngoingVersion LocalVersion { get; }
        public MechSyncServiceClient ServiceClient { get; }


        public OpenVersionCommand(VersionSynchronizer synchronizer, Label status, ProgressBar progress)
        {
            Synchronizer = synchronizer ?? throw new System.ArgumentNullException(nameof(synchronizer));
            this.status = status ?? throw new System.ArgumentNullException(nameof(status));
            this.progress = progress ?? throw new System.ArgumentNullException(nameof(progress));

            LocalVersion = Synchronizer.Version;
            RemoteVersion = Synchronizer.Version.RemoteVersion;
        }

        public async Task RunAsync()
        {
            status.Text = "Starting...";

            if (ShallDownloadFiles)
            {
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
            var response = MessageBox.Show(
                $"A folder for this version already exists at {LocalVersion.LocalDirectory}, the folder and its contents will be sent to recycle bin and most recent files will be downloaded from server.{System.Environment.NewLine}" +
                $"{System.Environment.NewLine}Do you want to proceed?",
                "Folder already exists",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (response != DialogResult.Yes)
                throw new VersionFolderAlreadyExistsException($"A folder for this version already exists at {LocalVersion.LocalDirectory}.");

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

        public bool ShallDownloadFiles { 
            get {
                // directory doesn't exist or DownloadRequired flag is enabled in server
                return !Directory.Exists(LocalVersion.LocalDirectory) || RemoteVersion.DownloadRequired;      
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
