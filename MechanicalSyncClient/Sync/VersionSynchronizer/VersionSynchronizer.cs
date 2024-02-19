using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync.VersionSynchronizer.Commands;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer
{
    public class VersionSynchronizer : IVersionSynchronizer
    {
        public SyncCheckSummary OnlineWorkSummary { get; set; }

        public IMechSyncServiceClient SyncServiceClient { get; set; }
        public IAuthenticationServiceClient AuthServiceClient { get; set; }

        public IVersionChangeMonitor ChangeMonitor { get; private set; }

        public ConcurrentDictionary<string, FileMetadata> LocalFileIndex { get; set; }
        public ConcurrentDictionary<string, FileMetadata> RemoteFileIndex { get; private set; }

        public VersionSynchronizerUI UI { get; private set; }

        public string SnapshotDirectory { get; private set; } = Path.Combine(Path.GetTempPath(), "sync-snapshot");

        private VersionSynchronizerState state;
        private bool disposedValue;

        public LocalVersion Version { get; }

        public string FileExtensionFilter { get; private set; } = "*.sldasm | *.sldprt | *.slddrw";

        public VersionSynchronizer(LocalVersion version, 
                                   VersionSynchronizerUI ui,
                                   IMechSyncServiceClient syncServiceClient,
                                   IAuthenticationServiceClient authenticationServiceClient)
        {
            Version = version ?? throw new ArgumentNullException(nameof(version));

            UI = ui ?? throw new ArgumentNullException(nameof(ui));
            ChangeMonitor = new VersionChangeMonitor(version, FileExtensionFilter);
            SyncServiceClient = syncServiceClient;
            AuthServiceClient = authenticationServiceClient;

            LocalFileIndex = new ConcurrentDictionary<string, FileMetadata>();
            RemoteFileIndex = new ConcurrentDictionary<string, FileMetadata>();

            SetState(new IdleState());
            _ = RunStepAsync();
        }


        #region State management

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

        public async Task RunStepAsync()
        {
            if (state != null)
                await state.RunAsync();
        }

        #endregion

        #region Commands

        public async Task OpenVersionAsync()
        {
            await new OpenVersionCommand(this).RunAsync();
        }

        public async Task WorkOnlineAsync()
        {
           await new WorkOnlineCommand(this).RunAsync();
        }

        public async Task WorkOfflineAsync()
        {
            await new WorkOfflineCommand(this).RunAsync();
        }

        public async Task SyncRemoteAsync()
        {
            await new SyncRemoteCommand(this).RunAsync();
        }

        public async Task CloseVersionAsync()
        {
            await new CloseVersionCommand(this).RunAsync();
        }

        public async Task PublishVersionAsync()
        {
            await new PublishVersionCommand(this).RunAsync();
        }

        public async Task TransferOwnershipAsync()
        {
            await new TransferOwnershipCommand(this).RunAsync();
        }

        #endregion

        #region UI management

        public void InitializeUI()
        {
            UI.ProjectFolderNameLabel.Text = $"{Version.RemoteProject.FolderName} V{Version.RemoteVersion.Major} (Ongoing changes)";
            UI.InitializeLocalFileViewer(Version, ChangeMonitor);

            UI.LocalFileViewer.AttachListView(UI.FileViewerListView);
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
            UI.PublishVersionButton.Click += PublishVersionButton_Click;
            UI.TransferOwnershipButton.Click += TransferOwnershipButton_Click;

            UI.CopyLocalCopyPathMenuItem.Click += CopyLocalCopyPathMenuItem_Click;
            UI.OpenLocalCopyFolderMenuItem.Click += OpenLocalCopyFolderMenuItem_Click;
        }

     
        public void UpdateUI()
        {
            state?.UpdateUI();
        }

        private async void WorkOnlineButton_Click(object sender, EventArgs e)
        {
            await WorkOnlineAsync();
        }

        private async void SyncRemoteButton_Click(object sender, EventArgs e)
        {
            await SyncRemoteAsync();
        }

        private async void WorkOfflineButton_Click(object sender, EventArgs e)
        {
            await WorkOfflineAsync();
        }

        private void RefreshLocalFilesButton_Click(object sender, EventArgs e)
        {
            UI.LocalFileViewer.PopulateFiles();
        }

        private async void CloseVersionButton_Click(object sender, EventArgs e)
        {
            await CloseVersionAsync();
        }

        private async void PublishVersionButton_Click(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show(
                "Are you sure to publish this version?", 
                "Publish confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question
            );
            if (confirmation != DialogResult.Yes) return;

            await PublishVersionAsync();
        }

        private async void TransferOwnershipButton_Click(object sender, EventArgs e)
        {
            await TransferOwnershipAsync();
        }

        private void CopyLocalCopyPathMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Version.LocalDirectory);

            Point position = UI.CopyLocalCopyPathMenuItem.GetCurrentParent().PointToClient(Cursor.Position);

            new ToolTip().Show(
                "The path has been copied to clipboard.", 
                UI.CopyLocalCopyPathMenuItem.GetCurrentParent(), 
                position.X, position.Y,
                2000
            );
        }

        private void OpenLocalCopyFolderMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Version.LocalDirectory);
        }

        #endregion

        #region Disposing and shutdown

        private void RemoveEventListeners()
        {
            UI.WorkOnlineButton.Click -= WorkOnlineButton_Click;
            UI.WorkOfflineButton.Click -= WorkOfflineButton_Click;
            UI.SyncRemoteButton.Click -= SyncRemoteButton_Click;
            UI.RefreshLocalFilesButton.Click -= RefreshLocalFilesButton_Click;
            UI.CloseVersionButton.Click -= CloseVersionButton_Click;
            UI.PublishVersionButton.Click -= PublishVersionButton_Click;
            UI.TransferOwnershipButton.Click -= TransferOwnershipButton_Click;
            UI.CopyLocalCopyPathMenuItem.Click -= CopyLocalCopyPathMenuItem_Click;
            UI.OpenLocalCopyFolderMenuItem.Click -= OpenLocalCopyFolderMenuItem_Click;
        }

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
