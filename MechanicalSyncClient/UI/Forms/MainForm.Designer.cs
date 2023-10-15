
namespace MechanicalSyncApp.UI.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Assemblies", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Parts", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Drawings", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "Example",
            "Deleted",
            "other"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "Other",
            "Created"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("email_print.pdf", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Irving Martínez", 0);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Gustavo Avila", 0);
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.FileSyncStatusIcons = new System.Windows.Forms.ImageList(this.components);
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.VersionExplorerTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.RefreshVersionsButton = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.FileViewerListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SyncStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SyncProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.SynchronizerToolStrip = new System.Windows.Forms.ToolStrip();
            this.RefreshLocalFilesButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.StartWorkingButton = new System.Windows.Forms.ToolStripButton();
            this.StopWorkingButton = new System.Windows.Forms.ToolStripButton();
            this.SyncRemoteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInWindowsExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.DesignerNameOnReview = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ReviewersListView = new System.Windows.Forms.ListView();
            this.ReviewerListImages = new System.Windows.Forms.ImageList(this.components);
            this.ReviewerStateImages = new System.Windows.Forms.ImageList(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ReviewArtifactsTabImages = new System.Windows.Forms.ImageList(this.components);
            this.DesignReviewToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.ApproveRevisionButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBox3 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.ProjectTabImages = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.ProjectFolderNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SynchronizerToolStrip.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.DesignReviewToolStrip.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.VersionExplorerTreeView);
            this.MainSplitContainer.Panel1.Controls.Add(this.toolStrip3);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.tabControl1);
            this.MainSplitContainer.Panel2.Controls.Add(this.panel2);
            this.MainSplitContainer.Size = new System.Drawing.Size(1385, 754);
            this.MainSplitContainer.SplitterDistance = 296;
            this.MainSplitContainer.TabIndex = 16;
            // 
            // VersionExplorerTreeView
            // 
            this.VersionExplorerTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionExplorerTreeView.Location = new System.Drawing.Point(0, 25);
            this.VersionExplorerTreeView.Name = "VersionExplorerTreeView";
            this.VersionExplorerTreeView.Size = new System.Drawing.Size(296, 729);
            this.VersionExplorerTreeView.TabIndex = 0;
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
            this.RefreshVersionsButton.Image = global::MechanicalSyncApp.Properties.Resources.refresh_icon_24;
            this.RefreshVersionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshVersionsButton.Name = "RefreshVersionsButton";
            this.RefreshVersionsButton.Size = new System.Drawing.Size(66, 22);
            this.RefreshVersionsButton.Text = "Refresh";
            this.RefreshVersionsButton.Click += new System.EventHandler(this.RefreshVersionsButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.ProjectTabImages;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1085, 729);
            this.tabControl1.TabIndex = 16;
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
            this.tabPage1.Size = new System.Drawing.Size(1077, 694);
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
            listViewItem6.StateImageIndex = 0;
            this.FileViewerListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem6,
            listViewItem7});
            this.FileViewerListView.Location = new System.Drawing.Point(3, 28);
            this.FileViewerListView.MultiSelect = false;
            this.FileViewerListView.Name = "FileViewerListView";
            this.FileViewerListView.Size = new System.Drawing.Size(1071, 641);
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
            this.statusStrip1.Location = new System.Drawing.Point(3, 669);
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
            this.toolStripButton1,
            this.toolStripButton5,
            this.toolStripSeparator1,
            this.StartWorkingButton,
            this.StopWorkingButton,
            this.SyncRemoteButton,
            this.toolStripDropDownButton1,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.toolStripSeparator4,
            this.toolStripButton7});
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
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::MechanicalSyncApp.Properties.Resources.download_32;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(128, 22);
            this.toolStripButton1.Text = "Download changes";
            this.toolStripButton1.ToolTipText = "Download latest changes from remote server";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = global::MechanicalSyncApp.Properties.Resources.find_32;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(50, 22);
            this.toolStripButton5.Text = "Find";
            this.toolStripButton5.ToolTipText = "Find a local file by its name or path";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // StartWorkingButton
            // 
            this.StartWorkingButton.Image = global::MechanicalSyncApp.Properties.Resources.start_32;
            this.StartWorkingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartWorkingButton.Name = "StartWorkingButton";
            this.StartWorkingButton.Size = new System.Drawing.Size(91, 22);
            this.StartWorkingButton.Text = "Work online";
            this.StartWorkingButton.ToolTipText = "Start monitoring your changes for automatic sync";
            // 
            // StopWorkingButton
            // 
            this.StopWorkingButton.Image = global::MechanicalSyncApp.Properties.Resources.stop_32;
            this.StopWorkingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopWorkingButton.Name = "StopWorkingButton";
            this.StopWorkingButton.Size = new System.Drawing.Size(92, 22);
            this.StopWorkingButton.Text = "Work offline";
            this.StopWorkingButton.ToolTipText = "Stop monitoring your changes";
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
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem,
            this.openInWindowsExplorerToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::MechanicalSyncApp.Properties.Resources.open_folder_24;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(129, 22);
            this.toolStripDropDownButton1.Text = "Containing folder";
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy path to clipboard";
            // 
            // openInWindowsExplorerToolStripMenuItem
            // 
            this.openInWindowsExplorerToolStripMenuItem.Name = "openInWindowsExplorerToolStripMenuItem";
            this.openInWindowsExplorerToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.openInWindowsExplorerToolStripMenuItem.Text = "Open in Windows Explorer";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(82, 22);
            this.toolStripLabel3.Text = "You are owner";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(70, 22);
            this.toolStripLabel1.Text = "Ongoing V1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = global::MechanicalSyncApp.Properties.Resources.merge_icon_24;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(107, 22);
            this.toolStripButton7.Text = "Publish version";
            this.toolStripButton7.ToolTipText = "Publish revision changes";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Controls.Add(this.DesignReviewToolStrip);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1077, 694);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Design review";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 28);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.DesignerNameOnReview);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            this.splitContainer2.Panel1.Controls.Add(this.button4);
            this.splitContainer2.Panel1.Controls.Add(this.button3);
            this.splitContainer2.Panel1.Controls.Add(this.listView1);
            this.splitContainer2.Panel1.Controls.Add(this.ReviewersListView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer2.Size = new System.Drawing.Size(1071, 663);
            this.splitContainer2.SplitterDistance = 180;
            this.splitContainer2.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Reviewers:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DesignerNameOnReview
            // 
            this.DesignerNameOnReview.Location = new System.Drawing.Point(52, 10);
            this.DesignerNameOnReview.Name = "DesignerNameOnReview";
            this.DesignerNameOnReview.Size = new System.Drawing.Size(157, 21);
            this.DesignerNameOnReview.TabIndex = 2;
            this.DesignerNameOnReview.Text = "Ian Holguín";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Customer acceptance evidence:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Designer:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(100, 522);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Upload file";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(100, 238);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Add my review";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(7, 307);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(202, 209);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "pdf-32.png");
            // 
            // ReviewersListView
            // 
            this.ReviewersListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.ReviewersListView.FullRowSelect = true;
            this.ReviewersListView.HideSelection = false;
            listViewItem2.StateImageIndex = 0;
            this.ReviewersListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2,
            listViewItem3});
            this.ReviewersListView.LargeImageList = this.ReviewerListImages;
            this.ReviewersListView.Location = new System.Drawing.Point(7, 49);
            this.ReviewersListView.Name = "ReviewersListView";
            this.ReviewersListView.Size = new System.Drawing.Size(202, 183);
            this.ReviewersListView.SmallImageList = this.ReviewerListImages;
            this.ReviewersListView.StateImageList = this.ReviewerStateImages;
            this.ReviewersListView.TabIndex = 3;
            this.ReviewersListView.UseCompatibleStateImageBehavior = false;
            this.ReviewersListView.View = System.Windows.Forms.View.List;
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
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.ImageList = this.ReviewArtifactsTabImages;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(887, 663);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.splitContainer3);
            this.tabPage5.ImageIndex = 0;
            this.tabPage5.Location = new System.Drawing.Point(4, 31);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(879, 628);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Change requests";
            this.tabPage5.ToolTipText = "Change requests left by reviewers";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.textBox1);
            this.splitContainer3.Size = new System.Drawing.Size(873, 622);
            this.splitContainer3.SplitterDistance = 493;
            this.splitContainer3.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(873, 493);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Assembly";
            this.Column1.Name = "Column1";
            this.Column1.Width = 250;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "Required change";
            this.Column2.Name = "Column2";
            this.Column2.Width = 500;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Status";
            this.Column3.Items.AddRange(new object[] {
            "Pending",
            "Done",
            "Won\'t do"});
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column4
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column4.HeaderText = "Designer comments";
            this.Column4.Name = "Column4";
            this.Column4.Width = 300;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(873, 125);
            this.textBox1.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.splitContainer4);
            this.tabPage6.ImageIndex = 1;
            this.tabPage6.Location = new System.Drawing.Point(4, 31);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(879, 580);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Notes";
            this.tabPage6.ToolTipText = "Notes left by reviewers";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.dataGridView2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.textBox2);
            this.splitContainer4.Size = new System.Drawing.Size(873, 574);
            this.splitContainer4.SplitterDistance = 455;
            this.splitContainer4.TabIndex = 1;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(873, 455);
            this.dataGridView2.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Assembly";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Note";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(873, 115);
            this.textBox2.TabIndex = 0;
            // 
            // ReviewArtifactsTabImages
            // 
            this.ReviewArtifactsTabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ReviewArtifactsTabImages.ImageStream")));
            this.ReviewArtifactsTabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ReviewArtifactsTabImages.Images.SetKeyName(0, "change-request-24.png");
            this.ReviewArtifactsTabImages.Images.SetKeyName(1, "notes-icon-24.png");
            // 
            // DesignReviewToolStrip
            // 
            this.DesignReviewToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1,
            this.toolStripButton8,
            this.ApproveRevisionButton,
            this.toolStripLabel2});
            this.DesignReviewToolStrip.Location = new System.Drawing.Point(3, 3);
            this.DesignReviewToolStrip.Name = "DesignReviewToolStrip";
            this.DesignReviewToolStrip.Size = new System.Drawing.Size(1071, 25);
            this.DesignReviewToolStrip.TabIndex = 3;
            this.DesignReviewToolStrip.Text = "toolStrip2";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "All assemblies"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(300, 25);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Image = global::MechanicalSyncApp.Properties.Resources.send_icon_24;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(120, 22);
            this.toolStripButton8.Text = "Submit for review";
            this.toolStripButton8.ToolTipText = "Submit changes to selected reviewer for verification";
            // 
            // ApproveRevisionButton
            // 
            this.ApproveRevisionButton.Image = global::MechanicalSyncApp.Properties.Resources.thumbs_up_24;
            this.ApproveRevisionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ApproveRevisionButton.Name = "ApproveRevisionButton";
            this.ApproveRevisionButton.Size = new System.Drawing.Size(110, 22);
            this.ApproveRevisionButton.Text = "Approve design";
            this.ApproveRevisionButton.ToolTipText = "Approve designer\'s changes";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel2.Text = "Assembly file:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.toolStrip1);
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 31);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1077, 694);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "3D Review";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox2,
            this.toolStripButton9,
            this.toolStripButton10,
            this.toolStripLabel4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1077, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip2";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox2.Items.AddRange(new object[] {
            "All assemblies"});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(300, 25);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.Image = global::MechanicalSyncApp.Properties.Resources.send_icon_24;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(120, 22);
            this.toolStripButton9.Text = "Submit for review";
            this.toolStripButton9.ToolTipText = "Submit changes to selected reviewer for verification";
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.Image = global::MechanicalSyncApp.Properties.Resources.thumbs_up_24;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(89, 22);
            this.toolStripButton10.Text = "Approve 3D";
            this.toolStripButton10.ToolTipText = "Approve designer\'s changes";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel4.Text = "Assembly file:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.toolStrip2);
            this.tabPage4.ImageIndex = 3;
            this.tabPage4.Location = new System.Drawing.Point(4, 31);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1077, 694);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Drawing review";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox3,
            this.toolStripButton11,
            this.toolStripButton12,
            this.toolStripLabel5});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1077, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripComboBox3
            // 
            this.toolStripComboBox3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox3.Items.AddRange(new object[] {
            "All assemblies"});
            this.toolStripComboBox3.Name = "toolStripComboBox3";
            this.toolStripComboBox3.Size = new System.Drawing.Size(300, 25);
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.Image = global::MechanicalSyncApp.Properties.Resources.send_icon_24;
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(120, 22);
            this.toolStripButton11.Text = "Submit for review";
            this.toolStripButton11.ToolTipText = "Submit changes to selected reviewer for verification";
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.Image = global::MechanicalSyncApp.Properties.Resources.thumbs_up_24;
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(123, 22);
            this.toolStripButton12.Text = "Approve drawings";
            this.toolStripButton12.ToolTipText = "Approve designer\'s changes";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel5.Text = "Assembly file:";
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
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.ProjectFolderNameLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1085, 25);
            this.panel2.TabIndex = 18;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Image = global::MechanicalSyncApp.Properties.Resources.close_16;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(990, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 25);
            this.button2.TabIndex = 0;
            this.button2.Text = "Close project";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ProjectFolderNameLabel
            // 
            this.ProjectFolderNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ProjectFolderNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectFolderNameLabel.Location = new System.Drawing.Point(0, 0);
            this.ProjectFolderNameLabel.Name = "ProjectFolderNameLabel";
            this.ProjectFolderNameLabel.Size = new System.Drawing.Size(741, 25);
            this.ProjectFolderNameLabel.TabIndex = 17;
            this.ProjectFolderNameLabel.Text = "220214NY-OBERG SML HEAD TOOLING";
            this.ProjectFolderNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1385, 754);
            this.Controls.Add(this.MainSplitContainer);
            this.Name = "MainForm";
            this.Text = "Mechanical Sync Designer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.DemoForm_Load);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel1.PerformLayout();
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.SynchronizerToolStrip.ResumeLayout(false);
            this.SynchronizerToolStrip.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.DesignReviewToolStrip.ResumeLayout(false);
            this.DesignReviewToolStrip.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList FileSyncStatusIcons;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.TreeView VersionExplorerTreeView;
        private System.Windows.Forms.ToolStrip SynchronizerToolStrip;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView FileViewerListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Label ProjectFolderNameLabel;
        private System.Windows.Forms.ToolStripButton SyncRemoteButton;
        private System.Windows.Forms.ToolStripButton RefreshLocalFilesButton;
        private System.Windows.Forms.ImageList ProjectTabImages;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton StopWorkingButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel SyncStatusLabel;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton StartWorkingButton;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripProgressBar SyncProgressBar;
        private System.Windows.Forms.ToolStrip DesignReviewToolStrip;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton ApproveRevisionButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label DesignerNameOnReview;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ImageList ReviewerListImages;
        private System.Windows.Forms.ImageList ReviewerStateImages;
        private System.Windows.Forms.ListView ReviewersListView;
        private System.Windows.Forms.ImageList ReviewArtifactsTabImages;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox3;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInWindowsExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton RefreshVersionsButton;
    }
}