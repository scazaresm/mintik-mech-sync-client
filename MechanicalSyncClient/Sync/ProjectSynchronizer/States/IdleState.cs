using MechanicalSyncApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.States
{
    class IdleState : ProjectSynchronizerState
    {
        public override async Task RunTransitionLogicAsync()
        {
            await Task.Delay(500);
        }

        public override void UpdateUI()
        {
            Synchronizer.UI.StatusLabel.Text = "Local copy is in read-only mode";
        }
    }
}
