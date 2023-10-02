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
                    Synchronizer.SetState(this); 
                    await Task.Delay(1000);
                }
                else
                {
                    // change monitoring has been disabled
                    Synchronizer.SetState(new ChangeMonitorDisabledState());
                }
            }
            else
            {
                // process queued change events
                var nextState = new ProcessChangeEventsState(MechSyncServiceClient.Instance);
                Synchronizer.SetState(nextState);
            }
            await Synchronizer.RunTransitionLogicAsync();
        }

        public override void UpdateUI()
        {

        }
    }
}
