using MechanicalSyncApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.States
{
    class StopSynchronizerState : ProjectSynchronizerState
    {
        public override async Task RunTransitionLogicAsync()
        {
            await Task.Delay(100);
        }

        public override void UpdateUI()
        {
            
        }
    }
}
