using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.States
{
    public class IdleState : ProjectSynchronizerState
    {
        public override async Task RunTransitionLogicAsync()
        {
            if(synchronizer.Monitor.IsEventQueueEmpty())
            {
                await Task.Delay(1000);
                synchronizer.SetState(this);
            }
            else
            {
                var nextState = new ProcessQueuedEventsState(MechSyncServiceClient.Instance);
                synchronizer.SetState(nextState);
            }
            await synchronizer.RunTransitionLogicAsync();
        }

        public override void UpdateUI()
        {
            
        }
    }
}
