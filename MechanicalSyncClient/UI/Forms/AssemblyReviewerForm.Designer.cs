﻿namespace MechanicalSyncApp.UI.Forms
{
    partial class AssemblyReviewerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssemblyReviewerForm));
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.DesignerLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.DeltaAssembliesTreeView = new System.Windows.Forms.TreeView();
            this.SynchronizerToolStrip = new System.Windows.Forms.ToolStrip();
            this.RefreshReviewTargetsButton = new System.Windows.Forms.ToolStripButton();
            this.ReviewTabs = new System.Windows.Forms.TabControl();
            this.ChangeRequestsTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ChangeRequestsGrid = new System.Windows.Forms.DataGridView();
            this.ChangeRequestDescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeRequestStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeRequestInput = new System.Windows.Forms.TextBox();
            this.ObservationsTab = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.OvservationsGrid = new System.Windows.Forms.DataGridView();
            this.ObservationToolStrip = new System.Windows.Forms.ToolStrip();
            this.ObservationInput = new System.Windows.Forms.TextBox();
            this.TabIcons = new System.Windows.Forms.ImageList(this.components);
            this.ReviewToolStrip = new System.Windows.Forms.ToolStrip();
            this.CloseAssemblyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ReviewTargetStatus = new System.Windows.Forms.ToolStripLabel();
            this.ApproveAssemblyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.RejectAssemblyButton = new System.Windows.Forms.ToolStripButton();
            this.MarkupStatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TreeViewIcons = new System.Windows.Forms.ImageList(this.components);
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
            this.MainSplit.Panel1.SuspendLayout();
            this.MainSplit.Panel2.SuspendLayout();
            this.MainSplit.SuspendLayout();
            this.SynchronizerToolStrip.SuspendLayout();
            this.ReviewTabs.SuspendLayout();
            this.ChangeRequestsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChangeRequestsGrid)).BeginInit();
            this.ObservationsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OvservationsGrid)).BeginInit();
            this.ReviewToolStrip.SuspendLayout();
            this.MarkupStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Controls.Add(this.DesignerLabel);
            this.HeaderPanel.Controls.Add(this.label1);
            this.HeaderPanel.Controls.Add(this.HeaderLabel);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(974, 59);
            this.HeaderPanel.TabIndex = 2;
            // 
            // DesignerLabel
            // 
            this.DesignerLabel.AutoSize = true;
            this.DesignerLabel.Location = new System.Drawing.Point(66, 34);
            this.DesignerLabel.Name = "DesignerLabel";
            this.DesignerLabel.Size = new System.Drawing.Size(52, 13);
            this.DesignerLabel.TabIndex = 2;
            this.DesignerLabel.Text = "Designer:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Designer:";
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderLabel.Location = new System.Drawing.Point(8, 3);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(957, 25);
            this.HeaderLabel.TabIndex = 0;
            this.HeaderLabel.Text = "Loading...";
            this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainSplit
            // 
            this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplit.Location = new System.Drawing.Point(0, 59);
            this.MainSplit.Name = "MainSplit";
            // 
            // MainSplit.Panel1
            // 
            this.MainSplit.Panel1.Controls.Add(this.DeltaAssembliesTreeView);
            this.MainSplit.Panel1.Controls.Add(this.SynchronizerToolStrip);
            // 
            // MainSplit.Panel2
            // 
            this.MainSplit.Panel2.Controls.Add(this.ReviewTabs);
            this.MainSplit.Panel2.Controls.Add(this.ReviewToolStrip);
            this.MainSplit.Panel2.Controls.Add(this.MarkupStatusStrip);
            this.MainSplit.Size = new System.Drawing.Size(974, 472);
            this.MainSplit.SplitterDistance = 274;
            this.MainSplit.TabIndex = 3;
            // 
            // DeltaAssembliesTreeView
            // 
            this.DeltaAssembliesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeltaAssembliesTreeView.Location = new System.Drawing.Point(0, 25);
            this.DeltaAssembliesTreeView.Name = "DeltaAssembliesTreeView";
            this.DeltaAssembliesTreeView.Size = new System.Drawing.Size(274, 447);
            this.DeltaAssembliesTreeView.TabIndex = 1;
            // 
            // SynchronizerToolStrip
            // 
            this.SynchronizerToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshReviewTargetsButton});
            this.SynchronizerToolStrip.Location = new System.Drawing.Point(0, 0);
            this.SynchronizerToolStrip.Name = "SynchronizerToolStrip";
            this.SynchronizerToolStrip.Size = new System.Drawing.Size(274, 25);
            this.SynchronizerToolStrip.TabIndex = 16;
            this.SynchronizerToolStrip.Text = "toolStrip2";
            // 
            // RefreshReviewTargetsButton
            // 
            this.RefreshReviewTargetsButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RefreshReviewTargetsButton.Image = global::MechanicalSyncApp.Properties.Resources.refresh_icon_24;
            this.RefreshReviewTargetsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshReviewTargetsButton.Name = "RefreshReviewTargetsButton";
            this.RefreshReviewTargetsButton.Size = new System.Drawing.Size(66, 22);
            this.RefreshReviewTargetsButton.Text = "Refresh";
            this.RefreshReviewTargetsButton.ToolTipText = "Refresh assemblies";
            // 
            // ReviewTabs
            // 
            this.ReviewTabs.Controls.Add(this.ChangeRequestsTab);
            this.ReviewTabs.Controls.Add(this.ObservationsTab);
            this.ReviewTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewTabs.ImageList = this.TabIcons;
            this.ReviewTabs.Location = new System.Drawing.Point(0, 33);
            this.ReviewTabs.Name = "ReviewTabs";
            this.ReviewTabs.SelectedIndex = 0;
            this.ReviewTabs.Size = new System.Drawing.Size(696, 417);
            this.ReviewTabs.TabIndex = 6;
            // 
            // ChangeRequestsTab
            // 
            this.ChangeRequestsTab.Controls.Add(this.splitContainer1);
            this.ChangeRequestsTab.ImageIndex = 0;
            this.ChangeRequestsTab.Location = new System.Drawing.Point(4, 31);
            this.ChangeRequestsTab.Name = "ChangeRequestsTab";
            this.ChangeRequestsTab.Padding = new System.Windows.Forms.Padding(3);
            this.ChangeRequestsTab.Size = new System.Drawing.Size(688, 382);
            this.ChangeRequestsTab.TabIndex = 0;
            this.ChangeRequestsTab.Text = "Change requests";
            this.ChangeRequestsTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ChangeRequestsGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ChangeRequestInput);
            this.splitContainer1.Size = new System.Drawing.Size(682, 376);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.TabIndex = 0;
            // 
            // ChangeRequestsGrid
            // 
            this.ChangeRequestsGrid.AllowUserToAddRows = false;
            this.ChangeRequestsGrid.AllowUserToDeleteRows = false;
            this.ChangeRequestsGrid.AllowUserToResizeRows = false;
            this.ChangeRequestsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChangeRequestsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChangeRequestDescriptionColumn,
            this.ChangeRequestStatusColumn});
            this.ChangeRequestsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeRequestsGrid.Location = new System.Drawing.Point(0, 0);
            this.ChangeRequestsGrid.MultiSelect = false;
            this.ChangeRequestsGrid.Name = "ChangeRequestsGrid";
            this.ChangeRequestsGrid.ReadOnly = true;
            this.ChangeRequestsGrid.RowHeadersVisible = false;
            this.ChangeRequestsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ChangeRequestsGrid.Size = new System.Drawing.Size(682, 291);
            this.ChangeRequestsGrid.TabIndex = 1;
            // 
            // ChangeRequestDescriptionColumn
            // 
            this.ChangeRequestDescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ChangeRequestDescriptionColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ChangeRequestDescriptionColumn.HeaderText = "Description";
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
            // ChangeRequestInput
            // 
            this.ChangeRequestInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeRequestInput.ForeColor = System.Drawing.Color.Silver;
            this.ChangeRequestInput.Location = new System.Drawing.Point(0, 0);
            this.ChangeRequestInput.Multiline = true;
            this.ChangeRequestInput.Name = "ChangeRequestInput";
            this.ChangeRequestInput.Size = new System.Drawing.Size(682, 81);
            this.ChangeRequestInput.TabIndex = 0;
            this.ChangeRequestInput.Text = "Type here to create a new change request...";
            // 
            // ObservationsTab
            // 
            this.ObservationsTab.Controls.Add(this.splitContainer2);
            this.ObservationsTab.ImageIndex = 1;
            this.ObservationsTab.Location = new System.Drawing.Point(4, 31);
            this.ObservationsTab.Name = "ObservationsTab";
            this.ObservationsTab.Padding = new System.Windows.Forms.Padding(3);
            this.ObservationsTab.Size = new System.Drawing.Size(688, 382);
            this.ObservationsTab.TabIndex = 1;
            this.ObservationsTab.Text = "Observations";
            this.ObservationsTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.OvservationsGrid);
            this.splitContainer2.Panel1.Controls.Add(this.ObservationToolStrip);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ObservationInput);
            this.splitContainer2.Size = new System.Drawing.Size(682, 376);
            this.splitContainer2.SplitterDistance = 291;
            this.splitContainer2.TabIndex = 1;
            // 
            // OvservationsGrid
            // 
            this.OvservationsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OvservationsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OvservationsGrid.Location = new System.Drawing.Point(0, 25);
            this.OvservationsGrid.Name = "OvservationsGrid";
            this.OvservationsGrid.Size = new System.Drawing.Size(682, 266);
            this.OvservationsGrid.TabIndex = 1;
            // 
            // ObservationToolStrip
            // 
            this.ObservationToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ObservationToolStrip.Name = "ObservationToolStrip";
            this.ObservationToolStrip.Size = new System.Drawing.Size(682, 25);
            this.ObservationToolStrip.TabIndex = 0;
            this.ObservationToolStrip.Text = "toolStrip2";
            // 
            // ObservationInput
            // 
            this.ObservationInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObservationInput.ForeColor = System.Drawing.Color.Silver;
            this.ObservationInput.Location = new System.Drawing.Point(0, 0);
            this.ObservationInput.Multiline = true;
            this.ObservationInput.Name = "ObservationInput";
            this.ObservationInput.Size = new System.Drawing.Size(682, 81);
            this.ObservationInput.TabIndex = 0;
            this.ObservationInput.Text = "Type here to create a new observation...";
            // 
            // TabIcons
            // 
            this.TabIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TabIcons.ImageStream")));
            this.TabIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.TabIcons.Images.SetKeyName(0, "change-request-24.png");
            this.TabIcons.Images.SetKeyName(1, "observation-24.png");
            // 
            // ReviewToolStrip
            // 
            this.ReviewToolStrip.AutoSize = false;
            this.ReviewToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseAssemblyButton,
            this.toolStripSeparator2,
            this.ReviewTargetStatus,
            this.ApproveAssemblyButton,
            this.toolStripSeparator1,
            this.RejectAssemblyButton});
            this.ReviewToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ReviewToolStrip.Name = "ReviewToolStrip";
            this.ReviewToolStrip.Size = new System.Drawing.Size(696, 33);
            this.ReviewToolStrip.TabIndex = 5;
            this.ReviewToolStrip.Text = "toolStrip1";
            // 
            // CloseAssemblyButton
            // 
            this.CloseAssemblyButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CloseAssemblyButton.Image = global::MechanicalSyncApp.Properties.Resources.close_16;
            this.CloseAssemblyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseAssemblyButton.Name = "CloseAssemblyButton";
            this.CloseAssemblyButton.Size = new System.Drawing.Size(56, 30);
            this.CloseAssemblyButton.Text = "Close";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // ReviewTargetStatus
            // 
            this.ReviewTargetStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ReviewTargetStatus.AutoSize = false;
            this.ReviewTargetStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReviewTargetStatus.Name = "ReviewTargetStatus";
            this.ReviewTargetStatus.Size = new System.Drawing.Size(59, 30);
            this.ReviewTargetStatus.Text = "Loading...";
            // 
            // ApproveAssemblyButton
            // 
            this.ApproveAssemblyButton.Image = global::MechanicalSyncApp.Properties.Resources.ok_apply_icon_24;
            this.ApproveAssemblyButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ApproveAssemblyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ApproveAssemblyButton.Name = "ApproveAssemblyButton";
            this.ApproveAssemblyButton.Size = new System.Drawing.Size(105, 30);
            this.ApproveAssemblyButton.Text = "Approve assy";
            this.ApproveAssemblyButton.ToolTipText = "Approve assembly";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // RejectAssemblyButton
            // 
            this.RejectAssemblyButton.Image = global::MechanicalSyncApp.Properties.Resources.reject_24;
            this.RejectAssemblyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RejectAssemblyButton.Name = "RejectAssemblyButton";
            this.RejectAssemblyButton.Size = new System.Drawing.Size(84, 30);
            this.RejectAssemblyButton.Text = "Reject assy";
            this.RejectAssemblyButton.ToolTipText = "Reject assembly";
            // 
            // MarkupStatusStrip
            // 
            this.MarkupStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.MarkupStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MarkupStatusStrip.Location = new System.Drawing.Point(0, 450);
            this.MarkupStatusStrip.Name = "MarkupStatusStrip";
            this.MarkupStatusStrip.Size = new System.Drawing.Size(696, 22);
            this.MarkupStatusStrip.TabIndex = 3;
            this.MarkupStatusStrip.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(59, 17);
            this.StatusLabel.Text = "Loading...";
            // 
            // TreeViewIcons
            // 
            this.TreeViewIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeViewIcons.ImageStream")));
            this.TreeViewIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.TreeViewIcons.Images.SetKeyName(0, "folder-icon-24.png");
            this.TreeViewIcons.Images.SetKeyName(1, "file-icon-24.png");
            this.TreeViewIcons.Images.SetKeyName(2, "file-ok-24.png");
            this.TreeViewIcons.Images.SetKeyName(3, "file-nok-24.png");
            this.TreeViewIcons.Images.SetKeyName(4, "tools-icon-24.png");
            // 
            // AssemblyReviewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 531);
            this.Controls.Add(this.MainSplit);
            this.Controls.Add(this.HeaderPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AssemblyReviewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "3D Review";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AssemblyReviewerForm_FormClosed);
            this.Load += new System.EventHandler(this.AssemblyReviewerForm_Load);
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            this.MainSplit.Panel1.ResumeLayout(false);
            this.MainSplit.Panel1.PerformLayout();
            this.MainSplit.Panel2.ResumeLayout(false);
            this.MainSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
            this.MainSplit.ResumeLayout(false);
            this.SynchronizerToolStrip.ResumeLayout(false);
            this.SynchronizerToolStrip.PerformLayout();
            this.ReviewTabs.ResumeLayout(false);
            this.ChangeRequestsTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChangeRequestsGrid)).EndInit();
            this.ObservationsTab.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OvservationsGrid)).EndInit();
            this.ReviewToolStrip.ResumeLayout(false);
            this.ReviewToolStrip.PerformLayout();
            this.MarkupStatusStrip.ResumeLayout(false);
            this.MarkupStatusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Label DesignerLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.SplitContainer MainSplit;
        private System.Windows.Forms.TreeView DeltaAssembliesTreeView;
        private System.Windows.Forms.ToolStrip SynchronizerToolStrip;
        private System.Windows.Forms.ToolStripButton RefreshReviewTargetsButton;
        private System.Windows.Forms.StatusStrip MarkupStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ImageList TreeViewIcons;
        private System.Windows.Forms.ToolStrip ReviewToolStrip;
        private System.Windows.Forms.ToolStripButton CloseAssemblyButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel ReviewTargetStatus;
        private System.Windows.Forms.ToolStripButton ApproveAssemblyButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton RejectAssemblyButton;
        private System.Windows.Forms.TabControl ReviewTabs;
        private System.Windows.Forms.TabPage ChangeRequestsTab;
        private System.Windows.Forms.TabPage ObservationsTab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox ChangeRequestInput;
        private System.Windows.Forms.DataGridView ChangeRequestsGrid;
        private System.Windows.Forms.ImageList TabIcons;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView OvservationsGrid;
        private System.Windows.Forms.ToolStrip ObservationToolStrip;
        private System.Windows.Forms.TextBox ObservationInput;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeRequestDescriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeRequestStatusColumn;
    }
}