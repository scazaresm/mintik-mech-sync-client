﻿namespace MechanicalSyncApp.UI.Forms
{
    partial class ManagementConsoleForm
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
            "Sergio Cazares",
            "test"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagementConsoleForm));
            this.Tabs = new System.Windows.Forms.TabControl();
            this.usersTabPage = new System.Windows.Forms.TabPage();
            this.UserList = new System.Windows.Forms.ListView();
            this.emailHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fullNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.displayNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.roleHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.enabledHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SynchronizerToolStrip = new System.Windows.Forms.ToolStrip();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.AddUserButton = new System.Windows.Forms.ToolStripButton();
            this.EditUserButton = new System.Windows.Forms.ToolStripButton();
            this.ResetPasswordButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FilterUserTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.syncTabPage = new System.Windows.Forms.TabPage();
            this.BrowseEDrawingsExePath = new System.Windows.Forms.Button();
            this.eDrawingsExePath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BrowsePublishingDirectory = new System.Windows.Forms.Button();
            this.PublishingDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BrowseSolidWorksExePath = new System.Windows.Forms.Button();
            this.SolidWorksExePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BrowseWorkspaceDirectory = new System.Windows.Forms.Button();
            this.ApplySyncChanges = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.EdrawingsViewerClsid = new System.Windows.Forms.TextBox();
            this.WorkspaceDirectory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UsersTabImages = new System.Windows.Forms.ImageList(this.components);
            this.Tabs.SuspendLayout();
            this.usersTabPage.SuspendLayout();
            this.SynchronizerToolStrip.SuspendLayout();
            this.syncTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.usersTabPage);
            this.Tabs.Controls.Add(this.syncTabPage);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.ImageList = this.UsersTabImages;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(728, 479);
            this.Tabs.TabIndex = 0;
            this.Tabs.SelectedIndexChanged += new System.EventHandler(this.Tabs_SelectedIndexChanged);
            // 
            // usersTabPage
            // 
            this.usersTabPage.Controls.Add(this.UserList);
            this.usersTabPage.Controls.Add(this.SynchronizerToolStrip);
            this.usersTabPage.ImageIndex = 0;
            this.usersTabPage.Location = new System.Drawing.Point(4, 31);
            this.usersTabPage.Name = "usersTabPage";
            this.usersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.usersTabPage.Size = new System.Drawing.Size(720, 444);
            this.usersTabPage.TabIndex = 0;
            this.usersTabPage.Text = "Users";
            this.usersTabPage.UseVisualStyleBackColor = true;
            // 
            // UserList
            // 
            this.UserList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.emailHeader,
            this.fullNameHeader,
            this.displayNameHeader,
            this.roleHeader,
            this.enabledHeader});
            this.UserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserList.FullRowSelect = true;
            this.UserList.HideSelection = false;
            this.UserList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.UserList.Location = new System.Drawing.Point(3, 28);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(714, 413);
            this.UserList.TabIndex = 17;
            this.UserList.UseCompatibleStateImageBehavior = false;
            this.UserList.View = System.Windows.Forms.View.Details;
            this.UserList.SelectedIndexChanged += new System.EventHandler(this.UserList_SelectedIndexChanged);
            // 
            // emailHeader
            // 
            this.emailHeader.Text = "Email";
            this.emailHeader.Width = 200;
            // 
            // fullNameHeader
            // 
            this.fullNameHeader.Text = "Full name";
            this.fullNameHeader.Width = 200;
            // 
            // displayNameHeader
            // 
            this.displayNameHeader.Text = "Display name";
            this.displayNameHeader.Width = 150;
            // 
            // roleHeader
            // 
            this.roleHeader.Text = "Role";
            this.roleHeader.Width = 100;
            // 
            // enabledHeader
            // 
            this.enabledHeader.Text = "Enabled";
            // 
            // SynchronizerToolStrip
            // 
            this.SynchronizerToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshButton,
            this.toolStripSeparator1,
            this.AddUserButton,
            this.EditUserButton,
            this.ResetPasswordButton,
            this.toolStripSeparator2,
            this.FilterUserTextBox,
            this.toolStripLabel1});
            this.SynchronizerToolStrip.Location = new System.Drawing.Point(3, 3);
            this.SynchronizerToolStrip.Name = "SynchronizerToolStrip";
            this.SynchronizerToolStrip.Size = new System.Drawing.Size(714, 25);
            this.SynchronizerToolStrip.TabIndex = 16;
            this.SynchronizerToolStrip.Text = "toolStrip2";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Image = global::MechanicalSyncApp.Properties.Resources.refresh_icon_24;
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(66, 22);
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.ToolTipText = "Refresh local files";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // AddUserButton
            // 
            this.AddUserButton.Image = global::MechanicalSyncApp.Properties.Resources.plus_icon_24;
            this.AddUserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddUserButton.Name = "AddUserButton";
            this.AddUserButton.Size = new System.Drawing.Size(49, 22);
            this.AddUserButton.Text = "Add";
            this.AddUserButton.ToolTipText = "Refresh local files";
            this.AddUserButton.Click += new System.EventHandler(this.AddUserButton_Click);
            // 
            // EditUserButton
            // 
            this.EditUserButton.Enabled = false;
            this.EditUserButton.Image = global::MechanicalSyncApp.Properties.Resources.edit_icon_24;
            this.EditUserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditUserButton.Name = "EditUserButton";
            this.EditUserButton.Size = new System.Drawing.Size(47, 22);
            this.EditUserButton.Text = "Edit";
            this.EditUserButton.ToolTipText = "Refresh local files";
            this.EditUserButton.Click += new System.EventHandler(this.EditUserButton_Click);
            // 
            // ResetPasswordButton
            // 
            this.ResetPasswordButton.Enabled = false;
            this.ResetPasswordButton.Image = global::MechanicalSyncApp.Properties.Resources.generate_keys_24_icon;
            this.ResetPasswordButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ResetPasswordButton.Name = "ResetPasswordButton";
            this.ResetPasswordButton.Size = new System.Drawing.Size(108, 22);
            this.ResetPasswordButton.Text = "Reset password";
            this.ResetPasswordButton.ToolTipText = "Refresh local files";
            this.ResetPasswordButton.Click += new System.EventHandler(this.ResetPasswordButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FilterUserTextBox
            // 
            this.FilterUserTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.FilterUserTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FilterUserTextBox.Name = "FilterUserTextBox";
            this.FilterUserTextBox.Size = new System.Drawing.Size(100, 25);
            this.FilterUserTextBox.TextChanged += new System.EventHandler(this.FilterUserTextBox_TextChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Filter:";
            // 
            // syncTabPage
            // 
            this.syncTabPage.Controls.Add(this.BrowseEDrawingsExePath);
            this.syncTabPage.Controls.Add(this.eDrawingsExePath);
            this.syncTabPage.Controls.Add(this.label5);
            this.syncTabPage.Controls.Add(this.BrowsePublishingDirectory);
            this.syncTabPage.Controls.Add(this.PublishingDirectory);
            this.syncTabPage.Controls.Add(this.label2);
            this.syncTabPage.Controls.Add(this.BrowseSolidWorksExePath);
            this.syncTabPage.Controls.Add(this.SolidWorksExePath);
            this.syncTabPage.Controls.Add(this.label1);
            this.syncTabPage.Controls.Add(this.BrowseWorkspaceDirectory);
            this.syncTabPage.Controls.Add(this.ApplySyncChanges);
            this.syncTabPage.Controls.Add(this.label4);
            this.syncTabPage.Controls.Add(this.EdrawingsViewerClsid);
            this.syncTabPage.Controls.Add(this.WorkspaceDirectory);
            this.syncTabPage.Controls.Add(this.label3);
            this.syncTabPage.ImageIndex = 1;
            this.syncTabPage.Location = new System.Drawing.Point(4, 31);
            this.syncTabPage.Name = "syncTabPage";
            this.syncTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.syncTabPage.Size = new System.Drawing.Size(720, 444);
            this.syncTabPage.TabIndex = 1;
            this.syncTabPage.Text = "Sync";
            this.syncTabPage.UseVisualStyleBackColor = true;
            // 
            // BrowseEDrawingsExePath
            // 
            this.BrowseEDrawingsExePath.Location = new System.Drawing.Point(527, 180);
            this.BrowseEDrawingsExePath.Name = "BrowseEDrawingsExePath";
            this.BrowseEDrawingsExePath.Size = new System.Drawing.Size(75, 23);
            this.BrowseEDrawingsExePath.TabIndex = 22;
            this.BrowseEDrawingsExePath.Text = "Browse...";
            this.BrowseEDrawingsExePath.UseVisualStyleBackColor = true;
            this.BrowseEDrawingsExePath.Click += new System.EventHandler(this.BrowseEDrawingsExePath_Click);
            // 
            // eDrawingsExePath
            // 
            this.eDrawingsExePath.Location = new System.Drawing.Point(11, 182);
            this.eDrawingsExePath.Name = "eDrawingsExePath";
            this.eDrawingsExePath.ReadOnly = true;
            this.eDrawingsExePath.Size = new System.Drawing.Size(510, 20);
            this.eDrawingsExePath.TabIndex = 20;
            this.eDrawingsExePath.TextChanged += new System.EventHandler(this.eDrawingsExePath_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "eDrawings Viewer executable path:";
            // 
            // BrowsePublishingDirectory
            // 
            this.BrowsePublishingDirectory.Location = new System.Drawing.Point(527, 76);
            this.BrowsePublishingDirectory.Name = "BrowsePublishingDirectory";
            this.BrowsePublishingDirectory.Size = new System.Drawing.Size(75, 23);
            this.BrowsePublishingDirectory.TabIndex = 19;
            this.BrowsePublishingDirectory.Text = "Browse...";
            this.BrowsePublishingDirectory.UseVisualStyleBackColor = true;
            this.BrowsePublishingDirectory.Click += new System.EventHandler(this.BrowsePublishingDirectory_Click);
            // 
            // PublishingDirectory
            // 
            this.PublishingDirectory.Location = new System.Drawing.Point(11, 78);
            this.PublishingDirectory.Name = "PublishingDirectory";
            this.PublishingDirectory.ReadOnly = true;
            this.PublishingDirectory.Size = new System.Drawing.Size(510, 20);
            this.PublishingDirectory.TabIndex = 17;
            this.PublishingDirectory.TextChanged += new System.EventHandler(this.PublishingDirectory_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Publishing directory:";
            // 
            // BrowseSolidWorksExePath
            // 
            this.BrowseSolidWorksExePath.Location = new System.Drawing.Point(527, 231);
            this.BrowseSolidWorksExePath.Name = "BrowseSolidWorksExePath";
            this.BrowseSolidWorksExePath.Size = new System.Drawing.Size(75, 23);
            this.BrowseSolidWorksExePath.TabIndex = 16;
            this.BrowseSolidWorksExePath.Text = "Browse...";
            this.BrowseSolidWorksExePath.UseVisualStyleBackColor = true;
            this.BrowseSolidWorksExePath.Click += new System.EventHandler(this.BrowseSolidWorksExePath_Click);
            // 
            // SolidWorksExePath
            // 
            this.SolidWorksExePath.Location = new System.Drawing.Point(11, 233);
            this.SolidWorksExePath.Name = "SolidWorksExePath";
            this.SolidWorksExePath.ReadOnly = true;
            this.SolidWorksExePath.Size = new System.Drawing.Size(510, 20);
            this.SolidWorksExePath.TabIndex = 14;
            this.SolidWorksExePath.TextChanged += new System.EventHandler(this.SolidWorksExePath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "SolidWorks executable path:";
            // 
            // BrowseWorkspaceDirectory
            // 
            this.BrowseWorkspaceDirectory.Location = new System.Drawing.Point(527, 28);
            this.BrowseWorkspaceDirectory.Name = "BrowseWorkspaceDirectory";
            this.BrowseWorkspaceDirectory.Size = new System.Drawing.Size(75, 23);
            this.BrowseWorkspaceDirectory.TabIndex = 10;
            this.BrowseWorkspaceDirectory.Text = "Browse...";
            this.BrowseWorkspaceDirectory.UseVisualStyleBackColor = true;
            this.BrowseWorkspaceDirectory.Click += new System.EventHandler(this.BrowseWorkspaceDirectory_Click);
            // 
            // ApplySyncChanges
            // 
            this.ApplySyncChanges.Enabled = false;
            this.ApplySyncChanges.Location = new System.Drawing.Point(527, 284);
            this.ApplySyncChanges.Name = "ApplySyncChanges";
            this.ApplySyncChanges.Size = new System.Drawing.Size(75, 23);
            this.ApplySyncChanges.TabIndex = 11;
            this.ApplySyncChanges.Text = "Apply";
            this.ApplySyncChanges.UseVisualStyleBackColor = true;
            this.ApplySyncChanges.Click += new System.EventHandler(this.ApplySyncChanges_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "eDrawings Viewer CLSID:";
            // 
            // EdrawingsViewerClsid
            // 
            this.EdrawingsViewerClsid.Location = new System.Drawing.Point(11, 129);
            this.EdrawingsViewerClsid.Name = "EdrawingsViewerClsid";
            this.EdrawingsViewerClsid.Size = new System.Drawing.Size(297, 20);
            this.EdrawingsViewerClsid.TabIndex = 12;
            this.EdrawingsViewerClsid.TextChanged += new System.EventHandler(this.EdrawingsViewerClsid_TextChanged);
            // 
            // WorkspaceDirectory
            // 
            this.WorkspaceDirectory.Location = new System.Drawing.Point(11, 30);
            this.WorkspaceDirectory.Name = "WorkspaceDirectory";
            this.WorkspaceDirectory.ReadOnly = true;
            this.WorkspaceDirectory.Size = new System.Drawing.Size(510, 20);
            this.WorkspaceDirectory.TabIndex = 8;
            this.WorkspaceDirectory.TextChanged += new System.EventHandler(this.WorkspaceDirectory_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Workspace directory:";
            // 
            // UsersTabImages
            // 
            this.UsersTabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("UsersTabImages.ImageStream")));
            this.UsersTabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.UsersTabImages.Images.SetKeyName(0, "users-icon-24.png");
            this.UsersTabImages.Images.SetKeyName(1, "sync-24.png");
            // 
            // ManagementConsoleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 479);
            this.Controls.Add(this.Tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManagementConsoleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mechanical Sync - Management Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManagementConsoleForm_FormClosing);
            this.Load += new System.EventHandler(this.ManagementConsoleForm_Load);
            this.Tabs.ResumeLayout(false);
            this.usersTabPage.ResumeLayout(false);
            this.usersTabPage.PerformLayout();
            this.SynchronizerToolStrip.ResumeLayout(false);
            this.SynchronizerToolStrip.PerformLayout();
            this.syncTabPage.ResumeLayout(false);
            this.syncTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage usersTabPage;
        private System.Windows.Forms.ToolStrip SynchronizerToolStrip;
        private System.Windows.Forms.ToolStripButton AddUserButton;
        private System.Windows.Forms.ToolStripButton EditUserButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ResetPasswordButton;
        private System.Windows.Forms.ListView UserList;
        private System.Windows.Forms.ColumnHeader fullNameHeader;
        private System.Windows.Forms.ColumnHeader emailHeader;
        private System.Windows.Forms.ColumnHeader displayNameHeader;
        private System.Windows.Forms.ColumnHeader roleHeader;
        private System.Windows.Forms.ImageList UsersTabImages;
        private System.Windows.Forms.ColumnHeader enabledHeader;
        private System.Windows.Forms.ToolStripTextBox FilterUserTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton RefreshButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabPage syncTabPage;
        private System.Windows.Forms.Button BrowseWorkspaceDirectory;
        private System.Windows.Forms.Button ApplySyncChanges;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox EdrawingsViewerClsid;
        private System.Windows.Forms.TextBox WorkspaceDirectory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BrowseSolidWorksExePath;
        private System.Windows.Forms.TextBox SolidWorksExePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BrowsePublishingDirectory;
        private System.Windows.Forms.TextBox PublishingDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BrowseEDrawingsExePath;
        private System.Windows.Forms.TextBox eDrawingsExePath;
        private System.Windows.Forms.Label label5;
    }
}