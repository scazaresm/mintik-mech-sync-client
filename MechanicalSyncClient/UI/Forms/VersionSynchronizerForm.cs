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
            MainSplitContainer.Panel2Collapsed = true;

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
                RefreshDrawingExplorerButton = RefreshDrawingExplorerButton,
                CloseVersionButton = CloseVersionButton,
                PublishDeliverablesButton = PublishDeliverablesButton,
                TransferOwnershipButton = TransferOwnershipButton,
                SyncProgressBar = SyncProgressBar,
                MainSplitContainer = MainSplitContainer,
                CopyLocalCopyPathMenuItem = CopyLocalCopyPathMenuItem,
                OpenLocalCopyFolderMenuItem = OpenLocalCopyFolderMenuItem,
                VersionSynchronizerTabs = VersionSynchronizerTabs,
                DrawingReviewsTreeView = DrawingReviewsTreeView,
                AssemblyReviewsTreeView = AssemblyReviewsTreeView,
                DrawingReviewContainer = DrawingReviewContainer,
                DrawingReviewerPanel = DrawingReviewerPanel,
                DrawingReviewerProgress = DrawingReviewerProgress,
                DrawingReviewerStatusText = DrawingReviewerStatusText,
                DrawingReviewerDrawingStatus = DrawingReviewerDrawingStatus,
                DrawingReviewerTitle = DrawingReviewerTitle,
                MarkDrawingAsFixedButton = MarkDrawingAsFixedButton,
                ArchiveVersionButton = ArchiveVersionButton,
                AssemblyChangeRequestGrid = AssemblyChangeRequestsGrid,
                AssemblyReviewViewerToolStrip = AssemblyReviewViewerToolStrip,
                AssemblyReviewStatus = AssemblyReviewStatus,
                MarkAssemblyAsFixedButton = MarkAssemblyAsFixedButton,
                AssemblyReviewViewerTitle = AssemblyReviewViewerTitle,
            };

            // create a new version synchronizer
            synchronizer = new VersionSynchronizer(
                version, 
                synchronizerUI, 
                MechSyncServiceClient.Instance, 
                AuthenticationServiceClient.Instance,
                Log.Logger
            );

            try
            {
                await synchronizer.OpenVersionAsync();
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
                Log.Error($"Could not open version: {ex.Message}");
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

            try
            {
                if (e.Review.RemoteVersion.Status != "Ongoing")
                    throw new InvalidOperationException("This review belongs to a version which is no longer in Ongoing status.");

                var targetType = e.Review.RemoteReview.TargetType;

                switch (targetType)
                {
                    case "DrawingFile":
                        reviewForm = new DrawingReviewerForm(
                            AuthenticationServiceClient.Instance,
                            MechSyncServiceClient.Instance,
                            e.Review,
                            Log.Logger
                        );
                        break;

                    case "AssemblyFile":
                        reviewForm = new AssemblyReviewerForm(
                            AuthenticationServiceClient.Instance,
                            MechSyncServiceClient.Instance,
                            e.Review,
                            Log.Logger
                        );
                        break;

                    default:
                        throw new Exception($"Unexcpected TargetType '{targetType}'");
                }

                if (reviewForm != null)
                {
                    Hide();
                    reviewForm.Show();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Could not open review: {ex.Message}");
                MessageBox.Show(
                    $"Could not open review: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
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

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var configurationForm = new ConnectionSettingsForm(Log.Logger);
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

        private async void NewReviewButton_Click(object sender, EventArgs e)
        {
            var createReviewForm = new CreateReviewForm(Log.Logger);
            var response = createReviewForm.ShowDialog();

            if (response == DialogResult.OK)
            {
                MessageBox.Show(
                    "The new review has been created!",
                    "Review created",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                await workspaceTreeView.Refresh();
            }
        }
    }
}
