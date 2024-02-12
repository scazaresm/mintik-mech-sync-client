namespace MechanicalSyncApp.UI.Forms
{
    partial class ProjectExplorerForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Example",
            "Deleted",
            "other"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Other",
            "Created"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectExplorerForm));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Assemblies", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Parts", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Drawings", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Example",
            "Deleted",
            "other"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Other",
            "Created"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            this.ExplorerContainer = new System.Windows.Forms.SplitContainer();
            this.ProjectList = new System.Windows.Forms.ListView();
            this.folderNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.createdAtHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.createdByHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.latestVersionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ongoingVersionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProjectListIcons = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.ProjectSearchFilter = new System.Windows.Forms.TextBox();
            this.FileList = new System.Windows.Forms.ListView();
            this.remoteFileHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileSizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uploadedAtHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileChecksumHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.VersionSelector = new System.Windows.Forms.ToolStripComboBox();
            this.CloseProjectButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.FileSearchFilter = new System.Windows.Forms.ToolStripTextBox();
            this.RefreshFilesButton = new System.Windows.Forms.ToolStripButton();
            this.ProjectFolderNameLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.VersionFilesStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FileListIcons = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ExplorerContainer)).BeginInit();
            this.ExplorerContainer.Panel1.SuspendLayout();
            this.ExplorerContainer.Panel2.SuspendLayout();
            this.ExplorerContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExplorerContainer
            // 
            this.ExplorerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExplorerContainer.Location = new System.Drawing.Point(0, 0);
            this.ExplorerContainer.Name = "ExplorerContainer";
            // 
            // ExplorerContainer.Panel1
            // 
            this.ExplorerContainer.Panel1.Controls.Add(this.ProjectList);
            this.ExplorerContainer.Panel1.Controls.Add(this.panel1);
            // 
            // ExplorerContainer.Panel2
            // 
            this.ExplorerContainer.Panel2.Controls.Add(this.FileList);
            this.ExplorerContainer.Panel2.Controls.Add(this.toolStrip1);
            this.ExplorerContainer.Panel2.Controls.Add(this.ProjectFolderNameLabel);
            this.ExplorerContainer.Panel2.Controls.Add(this.statusStrip1);
            this.ExplorerContainer.Size = new System.Drawing.Size(1316, 539);
            this.ExplorerContainer.SplitterDistance = 746;
            this.ExplorerContainer.TabIndex = 0;
            // 
            // ProjectList
            // 
            this.ProjectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.folderNameHeader,
            this.createdAtHeader,
            this.createdByHeader,
            this.latestVersionColumn,
            this.ongoingVersionColumn});
            this.ProjectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectList.FullRowSelect = true;
            this.ProjectList.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.ProjectList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.ProjectList.Location = new System.Drawing.Point(0, 44);
            this.ProjectList.MultiSelect = false;
            this.ProjectList.Name = "ProjectList";
            this.ProjectList.Size = new System.Drawing.Size(746, 495);
            this.ProjectList.SmallImageList = this.ProjectListIcons;
            this.ProjectList.TabIndex = 17;
            this.ProjectList.TileSize = new System.Drawing.Size(150, 30);
            this.ProjectList.UseCompatibleStateImageBehavior = false;
            this.ProjectList.View = System.Windows.Forms.View.Details;
            // 
            // folderNameHeader
            // 
            this.folderNameHeader.Text = "Project folder name";
            this.folderNameHeader.Width = 300;
            // 
            // createdAtHeader
            // 
            this.createdAtHeader.Text = "Created at";
            this.createdAtHeader.Width = 150;
            // 
            // createdByHeader
            // 
            this.createdByHeader.Text = "Created by";
            this.createdByHeader.Width = 150;
            // 
            // latestVersionColumn
            // 
            this.latestVersionColumn.Text = "Latest version";
            this.latestVersionColumn.Width = 200;
            // 
            // ongoingVersionColumn
            // 
            this.ongoingVersionColumn.Text = "Ongoing version";
            this.ongoingVersionColumn.Width = 200;
            // 
            // ProjectListIcons
            // 
            this.ProjectListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ProjectListIcons.ImageStream")));
            this.ProjectListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ProjectListIcons.Images.SetKeyName(0, "folder-icon-24.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SearchLabel);
            this.panel1.Controls.Add(this.ProjectSearchFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(746, 44);
            this.panel1.TabIndex = 18;
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(12, 15);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(44, 13);
            this.SearchLabel.TabIndex = 14;
            this.SearchLabel.Text = "Search:";
            // 
            // ProjectSearchFilter
            // 
            this.ProjectSearchFilter.Location = new System.Drawing.Point(62, 12);
            this.ProjectSearchFilter.Name = "ProjectSearchFilter";
            this.ProjectSearchFilter.Size = new System.Drawing.Size(230, 20);
            this.ProjectSearchFilter.TabIndex = 13;
            // 
            // FileList
            // 
            this.FileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.remoteFileHeader,
            this.fileSizeHeader,
            this.uploadedAtHeader,
            this.fileChecksumHeader});
            this.FileList.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Assemblies";
            listViewGroup1.Name = "assembliesGroup";
            listViewGroup2.Header = "Parts";
            listViewGroup2.Name = "partsGroup";
            listViewGroup3.Header = "Drawings";
            listViewGroup3.Name = "drawingsGroup";
            this.FileList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.FileList.HideSelection = false;
            listViewItem3.StateImageIndex = 0;
            this.FileList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
            this.FileList.Location = new System.Drawing.Point(0, 50);
            this.FileList.MultiSelect = false;
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(566, 467);
            this.FileList.SmallImageList = this.FileListIcons;
            this.FileList.TabIndex = 17;
            this.FileList.TileSize = new System.Drawing.Size(150, 30);
            this.FileList.UseCompatibleStateImageBehavior = false;
            this.FileList.View = System.Windows.Forms.View.Details;
            // 
            // remoteFileHeader
            // 
            this.remoteFileHeader.Text = "Remote file";
            this.remoteFileHeader.Width = 650;
            // 
            // fileSizeHeader
            // 
            this.fileSizeHeader.Text = "File size";
            this.fileSizeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fileSizeHeader.Width = 100;
            // 
            // uploadedAtHeader
            // 
            this.uploadedAtHeader.Text = "Uploaded at";
            this.uploadedAtHeader.Width = 150;
            // 
            // fileChecksumHeader
            // 
            this.fileChecksumHeader.Text = "Checksum";
            this.fileChecksumHeader.Width = 400;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.VersionSelector,
            this.CloseProjectButton,
            this.toolStripLabel2,
            this.FileSearchFilter,
            this.RefreshFilesButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(566, 25);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(48, 22);
            this.toolStripLabel1.Text = "Version:";
            // 
            // VersionSelector
            // 
            this.VersionSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VersionSelector.Name = "VersionSelector";
            this.VersionSelector.Size = new System.Drawing.Size(121, 25);
            // 
            // CloseProjectButton
            // 
            this.CloseProjectButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CloseProjectButton.Image = global::MechanicalSyncApp.Properties.Resources.close_16;
            this.CloseProjectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseProjectButton.Name = "CloseProjectButton";
            this.CloseProjectButton.Size = new System.Drawing.Size(56, 22);
            this.CloseProjectButton.Text = "Close";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel2.Text = "Filter:";
            // 
            // FileSearchFilter
            // 
            this.FileSearchFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FileSearchFilter.Name = "FileSearchFilter";
            this.FileSearchFilter.Size = new System.Drawing.Size(180, 25);
            // 
            // RefreshFilesButton
            // 
            this.RefreshFilesButton.Image = global::MechanicalSyncApp.Properties.Resources.refresh_icon_24;
            this.RefreshFilesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshFilesButton.Name = "RefreshFilesButton";
            this.RefreshFilesButton.Size = new System.Drawing.Size(66, 22);
            this.RefreshFilesButton.Text = "Refresh";
            this.RefreshFilesButton.ToolTipText = "Refresh local files";
            // 
            // ProjectFolderNameLabel
            // 
            this.ProjectFolderNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProjectFolderNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectFolderNameLabel.Location = new System.Drawing.Point(0, 0);
            this.ProjectFolderNameLabel.Name = "ProjectFolderNameLabel";
            this.ProjectFolderNameLabel.Size = new System.Drawing.Size(566, 25);
            this.ProjectFolderNameLabel.TabIndex = 19;
            this.ProjectFolderNameLabel.Text = "220214NY-OBERG SML HEAD TOOLING";
            this.ProjectFolderNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VersionFilesStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 517);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(566, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // VersionFilesStatusLabel
            // 
            this.VersionFilesStatusLabel.Name = "VersionFilesStatusLabel";
            this.VersionFilesStatusLabel.Size = new System.Drawing.Size(83, 17);
            this.VersionFilesStatusLabel.Text = "Loading files...";
            // 
            // FileListIcons
            // 
            this.FileListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FileListIcons.ImageStream")));
            this.FileListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.FileListIcons.Images.SetKeyName(0, "file-icon-24.png");
            // 
            // ProjectExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 539);
            this.Controls.Add(this.ExplorerContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProjectExplorerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Explorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProjectExplorerForm_FormClosing);
            this.ExplorerContainer.Panel1.ResumeLayout(false);
            this.ExplorerContainer.Panel2.ResumeLayout(false);
            this.ExplorerContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExplorerContainer)).EndInit();
            this.ExplorerContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer ExplorerContainer;
        private System.Windows.Forms.ListView ProjectList;
        private System.Windows.Forms.ColumnHeader folderNameHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.TextBox ProjectSearchFilter;
        private System.Windows.Forms.ColumnHeader createdAtHeader;
        private System.Windows.Forms.ColumnHeader createdByHeader;
        private System.Windows.Forms.ImageList ProjectListIcons;
        private System.Windows.Forms.ColumnHeader latestVersionColumn;
        private System.Windows.Forms.ColumnHeader ongoingVersionColumn;
        private System.Windows.Forms.ListView FileList;
        private System.Windows.Forms.ColumnHeader remoteFileHeader;
        private System.Windows.Forms.ColumnHeader fileSizeHeader;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox VersionSelector;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Label ProjectFolderNameLabel;
        private System.Windows.Forms.ToolStripButton RefreshFilesButton;
        private System.Windows.Forms.ToolStripButton CloseProjectButton;
        private System.Windows.Forms.ToolStripTextBox FileSearchFilter;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel VersionFilesStatusLabel;
        private System.Windows.Forms.ColumnHeader uploadedAtHeader;
        private System.Windows.Forms.ColumnHeader fileChecksumHeader;
        private System.Windows.Forms.ImageList FileListIcons;
    }
}