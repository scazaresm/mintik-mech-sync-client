namespace MechanicalSyncApp.UI.Forms
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
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
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
            this.SynchronizerToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.UsersTabImages = new System.Windows.Forms.ImageList(this.components);
            this.enabledHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilterUserTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.AddUserButton = new System.Windows.Forms.ToolStripButton();
            this.EditUserButton = new System.Windows.Forms.ToolStripButton();
            this.ResetPasswordButton = new System.Windows.Forms.ToolStripButton();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Tabs.SuspendLayout();
            this.usersTabPage.SuspendLayout();
            this.SynchronizerToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.usersTabPage);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.ImageList = this.UsersTabImages;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(728, 479);
            this.Tabs.TabIndex = 0;
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
            listViewItem3});
            this.UserList.Location = new System.Drawing.Point(3, 28);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(714, 413);
            this.UserList.TabIndex = 17;
            this.UserList.UseCompatibleStateImageBehavior = false;
            this.UserList.View = System.Windows.Forms.View.Details;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // UsersTabImages
            // 
            this.UsersTabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("UsersTabImages.ImageStream")));
            this.UsersTabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.UsersTabImages.Images.SetKeyName(0, "users-icon-24.png");
            // 
            // enabledHeader
            // 
            this.enabledHeader.Text = "Enabled";
            // 
            // FilterUserTextBox
            // 
            this.FilterUserTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.FilterUserTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FilterUserTextBox.Name = "FilterUserTextBox";
            this.FilterUserTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Filter:";
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
            this.EditUserButton.Image = global::MechanicalSyncApp.Properties.Resources.edit_icon_24;
            this.EditUserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditUserButton.Name = "EditUserButton";
            this.EditUserButton.Size = new System.Drawing.Size(47, 22);
            this.EditUserButton.Text = "Edit";
            this.EditUserButton.ToolTipText = "Refresh local files";
            // 
            // ResetPasswordButton
            // 
            this.ResetPasswordButton.Image = global::MechanicalSyncApp.Properties.Resources.generate_keys_24_icon;
            this.ResetPasswordButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ResetPasswordButton.Name = "ResetPasswordButton";
            this.ResetPasswordButton.Size = new System.Drawing.Size(108, 22);
            this.ResetPasswordButton.Text = "Reset password";
            this.ResetPasswordButton.ToolTipText = "Refresh local files";
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
    }
}