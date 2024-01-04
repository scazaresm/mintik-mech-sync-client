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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingReviewerForm));
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.DeltaDrawingsTreeView = new System.Windows.Forms.TreeView();
            this.MarkupPanel = new System.Windows.Forms.Panel();
            this.MarkupOperatorsToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.MarkupStatusStrip = new System.Windows.Forms.StatusStrip();
            this.MarkupStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.DownloadProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ChangeRequestButton = new System.Windows.Forms.ToolStripButton();
            this.PanButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomToAreaButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomButton = new System.Windows.Forms.ToolStripButton();
            this.CloseDrawingButton = new System.Windows.Forms.ToolStripButton();
            this.SaveProgressButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
            this.MainSplit.Panel1.SuspendLayout();
            this.MainSplit.Panel2.SuspendLayout();
            this.MainSplit.SuspendLayout();
            this.MarkupPanel.SuspendLayout();
            this.MarkupOperatorsToolStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.MarkupStatusStrip.SuspendLayout();
            this.HeaderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainSplit
            // 
            this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplit.Location = new System.Drawing.Point(0, 25);
            this.MainSplit.Name = "MainSplit";
            // 
            // MainSplit.Panel1
            // 
            this.MainSplit.Panel1.Controls.Add(this.DeltaDrawingsTreeView);
            // 
            // MainSplit.Panel2
            // 
            this.MainSplit.Panel2.Controls.Add(this.MarkupPanel);
            this.MainSplit.Panel2.Controls.Add(this.toolStrip1);
            this.MainSplit.Panel2.Controls.Add(this.MarkupStatusStrip);
            this.MainSplit.Size = new System.Drawing.Size(977, 506);
            this.MainSplit.SplitterDistance = 279;
            this.MainSplit.TabIndex = 0;
            // 
            // DeltaDrawingsTreeView
            // 
            this.DeltaDrawingsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeltaDrawingsTreeView.Location = new System.Drawing.Point(0, 0);
            this.DeltaDrawingsTreeView.Name = "DeltaDrawingsTreeView";
            this.DeltaDrawingsTreeView.Size = new System.Drawing.Size(279, 506);
            this.DeltaDrawingsTreeView.TabIndex = 1;
            // 
            // MarkupPanel
            // 
            this.MarkupPanel.Controls.Add(this.MarkupOperatorsToolStrip);
            this.MarkupPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarkupPanel.Location = new System.Drawing.Point(0, 33);
            this.MarkupPanel.Name = "MarkupPanel";
            this.MarkupPanel.Size = new System.Drawing.Size(694, 451);
            this.MarkupPanel.TabIndex = 2;
            // 
            // MarkupOperatorsToolStrip
            // 
            this.MarkupOperatorsToolStrip.AutoSize = false;
            this.MarkupOperatorsToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.MarkupOperatorsToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.MarkupOperatorsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangeRequestButton,
            this.PanButton,
            this.ZoomToAreaButton,
            this.ZoomButton});
            this.MarkupOperatorsToolStrip.Location = new System.Drawing.Point(654, 0);
            this.MarkupOperatorsToolStrip.Name = "MarkupOperatorsToolStrip";
            this.MarkupOperatorsToolStrip.Size = new System.Drawing.Size(40, 451);
            this.MarkupOperatorsToolStrip.TabIndex = 3;
            this.MarkupOperatorsToolStrip.Text = "toolStrip2";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseDrawingButton,
            this.SaveProgressButton,
            this.toolStripSeparator1,
            this.toolStripButton3,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(694, 33);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // MarkupStatusStrip
            // 
            this.MarkupStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MarkupStatus,
            this.DownloadProgressBar});
            this.MarkupStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MarkupStatusStrip.Location = new System.Drawing.Point(0, 484);
            this.MarkupStatusStrip.Name = "MarkupStatusStrip";
            this.MarkupStatusStrip.Size = new System.Drawing.Size(694, 22);
            this.MarkupStatusStrip.TabIndex = 3;
            this.MarkupStatusStrip.Text = "statusStrip1";
            // 
            // MarkupStatus
            // 
            this.MarkupStatus.Name = "MarkupStatus";
            this.MarkupStatus.Size = new System.Drawing.Size(122, 17);
            this.MarkupStatus.Text = "Loading 2D drawing...";
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Controls.Add(this.HeaderLabel);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(977, 25);
            this.HeaderPanel.TabIndex = 1;
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderLabel.Location = new System.Drawing.Point(0, 0);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(977, 25);
            this.HeaderLabel.TabIndex = 0;
            this.HeaderLabel.Text = "Reviewing...";
            this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
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
            // CloseDrawingButton
            // 
            this.CloseDrawingButton.Image = global::MechanicalSyncApp.Properties.Resources.close_16;
            this.CloseDrawingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseDrawingButton.Name = "CloseDrawingButton";
            this.CloseDrawingButton.Size = new System.Drawing.Size(102, 30);
            this.CloseDrawingButton.Text = "Close drawing";
            // 
            // SaveProgressButton
            // 
            this.SaveProgressButton.Image = global::MechanicalSyncApp.Properties.Resources.save_icon_24;
            this.SaveProgressButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveProgressButton.Name = "SaveProgressButton";
            this.SaveProgressButton.Size = new System.Drawing.Size(99, 30);
            this.SaveProgressButton.Text = "Save progress";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::MechanicalSyncApp.Properties.Resources.ok_apply_icon_24;
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(80, 30);
            this.toolStripButton3.Text = "Approve";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::MechanicalSyncApp.Properties.Resources.delete_icon_24;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(59, 30);
            this.toolStripButton2.Text = "Reject";
            // 
            // DrawingReviewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 531);
            this.Controls.Add(this.MainSplit);
            this.Controls.Add(this.HeaderPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DrawingReviewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "2D Review";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DrawingReviewerForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DrawingReviewerForm_FormClosed);
            this.MainSplit.Panel1.ResumeLayout(false);
            this.MainSplit.Panel2.ResumeLayout(false);
            this.MainSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
            this.MainSplit.ResumeLayout(false);
            this.MarkupPanel.ResumeLayout(false);
            this.MarkupOperatorsToolStrip.ResumeLayout(false);
            this.MarkupOperatorsToolStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.MarkupStatusStrip.ResumeLayout(false);
            this.MarkupStatusStrip.PerformLayout();
            this.HeaderPanel.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton CloseDrawingButton;
        private System.Windows.Forms.ToolStripProgressBar DownloadProgressBar;
        private System.Windows.Forms.ToolStripButton ChangeRequestButton;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton SaveProgressButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
    }
}