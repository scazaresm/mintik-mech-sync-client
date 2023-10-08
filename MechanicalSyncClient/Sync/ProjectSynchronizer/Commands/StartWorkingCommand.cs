using MechanicalSyncApp.Core;
using MechanicalSyncApp.Sync.ProjectSynchronizer.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.Commands
{
    public class StartWorkingCommand : IUserInterfaceCommand
    {
        private readonly IProjectSynchronizer synchronizer;

        public StartWorkingCommand(IProjectSynchronizer synchronizer)
        {
            this.synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public void Execute()
        {
            var ui = synchronizer.UI;

            ui.StartWorkingButton.Visible = false;
            ui.StopWorkingButton.Visible = true;
            ui.SyncRemoteButton.Visible = true;

            synchronizer.ChangeMonitor.StartMonitoring();
            synchronizer.SetState(new CheckSyncState());
            _ = synchronizer.RunTransitionLogicAsync();
        }
    }
}
