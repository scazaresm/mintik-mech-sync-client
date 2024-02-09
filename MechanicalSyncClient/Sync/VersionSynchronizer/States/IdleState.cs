using MechanicalSyncApp.Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.States
{
    class IdleState : VersionSynchronizerState
    {
        public override async Task RunAsync()
        {
            Log.Information("State machine moved to IdleState, delaying for 1000ms.");
            await Task.Delay(1000);
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Working offline, remember to sync frequently";
            ui.SyncProgressBar.Visible = false;

            ui.SynchronizerToolStrip.Enabled = true;

            ui.SyncRemoteButton.Enabled = true;
            ui.SyncRemoteButton.Visible = true;

            ui.WorkOnlineButton.Visible = true;
            ui.WorkOnlineButton.Enabled = true;

            ui.WorkOfflineButton.Visible = false;

            ui.PublishVersionButton.Enabled = true;
            ui.TransferOwnershipButton.Enabled = true;
        }
    }
}
