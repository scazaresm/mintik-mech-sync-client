﻿using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
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

        public void ShowVersionExplorer()
        {
            MainSplitContainer.Panel2Collapsed = false;
            MainSplitContainer.Panel1Collapsed = true;
        }

        public void ShowWorkspaceExplorer()
        {
            MainSplitContainer.Panel2Collapsed = true;
            MainSplitContainer.Panel1Collapsed = false;
        }

        private void DisposeFileViewer()
        {
            if (LocalFileViewer != null)
            {
                LocalFileViewer.Dispose();
                LocalFileViewer = null;
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
