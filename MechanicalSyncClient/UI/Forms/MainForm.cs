using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync.VersionSynchronizer;
using MechanicalSyncApp.Sync.VersionSynchronizer.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class MainForm : Form
    {
        private IVersionSynchronizer synchronizer;
        private WorkspaceTreeView workspace;
        private VersionSynchronizerUI synchronizerUI;

        public MainForm()
        {
            InitializeComponent();
            workspace = new WorkspaceTreeView(MechSyncServiceClient.Instance, @"D:\sync_demo");
            workspace.AttachTreeView(WorkspaceTreeView);
            workspace.OpenVersion += Workspace_OpenVersion;
        }

        private void Workspace_OpenVersion(object sender, OpenVersionEventArgs e)
        {
            var version = e.Version;
            if (version is null)
                throw new ArgumentNullException(nameof(version));

            try
            {
                if (synchronizer != null)
                {
                    // need to open another version, dispose the current synchronizer
                    synchronizer.Dispose();
                    synchronizer = null;
                }

                // map Form controls to UI object
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

                // create a new version synchronizer
                synchronizer = new VersionSynchronizer(version, synchronizerUI);

                // show progress in a dialog...
                var openVersionDialog = new OpenVersionDialog(synchronizer);

                if (openVersionDialog.ShowDialog() == DialogResult.OK)
                {
                    ShowVersionExplorer();
                }
                else
                {
                    synchronizer.Dispose();
                    synchronizer = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        $"Failed to open version: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                );
            }
        }

        private void RefreshVersionsButton_Click(object sender, EventArgs e)
        {
            workspace.Refresh();
        }

        private void DemoForm_Load(object sender, EventArgs e)
        {
            MainSplitContainer.Panel2Collapsed = true;
        }

        private void ShowVersionExplorer()
        {
            MainSplitContainer.Panel2Collapsed = false;
            MainSplitContainer.Panel1Collapsed = true;
        }

        private void ShowWorkspace()
        {
            MainSplitContainer.Panel2Collapsed = true;
            MainSplitContainer.Panel1Collapsed = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
