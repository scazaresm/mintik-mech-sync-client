using MechanicalSyncApp.Core;
using MechanicalSyncApp.Sync.ProjectSynchronizer.States;
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
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync()
        {
            await synchronizer.StopMonitoringEvents();
        }
    }
}
