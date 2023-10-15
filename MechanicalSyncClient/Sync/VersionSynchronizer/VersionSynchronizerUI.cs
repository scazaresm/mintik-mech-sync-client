using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer
{
    public class VersionSynchronizerUI : IDisposable
    {
        private bool disposedValue;

        public FileListViewControl FileViewer { get; private set; }

        // Form components
        public Label ProjectFolderNameLabel { get; set; }
        public ListView FileViewerListView { get; set; }
        public ToolStrip SynchronizerToolStrip { get; set; }
        public ToolStripStatusLabel StatusLabel { get; set; }
        public ToolStripButton RefreshLocalFilesButton { get; set; }
        public ToolStripButton StartWorkingButton { get; set; }
        public ToolStripButton StopWorkingButton { get; set; }
        public ToolStripButton SyncRemoteButton { get; set; }
        public ToolStripProgressBar SyncProgressBar { get; set; }

        public void InitializeFileViewer(LocalVersion version)
        {
            if (version is null)
            {
                throw new ArgumentNullException(nameof(version));
            }
            DisposeFileViewer();
            FileViewer = new FileListViewControl(version.LocalDirectory);
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
