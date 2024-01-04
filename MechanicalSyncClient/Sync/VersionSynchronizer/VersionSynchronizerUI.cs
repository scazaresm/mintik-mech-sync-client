using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.UI;
using System;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer
{
    public class VersionSynchronizerUI : IDisposable
    {
        public FileListViewControl FileViewer { get; private set; }

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

        #endregion

        public void InitializeFileViewer(LocalVersion version, IVersionChangeMonitor changeMonitor)
        {
            if (version is null)
                throw new ArgumentNullException(nameof(version));

            if (changeMonitor is null)
                throw new ArgumentNullException(nameof(changeMonitor));

            DisposeFileViewer();
            FileViewer = new FileListViewControl(
                version.LocalDirectory, 
                "*.sldasm | *.sldprt | *.slddrw",
                changeMonitor
            );
        }

        private void DisposeFileViewer()
        {
            if (FileViewer != null)
            {
                FileViewer.Dispose();
                FileViewer = null;
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
