using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Util;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI.Forms;
using Microsoft.VisualBasic.Devices;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    internal class WorkOfflineCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer { get; private set; }

        public WorkOfflineCommand(VersionSynchronizer synchronizer, ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("Starting WorkOfflineCommand...");

            var ui = Synchronizer.UI;

            logger.Debug("\tDisabling tool strip...");
            ui.SynchronizerToolStrip.Enabled = true;
            ui.WorkOfflineButton.Visible = false;
            ui.WorkOnlineButton.Visible = true;
            ui.SyncRemoteButton.Visible = true;

            ui.PublishDeliverablesButton.Enabled = true;
            ui.PublishDeliverablesButton.Visible = true;

            ui.TransferOwnershipButton.Enabled = true;
            ui.TransferOwnershipButton.Visible = true;

            Synchronizer.ChangeMonitor.StopMonitoring();
            ui.LocalFileViewer.PopulateFiles();

            if (Synchronizer.OnlineWorkSummary != null && Synchronizer.OnlineWorkSummary.HasChanges)
            {
                new SyncCheckSummaryForm(Synchronizer, Synchronizer.OnlineWorkSummary, logger)
                {
                    OnlineWorkSummaryMode = true
                }.ShowDialog();
            }

            ui.StatusLabel.Text = "Deleting local copy snapshot...";
            DeleteLocalCopySnapshot();

            ui.StatusLabel.Text = "Clearing online work summary...";
            Synchronizer.OnlineWorkSummary = null;

            Synchronizer.SetState(new SynchronizerIdleState(logger));
            await Synchronizer.RunStepAsync();

            logger.Debug("Completed WorkOfflineCommand.");
        }

        private void DeleteLocalCopySnapshot()
        {
            try
            {
                string snapshotDirectory = Synchronizer.SnapshotDirectory;
                logger.Debug($"Deleting local copy snapshot: {snapshotDirectory}");

                if (Directory.Exists(snapshotDirectory))
                    DirectoryUtils.SafeDeleteTempDirectory(snapshotDirectory);
            }
            catch (Exception ex)
            {
                logger.Error($"Failed to delete local copy snapshot when going offline: {ex}");
            }
        }
    }
}
