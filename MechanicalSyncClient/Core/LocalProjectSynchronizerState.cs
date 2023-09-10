using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core
{
    public abstract class LocalProjectSynchronizerState
    {
        protected ILocalProjectSynchronizer _synchronizer;

        public void SetSynchronizer(ILocalProjectSynchronizer synchronizer)
        {
            _synchronizer = synchronizer;
        }

        public abstract void UpdateUI();
        public abstract Task RunTransitionLogicAsync();
    }
}
