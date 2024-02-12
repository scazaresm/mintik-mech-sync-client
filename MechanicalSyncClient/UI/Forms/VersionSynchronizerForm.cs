using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Sync.VersionSynchronizer;
using MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions;
using Serilog;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class VersionSynchronizerForm : Form
    {
        private IVersionSynchronizer synchronizer;
        private WorkspaceTreeView workspaceTreeView;
        private DrawingReviewsTreeView drawingReviewsTreeView;
        private VersionSynchronizerUI synchronizerUI;

        private bool isClosingDueToFatalException = false;
        private bool isClosingDueToLogout = false;

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

            var workspaceDirectory = ConfigurationManager.AppSettings["WORKSPACE_DIRECTORY"] ?? @"C:\Sync";

            workspaceTreeView = new WorkspaceTreeView(MechSyncServiceClient.Instance, workspaceDirectory);
            workspaceTreeView.AttachTreeView(WorkspaceTreeView);
            workspaceTreeView.OpenVersion += Workspace_OpenVersion;
            workspaceTreeView.OpenReview += Workspace_OpenReview;
        }
        #endregion

        private void VersionSynchronizerForm_Load(object sender, EventArgs e)
        {
            ShowWorkspaceExplorer();
            VersionSynchronizerTabs.TabPages.Remove(tabPage2);
            VersionSynchronizerTabs.TabPages.Remove(tabPage3);
            VersionSynchronizerTabs.TabPages.Remove(DrawingReviewPage);

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            isClosingDueToFatalException = true;
            Log.Error($"Unhandled exception: {e?.Exception}");
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            isClosingDueToFatalException = true;
            Log.Error($"Unhandled exception: {e?.ExceptionObject}");
        }

        private void VersionSynchronizerForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible) RefreshWorkspaceButton_Click(sender, e);
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
                WorkspaceTreeView = workspaceTreeView,
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
                CopyLocalCopyPathMenuItem = CopyLocalCopyPathMenuItem,
                OpenLocalCopyFolderMenuItem = OpenLocalCopyFolderMenuItem
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

            if (drawingReviewsTreeView != null)
            {
                drawingReviewsTreeView.Dispose();
            }

            drawingReviewsTreeView = new DrawingReviewsTreeView(
               MechSyncServiceClient.Instance,
               AuthenticationServiceClient.Instance,
               version
            );
            drawingReviewsTreeView.AttachTreeView(DrawingReviewsTreeView);
        }

        private void Workspace_OpenReview(object sender, OpenReviewEventArgs e)
        {
            Form reviewForm = null;

            switch (e.Review.RemoteReview.TargetType)
            {
                case "DrawingFile": 
                    reviewForm = new DrawingReviewerForm(
                        AuthenticationServiceClient.Instance, 
                        MechSyncServiceClient.Instance, 
                        e.Review
                    ); 
                    break;
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
                await workspaceTreeView.Refresh();
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

        private async void NewProjectButton_Click(object sender, EventArgs e)
        {
            CreateProjectForm createProjectForm = new CreateProjectForm();
            var response = createProjectForm.ShowDialog();

            if (response == DialogResult.OK)
            {
                MessageBox.Show(
                    "The new project has been created and the initial version owner will be notified by email soon!",
                    "Project created",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                await workspaceTreeView.Refresh();
            }
        }

        private async void NewVersionButton_Click(object sender, EventArgs e)
        {
            CreateVersionForm createVersionForm = new CreateVersionForm();
            var response = createVersionForm.ShowDialog();

            if (response == DialogResult.OK)
            {
                MessageBox.Show(
                   "The new version has been created and the owner will be notified by email soon!",
                   "Version created",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information
               );
               await Task.Delay(5000);
               await workspaceTreeView.Refresh();
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void VersionSynchronizerTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VersionSynchronizerTabs.SelectedTab.Text.StartsWith("2D") && drawingReviewsTreeView != null)
            {
                await drawingReviewsTreeView.Refresh();
                return;
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var configurationForm = new ConnectionSettingsForm();
            configurationForm.ShowDialog();
        }

        private void LogoutMenuItem_Click(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show("Are you sure to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes) return;

            isClosingDueToLogout = true;

            Application.Restart();
            Environment.Exit(0);
        }

        private void VersionSynchronizerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClosingDueToFatalException && !isClosingDueToLogout)
            {
                var confirmation = MessageBox.Show(
                    "Are you sure to close the application?",
                    "Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmation != DialogResult.Yes)
                    e.Cancel = true;
            }
        }

        private void VersionSynchronizerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ProjectExplorerMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new ProjectExplorerForm().Show();
        }
    }
}
