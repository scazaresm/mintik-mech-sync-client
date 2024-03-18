using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI.Forms;
using Microsoft.VisualBasic.Devices;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class WorkOnlineCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer { get; set; }

        public WorkOnlineCommand(VersionSynchronizer synchronizer, ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("Starting WorkOnlineCommand...");

            var confirmation = MessageBox.Show(
                "Are you sure to start working online?", "Go online",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question
            );

            logger.Debug($"\tAsking user to confirm before going online, answer: {confirmation}");

            if (confirmation != DialogResult.Yes) {
                logger.Debug("\tGoing online has been aborted by user.");
                return;
            }

            var UI = Synchronizer.UI;
            try
            {
                logger.Debug("Disabling tool strip buttons while working online...");

                UI.SynchronizerToolStrip.Enabled = false;
                UI.WorkOnlineButton.Visible = false;
                UI.WorkOfflineButton.Visible = true;
                UI.SyncRemoteButton.Visible = false;

                Synchronizer.SetState(new IndexRemoteFilesState(logger));
                await Synchronizer.RunStepAsync();

                Synchronizer.SetState(new IndexPublishingsState(logger));
                await Synchronizer.RunStepAsync();

                Synchronizer.SetState(new IndexLocalFilesState(logger));
                await Synchronizer.RunStepAsync();

                var syncCheckState = new SyncCheckState(logger);
                Synchronizer.SetState(syncCheckState);
                await Synchronizer.RunStepAsync();

                if (syncCheckState.Summary.HasChanges)
                {
                    var result = new SyncCheckSummaryForm(Synchronizer, syncCheckState.Summary, logger).ShowDialog();

                    if (result != DialogResult.OK)
                    {
                        await Synchronizer.WorkOfflineAsync();
                        return;
                    }
                    Synchronizer.SetState(new ProcessSyncCheckSummaryState(syncCheckState.Summary));
                    await Synchronizer.RunStepAsync();
                }

                UI.StatusLabel.Text = "Creating local copy snapshot...";
                CreateLocalCopySnapshot(Synchronizer.Version.LocalDirectory);

                UI.StatusLabel.Text = "Creating new online work summary...";
                Synchronizer.OnlineWorkSummary = new SyncCheckSummary();

                Synchronizer.ChangeMonitor.StartMonitoring();
                UI.LocalFileViewer.PopulateFiles();

                Synchronizer.SetState(new MonitorFileSyncEventsState(logger));
                _ = Synchronizer.RunStepAsync();
            }
            catch(IOException ex)
            {
                logger.Error($"Could not go online because verion files seem to be used by other process: {ex}");
                MessageBox.Show(
                    "Could not go online, make sure that your version files are not being used by another process (such like SolidWorks) and try again.",
                    "Files already in use",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                await Synchronizer.WorkOfflineAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                await Synchronizer.WorkOfflineAsync();
            }
            logger.Debug("Finished WorkOnlineCommand.");
        }

        private void CreateLocalCopySnapshot(string localCopyDirectory)
        {
            try
            {
                if (!Directory.Exists(localCopyDirectory))
                {
                    throw new DirectoryNotFoundException($"Local copy directory not found: {localCopyDirectory}");
                }

                string snapshotDirectory = Synchronizer.SnapshotDirectory;

                if (Directory.Exists(snapshotDirectory))
                    Directory.Delete(snapshotDirectory, true);

                Directory.CreateDirectory(snapshotDirectory);

                new Computer().FileSystem.CopyDirectory(localCopyDirectory, snapshotDirectory);
            }
            catch(Exception ex)
            {
                logger.Error($"Failed to create a local copy snapshot before going online: {ex}");
            }
        }
    }
}
