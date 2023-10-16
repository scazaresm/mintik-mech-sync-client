using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
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

        public OngoingVersion Version { get; }

        public string FileExtensionFilter { get; private set; } = "*.sldasm | *.sldprt | *.slddrw";

        public VersionSynchronizer(OngoingVersion version, VersionSynchronizerUI ui)
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

        public void InitializeUI()
        {
            UI.ProjectFolderNameLabel.Text = Version.RemoteProject.FolderName;
            UI.InitializeFileViewer(Version, ChangeMonitor);
        
            UI.FileViewer.AttachListView(UI.FileViewerListView);
            UI.FileViewerListView.SetDoubleBuffered();

            UI.SyncProgressBar.Visible = false;
            UI.SyncRemoteButton.Visible = true;
            UI.SyncRemoteButton.Click += SyncRemoteButton_Click;

            UI.WorkOnlineButton.Click += WorkOnlineButton_Click;
            UI.WorkOnlineButton.Visible = true;

            UI.WorkOfflineButton.Click += WorkOfflineButton_Click;
            UI.WorkOfflineButton.Visible = false;

            UI.RefreshLocalFilesButton.Click += RefreshLocalFilesButton_Click;
            UI.CloseVersionButton.Click += CloseVersionButton_Click;
        }

        public void UpdateUI()
        {
            if (state != null)
                state.UpdateUI();
        }

        public void SetState(VersionSynchronizerState state)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state));
            this.state.SetSynchronizer(this);
            this.state.UpdateUI();
        }

        public async Task RunStepAsync()
        {
            if (state != null)
                await state.RunAsync();
        }

        public async Task WorkOnlineAsync()
        {
            UI.SynchronizerToolStrip.Enabled = false;
            UI.WorkOnlineButton.Visible = false;
            UI.WorkOfflineButton.Visible = true;
            UI.SyncRemoteButton.Visible = false;

            SetState(new IndexLocalFiles());
            await RunStepAsync();

            SetState(new IndexRemoteFilesState());
            await RunStepAsync();

            var syncCheckState = new SyncCheckState();
            SetState(syncCheckState);
            await RunStepAsync();

            if(syncCheckState.Summary.HasChanges)
            {
                var response = MessageBox.Show("Apply sync changes?", "Validate changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response != DialogResult.Yes)
                {
                    await WorkOfflineAsync();
                    return;
                }
                SetState(new ProcessSyncCheckSummaryState(syncCheckState.Summary));
                await RunStepAsync();
            }

            ChangeMonitor.StartMonitoring();
            UI.FileViewer.PopulateFiles();

            SetState(new HandleFileSyncEventsState());
            _ = RunStepAsync();
        }

        public async Task WorkOfflineAsync()
        {
            UI.SynchronizerToolStrip.Enabled = true;
            UI.WorkOfflineButton.Visible = false;
            UI.WorkOnlineButton.Visible = true;
            UI.SyncRemoteButton.Visible = true;

            ChangeMonitor.StopMonitoring();
            UI.FileViewer.PopulateFiles();

            SetState(new IdleState());
            await RunStepAsync();
        }

        public async Task SynchronizeVersionAsync()
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

            var syncCheckState = new SyncCheckState();
            SetState(syncCheckState);
            await RunStepAsync();

            if (syncCheckState.Summary.HasChanges)
            {
                var response = MessageBox.Show("Apply sync changes?", "Validate changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response != DialogResult.Yes)
                {
                    await WorkOfflineAsync();
                    return;
                }
                SetState(new ProcessSyncCheckSummaryState(syncCheckState.Summary));
                await RunStepAsync();
            }

            SetState(new HandleFileSyncEventsState());
            await RunStepAsync();

            MessageBox.Show(
                "The remote server is now synced with your local copy.", 
                "Successfully synced remote",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            UI.SynchronizerToolStrip.Enabled = true;
        }

        private void WorkOnlineButton_Click(object sender, EventArgs e)
        {
            _ = WorkOnlineAsync();
        }

        private void SyncRemoteButton_Click(object sender, EventArgs e)
        {
            _ = SynchronizeVersionAsync();
        }

        private void WorkOfflineButton_Click(object sender, EventArgs e)
        {
            _ = WorkOfflineAsync();
        }

        private void RefreshLocalFilesButton_Click(object sender, EventArgs e)
        {
            UI.FileViewer.PopulateFiles();
        }

        private async void CloseVersionButton_Click(object sender, EventArgs e)
        {
            if(ChangeMonitor.IsMonitoring())
            {
                var response = MessageBox.Show(
                    "You are currently working online on this version, are you sure to go offline and close it?",
                    "Close version",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (response != DialogResult.Yes)
                    return;

                await WorkOfflineAsync();
            }
            UI.MainSplitContainer.Panel2Collapsed = true;
            UI.MainSplitContainer.Panel1Collapsed = false;
        }

        private void RemoveEventListeners()
        {
            UI.WorkOnlineButton.Click -= WorkOnlineButton_Click;
            UI.WorkOfflineButton.Click -= WorkOfflineButton_Click;
            UI.SyncRemoteButton.Click -= SyncRemoteButton_Click;
            UI.RefreshLocalFilesButton.Click -= RefreshLocalFilesButton_Click;
            UI.CloseVersionButton.Click -= CloseVersionButton_Click;
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
