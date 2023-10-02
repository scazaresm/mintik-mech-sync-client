using MechanicalSyncApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.States
{
    public class SyncLocalProjectState : ProjectSynchronizerState
    {
        public override async Task RunTransitionLogicAsync()
        {
            // TODO: call handler for sync local project logic
            await Task.Delay(1000);

            Synchronizer.SetState(new WaitForChangeEventsState());
            _ = Synchronizer.RunTransitionLogicAsync();
        }

        public override void UpdateUI()
        {

        }
    }
}
