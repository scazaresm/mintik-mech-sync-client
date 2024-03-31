namespace MechanicalSyncApp.UI.Forms
{
    partial class DrawingReviewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingReviewerForm));
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.DeltaDrawingsTreeView = new System.Windows.Forms.TreeView();
            this.SynchronizerToolStrip = new System.Windows.Forms.ToolStrip();
            this.RefreshReviewTargetsButton = new System.Windows.Forms.ToolStripButton();
            this.MarkupPanel = new System.Windows.Forms.Panel();
            this.MarkupOperatorsToolStrip = new System.Windows.Forms.ToolStrip();
            this.ChangeRequestButton = new System.Windows.Forms.ToolStripButton();
            this.PanButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomToAreaButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomButton = new System.Windows.Forms.ToolStripButton();
            this.ReviewMarkupToolStrip = new System.Windows.Forms.ToolStrip();
            this.CloseDrawingButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ReviewTargetStatus = new System.Windows.Forms.ToolStripLabel();
            this.SaveProgressButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ApproveDrawingButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.RejectDrawingButton = new System.Windows.Forms.ToolStripButton();
            this.MarkupStatusStrip = new System.Windows.Forms.StatusStrip();
            this.MarkupStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.DownloadProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.DesignerLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.TreeViewIcons = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
            this.MainSplit.Panel1.SuspendLayout();
            this.MainSplit.Panel2.SuspendLayout();
            this.MainSplit.SuspendLayout();
            this.SynchronizerToolStrip.SuspendLayout();
            this.MarkupPanel.SuspendLayout();
            this.MarkupOperatorsToolStrip.SuspendLayout();
            this.ReviewMarkupToolStrip.SuspendLayout();
            this.MarkupStatusStrip.SuspendLayout();
            this.HeaderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainSplit
            // 
            this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplit.Location = new System.Drawing.Point(0, 59);
            this.MainSplit.Name = "MainSplit";
            // 
            // MainSplit.Panel1
            // 
            this.MainSplit.Panel1.Controls.Add(this.DeltaDrawingsTreeView);
            this.MainSplit.Panel1.Controls.Add(this.SynchronizerToolStrip);
            // 
            // MainSplit.Panel2
            // 
            this.MainSplit.Panel2.Controls.Add(this.MarkupPanel);
            this.MainSplit.Panel2.Controls.Add(this.ReviewMarkupToolStrip);
            this.MainSplit.Panel2.Controls.Add(this.MarkupStatusStrip);
            this.MainSplit.Size = new System.Drawing.Size(974, 472);
            this.MainSplit.SplitterDistance = 274;
            this.MainSplit.TabIndex = 0;
            // 
            // DeltaDrawingsTreeView
            // 
            this.DeltaDrawingsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeltaDrawingsTreeView.Location = new System.Drawing.Point(0, 25);
            this.DeltaDrawingsTreeView.Name = "DeltaDrawingsTreeView";
            this.DeltaDrawingsTreeView.Size = new System.Drawing.Size(274, 447);
            this.DeltaDrawingsTreeView.TabIndex = 1;
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
            this.RefreshReviewTargetsButton.ToolTipText = "Refresh drawings";
            // 
            // MarkupPanel
            // 
            this.MarkupPanel.Controls.Add(this.MarkupOperatorsToolStrip);
            this.MarkupPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarkupPanel.Location = new System.Drawing.Point(0, 33);
            this.MarkupPanel.Name = "MarkupPanel";
            this.MarkupPanel.Size = new System.Drawing.Size(696, 417);
            this.MarkupPanel.TabIndex = 2;
            // 
            // MarkupOperatorsToolStrip
            // 
            this.MarkupOperatorsToolStrip.AutoSize = false;
            this.MarkupOperatorsToolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.MarkupOperatorsToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.MarkupOperatorsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangeRequestButton,
            this.PanButton,
            this.ZoomToAreaButton,
            this.ZoomButton});
            this.MarkupOperatorsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MarkupOperatorsToolStrip.Name = "MarkupOperatorsToolStrip";
            this.MarkupOperatorsToolStrip.Size = new System.Drawing.Size(40, 417);
            this.MarkupOperatorsToolStrip.TabIndex = 3;
            this.MarkupOperatorsToolStrip.Text = "toolStrip2";
            // 
            // ChangeRequestButton
            // 
            this.ChangeRequestButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ChangeRequestButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeRequestButton.Image = global::MechanicalSyncApp.Properties.Resources.error_file_icon_32;
            this.ChangeRequestButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ChangeRequestButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChangeRequestButton.Name = "ChangeRequestButton";
            this.ChangeRequestButton.Size = new System.Drawing.Size(38, 36);
            this.ChangeRequestButton.Text = "Zoom to area";
            this.ChangeRequestButton.ToolTipText = "Add change request";
            // 
            // PanButton
            // 
            this.PanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PanButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanButton.Image = global::MechanicalSyncApp.Properties.Resources.move_icon_32;
            this.PanButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PanButton.Name = "PanButton";
            this.PanButton.Size = new System.Drawing.Size(38, 36);
            this.PanButton.Text = "Pan";
            // 
            // ZoomToAreaButton
            // 
            this.ZoomToAreaButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomToAreaButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZoomToAreaButton.Image = global::MechanicalSyncApp.Properties.Resources.zoom_fit_32;
            this.ZoomToAreaButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZoomToAreaButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomToAreaButton.Name = "ZoomToAreaButton";
            this.ZoomToAreaButton.Size = new System.Drawing.Size(38, 36);
            this.ZoomToAreaButton.Text = "Zoom to area";
            // 
            // ZoomButton
            // 
            this.ZoomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZoomButton.Image = global::MechanicalSyncApp.Properties.Resources.zoom_32;
            this.ZoomButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZoomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomButton.Name = "ZoomButton";
            this.ZoomButton.Size = new System.Drawing.Size(38, 36);
            this.ZoomButton.Text = "Zoom";
            // 
            // ReviewMarkupToolStrip
            // 
            this.ReviewMarkupToolStrip.AutoSize = false;
            this.ReviewMarkupToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseDrawingButton,
            this.toolStripSeparator2,
            this.ReviewTargetStatus,
            this.SaveProgressButton,
            this.toolStripSeparator3,
            this.ApproveDrawingButton,
            this.toolStripSeparator1,
            this.RejectDrawingButton});
            this.ReviewMarkupToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ReviewMarkupToolStrip.Name = "ReviewMarkupToolStrip";
            this.ReviewMarkupToolStrip.Size = new System.Drawing.Size(696, 33);
            this.ReviewMarkupToolStrip.TabIndex = 4;
            this.ReviewMarkupToolStrip.Text = "toolStrip1";
            // 
            // CloseDrawingButton
            // 
            this.CloseDrawingButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CloseDrawingButton.Image = global::MechanicalSyncApp.Properties.Resources.close_16;
            this.CloseDrawingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseDrawingButton.Name = "CloseDrawingButton";
            this.CloseDrawingButton.Size = new System.Drawing.Size(56, 30);
            this.CloseDrawingButton.Text = "Close";
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
            // SaveProgressButton
            // 
            this.SaveProgressButton.Image = global::MechanicalSyncApp.Properties.Resources.save_icon_24;
            this.SaveProgressButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveProgressButton.Name = "SaveProgressButton";
            this.SaveProgressButton.Size = new System.Drawing.Size(99, 30);
            this.SaveProgressButton.Text = "Save progress";
            this.SaveProgressButton.ToolTipText = "Save progress (Ctrl + S)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            // 
            // ApproveDrawingButton
            // 
            this.ApproveDrawingButton.Image = global::MechanicalSyncApp.Properties.Resources.ok_apply_icon_24;
            this.ApproveDrawingButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ApproveDrawingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ApproveDrawingButton.Name = "ApproveDrawingButton";
            this.ApproveDrawingButton.Size = new System.Drawing.Size(80, 30);
            this.ApproveDrawingButton.Text = "Approve";
            this.ApproveDrawingButton.ToolTipText = "Approve drawing";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // RejectDrawingButton
            // 
            this.RejectDrawingButton.Image = global::MechanicalSyncApp.Properties.Resources.reject_24;
            this.RejectDrawingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RejectDrawingButton.Name = "RejectDrawingButton";
            this.RejectDrawingButton.Size = new System.Drawing.Size(59, 30);
            this.RejectDrawingButton.Text = "Reject";
            this.RejectDrawingButton.ToolTipText = "Reject drawing";
            // 
            // MarkupStatusStrip
            // 
            this.MarkupStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MarkupStatus,
            this.DownloadProgressBar});
            this.MarkupStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MarkupStatusStrip.Location = new System.Drawing.Point(0, 450);
            this.MarkupStatusStrip.Name = "MarkupStatusStrip";
            this.MarkupStatusStrip.Size = new System.Drawing.Size(696, 22);
            this.MarkupStatusStrip.TabIndex = 3;
            this.MarkupStatusStrip.Text = "statusStrip1";
            // 
            // MarkupStatus
            // 
            this.MarkupStatus.Name = "MarkupStatus";
            this.MarkupStatus.Size = new System.Drawing.Size(108, 17);
            this.MarkupStatus.Text = "Opening drawing...";
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(100, 16);
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
            this.HeaderPanel.TabIndex = 1;
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
            // DrawingReviewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 531);
            this.Controls.Add(this.MainSplit);
            this.Controls.Add(this.HeaderPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "DrawingReviewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "2D Review";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DrawingReviewerForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DrawingReviewerForm_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DrawingReviewerForm_KeyDown);
            this.MainSplit.Panel1.ResumeLayout(false);
            this.MainSplit.Panel1.PerformLayout();
            this.MainSplit.Panel2.ResumeLayout(false);
            this.MainSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
            this.MainSplit.ResumeLayout(false);
            this.SynchronizerToolStrip.ResumeLayout(false);
            this.SynchronizerToolStrip.PerformLayout();
            this.MarkupPanel.ResumeLayout(false);
            this.MarkupOperatorsToolStrip.ResumeLayout(false);
            this.MarkupOperatorsToolStrip.PerformLayout();
            this.ReviewMarkupToolStrip.ResumeLayout(false);
            this.ReviewMarkupToolStrip.PerformLayout();
            this.MarkupStatusStrip.ResumeLayout(false);
            this.MarkupStatusStrip.PerformLayout();
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplit;
        private System.Windows.Forms.TreeView DeltaDrawingsTreeView;
        private System.Windows.Forms.Panel MarkupPanel;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.StatusStrip MarkupStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel MarkupStatus;
        private System.Windows.Forms.ToolStrip MarkupOperatorsToolStrip;
        private System.Windows.Forms.ToolStripButton PanButton;
        private System.Windows.Forms.ToolStripButton ZoomToAreaButton;
        private System.Windows.Forms.ToolStripButton ZoomButton;
        private System.Windows.Forms.ToolStrip ReviewMarkupToolStrip;
        private System.Windows.Forms.ToolStripButton CloseDrawingButton;
        private System.Windows.Forms.ToolStripProgressBar DownloadProgressBar;
        private System.Windows.Forms.ToolStripButton ChangeRequestButton;
        private System.Windows.Forms.ToolStripButton RejectDrawingButton;
        private System.Windows.Forms.ToolStripButton SaveProgressButton;
        private System.Windows.Forms.ToolStripButton ApproveDrawingButton;
        private System.Windows.Forms.ToolStripLabel ReviewTargetStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label DesignerLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList TreeViewIcons;
        private System.Windows.Forms.ToolStrip SynchronizerToolStrip;
        private System.Windows.Forms.ToolStripButton RefreshReviewTargetsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}