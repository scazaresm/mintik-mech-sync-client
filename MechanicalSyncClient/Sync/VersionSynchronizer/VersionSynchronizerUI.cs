using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.UI;
using System;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer
{
    public class VersionSynchronizerUI : IDisposable
    {
        public LocalFileListViewControl LocalFileViewer { get; private set; }

        #region Form Components

        public Label ProjectFolderNameLabel { get; set; }
        public ListView FileViewerListView { get; set; }
        public ToolStrip SynchronizerToolStrip { get; set; }
        public ToolStripStatusLabel StatusLabel { get; set; }
        public ToolStripButton RefreshLocalFilesButton { get; set; }
        public ToolStripButton WorkOnlineButton { get; set; }
        public ToolStripButton WorkOfflineButton { get; set; }
        public ToolStripButton SyncRemoteButton { get; set; }
        public ToolStripButton CloseVersionButton { get; set; }
        public ToolStripButton PublishVersionButton { get; set; }
        public ToolStripButton TransferOwnershipButton { get; set; }

        public ToolStripProgressBar SyncProgressBar { get; set; }
        public SplitContainer MainSplitContainer { get; set; }

        public ToolStripMenuItem CopyLocalCopyPathMenuItem { get; set; }

        public ToolStripMenuItem OpenLocalCopyFolderMenuItem { get; set; }


        public WorkspaceTreeView WorkspaceTreeView { get; set; }


        public TreeView DrawingReviewsTreeView { get; set; }

        public DrawingReviewsTreeView DrawingReviewsExplorer { get; private set; }

        public TabControl VersionSynchronizerTabs { get; set; }

        #endregion


        public void InitializeLocalFileViewer(LocalVersion version, IVersionChangeMonitor changeMonitor)
        {
            if (version is null)
                throw new ArgumentNullException(nameof(version));

            if (changeMonitor is null)
                throw new ArgumentNullException(nameof(changeMonitor));

            DisposeFileViewer();
            LocalFileViewer = new LocalFileListViewControl(
                version.LocalDirectory, 
                "*.sldasm | *.sldprt | *.slddrw",
                changeMonitor
            );
        }

        public void InitializeDrawingReviews(LocalVersion version)
        {
            if (DrawingReviewsExplorer != null)
                DrawingReviewsExplorer.Dispose();

            DrawingReviewsExplorer = new DrawingReviewsTreeView(
               MechSyncServiceClient.Instance,
               AuthenticationServiceClient.Instance,
               version
            );
            DrawingReviewsExplorer.AttachTreeView(DrawingReviewsTreeView);
            DrawingReviewsExplorer.OpenDrawingForViewing += DrawingReviewsExplorer_OpenDrawingForViewing;
        }

        private void DrawingReviewsExplorer_OpenDrawingForViewing(object sender, OpenDrawingForViewingEventArgs e)
        {
            Console.WriteLine(e.Review.Id);
        }
        
        private async void VersionSynchronizerTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VersionSynchronizerTabs.SelectedTab.Text.StartsWith("2D") && DrawingReviewsExplorer != null)
            {
                await DrawingReviewsExplorer.Refresh();
                return;
            }
        }

        public void ShowVersionExplorer()
        {
            MainSplitContainer.Panel2Collapsed = false;
            MainSplitContainer.Panel1Collapsed = true;
            VersionSynchronizerTabs.SelectedIndexChanged += VersionSynchronizerTabs_SelectedIndexChanged;
        }

        public void ShowWorkspaceExplorer()
        {
            MainSplitContainer.Panel2Collapsed = true;
            MainSplitContainer.Panel1Collapsed = false;
            VersionSynchronizerTabs.SelectedIndexChanged -= VersionSynchronizerTabs_SelectedIndexChanged;
        }

        private void DisposeFileViewer()
        {
            if (LocalFileViewer != null)
            {
                LocalFileViewer.Dispose();
                LocalFileViewer = null;
            }
        }

        private void DisposeDrawingReviewsExplorer()
        {
            if (DrawingReviewsExplorer != null)
            {
                DrawingReviewsExplorer.Dispose();
                DrawingReviewsExplorer = null;
            }
        }

        #region Dispose pattern

        private bool disposedValue;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeFileViewer();
                    DisposeDrawingReviewsExplorer();
                    VersionSynchronizerTabs.SelectedIndexChanged -= VersionSynchronizerTabs_SelectedIndexChanged;
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
