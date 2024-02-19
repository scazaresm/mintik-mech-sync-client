namespace MechanicalSyncApp.UI.Forms
{
    partial class SyncCheckSummaryForm
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Created", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Modified", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Deleted", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Example",
            "Deleted",
            "other"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Other",
            "Created"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncCheckSummaryForm));
            this.SummaryListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileIcons = new System.Windows.Forms.ImageList(this.components);
            this.CancelSyncButton = new System.Windows.Forms.Button();
            this.SyncNowButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CompareButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SummaryListView
            // 
            this.SummaryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            listViewGroup1.Header = "Created";
            listViewGroup1.Name = "createdGroup";
            listViewGroup2.Header = "Modified";
            listViewGroup2.Name = "modifiedGroup";
            listViewGroup3.Header = "Deleted";
            listViewGroup3.Name = "deletedGroup";
            this.SummaryListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.SummaryListView.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.SummaryListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.SummaryListView.Location = new System.Drawing.Point(12, 75);
            this.SummaryListView.MultiSelect = false;
            this.SummaryListView.Name = "SummaryListView";
            this.SummaryListView.Size = new System.Drawing.Size(736, 407);
            this.SummaryListView.SmallImageList = this.FileIcons;
            this.SummaryListView.TabIndex = 17;
            this.SummaryListView.TileSize = new System.Drawing.Size(150, 30);
            this.SummaryListView.UseCompatibleStateImageBehavior = false;
            this.SummaryListView.View = System.Windows.Forms.View.Details;
            this.SummaryListView.SelectedIndexChanged += new System.EventHandler(this.SummaryListView_SelectedIndexChanged);
            this.SummaryListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SummaryListView_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Local file";
            this.columnHeader1.Width = 550;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Action in remote";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 150;
            // 
            // FileIcons
            // 
            this.FileIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FileIcons.ImageStream")));
            this.FileIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.FileIcons.Images.SetKeyName(0, "add-24.png");
            this.FileIcons.Images.SetKeyName(1, "pencil-24.png");
            this.FileIcons.Images.SetKeyName(2, "remove-24.png");
            // 
            // CancelSyncButton
            // 
            this.CancelSyncButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelSyncButton.Location = new System.Drawing.Point(586, 509);
            this.CancelSyncButton.Name = "CancelSyncButton";
            this.CancelSyncButton.Size = new System.Drawing.Size(75, 23);
            this.CancelSyncButton.TabIndex = 1;
            this.CancelSyncButton.Text = "Cancel";
            this.CancelSyncButton.UseVisualStyleBackColor = true;
            this.CancelSyncButton.Click += new System.EventHandler(this.CancelSyncButton_Click);
            // 
            // SyncNowButton
            // 
            this.SyncNowButton.Location = new System.Drawing.Point(673, 509);
            this.SyncNowButton.Name = "SyncNowButton";
            this.SyncNowButton.Size = new System.Drawing.Size(75, 23);
            this.SyncNowButton.TabIndex = 2;
            this.SyncNowButton.Text = "Sync now";
            this.SyncNowButton.UseVisualStyleBackColor = true;
            this.SyncNowButton.Click += new System.EventHandler(this.SyncNowButton_Click);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(9, 19);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(673, 13);
            this.TitleLabel.TabIndex = 19;
            this.TitleLabel.Text = "The following changes were detected in your local copy, please validate and click" +
    " the Sync now button to start pushing all changes to server:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CompareButton});
            this.toolStrip1.Location = new System.Drawing.Point(12, 47);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(88, 25);
            this.toolStrip1.TabIndex = 20;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CompareButton
            // 
            this.CompareButton.Enabled = false;
            this.CompareButton.Image = ((System.Drawing.Image)(resources.GetObject("CompareButton.Image")));
            this.CompareButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CompareButton.Name = "CompareButton";
            this.CompareButton.Size = new System.Drawing.Size(76, 22);
            this.CompareButton.Text = "Compare";
            this.CompareButton.Click += new System.EventHandler(this.CompareButton_Click);
            // 
            // SyncCheckSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelSyncButton;
            this.ClientSize = new System.Drawing.Size(759, 556);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.SyncNowButton);
            this.Controls.Add(this.CancelSyncButton);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.SummaryListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SyncCheckSummaryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sync check summary";
            this.Load += new System.EventHandler(this.SyncSummaryForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView SummaryListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button SyncNowButton;
        private System.Windows.Forms.Button CancelSyncButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.ImageList FileIcons;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton CompareButton;
    }
}