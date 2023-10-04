using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.States
{
    public class WaitForChangeEventsState : ProjectSynchronizerState
    {
        public override async Task RunTransitionLogicAsync()
        {
            if (Synchronizer.ChangeMonitor.IsEventQueueEmpty())
            {
                if(Synchronizer.ChangeMonitor.IsMonitoring)
                {
                    // change monitoring stills enabled, wait for change events...
                    await Task.Delay(500);
                    Synchronizer.SetState(this);
                    _ = Synchronizer.RunTransitionLogicAsync();
                }
                else
                {
                    Synchronizer.SetState(new StopSynchronizerState());
                    _ = Synchronizer.RunTransitionLogicAsync();
                }
            }
            else
            {
                // process queued change events
                var nextState = new ProcessChangeEventsState(MechSyncServiceClient.Instance);
                Synchronizer.SetState(nextState);
                _ = Synchronizer.RunTransitionLogicAsync();
            }
        }

        public override void UpdateUI()
        {

        }
    }
}
