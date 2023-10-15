using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.Authentication;
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
            versionExplorer = new VersionExplorerControl(MechSyncServiceClient.Instance, @"C:\sync_demo");
            versionExplorer.AttachTreeView(VersionExplorerTreeView);
            versionExplorer.OpenVersion += VersionExplorer_OpenVersion;
        }

        private void RefreshVersionsButton_Click(object sender, EventArgs e)
        {
            versionExplorer.Refresh();
        }

        private void VersionExplorer_OpenVersion(object sender, OpenVersionEventArgs e)
        {
            var version = e.Version;
            OpenVersionSynchronizer(version);
        }

        private void OpenVersionSynchronizer(OngoingVersion version)
        {
            if (version is null)
            {
                throw new ArgumentNullException(nameof(version));
            }

            if (synchronizer != null)
            {
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
                WorkOnlineButton = WorkOnlineButton,
                WorkOfflineButton = WorkOfflineButton,
                SyncRemoteButton = SyncRemoteButton,
                RefreshLocalFilesButton = RefreshLocalFilesButton,
                CloseVersionButton = CloseVersionButton,
                SyncProgressBar = SyncProgressBar,
                MainSplitContainer = MainSplitContainer,
            };

            // open the version and show the panel with related controls
            synchronizer = new VersionSynchronizer(version, synchronizerUI);
            MainSplitContainer.Panel2Collapsed = false;
            MainSplitContainer.Panel1Collapsed = true;
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
