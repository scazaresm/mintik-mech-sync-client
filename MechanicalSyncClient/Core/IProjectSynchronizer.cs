using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core
{
    public interface IProjectSynchronizer
    {
        // State related methods
        ProjectSynchronizerState GetState();
        void SetState(ProjectSynchronizerState state);
        Task RunTransitionLogicAsync();

        // UI related methods
        void UpdateUI();
    }
}
