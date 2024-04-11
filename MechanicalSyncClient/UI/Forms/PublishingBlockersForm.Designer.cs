namespace MechanicalSyncApp.UI.Forms
{
    partial class PublishingBlockersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublishingBlockersForm));
            this.Header = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.IssuesGrid = new System.Windows.Forms.DataGridView();
            this.IssueDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.IssuesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.Location = new System.Drawing.Point(13, 17);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(384, 38);
            this.Header.TabIndex = 0;
            this.Header.Text = "Loading...";
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OkButton.Location = new System.Drawing.Point(322, 298);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // IssuesGrid
            // 
            this.IssuesGrid.AllowUserToAddRows = false;
            this.IssuesGrid.AllowUserToDeleteRows = false;
            this.IssuesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IssuesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IssueDetails});
            this.IssuesGrid.Location = new System.Drawing.Point(13, 58);
            this.IssuesGrid.MultiSelect = false;
            this.IssuesGrid.Name = "IssuesGrid";
            this.IssuesGrid.ReadOnly = true;
            this.IssuesGrid.RowHeadersVisible = false;
            this.IssuesGrid.Size = new System.Drawing.Size(384, 227);
            this.IssuesGrid.TabIndex = 3;
            // 
            // IssueDetails
            // 
            this.IssueDetails.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IssueDetails.DefaultCellStyle = dataGridViewCellStyle1;
            this.IssueDetails.HeaderText = "Issue details";
            this.IssueDetails.Name = "IssueDetails";
            this.IssueDetails.ReadOnly = true;
            // 
            // PublishingBlockersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.OkButton;
            this.ClientSize = new System.Drawing.Size(410, 335);
            this.Controls.Add(this.IssuesGrid);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PublishingBlockersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Publishing blockers";
            this.Load += new System.EventHandler(this.PublishingBlockersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IssuesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.DataGridView IssuesGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssueDetails;
    }
}