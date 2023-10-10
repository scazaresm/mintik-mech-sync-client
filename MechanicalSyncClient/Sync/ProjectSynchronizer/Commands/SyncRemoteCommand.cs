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
            await synchronizer.Sync();
        }
    }
}
