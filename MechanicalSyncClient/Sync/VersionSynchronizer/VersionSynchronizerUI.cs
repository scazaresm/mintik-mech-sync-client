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

        public DrawingReviewerControl DrawingReviewer { get; set; }

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

        public TreeView DrawingReviewsTreeView { get; set; }

        public TreeView AssemblyReviewsTreeView { get; set; }

        public DeliverableReviewsTreeView DrawingReviewsExplorer { get; private set; }

        public DeliverableReviewsTreeView AssemblyReviewsExplorer { get; private set; }

        public TabControl VersionSynchronizerTabs { get; set; }

        public SplitContainer DrawingReviewContainer { get; set; }

        public Panel DrawingReviewerPanel { get; set; }

        public ToolStripLabel DrawingReviewerStatusText { get; set; }

        public ToolStripProgressBar DrawingReviewerProgress { get; set; }

        public ToolStripLabel DrawingReviewerDrawingStatus { get; set; }

        public ToolStripLabel DrawingReviewerTitle { get; set; }

        public ToolStripButton MarkDrawingAsFixedButton { get; set; }

        public DataGridView AssemblyChangeRequestGrid { get; set; }

        public ToolStripLabel AssemblyReviewViewerTitle { get; set; }

        public ToolStrip AssemblyReviewViewerToolStrip { get; set; }

        public ToolStripLabel AssemblyReviewStatus { get; set; }

        public ToolStripButton MarkAssemblyAsFixedButton { get; set; }

        public SplitContainer AssemblyReviewsSplit { get; set; }

        public SplitContainer DrawingReviewsSplit { get; set; }

        public ToolStripButton RefreshAssemblyExplorerButton { get; set; }

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
                "*.sldasm | *.sldprt | *.slddrw",
                changeMonitor
            );
        }

        public void InitializeDrawingReviews(LocalVersion version)
        {
            if (DrawingReviewsExplorer != null)
                DrawingReviewsExplorer.Dispose();

            DrawingReviewsExplorer = new DeliverableReviewsTreeView(
               MechSyncServiceClient.Instance,
               version,
               ReviewTargetType.DrawingFile
            );
            DrawingReviewsExplorer.AttachTreeView(DrawingReviewsTreeView);
            SetDefaultDrawingReviewControls();
            DrawingReviewsSplit.Panel2Collapsed = true;
        }

        public void InitializeAssemblyReviews(LocalVersion version)
        {
            if (AssemblyReviewsExplorer != null)
                AssemblyReviewsExplorer.Dispose();

            AssemblyReviewsExplorer = new DeliverableReviewsTreeView(
                MechSyncServiceClient.Instance,
                version,
                ReviewTargetType.AssemblyFile
            );
            AssemblyReviewsExplorer.AttachTreeView(AssemblyReviewsTreeView);
            AssemblyReviewsSplit.Panel2Collapsed = true;
        }

        public void SetDefaultDrawingReviewControls()
        {
            DrawingReviewerProgress.Visible = false;
            DrawingReviewerStatusText.Text = "Select a drawing from the list";
            DrawingReviewerDrawingStatus.Visible = false;
            DrawingReviewerTitle.Visible = false;
            MarkDrawingAsFixedButton.Visible = false;
        }

        private async void VersionSynchronizerTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTabText = VersionSynchronizerTabs.SelectedTab.Text;
            DeliverableReviewsTreeView deliverableExplorer = null;

            if (selectedTabText.StartsWith("3D"))
                deliverableExplorer = AssemblyReviewsExplorer;
            else if (selectedTabText.StartsWith("2D"))
                deliverableExplorer = DrawingReviewsExplorer;

            if (deliverableExplorer != null)
                await deliverableExplorer.Refresh();
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

        private void DisposeDrawingReviews()
        {
            if (DrawingReviewsExplorer != null)
            {
                DrawingReviewsExplorer.Dispose();
                DrawingReviewsExplorer = null;
            }
            if (DrawingReviewer != null)
            {
                DrawingReviewer.Dispose();
                DrawingReviewer = null;
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
                    DisposeDrawingReviews();
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
