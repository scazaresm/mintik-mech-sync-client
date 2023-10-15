using MechanicalSyncApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class StopWorkingCommand : IUserInterfaceCommand
    {
        private readonly IVersionSynchronizer synchronizer;

        public StopWorkingCommand(IVersionSynchronizer synchronizer)
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
