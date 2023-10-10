using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync.ProjectSynchronizer.Commands;
using MechanicalSyncApp.Sync.ProjectSynchronizer.States;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer
{
    public class ProjectSynchronizer : IProjectSynchronizer
    {
        public IMechSyncServiceClient ServiceClient { get; set; }
        public IProjectChangeMonitor ChangeMonitor { get; private set; }

        public Dictionary<string, FileMetadata> LocalFileIndex { get; private set; }
        public Dictionary<string, FileMetadata> RemoteFileIndex { get; private set; }

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
            LocalFileIndex = new Dictionary<string, FileMetadata>();
            RemoteFileIndex = new Dictionary<string, FileMetadata>();
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
            UI.SyncRemoteButton.Click += async (object sender, EventArgs e) => await new SyncRemoteCommand(this).ExecuteAsync();

            UI.StartWorkingButton.Click += async (object sender, EventArgs e) => await new StartWorkingCommand(this).ExecuteAsync();
            UI.StartWorkingButton.Visible = true;

            UI.StopWorkingButton.Click += async (object sender, EventArgs e) => await new StopWorkingCommand(this).ExecuteAsync();
            UI.StopWorkingButton.Visible = false;

        }

        public void UpdateUI()
        {
            if (state != null)
                state.UpdateUI();
        }

        public async Task RunStepAsync()
        {
            if (state != null)
                await state.RunAsync();
        }

        public async Task StartMonitoringEvents()
        {
            UI.SynchronizerToolStrip.Enabled = false;
            UI.StartWorkingButton.Visible = false;
            UI.StopWorkingButton.Visible = true;
            UI.SyncRemoteButton.Visible = true;

            SetState(new IndexLocalFiles());
            await RunStepAsync();

            SetState(new IndexRemoteFilesState());
            await RunStepAsync();

            SetState(new SyncCheckState());
            await RunStepAsync();

            ChangeMonitor.StartMonitoring();

            SetState(new HandleFileSyncEventsState());
            _ = RunStepAsync();
        }

        public async Task StopMonitoringEvents()
        {
            UI.StopWorkingButton.Visible = false;
            UI.SyncRemoteButton.Visible = false;
            UI.StartWorkingButton.Visible = true;

            ChangeMonitor.StopMonitoring();
            SetState(new IdleState());
            await RunStepAsync();
        }

        public async Task Sync()
        {
            UI.SyncRemoteButton.Enabled = false;
            UI.SynchronizerToolStrip.Enabled = false;
            UI.SyncRemoteButton.Enabled = true;
            ChangeMonitor.StopMonitoring();

            SetState(new IdleState());
            await RunStepAsync();

            SetState(new IndexLocalFiles());
            await RunStepAsync();

            SetState(new IndexRemoteFilesState());
            await RunStepAsync();

            SetState(new SyncCheckState());
            await RunStepAsync();

            ChangeMonitor.StartMonitoring();

            SetState(new HandleFileSyncEventsState());
            _ = RunStepAsync();
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
