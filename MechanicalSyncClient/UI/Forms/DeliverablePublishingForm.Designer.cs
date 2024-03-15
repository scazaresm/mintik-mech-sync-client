namespace MechanicalSyncApp.UI.Forms
{
    partial class DeliverablePublishingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeliverablePublishingForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.DrawingsGridView = new System.Windows.Forms.DataGridView();
            this.DrawingIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.DrawingFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApprovalCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PublishingStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Progress = new System.Windows.Forms.ToolStripProgressBar();
            this.ViewBlockersButton = new System.Windows.Forms.ToolStripButton();
            this.CancelSelectedButton = new System.Windows.Forms.ToolStripButton();
            this.PublishSelectedButton = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingsGridView)).BeginInit();
            this.MainToolStrip.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DrawingsGridView);
            this.panel1.Controls.Add(this.MainToolStrip);
            this.panel1.Location = new System.Drawing.Point(16, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(613, 316);
            this.panel1.TabIndex = 6;
            // 
            // DrawingsGridView
            // 
            this.DrawingsGridView.AllowUserToAddRows = false;
            this.DrawingsGridView.AllowUserToResizeRows = false;
            this.DrawingsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DrawingsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrawingIcon,
            this.DrawingFile,
            this.ApprovalCount,
            this.PublishingStatus});
            this.DrawingsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawingsGridView.Location = new System.Drawing.Point(0, 25);
            this.DrawingsGridView.Name = "DrawingsGridView";
            this.DrawingsGridView.ReadOnly = true;
            this.DrawingsGridView.RowHeadersVisible = false;
            this.DrawingsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DrawingsGridView.Size = new System.Drawing.Size(613, 291);
            this.DrawingsGridView.TabIndex = 1;
            // 
            // DrawingIcon
            // 
            this.DrawingIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DrawingIcon.HeaderText = "";
            this.DrawingIcon.Name = "DrawingIcon";
            this.DrawingIcon.ReadOnly = true;
            this.DrawingIcon.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DrawingIcon.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DrawingIcon.Width = 19;
            // 
            // DrawingFile
            // 
            this.DrawingFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrawingFile.HeaderText = "Drawing file";
            this.DrawingFile.Name = "DrawingFile";
            this.DrawingFile.ReadOnly = true;
            // 
            // ApprovalCount
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ApprovalCount.DefaultCellStyle = dataGridViewCellStyle1;
            this.ApprovalCount.HeaderText = "Approvals";
            this.ApprovalCount.Name = "ApprovalCount";
            this.ApprovalCount.ReadOnly = true;
            this.ApprovalCount.Width = 80;
            // 
            // PublishingStatus
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PublishingStatus.DefaultCellStyle = dataGridViewCellStyle2;
            this.PublishingStatus.HeaderText = "Publishing status";
            this.PublishingStatus.Name = "PublishingStatus";
            this.PublishingStatus.ReadOnly = true;
            this.PublishingStatus.Width = 200;
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewBlockersButton,
            this.CancelSelectedButton,
            this.PublishSelectedButton});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(613, 25);
            this.MainToolStrip.TabIndex = 2;
            this.MainToolStrip.Text = "toolStrip1";
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.Progress});
            this.MainStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 357);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(644, 22);
            this.MainStatusStrip.TabIndex = 7;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(59, 17);
            this.StatusLabel.Text = "Loading...";
            // 
            // Progress
            // 
            this.Progress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(100, 16);
            // 
            // ViewBlockersButton
            // 
            this.ViewBlockersButton.Image = global::MechanicalSyncApp.Properties.Resources.error_alert_24;
            this.ViewBlockersButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewBlockersButton.Name = "ViewBlockersButton";
            this.ViewBlockersButton.Size = new System.Drawing.Size(99, 22);
            this.ViewBlockersButton.Text = "View blockers";
            this.ViewBlockersButton.ToolTipText = "Publish deliverables for manufacturing";
            // 
            // CancelSelectedButton
            // 
            this.CancelSelectedButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CancelSelectedButton.Image = global::MechanicalSyncApp.Properties.Resources.undo_24;
            this.CancelSelectedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CancelSelectedButton.Name = "CancelSelectedButton";
            this.CancelSelectedButton.Size = new System.Drawing.Size(109, 22);
            this.CancelSelectedButton.Text = "Cancel selected";
            this.CancelSelectedButton.ToolTipText = "Publish deliverables for manufacturing";
            // 
            // PublishSelectedButton
            // 
            this.PublishSelectedButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.PublishSelectedButton.Image = global::MechanicalSyncApp.Properties.Resources.paper_plane_24;
            this.PublishSelectedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PublishSelectedButton.Name = "PublishSelectedButton";
            this.PublishSelectedButton.Size = new System.Drawing.Size(112, 22);
            this.PublishSelectedButton.Text = "Publish selected";
            this.PublishSelectedButton.ToolTipText = "Publish deliverables for manufacturing";
            // 
            // DeliverablePublishingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 379);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeliverablePublishingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Publish deliverables";
            this.Load += new System.EventHandler(this.DeliverablePublishingForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingsGridView)).EndInit();
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DrawingsGridView;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton PublishSelectedButton;
        private System.Windows.Forms.ToolStripButton ViewBlockersButton;
        private System.Windows.Forms.ToolStripButton CancelSelectedButton;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripProgressBar Progress;
        private System.Windows.Forms.DataGridViewImageColumn DrawingIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawingFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApprovalCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PublishingStatus;
    }
}