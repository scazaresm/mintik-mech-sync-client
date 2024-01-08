namespace MechanicalSyncApp.UI.Forms
{
    partial class UserSelectorForm
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
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Sergio Cazares",
            "test"}, -1);
            this.OkButton = new System.Windows.Forms.Button();
            this.UsersList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CancelUserSelectButton = new System.Windows.Forms.Button();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.SearchFilter = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Enabled = false;
            this.OkButton.Location = new System.Drawing.Point(343, 313);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // UsersList
            // 
            this.UsersList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.UsersList.FullRowSelect = true;
            this.UsersList.HideSelection = false;
            this.UsersList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.UsersList.Location = new System.Drawing.Point(12, 88);
            this.UsersList.Name = "UsersList";
            this.UsersList.Size = new System.Drawing.Size(406, 210);
            this.UsersList.TabIndex = 2;
            this.UsersList.UseCompatibleStateImageBehavior = false;
            this.UsersList.View = System.Windows.Forms.View.Details;
            this.UsersList.SelectedIndexChanged += new System.EventHandler(this.UsersList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Full name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Email";
            this.columnHeader2.Width = 200;
            // 
            // CancelUserSelectButton
            // 
            this.CancelUserSelectButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelUserSelectButton.Location = new System.Drawing.Point(262, 313);
            this.CancelUserSelectButton.Name = "CancelUserSelectButton";
            this.CancelUserSelectButton.Size = new System.Drawing.Size(75, 23);
            this.CancelUserSelectButton.TabIndex = 3;
            this.CancelUserSelectButton.Text = "Cancel";
            this.CancelUserSelectButton.UseVisualStyleBackColor = true;
            this.CancelUserSelectButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // MessageLabel
            // 
            this.MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(12, 9);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(406, 35);
            this.MessageLabel.TabIndex = 4;
            this.MessageLabel.Text = "Select the user who will take the ownership of this version:\r\n";
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(192, 61);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(44, 13);
            this.SearchLabel.TabIndex = 6;
            this.SearchLabel.Text = "Search:";
            // 
            // SearchFilter
            // 
            this.SearchFilter.Location = new System.Drawing.Point(242, 58);
            this.SearchFilter.Name = "SearchFilter";
            this.SearchFilter.Size = new System.Drawing.Size(177, 20);
            this.SearchFilter.TabIndex = 5;
            this.SearchFilter.TextChanged += new System.EventHandler(this.SearchFilter_TextChanged);
            // 
            // UserSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelUserSelectButton;
            this.ClientSize = new System.Drawing.Size(431, 348);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.SearchFilter);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.CancelUserSelectButton);
            this.Controls.Add(this.UsersList);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select user";
            this.Load += new System.EventHandler(this.UserSelectorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.ListView UsersList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button CancelUserSelectButton;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.TextBox SearchFilter;
    }
}