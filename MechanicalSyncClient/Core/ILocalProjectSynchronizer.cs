using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core
{
    public interface ILocalProjectSynchronizer
    {
        // State related methods
        LocalProjectSynchronizerState GetState();
        void SetState(LocalProjectSynchronizerState state);
        Task RunTransitionLogicAsync();

        // UI related methods
        void UpdateUI();
    }
}
