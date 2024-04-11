using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public abstract class VersionSynchronizerState
    {
        public IVersionSynchronizer Synchronizer { get; private set; }

        public void SetSynchronizer(IVersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public abstract void UpdateUI();
        public abstract Task RunAsync();
    }
}
