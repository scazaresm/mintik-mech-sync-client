using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer
{
    public class VersionSynchronizerUI : IDisposable
    {
        public LocalFileListView LocalFileViewer { get; private set; }


        #region Form Components

        public Label ProjectFolderNameLabel { get; set; }
        public ListView FileViewerListView { get; set; }
        public ToolStrip SynchronizerToolStrip { get; set; }
        public ToolStripStatusLabel StatusLabel { get; set; }
        public ToolStripButton RefreshLocalFilesButton { get; set; }
        public ToolStripButton WorkOnlineButton { get; set; }
        public ToolStripButton WorkOfflineButton { get; set; }
        public ToolStripButton SyncRemoteButton { get; set; }
        public ToolStripButton RefreshDrawingExplorerButton { get; set; }
        public Button CloseVersionButton { get; set; }
        public ToolStripButton PublishDeliverablesButton { get; set; }
        public ToolStripButton TransferOwnershipButton { get; set; }
        public ToolStripButton ArchiveVersionButton { get; set; }

        public ToolStripProgressBar SyncProgressBar { get; set; }
        public SplitContainer MainSplitContainer { get; set; }

        public ToolStripMenuItem CopyLocalCopyPathMenuItem { get; set; }

        public ToolStripMenuItem OpenLocalCopyFolderMenuItem { get; set; }


        public WorkspaceTreeView WorkspaceTreeView { get; set; }

        public TreeView FileReviewsTreeView { get; set; }

        public DeliverableReviewsTreeView ReviewsExplorer { get; private set; }

        public TabControl VersionSynchronizerTabs { get; set; }

    
        public DataGridView FileChangeRequestGrid { get; set; }

        public ToolStripLabel FileReviewViewerTitle { get; set; }

        public ToolStrip FileReviewsToolStrip { get; set; }

        public ToolStripLabel FileReviewStatus { get; set; }

        public ToolStripButton MarkFileAsFixedButton { get; set; }

        public SplitContainer FileReviewsSplit { get; set; }

        public ToolStripButton RefreshFileReviewExplorerButton { get; set; }

        public ToolStripMenuItem VersionMenu { get; set; }

        #endregion


        public void InitializeLocalFileViewer(LocalVersion version, IVersionChangeMonitor changeMonitor)
        {
            if (version is null)
                throw new ArgumentNullException(nameof(version));

            if (changeMonitor is null)
                throw new ArgumentNullException(nameof(changeMonitor));

            DisposeFileViewer();
            LocalFileViewer = new LocalFileListView(
                version.LocalDirectory, 
                "", //"*.sldasm | *.sldprt | *.slddrw",
                changeMonitor
            );
        }

        public void InitializeFileReviews(LocalVersion version)
        {
            if (ReviewsExplorer != null)
                ReviewsExplorer.Dispose();

            ReviewsExplorer = new DeliverableReviewsTreeView(
                MechSyncServiceClient.Instance,
                version,
                ReviewTargetType.AssemblyFile
            );
            ReviewsExplorer.AttachTreeView(FileReviewsTreeView);
            FileReviewsSplit.Panel2Collapsed = true;
        }

        private async void VersionSynchronizerTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTabText = VersionSynchronizerTabs.SelectedTab.Text;

            if (selectedTabText == "Reviews") 
            {
                await ReviewsExplorer.Refresh();
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

        public void SetDeliverableStatusText(ToolStripLabel label, string status)
        {
            label.Visible = true;
            label.Text = status;

            label.BackgroundImageLayout = ImageLayout.Stretch;
            label.BackgroundImage = new Bitmap(1, 1);
            var g = Graphics.FromImage(label.BackgroundImage);

            switch (status)
            {
                case "Pending":
                default:
                    g.Clear(Color.Black);
                    label.ForeColor = Color.White;
                    break;

                case "Reviewing":
                    g.Clear(Color.Black);
                    label.ForeColor = Color.Yellow;
                    break;

                case "Approved":
                    g.Clear(Color.DarkGreen);
                    label.ForeColor = Color.White;
                    break;

                case "Rejected":
                    g.Clear(Color.DarkRed);
                    label.ForeColor = Color.White;
                    break;

                case "Fixed":
                    g.Clear(Color.Black);
                    label.ForeColor = Color.Aqua;
                    break;
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
