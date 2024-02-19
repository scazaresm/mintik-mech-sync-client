using MechanicalSyncApp.Core;
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
        public IVersionSynchronizer Synchronizer { get; private set; }

        public WorkOfflineCommand(VersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public async Task RunAsync()
        {
            Log.Debug("Starting WorkOfflineCommand...");

            var ui = Synchronizer.UI;

            Log.Debug("\tDisabling tool strip...");
            ui.SynchronizerToolStrip.Enabled = true;
            ui.WorkOfflineButton.Visible = false;
            ui.WorkOnlineButton.Visible = true;
            ui.SyncRemoteButton.Visible = true;

            ui.PublishVersionButton.Enabled = true;
            ui.PublishVersionButton.Visible = true;

            ui.TransferOwnershipButton.Enabled = true;
            ui.TransferOwnershipButton.Visible = true;

            Synchronizer.ChangeMonitor.StopMonitoring();
            ui.LocalFileViewer.PopulateFiles();

            if (Synchronizer.OnlineWorkSummary != null && Synchronizer.OnlineWorkSummary.HasChanges)
            {
                new SyncCheckSummaryForm(Synchronizer, Synchronizer.OnlineWorkSummary)
                {
                    OnlineWorkSummaryMode = true
                }.ShowDialog();
            }

            ui.StatusLabel.Text = "Deleting local copy snapshot...";
            DeleteLocalCopySnapshot();

            ui.StatusLabel.Text = "Clearing online work summary...";
            Synchronizer.OnlineWorkSummary = null;

            Synchronizer.SetState(new IdleState());
            await Synchronizer.RunStepAsync();

            Log.Debug("Completed WorkOfflineCommand.");
        }

        private void DeleteLocalCopySnapshot()
        {
            try
            {
                string snapshotDirectory = Synchronizer.SnapshotDirectory;
                Log.Debug($"Deleting local copy snapshot: {snapshotDirectory}");

                if (Directory.Exists(snapshotDirectory))
                    Directory.Delete(snapshotDirectory, true);
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to delete local copy snapshot when going offline: {ex}");
            }
        }
    }
}
