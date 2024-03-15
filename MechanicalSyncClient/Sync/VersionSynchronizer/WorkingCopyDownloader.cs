using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.Sync.VersionSynchronizer
{
    public class WorkingCopyDownloader : IWorkingCopyDownloader
    {
        private readonly IVersionSynchronizer synchronizer;
        private readonly ILogger logger;

        public DownloadWorkingCopyDialog Dialog { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }

        private readonly Version remoteVersion;
        private readonly Project remoteProject;
        private readonly LocalVersion localVersion;

        public WorkingCopyDownloader(IVersionSynchronizer synchronizer, ILogger logger)
        {
            this.synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            remoteVersion = synchronizer.Version.RemoteVersion;
            remoteProject = synchronizer.Version.RemoteProject;
            localVersion = synchronizer.Version;
        }

        public async Task<SyncCheckState> DownloadAsync()
        {
            // Get all file metadata for this version
            var client = synchronizer.SyncServiceClient;

            var allFileMetadata = await client.GetFileMetadataAsync(remoteVersion.Id, null);
            var totalFiles = allFileMetadata.Count;

            int i = 0;

            // download each file based on its metadata
            foreach (var fileMetadata in allFileMetadata)
            {
                CancellationTokenSource?.Token.ThrowIfCancellationRequested();

                var localFileName = Path.Combine(localVersion.LocalDirectory, fileMetadata.RelativeFilePath);

                // remote file path is linux-based with forward slash, convert to windows-based
                localFileName = localFileName.Replace('/', Path.DirectorySeparatorChar);

                Dialog?.SetStatus($"Downloading {Path.GetFileName(localFileName)}...");

                var fileDirectory = Path.GetDirectoryName(localFileName);
                if (!Directory.Exists(fileDirectory))
                    Directory.CreateDirectory(fileDirectory);

                await client.DownloadFileAsync(new DownloadFileRequest()
                {
                    LocalFilename = localFileName,
                    RelativeEquipmentPath = remoteProject.RelativeEquipmentPath,
                    RelativeFilePath = fileMetadata.RelativeFilePath,
                    VersionFolder = "Ongoing"
                });

                i++;
                var currentProgress = totalFiles > 0
                    ? (int)((double)i / totalFiles * 100.0)
                    : 0;

                Dialog?.SetProgress(currentProgress);
            }

            Dialog?.SetStatus("Checking local copy...");
            
            return await CheckLocalCopyAsync();
        }

        private async Task<SyncCheckState> CheckLocalCopyAsync()
        {
            synchronizer.SetState(new IndexLocalFiles(logger));
            await synchronizer.RunStepAsync();

            synchronizer.SetState(new IndexRemoteFilesState(logger));
            await synchronizer.RunStepAsync();

            var syncCheckStep = new SyncCheckState(logger);
            synchronizer.SetState(syncCheckStep);
            await synchronizer.RunStepAsync();

            synchronizer.SetState(new SynchronizerIdleState(logger));
            await synchronizer.RunStepAsync();

            return syncCheckStep;
        }
    }
}
