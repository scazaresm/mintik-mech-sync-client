using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.States
{
    public class MonitorFileSyncEventsState : VersionSynchronizerState
    {
        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;

            ui.SyncProgressBar.Visible = false;
            ui.SynchronizerToolStrip.Enabled = true;
            ui.StatusLabel.Text = "Working online, remote server is currently synced with your local copy";
        }

        public override async Task RunAsync()
        {
            if (Synchronizer.ChangeMonitor.IsEventQueueEmpty())
            {
                if (Synchronizer.ChangeMonitor.IsMonitoring())
                    Synchronizer.SetState(this);
                else
                    Synchronizer.SetState(new IdleState());

                await Task.Delay(1000);
                _ = Synchronizer.RunStepAsync();
            }
            else
            {
                Synchronizer.SetState(new HandleFileSyncEventsState());
                await Task.Delay(1000);
                _ = Synchronizer.RunStepAsync();
            }
        }
    }
}
