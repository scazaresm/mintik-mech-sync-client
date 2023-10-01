using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IProjectSynchronizer
    {
        IProjectMonitor Monitor { get; }

        // State related methods
        ProjectSynchronizerState GetState();
        void SetState(ProjectSynchronizerState state);
        Task RunTransitionLogicAsync();

        // UI related methods
        void UpdateUI();
    }
}
