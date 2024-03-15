namespace MechanicalSyncApp.UI.Forms
{
    partial class ReviewSummaryForm
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
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "ExampleDrawing.slddrw",
            "0",
            "Needs at least 1 approval"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReviewSummaryForm));
            this.ChecklistTabs = new System.Windows.Forms.TabControl();
            this.assemblyVerificationTab = new System.Windows.Forms.TabPage();
            this.drawingVerificationTab = new System.Windows.Forms.TabPage();
            this.ReviewableDrawingsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabIcons = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.PublishAllButton = new System.Windows.Forms.Button();
            this.CancelPublishButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.ChecklistTabs.SuspendLayout();
            this.drawingVerificationTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChecklistTabs
            // 
            this.ChecklistTabs.Controls.Add(this.assemblyVerificationTab);
            this.ChecklistTabs.Controls.Add(this.drawingVerificationTab);
            this.ChecklistTabs.ImageList = this.TabIcons;
            this.ChecklistTabs.Location = new System.Drawing.Point(16, 64);
            this.ChecklistTabs.Name = "ChecklistTabs";
            this.ChecklistTabs.SelectedIndex = 0;
            this.ChecklistTabs.Size = new System.Drawing.Size(691, 312);
            this.ChecklistTabs.TabIndex = 1;
            // 
            // assemblyVerificationTab
            // 
            this.assemblyVerificationTab.Location = new System.Drawing.Point(4, 23);
            this.assemblyVerificationTab.Name = "assemblyVerificationTab";
            this.assemblyVerificationTab.Size = new System.Drawing.Size(683, 193);
            this.assemblyVerificationTab.TabIndex = 1;
            this.assemblyVerificationTab.Text = "Assemblies";
            this.assemblyVerificationTab.UseVisualStyleBackColor = true;
            // 
            // drawingVerificationTab
            // 
            this.drawingVerificationTab.Controls.Add(this.ReviewableDrawingsListView);
            this.drawingVerificationTab.Location = new System.Drawing.Point(4, 23);
            this.drawingVerificationTab.Name = "drawingVerificationTab";
            this.drawingVerificationTab.Padding = new System.Windows.Forms.Padding(3);
            this.drawingVerificationTab.Size = new System.Drawing.Size(683, 285);
            this.drawingVerificationTab.TabIndex = 0;
            this.drawingVerificationTab.Text = "Drawings";
            this.drawingVerificationTab.UseVisualStyleBackColor = true;
            // 
            // ReviewableDrawingsListView
            // 
            this.ReviewableDrawingsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.ReviewableDrawingsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewableDrawingsListView.HideSelection = false;
            listViewItem2.StateImageIndex = 0;
            this.ReviewableDrawingsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.ReviewableDrawingsListView.Location = new System.Drawing.Point(3, 3);
            this.ReviewableDrawingsListView.MultiSelect = false;
            this.ReviewableDrawingsListView.Name = "ReviewableDrawingsListView";
            this.ReviewableDrawingsListView.Size = new System.Drawing.Size(677, 279);
            this.ReviewableDrawingsListView.TabIndex = 17;
            this.ReviewableDrawingsListView.TileSize = new System.Drawing.Size(150, 30);
            this.ReviewableDrawingsListView.UseCompatibleStateImageBehavior = false;
            this.ReviewableDrawingsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Drawing";
            this.columnHeader1.Width = 350;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Approval count";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Status";
            this.columnHeader3.Width = 220;
            // 
            // TabIcons
            // 
            this.TabIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TabIcons.ImageStream")));
            this.TabIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.TabIcons.Images.SetKeyName(0, "ok-apply-icon-32.png");
            this.TabIcons.Images.SetKeyName(1, "remove-24.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Here is the summary of 3D and 2D reviews for this version. \r\n";
            // 
            // PublishAllButton
            // 
            this.PublishAllButton.Enabled = false;
            this.PublishAllButton.Location = new System.Drawing.Point(612, 388);
            this.PublishAllButton.Name = "PublishAllButton";
            this.PublishAllButton.Size = new System.Drawing.Size(95, 23);
            this.PublishAllButton.TabIndex = 3;
            this.PublishAllButton.Text = "Next";
            this.PublishAllButton.UseVisualStyleBackColor = true;
            // 
            // CancelPublishButton
            // 
            this.CancelPublishButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelPublishButton.Location = new System.Drawing.Point(507, 388);
            this.CancelPublishButton.Name = "CancelPublishButton";
            this.CancelPublishButton.Size = new System.Drawing.Size(95, 23);
            this.CancelPublishButton.TabIndex = 4;
            this.CancelPublishButton.Text = "Cancel";
            this.CancelPublishButton.UseVisualStyleBackColor = true;
            // 
            // RefreshButton
            // 
            this.RefreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RefreshButton.Location = new System.Drawing.Point(611, 34);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(92, 24);
            this.RefreshButton.TabIndex = 5;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ReviewSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelPublishButton;
            this.ClientSize = new System.Drawing.Size(719, 427);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.CancelPublishButton);
            this.Controls.Add(this.PublishAllButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChecklistTabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReviewSummaryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Review summary";
            this.ChecklistTabs.ResumeLayout(false);
            this.drawingVerificationTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl ChecklistTabs;
        private System.Windows.Forms.TabPage drawingVerificationTab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button PublishAllButton;
        private System.Windows.Forms.TabPage assemblyVerificationTab;
        private System.Windows.Forms.Button CancelPublishButton;
        private System.Windows.Forms.ListView ReviewableDrawingsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList TabIcons;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}