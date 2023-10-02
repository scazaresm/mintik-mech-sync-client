using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public abstract class ProjectSynchronizerState
    {
        public IProjectSynchronizer Synchronizer { get; private set; }

        public void SetSynchronizer(IProjectSynchronizer synchronizer)
        {
            Synchronizer = synchronizer;
        }

        public abstract void UpdateUI();
        public abstract Task RunTransitionLogicAsync();
    }
}
