namespace MechanicalSyncApp.UI.Forms
{
    partial class CreateProjectForm
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
            this.FolderNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.InitialVersionOwnerTextBox = new System.Windows.Forms.TextBox();
            this.SelectUserButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CreateProjectButton = new System.Windows.Forms.Button();
            this.CancelCreateProjectButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.ErrorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FolderNameTextBox
            // 
            this.FolderNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FolderNameTextBox.Location = new System.Drawing.Point(10, 103);
            this.FolderNameTextBox.Name = "FolderNameTextBox";
            this.FolderNameTextBox.Size = new System.Drawing.Size(326, 21);
            this.FolderNameTextBox.TabIndex = 0;
            this.FolderNameTextBox.TextChanged += new System.EventHandler(this.FolderNameTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Folder name:";
            // 
            // InitialVersionOwnerTextBox
            // 
            this.InitialVersionOwnerTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InitialVersionOwnerTextBox.Location = new System.Drawing.Point(10, 157);
            this.InitialVersionOwnerTextBox.Name = "InitialVersionOwnerTextBox";
            this.InitialVersionOwnerTextBox.ReadOnly = true;
            this.InitialVersionOwnerTextBox.Size = new System.Drawing.Size(245, 21);
            this.InitialVersionOwnerTextBox.TabIndex = 2;
            // 
            // SelectUserButton
            // 
            this.SelectUserButton.Location = new System.Drawing.Point(263, 157);
            this.SelectUserButton.Name = "SelectUserButton";
            this.SelectUserButton.Size = new System.Drawing.Size(73, 23);
            this.SelectUserButton.TabIndex = 3;
            this.SelectUserButton.Text = "Select...";
            this.SelectUserButton.UseVisualStyleBackColor = true;
            this.SelectUserButton.Click += new System.EventHandler(this.SelectUserButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Initial version owner:";
            // 
            // CreateProjectButton
            // 
            this.CreateProjectButton.Enabled = false;
            this.CreateProjectButton.Location = new System.Drawing.Point(261, 210);
            this.CreateProjectButton.Name = "CreateProjectButton";
            this.CreateProjectButton.Size = new System.Drawing.Size(75, 23);
            this.CreateProjectButton.TabIndex = 5;
            this.CreateProjectButton.Text = "Create";
            this.CreateProjectButton.UseVisualStyleBackColor = true;
            this.CreateProjectButton.Click += new System.EventHandler(this.CreateProjectButton_Click);
            // 
            // CancelCreateProjectButton
            // 
            this.CancelCreateProjectButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelCreateProjectButton.Location = new System.Drawing.Point(180, 210);
            this.CancelCreateProjectButton.Name = "CancelCreateProjectButton";
            this.CancelCreateProjectButton.Size = new System.Drawing.Size(75, 23);
            this.CancelCreateProjectButton.TabIndex = 6;
            this.CancelCreateProjectButton.Text = "Cancel";
            this.CancelCreateProjectButton.UseVisualStyleBackColor = true;
            this.CancelCreateProjectButton.Click += new System.EventHandler(this.CancelCreateProjectButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(5, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 29);
            this.label5.TabIndex = 7;
            this.label5.Text = "Create project";
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ErrorMessage.Location = new System.Drawing.Point(10, 42);
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.Size = new System.Drawing.Size(326, 36);
            this.ErrorMessage.TabIndex = 8;
            this.ErrorMessage.Text = "Error";
            this.ErrorMessage.Visible = false;
            // 
            // CreateProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelCreateProjectButton;
            this.ClientSize = new System.Drawing.Size(348, 250);
            this.Controls.Add(this.ErrorMessage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CancelCreateProjectButton);
            this.Controls.Add(this.CreateProjectButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SelectUserButton);
            this.Controls.Add(this.InitialVersionOwnerTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FolderNameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FolderNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox InitialVersionOwnerTextBox;
        private System.Windows.Forms.Button SelectUserButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CreateProjectButton;
        private System.Windows.Forms.Button CancelCreateProjectButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ErrorMessage;
    }
}