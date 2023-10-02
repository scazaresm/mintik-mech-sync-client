using MechanicalSyncApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.States
{
    public class ChangeMonitorDisabledState : ProjectSynchronizerState
    {
        public override async Task RunTransitionLogicAsync()
        {
            if(Synchronizer.ChangeMonitor.IsMonitoring)
            {
                Synchronizer.SetState(new SyncLocalProjectState());
            }
            else
            {
                Synchronizer.SetState(this);
                await Task.Delay(1000);
            }
            await Synchronizer.RunTransitionLogicAsync();
        }

        public override void UpdateUI()
        {

        }
    }
}
