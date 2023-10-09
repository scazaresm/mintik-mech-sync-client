using MechanicalSyncApp.Core;
using MechanicalSyncApp.Sync.ProjectSynchronizer.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.Commands
{
    public class SyncRemoteCommand : IUserInterfaceCommand
    {
        private readonly IProjectSynchronizer synchronizer;

        public SyncRemoteCommand(IProjectSynchronizer synchronizer)
        {
            this.synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public void Execute()
        {
        }

        public async Task ExecuteAsync()
        {
            synchronizer.UI.SynchronizerToolStrip.Enabled = false;

            synchronizer.ChangeMonitor.StopMonitoring();
            synchronizer.SetState(new IdleState());
            await synchronizer.RunTransitionLogicAsync();

            await Task.Delay(1000);

            synchronizer.ChangeMonitor.StartMonitoring();
            synchronizer.SetState(new IndexLocalFiles());
            _ = synchronizer.RunTransitionLogicAsync();
        }
    }
}
