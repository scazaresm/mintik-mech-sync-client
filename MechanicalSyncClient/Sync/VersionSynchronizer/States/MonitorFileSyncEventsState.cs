using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.States
{
    public class MonitorFileSyncEventsState : VersionSynchronizerState
    {
        private readonly ILogger logger;

        public MonitorFileSyncEventsState(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.StatusLabel.Text = "Working online, changes to your local folder will be pushed to server in real time...";

            ui.SyncProgressBar.Visible = false;
            ui.SynchronizerToolStrip.Enabled = true;

            ui.SyncRemoteButton.Enabled = false;
            ui.SyncRemoteButton.Visible = true;

            ui.WorkOnlineButton.Visible = false;

            ui.WorkOfflineButton.Enabled = true;
            ui.WorkOfflineButton.Visible = true;

            ui.PublishVersionButton.Enabled = false;
            ui.PublishVersionButton.Visible = true;

            ui.TransferOwnershipButton.Enabled = false;
            ui.TransferOwnershipButton.Visible = true;
        }

        public override async Task RunAsync()
        {
            if (Synchronizer.ChangeMonitor.IsEventQueueEmpty())
            {
                if (Synchronizer.ChangeMonitor.IsMonitoring())
                    Synchronizer.SetState(this);
                else
                    Synchronizer.SetState(new IdleState(logger));

                await Task.Delay(1000);
                _ = Synchronizer.RunStepAsync();
            }
            else
            {
                Synchronizer.SetState(new HandleFileSyncEventsState(logger));
                await Synchronizer.RunStepAsync();
            }
        }
    }
}
