using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync.VersionSynchronizer.Commands;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI;
using MechanicalSyncApp.UI.Forms;
using Serilog;
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

        // Local file metadata indexed by relative file path (linux-based)
        public ConcurrentDictionary<string, FileMetadata> LocalFileIndex { get; set; }

        // Remote file metadata indexed by relative file path (linux-based)
        public ConcurrentDictionary<string, FileMetadata> RemoteFileIndex { get; private set; }

        // File publishings indexed by relative file path (windows-based)
        public ConcurrentDictionary<string, FilePublishing> PublishingIndexByPartNumber { get; private set; }

        public VersionSynchronizerUI UI { get; private set; }

        public string SnapshotDirectory { get; private set; } = Path.Combine(Path.GetTempPath(), "sync-snapshot");

        public string BasePublishingDirectory { get; set; } = @"Z:\MANUFACTURING\";

        public string RelativePublishingSummaryDirectory { get; set; } = @".publishing\pending";


        private VersionSynchronizerState state;
        private bool disposedValue;
        private readonly ILogger logger;

        public LocalVersion Version { get; }

        public Review CurrentDrawingReview { get; set; }

        public ReviewTarget CurrentDrawingReviewTarget { get; set; }

        public string FileExtensionFilter { get; private set; } = "*.sldasm | *.sldprt | *.slddrw";

        public VersionSynchronizer(LocalVersion version, 
                                   VersionSynchronizerUI ui,
                                   IMechSyncServiceClient syncServiceClient,
                                   IAuthenticationServiceClient authenticationServiceClient,
                                   ILogger logger)
        {
            Version = version ?? throw new ArgumentNullException(nameof(version));

            UI = ui ?? throw new ArgumentNullException(nameof(ui));
            ChangeMonitor = new VersionChangeMonitor(version, FileExtensionFilter);
            SyncServiceClient = syncServiceClient;
            AuthServiceClient = authenticationServiceClient;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            LocalFileIndex = new ConcurrentDictionary<string, FileMetadata>();
            RemoteFileIndex = new ConcurrentDictionary<string, FileMetadata>();
            PublishingIndexByPartNumber = new ConcurrentDictionary<string, FilePublishing>();

            CurrentDrawingReview = null;
            CurrentDrawingReviewTarget = null;

            SetState(new SynchronizerIdleState(logger));
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
            else
                throw new InvalidOperationException("Trying to run a step before actually setting the current step.");
        }

        #endregion

        #region Commands

        public async Task ArchiveVersionAsync()
        {
            await new ArchiveVersionCommand(this, logger).RunAsync();
        }

        public async Task OpenVersionAsync()
        {
            await new OpenVersionCommand(this, logger).RunAsync();
        }

        public async Task WorkOnlineAsync()
        {
           await new WorkOnlineCommand(this, logger).RunAsync();
        }

        public async Task WorkOfflineAsync()
        {
            await new WorkOfflineCommand(this, logger).RunAsync();
        }

        public async Task SyncRemoteAsync()
        {
            await new SyncRemoteCommand(this, logger).RunAsync();
        }

        public async Task CloseVersionAsync()
        {
            await new CloseVersionCommand(this, logger).RunAsync();
        }

        public async Task PublishDeliverablesAsync()
        {
            await new PublishDeliverablesCommand(this, logger).RunAsync();
        }

        public async Task TransferOwnershipAsync()
        {
            await new TransferOwnershipCommand(this, logger).RunAsync();
        }

        public async Task OpenDrawingForViewingAsync(OpenDrawingForViewingEventArgs e)
        {
            await new OpenDrawingForViewingCommand(this, e, logger).RunAsync();
        }

        #endregion

        #region UI management

        public void InitializeUI()
        {
            UI.ProjectFolderNameLabel.Text = $"{Version.RemoteProject.FolderName} V{Version.RemoteVersion.Major} (Ongoing changes)";
            UI.InitializeLocalFileViewer(Version, ChangeMonitor);
            UI.InitializeDrawingReviews(Version);
            UI.InitializeAssemblyReviews(Version);

            UI.LocalFileViewer.AttachListView(UI.FileViewerListView);
            UI.FileViewerListView.SetDoubleBuffered();
            UI.SynchronizerToolStrip.Enabled = true;

            UI.SyncProgressBar.Visible = false;
            UI.SyncRemoteButton.Visible = true;
            UI.SyncRemoteButton.Click += SyncRemoteButton_Click;

            UI.WorkOnlineButton.Click += WorkOnlineButton_Click;
            UI.WorkOnlineButton.Visible = true;

            UI.WorkOfflineButton.Click += WorkOfflineButton_Click;
            UI.WorkOfflineButton.Visible = false;

            UI.RefreshLocalFilesButton.Click += RefreshLocalFilesButton_Click;
            UI.CloseVersionButton.Click += CloseVersionButton_Click;
            UI.PublishDeliverablesButton.Click += PublishDeliverablesButton_Click;
            UI.TransferOwnershipButton.Click += TransferOwnershipButton_Click;

            UI.CopyLocalCopyPathMenuItem.Click += CopyLocalCopyPathMenuItem_Click;
            UI.OpenLocalCopyFolderMenuItem.Click += OpenLocalCopyFolderMenuItem_Click;

            UI.VersionSynchronizerTabs.SelectedIndex = 0;
            UI.DrawingReviewsExplorer.OpenReviewForViewing += DrawingReviewsExplorer_OpenDrawingForViewing;

            UI.RefreshDrawingExplorerButton.Click += RefreshDrawingExplorerButton_Click;

            UI.MarkDrawingAsFixedButton.Click += MarkDrawingAsFixedButton_Click;

            UI.ArchiveVersionButton.Click += ArchiveVersionButton_Click;

            UI.ShowVersionExplorer();
        }


        private async void MarkDrawingAsFixedButton_Click(object sender, EventArgs e)
        {
            await new MarkDrawingAsFixedCommand(this).RunAsync();
        }

        private async void DrawingReviewsExplorer_OpenDrawingForViewing(object sender, OpenDrawingForViewingEventArgs e)
        {
            await OpenDrawingForViewingAsync(e);
        }

        public void UpdateUI()
        {
            state?.UpdateUI();
        }

        private async void ArchiveVersionButton_Click(object sender, EventArgs e)
        {
            await ArchiveVersionAsync();
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

        private async void PublishDeliverablesButton_Click(object sender, EventArgs e)
        {
            await PublishDeliverablesAsync();
        }

        private async void TransferOwnershipButton_Click(object sender, EventArgs e)
        {
            await TransferOwnershipAsync();
        }

        private async void RefreshDrawingExplorerButton_Click(object sender, EventArgs e)
        {
            await UI.DrawingReviewsExplorer.Refresh();
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
            UI.PublishDeliverablesButton.Click -= PublishDeliverablesButton_Click;
            UI.TransferOwnershipButton.Click -= TransferOwnershipButton_Click;
            UI.CopyLocalCopyPathMenuItem.Click -= CopyLocalCopyPathMenuItem_Click;
            UI.OpenLocalCopyFolderMenuItem.Click -= OpenLocalCopyFolderMenuItem_Click;
            UI.DrawingReviewsExplorer.OpenReviewForViewing -= DrawingReviewsExplorer_OpenDrawingForViewing;
            UI.RefreshDrawingExplorerButton.Click -= RefreshDrawingExplorerButton_Click;
            UI.MarkDrawingAsFixedButton.Click -= MarkDrawingAsFixedButton_Click;
            UI.ArchiveVersionButton.Click -= ArchiveVersionButton_Click;
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
