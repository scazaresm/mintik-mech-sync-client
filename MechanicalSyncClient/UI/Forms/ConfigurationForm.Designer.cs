namespace MechanicalSyncApp.UI.Forms
{
    partial class ConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ServerPage = new System.Windows.Forms.TabPage();
            this.ApplyServerChanges = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerTimeout = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.RemoteServer = new System.Windows.Forms.TextBox();
            this.SyncPage = new System.Windows.Forms.TabPage();
            this.ApplySyncChanges = new System.Windows.Forms.Button();
            this.BrowseWorkspaceDirectory = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.WorkspaceDirectory = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.EdrawingsViewerClsid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.ServerPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerTimeout)).BeginInit();
            this.SyncPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ServerPage);
            this.tabControl1.Controls.Add(this.SyncPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(346, 328);
            this.tabControl1.TabIndex = 0;
            // 
            // ServerPage
            // 
            this.ServerPage.Controls.Add(this.ApplyServerChanges);
            this.ServerPage.Controls.Add(this.label2);
            this.ServerPage.Controls.Add(this.ServerTimeout);
            this.ServerPage.Controls.Add(this.label1);
            this.ServerPage.Controls.Add(this.RemoteServer);
            this.ServerPage.Location = new System.Drawing.Point(4, 22);
            this.ServerPage.Name = "ServerPage";
            this.ServerPage.Padding = new System.Windows.Forms.Padding(3);
            this.ServerPage.Size = new System.Drawing.Size(338, 302);
            this.ServerPage.TabIndex = 1;
            this.ServerPage.Text = "Server";
            this.ServerPage.UseVisualStyleBackColor = true;
            // 
            // ApplyServerChanges
            // 
            this.ApplyServerChanges.Enabled = false;
            this.ApplyServerChanges.Location = new System.Drawing.Point(18, 124);
            this.ApplyServerChanges.Name = "ApplyServerChanges";
            this.ApplyServerChanges.Size = new System.Drawing.Size(75, 23);
            this.ApplyServerChanges.TabIndex = 4;
            this.ApplyServerChanges.Text = "Apply";
            this.ApplyServerChanges.UseVisualStyleBackColor = true;
            this.ApplyServerChanges.Click += new System.EventHandler(this.ApplyServerChanges_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Timeout (seconds)";
            // 
            // ServerTimeout
            // 
            this.ServerTimeout.Location = new System.Drawing.Point(18, 86);
            this.ServerTimeout.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ServerTimeout.Name = "ServerTimeout";
            this.ServerTimeout.Size = new System.Drawing.Size(71, 20);
            this.ServerTimeout.TabIndex = 2;
            this.ServerTimeout.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ServerTimeout.ValueChanged += new System.EventHandler(this.ServerTimeout_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(303, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Remote server URL (for example: http://192.168.100.10:8085)";
            // 
            // RemoteServer
            // 
            this.RemoteServer.Location = new System.Drawing.Point(18, 37);
            this.RemoteServer.Name = "RemoteServer";
            this.RemoteServer.Size = new System.Drawing.Size(297, 20);
            this.RemoteServer.TabIndex = 0;
            this.RemoteServer.TextChanged += new System.EventHandler(this.RemoteServer_TextChanged);
            // 
            // SyncPage
            // 
            this.SyncPage.Controls.Add(this.label4);
            this.SyncPage.Controls.Add(this.EdrawingsViewerClsid);
            this.SyncPage.Controls.Add(this.ApplySyncChanges);
            this.SyncPage.Controls.Add(this.BrowseWorkspaceDirectory);
            this.SyncPage.Controls.Add(this.label3);
            this.SyncPage.Controls.Add(this.WorkspaceDirectory);
            this.SyncPage.Location = new System.Drawing.Point(4, 22);
            this.SyncPage.Name = "SyncPage";
            this.SyncPage.Padding = new System.Windows.Forms.Padding(3);
            this.SyncPage.Size = new System.Drawing.Size(338, 302);
            this.SyncPage.TabIndex = 0;
            this.SyncPage.Text = "Sync";
            this.SyncPage.UseVisualStyleBackColor = true;
            // 
            // ApplySyncChanges
            // 
            this.ApplySyncChanges.Enabled = false;
            this.ApplySyncChanges.Location = new System.Drawing.Point(18, 182);
            this.ApplySyncChanges.Name = "ApplySyncChanges";
            this.ApplySyncChanges.Size = new System.Drawing.Size(75, 23);
            this.ApplySyncChanges.TabIndex = 5;
            this.ApplySyncChanges.Text = "Apply";
            this.ApplySyncChanges.UseVisualStyleBackColor = true;
            this.ApplySyncChanges.Click += new System.EventHandler(this.ApplySyncChanges_Click);
            // 
            // BrowseWorkspaceDirectory
            // 
            this.BrowseWorkspaceDirectory.Location = new System.Drawing.Point(240, 63);
            this.BrowseWorkspaceDirectory.Name = "BrowseWorkspaceDirectory";
            this.BrowseWorkspaceDirectory.Size = new System.Drawing.Size(75, 23);
            this.BrowseWorkspaceDirectory.TabIndex = 3;
            this.BrowseWorkspaceDirectory.Text = "Browse...";
            this.BrowseWorkspaceDirectory.UseVisualStyleBackColor = true;
            this.BrowseWorkspaceDirectory.Click += new System.EventHandler(this.BrowseWorkspaceDirectory_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Workspace directory:";
            // 
            // WorkspaceDirectory
            // 
            this.WorkspaceDirectory.Location = new System.Drawing.Point(18, 37);
            this.WorkspaceDirectory.Name = "WorkspaceDirectory";
            this.WorkspaceDirectory.ReadOnly = true;
            this.WorkspaceDirectory.Size = new System.Drawing.Size(297, 20);
            this.WorkspaceDirectory.TabIndex = 0;
            this.WorkspaceDirectory.TextChanged += new System.EventHandler(this.WorkspaceDirectory_TextChanged);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(279, 349);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // EdrawingsViewerClsid
            // 
            this.EdrawingsViewerClsid.Location = new System.Drawing.Point(18, 112);
            this.EdrawingsViewerClsid.Name = "EdrawingsViewerClsid";
            this.EdrawingsViewerClsid.Size = new System.Drawing.Size(297, 20);
            this.EdrawingsViewerClsid.TabIndex = 6;
            this.EdrawingsViewerClsid.TextChanged += new System.EventHandler(this.EdrawingsViewerClsid_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "eDrawings Viewer CLSID:";
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 384);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application settings";
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.ServerPage.ResumeLayout(false);
            this.ServerPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerTimeout)).EndInit();
            this.SyncPage.ResumeLayout(false);
            this.SyncPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ServerPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox RemoteServer;
        private System.Windows.Forms.TabPage SyncPage;
        private System.Windows.Forms.NumericUpDown ServerTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox WorkspaceDirectory;
        private System.Windows.Forms.Button BrowseWorkspaceDirectory;
        private System.Windows.Forms.Button ApplyServerChanges;
        private System.Windows.Forms.Button ApplySyncChanges;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox EdrawingsViewerClsid;
    }
}