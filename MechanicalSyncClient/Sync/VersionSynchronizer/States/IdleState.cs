using MechanicalSyncApp.Core;
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
            await Task.Delay(1000);
        }

        public override void UpdateUI()
        {
            Synchronizer.UI.StatusLabel.Text = "Working offline.";
        }
    }
}
