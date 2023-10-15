using MechanicalSyncApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class SyncRemoteCommand : IUserInterfaceCommand
    {
        private readonly IVersionSynchronizer synchronizer;

        public SyncRemoteCommand(IVersionSynchronizer synchronizer)
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
