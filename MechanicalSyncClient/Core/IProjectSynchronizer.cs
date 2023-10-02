using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IProjectSynchronizer
    {
        ProjectSynchronizerUI UI { get; }

        LocalProject LocalProject { get; }
        IProjectChangeMonitor ChangeMonitor { get; }

        // State related methods
        ProjectSynchronizerState GetState();
        void SetState(ProjectSynchronizerState state);
        Task RunTransitionLogicAsync();

        // UI related methods
        void UpdateUI();
    }
}
