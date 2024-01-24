﻿using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Sync.VersionSynchronizer;
using MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions;
using System;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class VersionSynchronizerForm : Form
    {
        private IVersionSynchronizer synchronizer;
        private WorkspaceTreeView workspaceTreeView;
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
            workspaceTreeView = new WorkspaceTreeView(MechSyncServiceClient.Instance, @"C:\Sync");
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
            VersionSynchronizerTabs.TabPages.Remove(tabPage4);
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
               await workspaceTreeView.Refresh();
            }
        }

        private void VersionSynchronizerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var confirmation = MessageBox.Show(
                "Are you sure you want to close the application?", 
                "Close", 
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmation != DialogResult.Yes) e.Cancel = true;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LogoutMenuItem_Click(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show(
                "Are you sure you want to logout?", "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmation != DialogResult.Yes) return;

            Hide();
            foreach (Form form in Application.OpenForms)
            {
                if (form is LoginForm loginForm)
                {
                    loginForm.Show();
                    break;
                }
            }
        }

        private void ManagementConsoleButton_Click(object sender, EventArgs e)
        {
            ManagementConsoleForm.Instance.ShowDialog();
        }
    }
}
