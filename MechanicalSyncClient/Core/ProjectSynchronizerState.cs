using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core
{
    public abstract class ProjectSynchronizerState
    {
        protected IProjectSynchronizer _synchronizer;

        public void SetSynchronizer(IProjectSynchronizer synchronizer)
        {
            _synchronizer = synchronizer;
        }

        public abstract void UpdateUI();
        public abstract Task RunTransitionLogicAsync();
    }
}
