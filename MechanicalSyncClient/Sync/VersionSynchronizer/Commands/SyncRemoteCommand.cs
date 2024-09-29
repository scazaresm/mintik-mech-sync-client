using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class SyncRemoteCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer { get; private set; }

        public SyncCheckSummary Summary { get; set; }

        public bool NotifyWhenComplete { get; set; } = true;

        public bool EnableToolStripWhenComplete { get; set; } = true;

        public bool Complete {  get; set; } = false;

        public string TargetFileName { get; set; }

        public SyncRemoteCommand(IVersionSynchronizer synchronizer, ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger;
            this.logger = logger;
        }

        public async Task RunAsync()
        {
            logger.Debug($"Starting SyncRemoteCommand, versionId = {Synchronizer.Version.RemoteVersion.Id} ...");
            try
            {
                if (Synchronizer.Version.RemoteVersion.Status != "Ongoing")
                {
                    logger.Error($"Cannot sync changes because this version is not in Ongoing status.");

                    throw new InvalidOperationException(
                        "Cannot sync changes because this version is not in Ongoing status."
                    );
                }
                Synchronizer.ChangeMonitor.StopMonitoring();

                Synchronizer.SetState(new IndexRemoteFilesState(logger));
                await Synchronizer.RunStepAsync();

                Synchronizer.SetState(new IndexLocalFilesState(logger) { TargetFileName = TargetFileName });
                await Synchronizer.RunStepAsync();

                Synchronizer.SetState(new IndexPublishingsState(logger));
                await Synchronizer.RunStepAsync();

                // skipping deleted files check when the sync is focused on a specific target file
                var syncCheckState = new SyncCheckState(logger) { SkipDeletedFilesCheck = !string.IsNullOrEmpty(TargetFileName) };
                Synchronizer.SetState(syncCheckState);
                await Synchronizer.RunStepAsync();

                Summary = syncCheckState.Summary;

                // something went wrong during sync check
                if (Summary.ExceptionObject != null) 
                    throw Summary.ExceptionObject;
                
                // sync check was ok, now sync the changes (if any)
                if (Summary.HasChanges)
                {
                    var response = new SyncCheckSummaryForm(Synchronizer, Summary, logger).ShowDialog();

                    if (response != DialogResult.OK)
                    {
                        await Synchronizer.WorkOfflineAsync();
                        return;
                    }

                    Synchronizer.SetState(new ProcessSyncCheckSummaryState(syncCheckState.Summary));
                    await Synchronizer.RunStepAsync();
                }

                Synchronizer.SetState(new MonitorFileSyncEventsState(logger));
                await Synchronizer.RunStepAsync();

                if (NotifyWhenComplete)
                {
                    var syncCompleteMessage = Summary.HasChanges
                            ? "The remote server has been synced with your local copy."
                            : "The remote server is already synced with your local copy.";

                    logger.Debug(syncCompleteMessage);

                    MessageBox.Show(
                        syncCompleteMessage,
                        "Synced remote",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                Complete = true;
            }
            catch (IOException ex)
            {
                logger.Error($"Could not sync remote because verion files seem to be used by other process: {ex}");
                MessageBox.Show(
                    "Could not sync remote, make sure that your version files are not being used by another process (such like SolidWorks) and try again.",
                    "Files already in use",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to sync remote: {ex} {ex?.InnerException?.Message}";
                logger.Error(errorMessage);
                MessageBox.Show(
                    errorMessage, "Sync error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                // always go back to idle state
                Synchronizer.SetState(new SynchronizerIdleState(logger));
                await Synchronizer.RunStepAsync();

                if (EnableToolStripWhenComplete)
                    Synchronizer.UI.SynchronizerToolStrip.Enabled = true;
            }
            logger.Debug("Completed SyncRemoteCommand...");
        }
    }
}
