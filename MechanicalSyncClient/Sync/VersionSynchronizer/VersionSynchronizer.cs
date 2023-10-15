using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync.VersionSynchronizer.Commands;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer
{
    public class VersionSynchronizer : IVersionSynchronizer
    {
        public IMechSyncServiceClient ServiceClient { get; set; }
        public IVersionChangeMonitor ChangeMonitor { get; private set; }

        public Dictionary<string, FileMetadata> LocalFileIndex { get; private set; }
        public Dictionary<string, FileMetadata> RemoteFileIndex { get; private set; }

        public VersionSynchronizerUI UI { get; private set; }

        private VersionSynchronizerState state;
        private bool disposedValue;

        public LocalVersion Version { get; }

        public string FileExtensionFilter { get; private set; } = "*.sldasm | *.sldprt | *.slddrw";

        public VersionSynchronizer(LocalVersion version, VersionSynchronizerUI ui)
        {
            if (version is null)
            {
                throw new ArgumentNullException(nameof(version));
            }
            Version = version;

            UI = ui ?? throw new ArgumentNullException(nameof(ui));
            ChangeMonitor = new VersionChangeMonitor(version, FileExtensionFilter);
            ServiceClient = MechSyncServiceClient.Instance;
            LocalFileIndex = new Dictionary<string, FileMetadata>();
            RemoteFileIndex = new Dictionary<string, FileMetadata>();
            InitializeUI();

            SetState(new IdleState());
            _ = RunStepAsync();
        }

        public VersionSynchronizerState GetState()
        {
            return state;
        }

        public void SetState(VersionSynchronizerState state)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state));
            this.state.SetSynchronizer(this);
            this.state.UpdateUI();
        }

        public void InitializeUI()
        {
            UI.ProjectFolderNameLabel.Text = Version.RemoteProject.FolderName;
            UI.InitializeFileViewer(Version);
        
            UI.FileViewer.AttachListView(UI.FileViewerListView);
            UI.FileViewerListView.SetDoubleBuffered();

            UI.SyncProgressBar.Visible = false;
            UI.SyncRemoteButton.Visible = false;
            UI.SyncRemoteButton.Click += SyncRemoteButton_Click;

            UI.StartWorkingButton.Click += StartWorkingButton_Click;
            UI.StartWorkingButton.Visible = true;

            UI.StopWorkingButton.Click += StopWorkingButton_Click;
            UI.StopWorkingButton.Visible = false;

            UI.RefreshLocalFilesButton.Click += RefreshLocalFilesButton_Click;
        }

        private void RefreshLocalFilesButton_Click(object sender, EventArgs e)
        {
            UI.FileViewer.PopulateFiles();
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

        private async void StartWorkingButton_Click(object sender, EventArgs e)
        {
            await new StartWorkingCommand(this).ExecuteAsync();
        }

        private async void SyncRemoteButton_Click(object sender, EventArgs e)
        {
            await new SyncRemoteCommand(this).ExecuteAsync();
        }

        private async void StopWorkingButton_Click(object sender, EventArgs e)
        {
            await new StopWorkingCommand(this).ExecuteAsync();
        }

        private void RemoveEventListeners()
        {
            UI.StartWorkingButton.Click -= StartWorkingButton_Click;
            UI.StopWorkingButton.Click -= StopWorkingButton_Click;
            UI.SyncRemoteButton.Click -= SyncRemoteButton_Click;
            UI.RefreshLocalFilesButton.Click -= RefreshLocalFilesButton_Click;
        }

        #region Dispose pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    RemoveEventListeners();
                    ChangeMonitor.Dispose();
                    UI.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
