
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
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Assemblies", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Parts", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Drawings", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Example",
            "Deleted",
            "other"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Other",
            "Created"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
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
            this.PublishVersionButton = new System.Windows.Forms.ToolStripButton();
            this.TransferOwnershipButton = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DrawingReviewPage = new System.Windows.Forms.TabPage();
            this.DrawingReviewContainer = new System.Windows.Forms.SplitContainer();
            this.DrawingReviewsTreeView = new System.Windows.Forms.TreeView();
            this.DrawingReviewExplorerIcons = new System.Windows.Forms.ImageList(this.components);
            this.DrawingReviewExplorerToolStrip = new System.Windows.Forms.ToolStrip();
            this.RefreshDrawingExplorerButton = new System.Windows.Forms.ToolStripButton();
            this.DrawingReviewerPanel = new System.Windows.Forms.Panel();
            this.MarkupStatusStrip = new System.Windows.Forms.StatusStrip();
            this.DrawingReviewerStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.DrawingReviewerProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.DrawingViewerToolStrip = new System.Windows.Forms.ToolStrip();
            this.MarkDrawingAsFixedButton = new System.Windows.Forms.ToolStripButton();
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
            this.DrawingReviewerDrawingStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.DrawingReviewerTitle = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.WorkspaceToolStrip.SuspendLayout();
            this.VersionSynchronizerTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.DesignFilesStatusStrip.SuspendLayout();
            this.SynchronizerToolStrip.SuspendLayout();
            this.DrawingReviewPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingReviewContainer)).BeginInit();
            this.DrawingReviewContainer.Panel1.SuspendLayout();
            this.DrawingReviewContainer.Panel2.SuspendLayout();
            this.DrawingReviewContainer.SuspendLayout();
            this.DrawingReviewExplorerToolStrip.SuspendLayout();
            this.MarkupStatusStrip.SuspendLayout();
            this.DrawingViewerToolStrip.SuspendLayout();
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
            this.VersionSynchronizerTabs.Controls.Add(this.DrawingReviewPage);
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
            listViewGroup4.Header = "Assemblies";
            listViewGroup4.Name = "assembliesGroup";
            listViewGroup5.Header = "Parts";
            listViewGroup5.Name = "partsGroup";
            listViewGroup6.Header = "Drawings";
            listViewGroup6.Name = "drawingsGroup";
            this.FileViewerListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup4,
            listViewGroup5,
            listViewGroup6});
            this.FileViewerListView.HideSelection = false;
            listViewItem3.StateImageIndex = 0;
            this.FileViewerListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
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
            // DrawingReviewPage
            // 
            this.DrawingReviewPage.Controls.Add(this.DrawingReviewContainer);
            this.DrawingReviewPage.ImageIndex = 3;
            this.DrawingReviewPage.Location = new System.Drawing.Point(4, 31);
            this.DrawingReviewPage.Name = "DrawingReviewPage";
            this.DrawingReviewPage.Size = new System.Drawing.Size(1077, 670);
            this.DrawingReviewPage.TabIndex = 3;
            this.DrawingReviewPage.Text = "2D review";
            this.DrawingReviewPage.UseVisualStyleBackColor = true;
            // 
            // DrawingReviewContainer
            // 
            this.DrawingReviewContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawingReviewContainer.Location = new System.Drawing.Point(0, 0);
            this.DrawingReviewContainer.Name = "DrawingReviewContainer";
            // 
            // DrawingReviewContainer.Panel1
            // 
            this.DrawingReviewContainer.Panel1.Controls.Add(this.DrawingReviewsTreeView);
            this.DrawingReviewContainer.Panel1.Controls.Add(this.DrawingReviewExplorerToolStrip);
            // 
            // DrawingReviewContainer.Panel2
            // 
            this.DrawingReviewContainer.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.DrawingReviewContainer.Panel2.Controls.Add(this.DrawingReviewerPanel);
            this.DrawingReviewContainer.Panel2.Controls.Add(this.MarkupStatusStrip);
            this.DrawingReviewContainer.Panel2.Controls.Add(this.DrawingViewerToolStrip);
            this.DrawingReviewContainer.Size = new System.Drawing.Size(1077, 670);
            this.DrawingReviewContainer.SplitterDistance = 236;
            this.DrawingReviewContainer.TabIndex = 0;
            // 
            // DrawingReviewsTreeView
            // 
            this.DrawingReviewsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawingReviewsTreeView.ImageIndex = 0;
            this.DrawingReviewsTreeView.ImageList = this.DrawingReviewExplorerIcons;
            this.DrawingReviewsTreeView.Location = new System.Drawing.Point(0, 25);
            this.DrawingReviewsTreeView.Name = "DrawingReviewsTreeView";
            this.DrawingReviewsTreeView.SelectedImageIndex = 0;
            this.DrawingReviewsTreeView.Size = new System.Drawing.Size(236, 645);
            this.DrawingReviewsTreeView.TabIndex = 0;
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
            // DrawingReviewExplorerToolStrip
            // 
            this.DrawingReviewExplorerToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshDrawingExplorerButton});
            this.DrawingReviewExplorerToolStrip.Location = new System.Drawing.Point(0, 0);
            this.DrawingReviewExplorerToolStrip.Name = "DrawingReviewExplorerToolStrip";
            this.DrawingReviewExplorerToolStrip.Size = new System.Drawing.Size(236, 25);
            this.DrawingReviewExplorerToolStrip.TabIndex = 1;
            this.DrawingReviewExplorerToolStrip.Text = "toolStrip1";
            // 
            // RefreshDrawingExplorerButton
            // 
            this.RefreshDrawingExplorerButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RefreshDrawingExplorerButton.Image = global::MechanicalSyncApp.Properties.Resources.refresh_icon_24;
            this.RefreshDrawingExplorerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshDrawingExplorerButton.Name = "RefreshDrawingExplorerButton";
            this.RefreshDrawingExplorerButton.Size = new System.Drawing.Size(66, 22);
            this.RefreshDrawingExplorerButton.Text = "Refresh";
            this.RefreshDrawingExplorerButton.ToolTipText = "Refresh local files";
            // 
            // DrawingReviewerPanel
            // 
            this.DrawingReviewerPanel.BackColor = System.Drawing.Color.DarkGray;
            this.DrawingReviewerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawingReviewerPanel.Location = new System.Drawing.Point(0, 27);
            this.DrawingReviewerPanel.Name = "DrawingReviewerPanel";
            this.DrawingReviewerPanel.Size = new System.Drawing.Size(837, 621);
            this.DrawingReviewerPanel.TabIndex = 5;
            // 
            // MarkupStatusStrip
            // 
            this.MarkupStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DrawingReviewerStatusText,
            this.DrawingReviewerProgress});
            this.MarkupStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MarkupStatusStrip.Location = new System.Drawing.Point(0, 648);
            this.MarkupStatusStrip.Name = "MarkupStatusStrip";
            this.MarkupStatusStrip.Size = new System.Drawing.Size(837, 22);
            this.MarkupStatusStrip.TabIndex = 4;
            this.MarkupStatusStrip.Text = "statusStrip1";
            // 
            // DrawingReviewerStatusText
            // 
            this.DrawingReviewerStatusText.Name = "DrawingReviewerStatusText";
            this.DrawingReviewerStatusText.Size = new System.Drawing.Size(108, 17);
            this.DrawingReviewerStatusText.Text = "Opening drawing...";
            // 
            // DrawingReviewerProgress
            // 
            this.DrawingReviewerProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.DrawingReviewerProgress.Name = "DrawingReviewerProgress";
            this.DrawingReviewerProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // DrawingViewerToolStrip
            // 
            this.DrawingViewerToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MarkDrawingAsFixedButton,
            this.toolStripSeparator2,
            this.DrawingReviewerDrawingStatus,
            this.DrawingReviewerTitle});
            this.DrawingViewerToolStrip.Location = new System.Drawing.Point(0, 0);
            this.DrawingViewerToolStrip.Name = "DrawingViewerToolStrip";
            this.DrawingViewerToolStrip.Size = new System.Drawing.Size(837, 27);
            this.DrawingViewerToolStrip.TabIndex = 0;
            this.DrawingViewerToolStrip.Text = "toolStrip2";
            // 
            // MarkDrawingAsFixedButton
            // 
            this.MarkDrawingAsFixedButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MarkDrawingAsFixedButton.Image = ((System.Drawing.Image)(resources.GetObject("MarkDrawingAsFixedButton.Image")));
            this.MarkDrawingAsFixedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MarkDrawingAsFixedButton.Name = "MarkDrawingAsFixedButton";
            this.MarkDrawingAsFixedButton.Size = new System.Drawing.Size(97, 24);
            this.MarkDrawingAsFixedButton.Text = "Mark as fixed";
            this.MarkDrawingAsFixedButton.ToolTipText = "Refresh local files";
            // 
            // ProjectTabImages
            // 
            this.ProjectTabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ProjectTabImages.ImageStream")));
            this.ProjectTabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ProjectTabImages.Images.SetKeyName(0, "folder-icon-24.png");
            this.ProjectTabImages.Images.SetKeyName(1, "design-32.png");
            this.ProjectTabImages.Images.SetKeyName(2, "3d-32.png");
            this.ProjectTabImages.Images.SetKeyName(3, "document-32.png");
            this.ProjectTabImages.Images.SetKeyName(4, "Icon-SOLIDWORKS.png");
            this.ProjectTabImages.Images.SetKeyName(5, "review-icon-32.png");
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
            this.testToolStripMenuItem});
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
            this.NewProjectButton.Size = new System.Drawing.Size(121, 22);
            this.NewProjectButton.Text = "Project...";
            this.NewProjectButton.Click += new System.EventHandler(this.NewProjectButton_Click);
            // 
            // NewVersionButton
            // 
            this.NewVersionButton.Name = "NewVersionButton";
            this.NewVersionButton.Size = new System.Drawing.Size(121, 22);
            this.NewVersionButton.Text = "Version...";
            this.NewVersionButton.Click += new System.EventHandler(this.NewVersionButton_Click);
            // 
            // NewReviewButton
            // 
            this.NewReviewButton.Name = "NewReviewButton";
            this.NewReviewButton.Size = new System.Drawing.Size(121, 22);
            this.NewReviewButton.Text = "Review...";
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
            // DrawingReviewerDrawingStatus
            // 
            this.DrawingReviewerDrawingStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.DrawingReviewerDrawingStatus.AutoSize = false;
            this.DrawingReviewerDrawingStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawingReviewerDrawingStatus.Name = "DrawingReviewerDrawingStatus";
            this.DrawingReviewerDrawingStatus.Size = new System.Drawing.Size(59, 24);
            this.DrawingReviewerDrawingStatus.Text = "Status";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // DrawingReviewerTitle
            // 
            this.DrawingReviewerTitle.Name = "DrawingReviewerTitle";
            this.DrawingReviewerTitle.Size = new System.Drawing.Size(59, 24);
            this.DrawingReviewerTitle.Text = "Loading...";
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
            this.DrawingReviewPage.ResumeLayout(false);
            this.DrawingReviewContainer.Panel1.ResumeLayout(false);
            this.DrawingReviewContainer.Panel1.PerformLayout();
            this.DrawingReviewContainer.Panel2.ResumeLayout(false);
            this.DrawingReviewContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingReviewContainer)).EndInit();
            this.DrawingReviewContainer.ResumeLayout(false);
            this.DrawingReviewExplorerToolStrip.ResumeLayout(false);
            this.DrawingReviewExplorerToolStrip.PerformLayout();
            this.MarkupStatusStrip.ResumeLayout(false);
            this.MarkupStatusStrip.PerformLayout();
            this.DrawingViewerToolStrip.ResumeLayout(false);
            this.DrawingViewerToolStrip.PerformLayout();
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
        private System.Windows.Forms.TabPage DrawingReviewPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton WorkOfflineButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip DesignFilesStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel SyncStatusLabel;
        private System.Windows.Forms.ToolStripButton WorkOnlineButton;
        private System.Windows.Forms.ToolStripProgressBar SyncProgressBar;
        private System.Windows.Forms.ToolStripButton PublishVersionButton;
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
        private System.Windows.Forms.SplitContainer DrawingReviewContainer;
        private System.Windows.Forms.TreeView DrawingReviewsTreeView;
        private System.Windows.Forms.ImageList WorkspaceIcons;
        private System.Windows.Forms.ToolStripMenuItem LogoutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProjectExplorerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewReviewButton;
        private System.Windows.Forms.Button CloseVersionButton;
        private System.Windows.Forms.ToolStrip DrawingReviewExplorerToolStrip;
        private System.Windows.Forms.ToolStripButton RefreshDrawingExplorerButton;
        private System.Windows.Forms.ToolStrip DrawingViewerToolStrip;
        private System.Windows.Forms.ToolStripButton MarkDrawingAsFixedButton;
        private System.Windows.Forms.ImageList DrawingReviewExplorerIcons;
        private System.Windows.Forms.StatusStrip MarkupStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel DrawingReviewerStatusText;
        private System.Windows.Forms.ToolStripProgressBar DrawingReviewerProgress;
        private System.Windows.Forms.Panel DrawingReviewerPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel DrawingReviewerDrawingStatus;
        private System.Windows.Forms.ToolStripLabel DrawingReviewerTitle;
    }
}