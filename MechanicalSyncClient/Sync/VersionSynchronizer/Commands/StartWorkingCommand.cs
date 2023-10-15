using MechanicalSyncApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class StartWorkingCommand : IUserInterfaceCommand
    {
        private readonly IVersionSynchronizer synchronizer;

        public StartWorkingCommand(IVersionSynchronizer synchronizer)
        {
            this.synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public void Execute()
        {

        }

        public async Task ExecuteAsync()
        {
            await synchronizer.StartMonitoringEvents();
        }
    }
}
