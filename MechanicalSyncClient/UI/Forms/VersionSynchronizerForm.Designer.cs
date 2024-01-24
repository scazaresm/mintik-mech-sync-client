
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
            this.FileSyncStatusIcons = new System.Windows.Forms.ImageList(this.components);
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.WorkspaceTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.RefreshVersionsButton = new System.Windows.Forms.ToolStripButton();
            this.VersionSynchronizerTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.FileViewerListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SyncStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SyncProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.SynchronizerToolStrip = new System.Windows.Forms.ToolStrip();
            this.RefreshLocalFilesButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.CopyLocalCopyPathMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenLocalCopyFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SyncRemoteButton = new System.Windows.Forms.ToolStripButton();
            this.CloseVersionButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.WorkOfflineButton = new System.Windows.Forms.ToolStripButton();
            this.WorkOnlineButton = new System.Windows.Forms.ToolStripButton();
            this.PublishVersionButton = new System.Windows.Forms.ToolStripButton();
            this.TransferOwnershipButton = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ProjectTabImages = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.ProjectFolderNameLabel = new System.Windows.Forms.Label();
            this.ReviewArtifactsTabImages = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ReviewerListImages = new System.Windows.Forms.ImageList(this.components);
            this.ReviewerStateImages = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileNewButton = new System.Windows.Forms.ToolStripMenuItem();
            this.NewProjectButton = new System.Windows.Forms.ToolStripMenuItem();
            this.NewVersionButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.VersionSynchronizerTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SynchronizerToolStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.MainSplitContainer.Panel1.Controls.Add(this.toolStrip3);
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
            this.WorkspaceTreeView.Location = new System.Drawing.Point(0, 25);
            this.WorkspaceTreeView.Name = "WorkspaceTreeView";
            this.WorkspaceTreeView.Size = new System.Drawing.Size(296, 705);
            this.WorkspaceTreeView.TabIndex = 0;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshVersionsButton});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(296, 25);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip3";
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
            this.VersionSynchronizerTabs.Controls.Add(this.tabPage2);
            this.VersionSynchronizerTabs.Controls.Add(this.tabPage3);
            this.VersionSynchronizerTabs.Controls.Add(this.tabPage4);
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
            this.tabPage1.Controls.Add(this.statusStrip1);
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SyncStatusLabel,
            this.SyncProgressBar});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(3, 645);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1071, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
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
            this.CloseVersionButton,
            this.toolStripSeparator2,
            this.WorkOfflineButton,
            this.WorkOnlineButton,
            this.PublishVersionButton,
            this.TransferOwnershipButton});
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
            // CloseVersionButton
            // 
            this.CloseVersionButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CloseVersionButton.Image = global::MechanicalSyncApp.Properties.Resources.close_16;
            this.CloseVersionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseVersionButton.Name = "CloseVersionButton";
            this.CloseVersionButton.Size = new System.Drawing.Size(56, 22);
            this.CloseVersionButton.Text = "Close";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // PublishVersionButton
            // 
            this.PublishVersionButton.Image = global::MechanicalSyncApp.Properties.Resources.merge_icon_24;
            this.PublishVersionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PublishVersionButton.Name = "PublishVersionButton";
            this.PublishVersionButton.Size = new System.Drawing.Size(107, 22);
            this.PublishVersionButton.Text = "Publish version";
            this.PublishVersionButton.ToolTipText = "Publish revision changes";
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
            // tabPage2
            // 
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1077, 670);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Design review";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 31);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1077, 670);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "3D Review";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.ImageIndex = 3;
            this.tabPage4.Location = new System.Drawing.Point(4, 31);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1077, 670);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Drawing review";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ProjectTabImages
            // 
            this.ProjectTabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ProjectTabImages.ImageStream")));
            this.ProjectTabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ProjectTabImages.Images.SetKeyName(0, "folder-24.png");
            this.ProjectTabImages.Images.SetKeyName(1, "design-32.png");
            this.ProjectTabImages.Images.SetKeyName(2, "3d-32.png");
            this.ProjectTabImages.Images.SetKeyName(3, "document-32.png");
            this.ProjectTabImages.Images.SetKeyName(4, "Icon-SOLIDWORKS.png");
            this.ProjectTabImages.Images.SetKeyName(5, "review-icon-32.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ProjectFolderNameLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1085, 25);
            this.panel2.TabIndex = 18;
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
            // ReviewArtifactsTabImages
            // 
            this.ReviewArtifactsTabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ReviewArtifactsTabImages.ImageStream")));
            this.ReviewArtifactsTabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ReviewArtifactsTabImages.Images.SetKeyName(0, "change-request-24.png");
            this.ReviewArtifactsTabImages.Images.SetKeyName(1, "notes-icon-24.png");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "pdf-32.png");
            // 
            // ReviewerListImages
            // 
            this.ReviewerListImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ReviewerListImages.ImageStream")));
            this.ReviewerListImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ReviewerListImages.Images.SetKeyName(0, "flat-user-24.png");
            // 
            // ReviewerStateImages
            // 
            this.ReviewerStateImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ReviewerStateImages.ImageStream")));
            this.ReviewerStateImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ReviewerStateImages.Images.SetKeyName(0, "thumbs-up-16.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1385, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNewButton,
            this.toolStripSeparator3,
            this.ExitMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.testToolStripMenuItem.Text = "File";
            // 
            // FileNewButton
            // 
            this.FileNewButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewProjectButton,
            this.NewVersionButton});
            this.FileNewButton.Name = "FileNewButton";
            this.FileNewButton.Size = new System.Drawing.Size(180, 22);
            this.FileNewButton.Text = "New";
            // 
            // NewProjectButton
            // 
            this.NewProjectButton.Name = "NewProjectButton";
            this.NewProjectButton.Size = new System.Drawing.Size(180, 22);
            this.NewProjectButton.Text = "Project...";
            this.NewProjectButton.Click += new System.EventHandler(this.NewProjectButton_Click);
            // 
            // NewVersionButton
            // 
            this.NewVersionButton.Name = "NewVersionButton";
            this.NewVersionButton.Size = new System.Drawing.Size(180, 22);
            this.NewVersionButton.Text = "Version...";
            this.NewVersionButton.Click += new System.EventHandler(this.NewVersionButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // VersionSynchronizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1385, 754);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.menuStrip1);
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
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.VersionSynchronizerTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.SynchronizerToolStrip.ResumeLayout(false);
            this.SynchronizerToolStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label ProjectFolderNameLabel;
        private System.Windows.Forms.ToolStripButton SyncRemoteButton;
        private System.Windows.Forms.ToolStripButton RefreshLocalFilesButton;
        private System.Windows.Forms.ImageList ProjectTabImages;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton WorkOfflineButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel SyncStatusLabel;
        private System.Windows.Forms.ToolStripButton WorkOnlineButton;
        private System.Windows.Forms.ToolStripProgressBar SyncProgressBar;
        private System.Windows.Forms.ImageList ReviewerListImages;
        private System.Windows.Forms.ImageList ReviewerStateImages;
        private System.Windows.Forms.ImageList ReviewArtifactsTabImages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton PublishVersionButton;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem CopyLocalCopyPathMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenLocalCopyFolderMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton RefreshVersionsButton;
        private System.Windows.Forms.ToolStripButton CloseVersionButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton TransferOwnershipButton;
        private System.Windows.Forms.ToolStripMenuItem FileNewButton;
        private System.Windows.Forms.ToolStripMenuItem NewProjectButton;
        private System.Windows.Forms.ToolStripMenuItem NewVersionButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}