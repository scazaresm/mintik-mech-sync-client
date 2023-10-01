using MechanicalSyncApp.Core;
using MechanicalSyncApp.Database.Domain;
using System;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync
{
    public class ProjectSynchronizer : IProjectSynchronizer
    {
        public IProjectMonitor Monitor { get; private set; }
        private ProjectSynchronizerState state;

        public LocalProject LocalProject { get; }

        public ProjectSynchronizer(LocalProject localProject, ProjectSynchronizerState initialState)
        {
            if (localProject is null)
            {
                throw new ArgumentNullException(nameof(localProject));
            }
            LocalProject = localProject;

            Monitor = new ProjectMonitor(localProject, "*.sldprt | *.sldasm | *.slddrw");
            SetState(initialState);
        }

        public ProjectSynchronizerState GetState()
        {
            return state;
        }

        public void SetState(ProjectSynchronizerState state)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state));
            this.state.SetSynchronizer(this);
            this.state.UpdateUI();
        }

        public void UpdateUI()
        {
            if (state != null)
                state.UpdateUI();
        }

        public async Task RunTransitionLogicAsync()
        {
            if (state != null)
                await state.RunTransitionLogicAsync();
        }
        
    }
}
