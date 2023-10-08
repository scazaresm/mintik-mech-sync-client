using MechanicalSyncApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.Commands
{
    public class StopWorkingCommand : IUserInterfaceCommand
    {
        private readonly IProjectSynchronizer synchronizer;

        public StopWorkingCommand(IProjectSynchronizer synchronizer)
        {
            this.synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public void Execute()
        {
            var ui = synchronizer.UI;

            ui.StopWorkingButton.Visible = false;
            ui.SyncRemoteButton.Visible = false;
            ui.StartWorkingButton.Visible = true;

            synchronizer.ChangeMonitor.StopMonitoring();
        }
    }
}
