namespace MechanicalSyncApp.UI.Forms
{
    partial class ProjectSelectorForm
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
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "PROJECT NAME",
            "1/9/2024"}, -1);
            this.SearchLabel = new System.Windows.Forms.Label();
            this.SearchFilter = new System.Windows.Forms.TextBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.CancelProjectSelectButton = new System.Windows.Forms.Button();
            this.ProjectList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(192, 59);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(44, 13);
            this.SearchLabel.TabIndex = 12;
            this.SearchLabel.Text = "Search:";
            // 
            // SearchFilter
            // 
            this.SearchFilter.Location = new System.Drawing.Point(242, 56);
            this.SearchFilter.Name = "SearchFilter";
            this.SearchFilter.Size = new System.Drawing.Size(177, 20);
            this.SearchFilter.TabIndex = 11;
            this.SearchFilter.TextChanged += new System.EventHandler(this.SearchFilter_TextChanged);
            // 
            // MessageLabel
            // 
            this.MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(12, 7);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(406, 35);
            this.MessageLabel.TabIndex = 10;
            this.MessageLabel.Text = "Select a project:";
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CancelProjectSelectButton
            // 
            this.CancelProjectSelectButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelProjectSelectButton.Location = new System.Drawing.Point(262, 311);
            this.CancelProjectSelectButton.Name = "CancelProjectSelectButton";
            this.CancelProjectSelectButton.Size = new System.Drawing.Size(75, 23);
            this.CancelProjectSelectButton.TabIndex = 9;
            this.CancelProjectSelectButton.Text = "Cancel";
            this.CancelProjectSelectButton.UseVisualStyleBackColor = true;
            this.CancelProjectSelectButton.Click += new System.EventHandler(this.CancelProjectSelectButton_Click);
            // 
            // ProjectList
            // 
            this.ProjectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ProjectList.FullRowSelect = true;
            this.ProjectList.HideSelection = false;
            this.ProjectList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3});
            this.ProjectList.Location = new System.Drawing.Point(12, 86);
            this.ProjectList.Name = "ProjectList";
            this.ProjectList.Size = new System.Drawing.Size(406, 210);
            this.ProjectList.TabIndex = 8;
            this.ProjectList.UseCompatibleStateImageBehavior = false;
            this.ProjectList.View = System.Windows.Forms.View.Details;
            this.ProjectList.SelectedIndexChanged += new System.EventHandler(this.ProjectList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Project name";
            this.columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Creation date";
            this.columnHeader2.Width = 150;
            // 
            // OkButton
            // 
            this.OkButton.Enabled = false;
            this.OkButton.Location = new System.Drawing.Point(343, 311);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 7;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // ProjectSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelProjectSelectButton;
            this.ClientSize = new System.Drawing.Size(431, 342);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.SearchFilter);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.CancelProjectSelectButton);
            this.Controls.Add(this.ProjectList);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select project";
            this.Load += new System.EventHandler(this.ProjectSelectorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.TextBox SearchFilter;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button CancelProjectSelectButton;
        private System.Windows.Forms.ListView ProjectList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button OkButton;
    }
}