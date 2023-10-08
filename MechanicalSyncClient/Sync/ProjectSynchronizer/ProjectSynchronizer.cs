using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Sync.ProjectSynchronizer.Commands;
using MechanicalSyncApp.UI;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer
{
    public class ProjectSynchronizer : IProjectSynchronizer
    {
        public IMechSyncServiceClient ServiceClient { get; set; }
        public IProjectChangeMonitor ChangeMonitor { get; private set; }
        public IFileMetadataChecker SyncChecker { get; private set; }

        public ProjectSynchronizerUI UI { get; private set; }

        private ProjectSynchronizerState state;
        private bool disposedValue;

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
            ServiceClient = MechSyncServiceClient.Instance;
            InitializeUI();
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

        public void InitializeUI()
        {
            UI.InitializeFileViewer(LocalProject);
            UI.FileViewer.AttachListView(UI.FileViewerListView);
            UI.FileViewerListView.SetDoubleBuffered();

            UI.SyncProgressBar.Visible = false;
            UI.SyncRemoteButton.Visible = false;

            UI.StartWorkingButton.Click += (object sender, EventArgs e) => new StartWorkingCommand(this).Execute();
            UI.StartWorkingButton.Visible = true;

            UI.StopWorkingButton.Click += (object sender, EventArgs e) => new StopWorkingCommand(this).Execute();
            UI.StopWorkingButton.Visible = false;


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


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ChangeMonitor.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
