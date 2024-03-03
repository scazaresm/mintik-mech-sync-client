using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using Serilog.Core;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class PublishVersionCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer { get; private set; }

        public PublishVersionCommand(IVersionSynchronizer synchronizer, ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            try
            {
                var workingCopySynced = await CheckWorkingCopySyncedAsync();
                if (!workingCopySynced) return;

                DesignFilesAnalysisResult designFilesAnalysisResult;

                using (var cts = new CancellationTokenSource())
                {
                    designFilesAnalysisResult = await AnalyzeDesignFilesAsync(cts);

                    if (designFilesAnalysisResult != null && designFilesAnalysisResult.ExceptionObject != null)
                        return;
                }

                var result = new VersionChecklistForm(Synchronizer).ShowDialog();

                if (result != DialogResult.OK)
                    return;

                string versionId = Synchronizer.Version.RemoteVersion.Id;

                string localDirectory = Synchronizer.Version.LocalDirectory;

                await Synchronizer.SyncServiceClient.PublishVersionAsync(new PublishVersionRequest()
                {
                    VersionId = versionId
                });

                var progressDialog = new PublishVersionProgressDialog(Synchronizer, versionId, localDirectory);
                progressDialog.ShowDialog();

                if (progressDialog.IsPublishingSuccess)
                    await new CloseVersionCommand(Synchronizer, logger).RunAsync();
            }
            catch(Exception ex)
            {
                var message = $"Publishing has failed: {ex}";
                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> CheckWorkingCopySyncedAsync()
        {
            Synchronizer.SetState(new IdleState(logger));
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new IndexRemoteFilesState(logger));
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new IndexLocalFiles(logger));
            await Synchronizer.RunStepAsync();

            var syncCheckState = new SyncCheckState(logger) { RethrowException = true };
            Synchronizer.SetState(syncCheckState);
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new IdleState(logger));
            await Synchronizer.RunStepAsync();

            if (syncCheckState.Summary.HasChanges)
            {
                logger.Debug("Local copy has changes that need to be uploaded to server before publishing, asked user to hit the Sync remote button first.");

                MessageBox.Show(
                    "Your local copy has changes that need to be uploaded to server before publishing, please use the Sync remote button first.",
                    "Unsynced changed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return false;
            }
            return true;
        }

        private async Task<DesignFilesAnalysisResult> AnalyzeDesignFilesAsync(CancellationTokenSource cts)
        {
            var dialog = new DesignFilesAnalysisDialog(cts);

            var result = new DesignFilesAnalysisResult();

            try
            {
                dialog.SetProgress(0);
                dialog.SetStatus("Connecting to SolidWorks"); 
                dialog.Show();

                using (var solidWorksStarter = new SolidWorksStarter(logger)
                {
                    SolidWorksExePath = @"C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS (2)\SLDWORKS.exe",
                    SolidWorksStartTimeoutSeconds = 60,
                    Hidden = true,
                    ShowSplash = false,
                })
                {
                    var reviewableFetcher = new ReviewableFileMetadataFetcher(Synchronizer);

                    await solidWorksStarter.StartSolidWorksAsync();

                    var relationshipAnalyzer = new AssemblyPartRelationshipAnalyzer(
                        solidWorksStarter,
                        Synchronizer,
                        reviewableFetcher,
                        cts,
                        logger
                    )
                    { Dialog = dialog };

                    result.PartsInAssemblyLookup = await relationshipAnalyzer.AnalyzeAsync();
                }
            }
            catch (OperationCanceledException ex)
            {
                // User has cancelled the operation, do nothing
                logger.Debug("The design file analysis has been cancelled.");
                result.ExceptionObject = ex;
            }
            finally
            {
                dialog?.Close();
            }
            return result;
        }
    }
}
