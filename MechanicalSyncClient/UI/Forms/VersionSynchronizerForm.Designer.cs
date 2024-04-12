
namespace MechanicalSyncApp.UI.Forms
{
    partial class VersionSynchronizerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionSynchronizerForm));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Assemblies", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Parts", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Drawings", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Example",
            "Deleted",
            "other"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Other",
            "Created"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.FileSyncStatusIcons = new System.Windows.Forms.ImageList(this.components);
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.WorkspaceTreeView = new System.Windows.Forms.TreeView();
            this.WorkspaceIcons = new System.Windows.Forms.ImageList(this.components);
            this.WorkspaceToolStrip = new System.Windows.Forms.ToolStrip();
            this.RefreshVersionsButton = new System.Windows.Forms.ToolStripButton();
            this.VersionSynchronizerTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.FileViewerListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DesignFilesStatusStrip = new System.Windows.Forms.StatusStrip();
            this.SyncStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SyncProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.SynchronizerToolStrip = new System.Windows.Forms.ToolStrip();
            this.RefreshLocalFilesButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.CopyLocalCopyPathMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenLocalCopyFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SyncRemoteButton = new System.Windows.Forms.ToolStripButton();
            this.WorkOfflineButton = new System.Windows.Forms.ToolStripButton();
            this.WorkOnlineButton = new System.Windows.Forms.ToolStripButton();
            this.TransferOwnershipButton = new System.Windows.Forms.ToolStripButton();
            this.PublishDeliverablesButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ArchiveVersionButton = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.FileReviewsSplit = new System.Windows.Forms.SplitContainer();
            this.FileReviewsTreeView = new System.Windows.Forms.TreeView();
            this.DrawingReviewExplorerIcons = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.RefreshAssemblyExplorerButton = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FileChangeRequestsGrid = new System.Windows.Forms.DataGridView();
            this.FileReviewsToolStrip = new System.Windows.Forms.ToolStrip();
            this.MarkFileAsFixedButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.FileReviewStatus = new System.Windows.Forms.ToolStripLabel();
            this.FileReviewViewerTitle = new System.Windows.Forms.ToolStripLabel();
            this.ProjectTabImages = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.CloseVersionButton = new System.Windows.Forms.Button();
            this.ProjectFolderNameLabel = new System.Windows.Forms.Label();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileNewButton = new System.Windows.Forms.ToolStripMenuItem();
            this.NewProjectButton = new System.Windows.Forms.ToolStripMenuItem();
            this.NewVersionButton = new System.Windows.Forms.ToolStripMenuItem();
            this.NewReviewButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.LogoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMechanicalSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeRequestDescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeRequestStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DesignerCommentsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.WorkspaceToolStrip.SuspendLayout();
            this.VersionSynchronizerTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.DesignFilesStatusStrip.SuspendLayout();
            this.SynchronizerToolStrip.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FileReviewsSplit)).BeginInit();
            this.FileReviewsSplit.Panel1.SuspendLayout();
            this.FileReviewsSplit.Panel2.SuspendLayout();
            this.FileReviewsSplit.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FileChangeRequestsGrid)).BeginInit();
            this.FileReviewsToolStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileSyncStatusIcons
            // 
            this.FileSyncStatusIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FileSyncStatusIcons.ImageStream")));
            this.FileSyncStatusIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.FileSyncStatusIcons.Images.SetKeyName(0, "ok-icon-32.png");
            this.FileSyncStatusIcons.Images.SetKeyName(1, "sync-icon-32.png");
            this.FileSyncStatusIcons.Images.SetKeyName(2, "error-icon-32.png");
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.WorkspaceTreeView);
            this.MainSplitContainer.Panel1.Controls.Add(this.WorkspaceToolStrip);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.VersionSynchronizerTabs);
            this.MainSplitContainer.Panel2.Controls.Add(this.panel2);
            this.MainSplitContainer.Size = new System.Drawing.Size(1385, 730);
            this.MainSplitContainer.SplitterDistance = 296;
            this.MainSplitContainer.TabIndex = 16;
            // 
            // WorkspaceTreeView
            // 
            this.WorkspaceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorkspaceTreeView.ImageIndex = 0;
            this.WorkspaceTreeView.ImageList = this.WorkspaceIcons;
            this.WorkspaceTreeView.Location = new System.Drawing.Point(0, 25);
            this.WorkspaceTreeView.Name = "WorkspaceTreeView";
            this.WorkspaceTreeView.SelectedImageIndex = 0;
            this.WorkspaceTreeView.Size = new System.Drawing.Size(296, 705);
            this.WorkspaceTreeView.TabIndex = 0;
            // 
            // WorkspaceIcons
            // 
            this.WorkspaceIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("WorkspaceIcons.ImageStream")));
            this.WorkspaceIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.WorkspaceIcons.Images.SetKeyName(0, "suitcase=icon-24.png");
            this.WorkspaceIcons.Images.SetKeyName(1, "folder-icon-24.png");
            this.WorkspaceIcons.Images.SetKeyName(2, "zoom-search-24.png");
            // 
            // WorkspaceToolStrip
            // 
            this.WorkspaceToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshVersionsButton});
            this.WorkspaceToolStrip.Location = new System.Drawing.Point(0, 0);
            this.WorkspaceToolStrip.Name = "WorkspaceToolStrip";
            this.WorkspaceToolStrip.Size = new System.Drawing.Size(296, 25);
            this.WorkspaceToolStrip.TabIndex = 1;
            this.WorkspaceToolStrip.Text = "toolStrip3";
            // 
            // RefreshVersionsButton
            // 
            this.RefreshVersionsButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RefreshVersionsButton.Image = global::MechanicalSyncApp.Properties.Resources.refresh_icon_24;
            this.RefreshVersionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshVersionsButton.Name = "RefreshVersionsButton";
            this.RefreshVersionsButton.Size = new System.Drawing.Size(66, 22);
            this.RefreshVersionsButton.Text = "Refresh";
            this.RefreshVersionsButton.Click += new System.EventHandler(this.RefreshWorkspaceButton_Click);
            // 
            // VersionSynchronizerTabs
            // 
            this.VersionSynchronizerTabs.Controls.Add(this.tabPage1);
            this.VersionSynchronizerTabs.Controls.Add(this.tabPage3);
            this.VersionSynchronizerTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionSynchronizerTabs.ImageList = this.ProjectTabImages;
            this.VersionSynchronizerTabs.Location = new System.Drawing.Point(0, 25);
            this.VersionSynchronizerTabs.Name = "VersionSynchronizerTabs";
            this.VersionSynchronizerTabs.SelectedIndex = 0;
            this.VersionSynchronizerTabs.Size = new System.Drawing.Size(1085, 705);
            this.VersionSynchronizerTabs.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.FileViewerListView);
            this.tabPage1.Controls.Add(this.DesignFilesStatusStrip);
            this.tabPage1.Controls.Add(this.SynchronizerToolStrip);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1077, 670);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Design files";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // FileViewerListView
            // 
            this.FileViewerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.FileViewerListView.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Assemblies";
            listViewGroup1.Name = "assembliesGroup";
            listViewGroup2.Header = "Parts";
            listViewGroup2.Name = "partsGroup";
            listViewGroup3.Header = "Drawings";
            listViewGroup3.Name = "drawingsGroup";
            this.FileViewerListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.FileViewerListView.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.FileViewerListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.FileViewerListView.Location = new System.Drawing.Point(3, 28);
            this.FileViewerListView.MultiSelect = false;
            this.FileViewerListView.Name = "FileViewerListView";
            this.FileViewerListView.Size = new System.Drawing.Size(1071, 617);
            this.FileViewerListView.StateImageList = this.FileSyncStatusIcons;
            this.FileViewerListView.TabIndex = 16;
            this.FileViewerListView.TileSize = new System.Drawing.Size(150, 30);
            this.FileViewerListView.UseCompatibleStateImageBehavior = false;
            this.FileViewerListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Local files";
            this.columnHeader1.Width = 878;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Sync status";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 150;
            // 
            // DesignFilesStatusStrip
            // 
            this.DesignFilesStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SyncStatusLabel,
            this.SyncProgressBar});
            this.DesignFilesStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.DesignFilesStatusStrip.Location = new System.Drawing.Point(3, 645);
            this.DesignFilesStatusStrip.Name = "DesignFilesStatusStrip";
            this.DesignFilesStatusStrip.Size = new System.Drawing.Size(1071, 22);
            this.DesignFilesStatusStrip.TabIndex = 17;
            this.DesignFilesStatusStrip.Text = "statusStrip1";
            // 
            // SyncStatusLabel
            // 
            this.SyncStatusLabel.AutoSize = false;
            this.SyncStatusLabel.Name = "SyncStatusLabel";
            this.SyncStatusLabel.Size = new System.Drawing.Size(700, 17);
            this.SyncStatusLabel.Text = "Status";
            this.SyncStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SyncProgressBar
            // 
            this.SyncProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.SyncProgressBar.Name = "SyncProgressBar";
            this.SyncProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // SynchronizerToolStrip
            // 
            this.SynchronizerToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshLocalFilesButton,
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.SyncRemoteButton,
            this.WorkOfflineButton,
            this.WorkOnlineButton,
            this.TransferOwnershipButton,
            this.PublishDeliverablesButton,
            this.toolStripSeparator4,
            this.ArchiveVersionButton});
            this.SynchronizerToolStrip.Location = new System.Drawing.Point(3, 3);
            this.SynchronizerToolStrip.Name = "SynchronizerToolStrip";
            this.SynchronizerToolStrip.Size = new System.Drawing.Size(1071, 25);
            this.SynchronizerToolStrip.TabIndex = 15;
            this.SynchronizerToolStrip.Text = "toolStrip2";
            // 
            // RefreshLocalFilesButton
            // 
            this.RefreshLocalFilesButton.Image = global::MechanicalSyncApp.Properties.Resources.refresh_icon_24;
            this.RefreshLocalFilesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshLocalFilesButton.Name = "RefreshLocalFilesButton";
            this.RefreshLocalFilesButton.Size = new System.Drawing.Size(66, 22);
            this.RefreshLocalFilesButton.Text = "Refresh";
            this.RefreshLocalFilesButton.ToolTipText = "Refresh local files";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyLocalCopyPathMenuItem,
            this.OpenLocalCopyFolderMenuItem});
            this.toolStripDropDownButton1.Image = global::MechanicalSyncApp.Properties.Resources.open_folder_24;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(93, 22);
            this.toolStripDropDownButton1.Text = "Local copy";
            // 
            // CopyLocalCopyPathMenuItem
            // 
            this.CopyLocalCopyPathMenuItem.Name = "CopyLocalCopyPathMenuItem";
            this.CopyLocalCopyPathMenuItem.Size = new System.Drawing.Size(214, 22);
            this.CopyLocalCopyPathMenuItem.Text = "Copy path to clipboard";
            // 
            // OpenLocalCopyFolderMenuItem
            // 
            this.OpenLocalCopyFolderMenuItem.Name = "OpenLocalCopyFolderMenuItem";
            this.OpenLocalCopyFolderMenuItem.Size = new System.Drawing.Size(214, 22);
            this.OpenLocalCopyFolderMenuItem.Text = "Open in Windows Explorer";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // SyncRemoteButton
            // 
            this.SyncRemoteButton.Image = global::MechanicalSyncApp.Properties.Resources.sync_24;
            this.SyncRemoteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SyncRemoteButton.Name = "SyncRemoteButton";
            this.SyncRemoteButton.Size = new System.Drawing.Size(93, 22);
            this.SyncRemoteButton.Text = "Sync remote";
            this.SyncRemoteButton.ToolTipText = "Send your local changes to remote server";
            // 
            // WorkOfflineButton
            // 
            this.WorkOfflineButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.WorkOfflineButton.Image = global::MechanicalSyncApp.Properties.Resources.stop_32;
            this.WorkOfflineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WorkOfflineButton.Name = "WorkOfflineButton";
            this.WorkOfflineButton.Size = new System.Drawing.Size(92, 22);
            this.WorkOfflineButton.Text = "Work offline";
            this.WorkOfflineButton.ToolTipText = "Stop monitoring your changes";
            // 
            // WorkOnlineButton
            // 
            this.WorkOnlineButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.WorkOnlineButton.Image = global::MechanicalSyncApp.Properties.Resources.start_32;
            this.WorkOnlineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WorkOnlineButton.Name = "WorkOnlineButton";
            this.WorkOnlineButton.Size = new System.Drawing.Size(91, 22);
            this.WorkOnlineButton.Text = "Work online";
            this.WorkOnlineButton.ToolTipText = "Start monitoring your changes for automatic sync";
            // 
            // TransferOwnershipButton
            // 
            this.TransferOwnershipButton.Image = global::MechanicalSyncApp.Properties.Resources.users_change_icon_24;
            this.TransferOwnershipButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TransferOwnershipButton.Name = "TransferOwnershipButton";
            this.TransferOwnershipButton.Size = new System.Drawing.Size(126, 22);
            this.TransferOwnershipButton.Text = "Transfer ownership";
            this.TransferOwnershipButton.ToolTipText = "Publish revision changes";
            // 
            // PublishDeliverablesButton
            // 
            this.PublishDeliverablesButton.Image = global::MechanicalSyncApp.Properties.Resources.paper_plane_24;
            this.PublishDeliverablesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PublishDeliverablesButton.Name = "PublishDeliverablesButton";
            this.PublishDeliverablesButton.Size = new System.Drawing.Size(131, 22);
            this.PublishDeliverablesButton.Text = "Publish deliverables";
            this.PublishDeliverablesButton.ToolTipText = "Publish deliverables for manufacturing";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // ArchiveVersionButton
            // 
            this.ArchiveVersionButton.Image = global::MechanicalSyncApp.Properties.Resources.archive_24;
            this.ArchiveVersionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ArchiveVersionButton.Name = "ArchiveVersionButton";
            this.ArchiveVersionButton.Size = new System.Drawing.Size(105, 22);
            this.ArchiveVersionButton.Text = "Archive design";
            this.ArchiveVersionButton.ToolTipText = "Archive design in server";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.FileReviewsSplit);
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 31);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1077, 670);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Reviews";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // FileReviewsSplit
            // 
            this.FileReviewsSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileReviewsSplit.Location = new System.Drawing.Point(0, 0);
            this.FileReviewsSplit.Name = "FileReviewsSplit";
            // 
            // FileReviewsSplit.Panel1
            // 
            this.FileReviewsSplit.Panel1.Controls.Add(this.FileReviewsTreeView);
            this.FileReviewsSplit.Panel1.Controls.Add(this.toolStrip1);
            // 
            // FileReviewsSplit.Panel2
            // 
            this.FileReviewsSplit.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.FileReviewsSplit.Panel2.Controls.Add(this.panel1);
            this.FileReviewsSplit.Panel2.Controls.Add(this.FileReviewsToolStrip);
            this.FileReviewsSplit.Size = new System.Drawing.Size(1077, 670);
            this.FileReviewsSplit.SplitterDistance = 236;
            this.FileReviewsSplit.TabIndex = 1;
            // 
            // FileReviewsTreeView
            // 
            this.FileReviewsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileReviewsTreeView.ImageIndex = 0;
            this.FileReviewsTreeView.ImageList = this.DrawingReviewExplorerIcons;
            this.FileReviewsTreeView.Location = new System.Drawing.Point(0, 25);
            this.FileReviewsTreeView.Name = "FileReviewsTreeView";
            this.FileReviewsTreeView.SelectedImageIndex = 0;
            this.FileReviewsTreeView.Size = new System.Drawing.Size(236, 645);
            this.FileReviewsTreeView.TabIndex = 0;
            // 
            // DrawingReviewExplorerIcons
            // 
            this.DrawingReviewExplorerIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("DrawingReviewExplorerIcons.ImageStream")));
            this.DrawingReviewExplorerIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.DrawingReviewExplorerIcons.Images.SetKeyName(0, "folder-icon-24.png");
            this.DrawingReviewExplorerIcons.Images.SetKeyName(1, "user-24.png");
            this.DrawingReviewExplorerIcons.Images.SetKeyName(2, "file-ok-24.png");
            this.DrawingReviewExplorerIcons.Images.SetKeyName(3, "file-nok-24.png");
            this.DrawingReviewExplorerIcons.Images.SetKeyName(4, "tools-icon-24.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshAssemblyExplorerButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(236, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // RefreshAssemblyExplorerButton
            // 
            this.RefreshAssemblyExplorerButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RefreshAssemblyExplorerButton.Image = global::MechanicalSyncApp.Properties.Resources.refresh_icon_24;
            this.RefreshAssemblyExplorerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshAssemblyExplorerButton.Name = "RefreshAssemblyExplorerButton";
            this.RefreshAssemblyExplorerButton.Size = new System.Drawing.Size(66, 22);
            this.RefreshAssemblyExplorerButton.Text = "Refresh";
            this.RefreshAssemblyExplorerButton.ToolTipText = "Refresh local files";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Controls.Add(this.FileChangeRequestsGrid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(837, 643);
            this.panel1.TabIndex = 5;
            // 
            // FileChangeRequestsGrid
            // 
            this.FileChangeRequestsGrid.AllowUserToAddRows = false;
            this.FileChangeRequestsGrid.AllowUserToDeleteRows = false;
            this.FileChangeRequestsGrid.AllowUserToResizeRows = false;
            this.FileChangeRequestsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FileChangeRequestsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChangeRequestDescriptionColumn,
            this.ChangeRequestStatusColumn,
            this.DesignerCommentsColumn});
            this.FileChangeRequestsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileChangeRequestsGrid.Location = new System.Drawing.Point(0, 0);
            this.FileChangeRequestsGrid.MultiSelect = false;
            this.FileChangeRequestsGrid.Name = "FileChangeRequestsGrid";
            this.FileChangeRequestsGrid.ReadOnly = true;
            this.FileChangeRequestsGrid.RowHeadersVisible = false;
            this.FileChangeRequestsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FileChangeRequestsGrid.Size = new System.Drawing.Size(837, 643);
            this.FileChangeRequestsGrid.TabIndex = 2;
            // 
            // FileReviewsToolStrip
            // 
            this.FileReviewsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MarkFileAsFixedButton,
            this.toolStripSeparator5,
            this.FileReviewStatus,
            this.FileReviewViewerTitle});
            this.FileReviewsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.FileReviewsToolStrip.Name = "FileReviewsToolStrip";
            this.FileReviewsToolStrip.Size = new System.Drawing.Size(837, 27);
            this.FileReviewsToolStrip.TabIndex = 0;
            this.FileReviewsToolStrip.Text = "toolStrip2";
            // 
            // MarkFileAsFixedButton
            // 
            this.MarkFileAsFixedButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MarkFileAsFixedButton.Image = ((System.Drawing.Image)(resources.GetObject("MarkFileAsFixedButton.Image")));
            this.MarkFileAsFixedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MarkFileAsFixedButton.Name = "MarkFileAsFixedButton";
            this.MarkFileAsFixedButton.Size = new System.Drawing.Size(97, 24);
            this.MarkFileAsFixedButton.Text = "Mark as fixed";
            this.MarkFileAsFixedButton.ToolTipText = "Mark assembly as fixed";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // FileReviewStatus
            // 
            this.FileReviewStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.FileReviewStatus.AutoSize = false;
            this.FileReviewStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileReviewStatus.Name = "FileReviewStatus";
            this.FileReviewStatus.Size = new System.Drawing.Size(59, 24);
            this.FileReviewStatus.Text = "Status";
            // 
            // FileReviewViewerTitle
            // 
            this.FileReviewViewerTitle.Name = "FileReviewViewerTitle";
            this.FileReviewViewerTitle.Size = new System.Drawing.Size(59, 24);
            this.FileReviewViewerTitle.Text = "Loading...";
            // 
            // ProjectTabImages
            // 
            this.ProjectTabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ProjectTabImages.ImageStream")));
            this.ProjectTabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ProjectTabImages.Images.SetKeyName(0, "folder-icon-24.png");
            this.ProjectTabImages.Images.SetKeyName(1, "design-32.png");
            this.ProjectTabImages.Images.SetKeyName(2, "review-icon-32.png");
            this.ProjectTabImages.Images.SetKeyName(3, "3d-32.png");
            this.ProjectTabImages.Images.SetKeyName(4, "document-32.png");
            this.ProjectTabImages.Images.SetKeyName(5, "Icon-SOLIDWORKS.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CloseVersionButton);
            this.panel2.Controls.Add(this.ProjectFolderNameLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1085, 25);
            this.panel2.TabIndex = 18;
            // 
            // CloseVersionButton
            // 
            this.CloseVersionButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseVersionButton.Image = global::MechanicalSyncApp.Properties.Resources.close_16;
            this.CloseVersionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CloseVersionButton.Location = new System.Drawing.Point(990, 0);
            this.CloseVersionButton.Name = "CloseVersionButton";
            this.CloseVersionButton.Size = new System.Drawing.Size(95, 25);
            this.CloseVersionButton.TabIndex = 18;
            this.CloseVersionButton.Text = "Close version";
            this.CloseVersionButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CloseVersionButton.UseVisualStyleBackColor = true;
            // 
            // ProjectFolderNameLabel
            // 
            this.ProjectFolderNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectFolderNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectFolderNameLabel.Location = new System.Drawing.Point(0, 0);
            this.ProjectFolderNameLabel.Name = "ProjectFolderNameLabel";
            this.ProjectFolderNameLabel.Size = new System.Drawing.Size(1085, 25);
            this.ProjectFolderNameLabel.TabIndex = 17;
            this.ProjectFolderNameLabel.Text = "220214NY-OBERG SML HEAD TOOLING";
            this.ProjectFolderNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(1385, 24);
            this.MainMenuStrip.TabIndex = 17;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNewButton,
            this.ProjectExplorerMenuItem,
            this.toolStripSeparator3,
            this.LogoutMenuItem,
            this.ExitMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.testToolStripMenuItem.Text = "File";
            // 
            // FileNewButton
            // 
            this.FileNewButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewProjectButton,
            this.NewVersionButton,
            this.NewReviewButton});
            this.FileNewButton.Name = "FileNewButton";
            this.FileNewButton.Size = new System.Drawing.Size(166, 22);
            this.FileNewButton.Text = "New";
            // 
            // NewProjectButton
            // 
            this.NewProjectButton.Name = "NewProjectButton";
            this.NewProjectButton.Size = new System.Drawing.Size(162, 22);
            this.NewProjectButton.Text = "Project...";
            this.NewProjectButton.Click += new System.EventHandler(this.NewProjectButton_Click);
            // 
            // NewVersionButton
            // 
            this.NewVersionButton.Name = "NewVersionButton";
            this.NewVersionButton.Size = new System.Drawing.Size(162, 22);
            this.NewVersionButton.Text = "Project change...";
            this.NewVersionButton.Click += new System.EventHandler(this.NewVersionButton_Click);
            // 
            // NewReviewButton
            // 
            this.NewReviewButton.Name = "NewReviewButton";
            this.NewReviewButton.Size = new System.Drawing.Size(162, 22);
            this.NewReviewButton.Text = "Change review...";
            this.NewReviewButton.Click += new System.EventHandler(this.NewReviewButton_Click);
            // 
            // ProjectExplorerMenuItem
            // 
            this.ProjectExplorerMenuItem.Name = "ProjectExplorerMenuItem";
            this.ProjectExplorerMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ProjectExplorerMenuItem.Text = "Project Explorer...";
            this.ProjectExplorerMenuItem.Click += new System.EventHandler(this.ProjectExplorerMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(163, 6);
            // 
            // LogoutMenuItem
            // 
            this.LogoutMenuItem.Name = "LogoutMenuItem";
            this.LogoutMenuItem.Size = new System.Drawing.Size(166, 22);
            this.LogoutMenuItem.Text = "Logout";
            this.LogoutMenuItem.Click += new System.EventHandler(this.LogoutMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMechanicalSyncToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutMechanicalSyncToolStripMenuItem
            // 
            this.aboutMechanicalSyncToolStripMenuItem.Name = "aboutMechanicalSyncToolStripMenuItem";
            this.aboutMechanicalSyncToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.aboutMechanicalSyncToolStripMenuItem.Text = "About Mechanical Sync";
            this.aboutMechanicalSyncToolStripMenuItem.Click += new System.EventHandler(this.aboutMechanicalSyncToolStripMenuItem_Click);
            // 
            // ChangeRequestDescriptionColumn
            // 
            this.ChangeRequestDescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ChangeRequestDescriptionColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.ChangeRequestDescriptionColumn.HeaderText = "Change request description";
            this.ChangeRequestDescriptionColumn.Name = "ChangeRequestDescriptionColumn";
            this.ChangeRequestDescriptionColumn.ReadOnly = true;
            // 
            // ChangeRequestStatusColumn
            // 
            this.ChangeRequestStatusColumn.HeaderText = "Status";
            this.ChangeRequestStatusColumn.Name = "ChangeRequestStatusColumn";
            this.ChangeRequestStatusColumn.ReadOnly = true;
            this.ChangeRequestStatusColumn.Width = 150;
            // 
            // DesignerCommentsColumn
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DesignerCommentsColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.DesignerCommentsColumn.HeaderText = "Designer comments";
            this.DesignerCommentsColumn.Name = "DesignerCommentsColumn";
            this.DesignerCommentsColumn.ReadOnly = true;
            this.DesignerCommentsColumn.Width = 250;
            // 
            // VersionSynchronizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1385, 754);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.MainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VersionSynchronizerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mechanical Sync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VersionSynchronizerForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VersionSynchronizerForm_FormClosed);
            this.Load += new System.EventHandler(this.VersionSynchronizerForm_Load);
            this.VisibleChanged += new System.EventHandler(this.VersionSynchronizerForm_VisibleChanged);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel1.PerformLayout();
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.WorkspaceToolStrip.ResumeLayout(false);
            this.WorkspaceToolStrip.PerformLayout();
            this.VersionSynchronizerTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.DesignFilesStatusStrip.ResumeLayout(false);
            this.DesignFilesStatusStrip.PerformLayout();
            this.SynchronizerToolStrip.ResumeLayout(false);
            this.SynchronizerToolStrip.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.FileReviewsSplit.Panel1.ResumeLayout(false);
            this.FileReviewsSplit.Panel1.PerformLayout();
            this.FileReviewsSplit.Panel2.ResumeLayout(false);
            this.FileReviewsSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FileReviewsSplit)).EndInit();
            this.FileReviewsSplit.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FileChangeRequestsGrid)).EndInit();
            this.FileReviewsToolStrip.ResumeLayout(false);
            this.FileReviewsToolStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList FileSyncStatusIcons;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.TreeView WorkspaceTreeView;
        private System.Windows.Forms.ToolStrip SynchronizerToolStrip;
        private System.Windows.Forms.TabControl VersionSynchronizerTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView FileViewerListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label ProjectFolderNameLabel;
        private System.Windows.Forms.ToolStripButton SyncRemoteButton;
        private System.Windows.Forms.ToolStripButton RefreshLocalFilesButton;
        private System.Windows.Forms.ImageList ProjectTabImages;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton WorkOfflineButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip DesignFilesStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel SyncStatusLabel;
        private System.Windows.Forms.ToolStripButton WorkOnlineButton;
        private System.Windows.Forms.ToolStripProgressBar SyncProgressBar;
        private System.Windows.Forms.ToolStripButton PublishDeliverablesButton;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem CopyLocalCopyPathMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenLocalCopyFolderMenuItem;
        private System.Windows.Forms.ToolStrip WorkspaceToolStrip;
        private System.Windows.Forms.ToolStripButton RefreshVersionsButton;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton TransferOwnershipButton;
        private System.Windows.Forms.ToolStripMenuItem FileNewButton;
        private System.Windows.Forms.ToolStripMenuItem NewProjectButton;
        private System.Windows.Forms.ToolStripMenuItem NewVersionButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ImageList WorkspaceIcons;
        private System.Windows.Forms.ToolStripMenuItem LogoutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProjectExplorerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewReviewButton;
        private System.Windows.Forms.Button CloseVersionButton;
        private System.Windows.Forms.ImageList DrawingReviewExplorerIcons;
        private System.Windows.Forms.ToolStripButton ArchiveVersionButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.SplitContainer FileReviewsSplit;
        private System.Windows.Forms.TreeView FileReviewsTreeView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton RefreshAssemblyExplorerButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip FileReviewsToolStrip;
        private System.Windows.Forms.ToolStripButton MarkFileAsFixedButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel FileReviewStatus;
        private System.Windows.Forms.ToolStripLabel FileReviewViewerTitle;
        private System.Windows.Forms.DataGridView FileChangeRequestsGrid;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMechanicalSyncToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeRequestDescriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeRequestStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DesignerCommentsColumn;
    }
}