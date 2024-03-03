namespace MechanicalSyncApp.UI.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssemblyReviewerForm));
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.DesignerLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.DeltaDrawingsTreeView = new System.Windows.Forms.TreeView();
            this.SynchronizerToolStrip = new System.Windows.Forms.ToolStrip();
            this.RefreshReviewTargetsButton = new System.Windows.Forms.ToolStripButton();
            this.ReviewMarkupToolStrip = new System.Windows.Forms.ToolStrip();
            this.SaveProgressButton = new System.Windows.Forms.ToolStripButton();
            this.ApproveDrawingButton = new System.Windows.Forms.ToolStripButton();
            this.RejectDrawingButton = new System.Windows.Forms.ToolStripButton();
            this.MarkupStatusStrip = new System.Windows.Forms.StatusStrip();
            this.MarkupStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.DownloadProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
            this.MainSplit.Panel1.SuspendLayout();
            this.MainSplit.Panel2.SuspendLayout();
            this.MainSplit.SuspendLayout();
            this.SynchronizerToolStrip.SuspendLayout();
            this.ReviewMarkupToolStrip.SuspendLayout();
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
            this.MainSplit.Panel1.Controls.Add(this.DeltaDrawingsTreeView);
            this.MainSplit.Panel1.Controls.Add(this.SynchronizerToolStrip);
            // 
            // MainSplit.Panel2
            // 
            this.MainSplit.Panel2.Controls.Add(this.ReviewMarkupToolStrip);
            this.MainSplit.Panel2.Controls.Add(this.MarkupStatusStrip);
            this.MainSplit.Size = new System.Drawing.Size(974, 472);
            this.MainSplit.SplitterDistance = 274;
            this.MainSplit.TabIndex = 3;
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
            this.RefreshReviewTargetsButton.ToolTipText = "Refresh local files";
            // 
            // ReviewMarkupToolStrip
            // 
            this.ReviewMarkupToolStrip.AutoSize = false;
            this.ReviewMarkupToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ApproveDrawingButton,
            this.toolStripSeparator2,
            this.RejectDrawingButton,
            this.toolStripSeparator1,
            this.SaveProgressButton,
            this.toolStripLabel1});
            this.ReviewMarkupToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ReviewMarkupToolStrip.Name = "ReviewMarkupToolStrip";
            this.ReviewMarkupToolStrip.Size = new System.Drawing.Size(696, 33);
            this.ReviewMarkupToolStrip.TabIndex = 4;
            this.ReviewMarkupToolStrip.Text = "toolStrip1";
            // 
            // SaveProgressButton
            // 
            this.SaveProgressButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.SaveProgressButton.Image = global::MechanicalSyncApp.Properties.Resources.save_icon_24;
            this.SaveProgressButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveProgressButton.Name = "SaveProgressButton";
            this.SaveProgressButton.Size = new System.Drawing.Size(99, 30);
            this.SaveProgressButton.Text = "Save progress";
            this.SaveProgressButton.ToolTipText = "Save progress (Ctrl + S)";
            // 
            // ApproveDrawingButton
            // 
            this.ApproveDrawingButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ApproveDrawingButton.Image = global::MechanicalSyncApp.Properties.Resources.ok_apply_icon_24;
            this.ApproveDrawingButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ApproveDrawingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ApproveDrawingButton.Name = "ApproveDrawingButton";
            this.ApproveDrawingButton.Size = new System.Drawing.Size(80, 30);
            this.ApproveDrawingButton.Text = "Approve";
            this.ApproveDrawingButton.ToolTipText = "Approve drawing";
            // 
            // RejectDrawingButton
            // 
            this.RejectDrawingButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RejectDrawingButton.Image = global::MechanicalSyncApp.Properties.Resources.delete_icon_24;
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
            this.MarkupStatus.Size = new System.Drawing.Size(59, 17);
            this.MarkupStatus.Text = "Loading...";
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(59, 30);
            this.toolStripLabel1.Text = "Loading...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
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
            this.ReviewMarkupToolStrip.ResumeLayout(false);
            this.ReviewMarkupToolStrip.PerformLayout();
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
        private System.Windows.Forms.TreeView DeltaDrawingsTreeView;
        private System.Windows.Forms.ToolStrip SynchronizerToolStrip;
        private System.Windows.Forms.ToolStripButton RefreshReviewTargetsButton;
        private System.Windows.Forms.ToolStrip ReviewMarkupToolStrip;
        private System.Windows.Forms.ToolStripButton SaveProgressButton;
        private System.Windows.Forms.ToolStripButton ApproveDrawingButton;
        private System.Windows.Forms.ToolStripButton RejectDrawingButton;
        private System.Windows.Forms.StatusStrip MarkupStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel MarkupStatus;
        private System.Windows.Forms.ToolStripProgressBar DownloadProgressBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}