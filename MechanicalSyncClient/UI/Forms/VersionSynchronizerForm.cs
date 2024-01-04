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
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class VersionSynchronizerForm : Form
    {
        private IVersionSynchronizer synchronizer;
        private WorkspaceTreeView workspace;
        private VersionSynchronizerUI synchronizerUI;

        #region Singleton
        private static Form _instance = null;

        public static Form Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VersionSynchronizerForm();
                return _instance;
            }
        }

        private VersionSynchronizerForm()
        {
            InitializeComponent();
            workspace = new WorkspaceTreeView(MechSyncServiceClient.Instance, @"D:\sync_demo");
            workspace.AttachTreeView(WorkspaceTreeView);
            workspace.OpenVersion += Workspace_OpenVersion;
            workspace.OpenReview += Workspace_OpenReview;
        }
        #endregion

        private void VersionSynchronizerForm_Load(object sender, EventArgs e)
        {
            ShowWorkspaceExplorer();
        }

        private void VersionSynchronizerForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible) RefreshWorkspaceButton_Click(sender, e);
        }

        private void VersionSynchronizerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private async void Workspace_OpenVersion(object sender, OpenVersionEventArgs e)
        {
            var version = e.Version;
            if (version is null)
                throw new ArgumentNullException(nameof(version));

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
            synchronizer = new VersionSynchronizer(
                version, 
                synchronizerUI, 
                MechSyncServiceClient.Instance, 
                AuthenticationServiceClient.Instance
            );

            try
            {
                await synchronizer.OpenVersionAsync();
                ShowVersionExplorer();
            }
            catch (OpenVersionCanceledException)
            {
                // abort if canceled
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
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not open version: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                synchronizer.Dispose();
                synchronizer = null;
            }
        }

        private void Workspace_OpenReview(object sender, OpenReviewEventArgs e)
        {
            Form reviewForm = null;

            switch (e.Review.RemoteReview.TargetType)
            {
                case "DrawingFile": 
                    reviewForm = new DrawingReviewerForm(MechSyncServiceClient.Instance, e.Review); break;
                default: break;
            }

            if(reviewForm != null)
            {
                Hide();
                reviewForm.Show();
            }
        }

        private async void RefreshWorkspaceButton_Click(object sender, EventArgs e)
        {
            try
            {
                await workspace.Refresh();
            } 
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private void ShowVersionExplorer()
        {
            MainSplitContainer.Panel2Collapsed = false;
            MainSplitContainer.Panel1Collapsed = true;
        }

        private void ShowWorkspaceExplorer()
        {
            MainSplitContainer.Panel2Collapsed = true;
            MainSplitContainer.Panel1Collapsed = false;
        }

    }
}
