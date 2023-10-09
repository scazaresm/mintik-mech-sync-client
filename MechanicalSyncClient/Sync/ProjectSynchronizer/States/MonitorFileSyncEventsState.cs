using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.States
{
    public class MonitorFileSyncEventsState : ProjectSynchronizerState
    {
        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            if (ui == null)
                return;

            ui.SyncProgressBar.Visible = false;
            ui.StatusLabel.Text = "Remote server is synced with your local copy";
            ui.SynchronizerToolStrip.Enabled = true;
        }

        public override async Task RunTransitionLogicAsync()
        {
            if (Synchronizer.ChangeMonitor.IsEventQueueEmpty())
            {
                if (Synchronizer.ChangeMonitor.IsMonitoring())
                    Synchronizer.SetState(this);
                else
                    Synchronizer.SetState(new IdleState());

                await Task.Delay(1000);
                _ = Synchronizer.RunTransitionLogicAsync();
            }
            else
            {
                Synchronizer.SetState(new HandleFileSyncEventsState());
                await Task.Delay(1000);
                _ = Synchronizer.RunTransitionLogicAsync();
            }
        }
    }
}
