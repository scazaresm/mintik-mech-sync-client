using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public abstract class ProjectSynchronizerState
    {
        protected IProjectSynchronizer synchronizer;

        public void SetSynchronizer(IProjectSynchronizer synchronizer)
        {
            this.synchronizer = synchronizer;
        }

        public abstract void UpdateUI();
        public abstract Task RunTransitionLogicAsync();
    }
}
