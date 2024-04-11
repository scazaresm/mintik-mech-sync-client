namespace MechanicalSyncApp.UI.Forms
{
    partial class VersionSelectorForm
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
            this.SearchLabel = new System.Windows.Forms.Label();
            this.SearchFilter = new System.Windows.Forms.TextBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.CancelVersionSelectButton = new System.Windows.Forms.Button();
            this.VersionList = new System.Windows.Forms.ListView();
            this.versionNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ownerHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OkButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(192, 61);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(44, 13);
            this.SearchLabel.TabIndex = 12;
            this.SearchLabel.Text = "Search:";
            // 
            // SearchFilter
            // 
            this.SearchFilter.Location = new System.Drawing.Point(242, 58);
            this.SearchFilter.Name = "SearchFilter";
            this.SearchFilter.Size = new System.Drawing.Size(177, 20);
            this.SearchFilter.TabIndex = 11;
            this.SearchFilter.TextChanged += new System.EventHandler(this.SearchFilter_TextChanged);
            // 
            // MessageLabel
            // 
            this.MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(12, 9);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(406, 35);
            this.MessageLabel.TabIndex = 10;
            this.MessageLabel.Text = "Select a version:";
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CancelVersionSelectButton
            // 
            this.CancelVersionSelectButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelVersionSelectButton.Location = new System.Drawing.Point(262, 313);
            this.CancelVersionSelectButton.Name = "CancelVersionSelectButton";
            this.CancelVersionSelectButton.Size = new System.Drawing.Size(75, 23);
            this.CancelVersionSelectButton.TabIndex = 9;
            this.CancelVersionSelectButton.Text = "Cancel";
            this.CancelVersionSelectButton.UseVisualStyleBackColor = true;
            this.CancelVersionSelectButton.Click += new System.EventHandler(this.CancelVersionSelectButton_Click);
            // 
            // VersionList
            // 
            this.VersionList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.versionNameHeader,
            this.ownerHeader});
            this.VersionList.FullRowSelect = true;
            this.VersionList.HideSelection = false;
            this.VersionList.Location = new System.Drawing.Point(12, 88);
            this.VersionList.Name = "VersionList";
            this.VersionList.Size = new System.Drawing.Size(406, 210);
            this.VersionList.TabIndex = 8;
            this.VersionList.UseCompatibleStateImageBehavior = false;
            this.VersionList.View = System.Windows.Forms.View.Details;
            this.VersionList.SelectedIndexChanged += new System.EventHandler(this.VersionList_SelectedIndexChanged);
            // 
            // versionNameHeader
            // 
            this.versionNameHeader.Text = "Version";
            this.versionNameHeader.Width = 270;
            // 
            // ownerHeader
            // 
            this.ownerHeader.Text = "Owner";
            this.ownerHeader.Width = 1120;
            // 
            // OkButton
            // 
            this.OkButton.Enabled = false;
            this.OkButton.Location = new System.Drawing.Point(343, 313);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 7;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(12, 56);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 13;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // VersionSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelVersionSelectButton;
            this.ClientSize = new System.Drawing.Size(431, 351);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.SearchFilter);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.CancelVersionSelectButton);
            this.Controls.Add(this.VersionList);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select version";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.TextBox SearchFilter;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button CancelVersionSelectButton;
        private System.Windows.Forms.ListView VersionList;
        private System.Windows.Forms.ColumnHeader versionNameHeader;
        private System.Windows.Forms.ColumnHeader ownerHeader;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button RefreshButton;
    }
}