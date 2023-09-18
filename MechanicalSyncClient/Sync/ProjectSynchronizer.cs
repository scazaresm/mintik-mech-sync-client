using MechanicalSyncClient.Core;
using MechanicalSyncClient.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Sync
{
    public class ProjectSynchronizer : IProjectSynchronizer
    {
        private IProjectMonitor _monitor;
        private ProjectSynchronizerState _state;

        public LocalProject LocalProject { get; }

        public ProjectSynchronizer(LocalProject localProject, ProjectSynchronizerState initialState)
        {
            if (localProject is null)
            {
                throw new ArgumentNullException(nameof(localProject));
            }
            LocalProject = localProject;

            _monitor = new ProjectMonitor(localProject, "*.sldprt | *.sldasm | *.slddrw");
            SetState(initialState);
        }

        public ProjectSynchronizerState GetState()
        {
            return _state;
        }

        public void SetState(ProjectSynchronizerState state)
        {
            _state = state ?? throw new ArgumentNullException(nameof(state));
            _state.SetSynchronizer(this);
            _state.UpdateUI();
        }

        public void UpdateUI()
        {
            if (_state != null)
                _state.UpdateUI();
        }

        public async Task RunTransitionLogicAsync()
        {
            if (_state != null)
                await _state.RunTransitionLogicAsync();
        }
        
    }
}
