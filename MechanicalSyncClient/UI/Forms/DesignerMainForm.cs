using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Sync.VersionSynchronizer;
using MechanicalSyncApp.Sync.VersionSynchronizer.Commands;
using MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class DesignerMainForm : Form
    {
        private IVersionSynchronizer synchronizer;
        private WorkspaceTreeView workspace;
        private VersionSynchronizerUI synchronizerUI;

        public DesignerMainForm()
        {
            InitializeComponent();
            workspace = new WorkspaceTreeView(MechSyncServiceClient.Instance, @"D:\sync_demo");
            workspace.AttachTreeView(WorkspaceTreeView);
            workspace.OpenVersion += Workspace_OpenVersion;
        }

        private async void Workspace_OpenVersion(object sender, OpenVersionEventArgs e)
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
                    PublishVersionButton = PublishVersionButton,
                    TransferOwnershipButton = TransferOwnershipButton,
                    SyncProgressBar = SyncProgressBar,
                    MainSplitContainer = MainSplitContainer,
                };

                // create a new version synchronizer
                synchronizer = new VersionSynchronizer(version, synchronizerUI);

                try
                {
                    await synchronizer.OpenVersionAsync();
                    ShowVersionExplorer();
                }
                catch (OpenVersionCanceledException)
                {
                    // abort if download cancelled
                    synchronizer.Dispose();
                    synchronizer = null;
                }
                catch (VersionFolderAlreadyExistsException)
                {
                    // abort if folder already exists and user didn't accept moving it to recycle bin
                    synchronizer.Dispose();
                    synchronizer = null;
                }
                catch (VersionOwnershipNotAcknowledgedException)
                {
                    // abort if user did not acknowledge version ownership
                    synchronizer.Dispose();
                    synchronizer = null;
                }
                catch (NotVersionOwnerException)
                {
                    // abort if user is not owner
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
                synchronizer.Dispose();
                synchronizer = null;
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
