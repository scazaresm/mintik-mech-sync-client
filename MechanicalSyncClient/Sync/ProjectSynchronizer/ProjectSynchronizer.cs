using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Sync.ProjectSynchronizer.States;
using MechanicalSyncApp.UI;
using System;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer
{
    public class ProjectSynchronizer : IProjectSynchronizer
    {
        public IProjectChangeMonitor ChangeMonitor { get; private set; }
        public ProjectSynchronizerUI UI { get; private set; }

        private ProjectSynchronizerState state;

        public LocalProject LocalProject { get; }

        public ProjectSynchronizer(LocalProject localProject, ProjectSynchronizerUI ui)
        {
            if (localProject is null)
            {
                throw new ArgumentNullException(nameof(localProject));
            }
            LocalProject = localProject;
            UI = ui ?? throw new ArgumentNullException(nameof(ui));

            ChangeMonitor = new ProjectChangeMonitor(localProject, "*.sldprt | *.sldasm | *.slddrw");
            SetState(new ChangeMonitorDisabledState());
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
