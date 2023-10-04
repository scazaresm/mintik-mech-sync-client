
namespace MechanicalSyncApp.UI.Forms
{
    partial class DemoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("220214NY-OBERG SML HEAD TOOLING");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Owned by me", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Owned by others");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Ongoing", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("2023");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("2024");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Published", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Assemblies", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Parts", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Drawings", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Example",
            "Deleted",
            "other"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Other",
            "Created"}, "Hopstarter-Sleek-Xp-Basic-Document-Blank.32.png");
            this.InitSynchronizerButton = new System.Windows.Forms.Button();
            this.StartMonitoringButton = new System.Windows.Forms.Button();
            this.StopMonitoringButton = new System.Windows.Forms.Button();
            this.LoginButton = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.ProcessEventsButton = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.NavigationTree = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // InitSynchronizerButton
            // 
            this.InitSynchronizerButton.Location = new System.Drawing.Point(419, 12);
            this.InitSynchronizerButton.Name = "InitSynchronizerButton";
            this.InitSynchronizerButton.Size = new System.Drawing.Size(131, 23);
            this.InitSynchronizerButton.TabIndex = 0;
            this.InitSynchronizerButton.Text = "Initialize synchronizer";
            this.InitSynchronizerButton.UseVisualStyleBackColor = true;
            this.InitSynchronizerButton.Click += new System.EventHandler(this.InitSynchronizerButton_Click);
            // 
            // StartMonitoringButton
            // 
            this.StartMonitoringButton.Location = new System.Drawing.Point(556, 12);
            this.StartMonitoringButton.Name = "StartMonitoringButton";
            this.StartMonitoringButton.Size = new System.Drawing.Size(131, 23);
            this.StartMonitoringButton.TabIndex = 1;
            this.StartMonitoringButton.Text = "Start monitoring";
            this.StartMonitoringButton.UseVisualStyleBackColor = true;
            this.StartMonitoringButton.Click += new System.EventHandler(this.StartMonitoringButton_Click);
            // 
            // StopMonitoringButton
            // 
            this.StopMonitoringButton.Location = new System.Drawing.Point(693, 12);
            this.StopMonitoringButton.Name = "StopMonitoringButton";
            this.StopMonitoringButton.Size = new System.Drawing.Size(131, 23);
            this.StopMonitoringButton.TabIndex = 2;
            this.StopMonitoringButton.Text = "Stop monitoring";
            this.StopMonitoringButton.UseVisualStyleBackColor = true;
            this.StopMonitoringButton.Click += new System.EventHandler(this.StopMonitoringButton_Click);
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(12, 12);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 3;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 557);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(973, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(150, 22);
            // 
            // ProcessEventsButton
            // 
            this.ProcessEventsButton.Enabled = false;
            this.ProcessEventsButton.Location = new System.Drawing.Point(830, 12);
            this.ProcessEventsButton.Name = "ProcessEventsButton";
            this.ProcessEventsButton.Size = new System.Drawing.Size(131, 23);
            this.ProcessEventsButton.TabIndex = 10;
            this.ProcessEventsButton.Text = "Process events";
            this.ProcessEventsButton.UseVisualStyleBackColor = true;
            this.ProcessEventsButton.Click += new System.EventHandler(this.ProcessEventsButton_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "ok-icon-32.png");
            this.imageList2.Images.SetKeyName(1, "sync-icon-32.png");
            this.imageList2.Images.SetKeyName(2, "error-icon-32.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 48);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.NavigationTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(973, 509);
            this.splitContainer1.SplitterDistance = 278;
            this.splitContainer1.TabIndex = 16;
            // 
            // NavigationTree
            // 
            this.NavigationTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigationTree.Location = new System.Drawing.Point(0, 0);
            this.NavigationTree.Name = "NavigationTree";
            treeNode1.Name = "Node2";
            treeNode1.Text = "220214NY-OBERG SML HEAD TOOLING";
            treeNode2.Name = "Node3";
            treeNode2.Text = "Owned by me";
            treeNode3.Name = "Node6";
            treeNode3.Text = "Owned by others";
            treeNode4.Name = "Node0";
            treeNode4.Text = "Ongoing";
            treeNode5.Name = "Node4";
            treeNode5.Text = "2023";
            treeNode6.Name = "Node5";
            treeNode6.Text = "2024";
            treeNode7.Name = "Node1";
            treeNode7.Text = "Published";
            this.NavigationTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode7});
            this.NavigationTree.Size = new System.Drawing.Size(278, 509);
            this.NavigationTree.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Assemblies";
            listViewGroup1.Name = "assembliesGroup";
            listViewGroup2.Header = "Parts";
            listViewGroup2.Name = "partsGroup";
            listViewGroup3.Header = "Drawings";
            listViewGroup3.Name = "drawingsGroup";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.listView1.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listView1.Location = new System.Drawing.Point(0, 25);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(691, 484);
            this.listView1.StateImageList = this.imageList2;
            this.listView1.TabIndex = 14;
            this.listView1.TileSize = new System.Drawing.Size(150, 30);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 800;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 150;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(691, 25);
            this.toolStrip2.TabIndex = 15;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshButton.Image")));
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(66, 22);
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.StartMonitoringButton);
            this.panel1.Controls.Add(this.InitSynchronizerButton);
            this.panel1.Controls.Add(this.StopMonitoringButton);
            this.panel1.Controls.Add(this.LoginButton);
            this.panel1.Controls.Add(this.ProcessEventsButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 48);
            this.panel1.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(202, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 582);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "DemoForm";
            this.Text = "DemoForm";
            this.Load += new System.EventHandler(this.DemoForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button InitSynchronizerButton;
        private System.Windows.Forms.Button StartMonitoringButton;
        private System.Windows.Forms.Button StopMonitoringButton;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button ProcessEventsButton;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView NavigationTree;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton RefreshButton;
        private System.Windows.Forms.Button button1;
    }
}