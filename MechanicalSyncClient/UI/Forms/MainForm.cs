using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Sync.VersionSynchronizer;
using System;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class MainForm : Form
    {
        private IVersionSynchronizer synchronizer;
        private VersionExplorerControl versionExplorer;
        private VersionSynchronizerUI synchronizerUI;

        public MainForm()
        {
            InitializeComponent();
            versionExplorer = new VersionExplorerControl(MechSyncServiceClient.Instance);
            versionExplorer.AttachTreeView(VersionExplorerTreeView);
            versionExplorer.OpenVersion += VersionExplorer_OpenVersion;
        }

        private void RefreshVersionsButton_Click(object sender, EventArgs e)
        {
            versionExplorer.Refresh();
        }

        private void VersionExplorer_OpenVersion(object sender, OpenVersionEventArgs e)
        {
            if (synchronizer != null)
            {
                // version is already open, do nothing
                if (synchronizer.Version.RemoteVersion.Id == e.Version.RemoteVersion.Id)
                    return;

                // need to open another version, dispose the one which is currently open
                synchronizer.Dispose();
                synchronizer = null;
            }
            synchronizerUI = new VersionSynchronizerUI()
            {
                ProjectFolderNameLabel = ProjectFolderNameLabel,
                SynchronizerToolStrip = SynchronizerToolStrip,
                FileViewerListView = FileViewerListView,
                StatusLabel = SyncStatusLabel,
                StartWorkingButton = StartWorkingButton,
                StopWorkingButton = StopWorkingButton,
                SyncRemoteButton = SyncRemoteButton,
                RefreshLocalFilesButton = RefreshLocalFilesButton,
                SyncProgressBar = SyncProgressBar,
            };

            // open the version and show the panel with controls
            synchronizer = new VersionSynchronizer(e.Version, synchronizerUI);
            MainSplitContainer.Panel2Collapsed = false;
        }

        private void DemoForm_Load(object sender, EventArgs e)
        {
            MainSplitContainer.Panel2Collapsed = true;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
