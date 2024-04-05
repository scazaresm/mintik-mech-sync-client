namespace MechanicalSyncApp.UI.Forms
{
    partial class CreateVersionForm
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
            this.ErrorMessage = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CancelCreateVersionButton = new System.Windows.Forms.Button();
            this.CreateVersionButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectProjectButton = new System.Windows.Forms.Button();
            this.ParentProjectFolderName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Goal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SelectOwnerButton = new System.Windows.Forms.Button();
            this.VersionOwnerFullName = new System.Windows.Forms.TextBox();
            this.Reason = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ErrorMessage.Location = new System.Drawing.Point(10, 42);
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.Size = new System.Drawing.Size(326, 36);
            this.ErrorMessage.TabIndex = 17;
            this.ErrorMessage.Text = "Error";
            this.ErrorMessage.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(5, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(246, 29);
            this.label5.TabIndex = 16;
            this.label5.Text = "New project change";
            // 
            // CancelCreateVersionButton
            // 
            this.CancelCreateVersionButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelCreateVersionButton.Location = new System.Drawing.Point(182, 336);
            this.CancelCreateVersionButton.Name = "CancelCreateVersionButton";
            this.CancelCreateVersionButton.Size = new System.Drawing.Size(75, 23);
            this.CancelCreateVersionButton.TabIndex = 15;
            this.CancelCreateVersionButton.Text = "Cancel";
            this.CancelCreateVersionButton.UseVisualStyleBackColor = true;
            // 
            // CreateVersionButton
            // 
            this.CreateVersionButton.Enabled = false;
            this.CreateVersionButton.Location = new System.Drawing.Point(263, 336);
            this.CreateVersionButton.Name = "CreateVersionButton";
            this.CreateVersionButton.Size = new System.Drawing.Size(75, 23);
            this.CreateVersionButton.TabIndex = 14;
            this.CreateVersionButton.Text = "Create";
            this.CreateVersionButton.UseVisualStyleBackColor = true;
            this.CreateVersionButton.Click += new System.EventHandler(this.CreateVersionButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Project:";
            // 
            // SelectProjectButton
            // 
            this.SelectProjectButton.Location = new System.Drawing.Point(265, 94);
            this.SelectProjectButton.Name = "SelectProjectButton";
            this.SelectProjectButton.Size = new System.Drawing.Size(73, 23);
            this.SelectProjectButton.TabIndex = 12;
            this.SelectProjectButton.Text = "Select...";
            this.SelectProjectButton.UseVisualStyleBackColor = true;
            this.SelectProjectButton.Click += new System.EventHandler(this.SelectProjectButton_Click);
            // 
            // ParentProjectFolderName
            // 
            this.ParentProjectFolderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ParentProjectFolderName.Location = new System.Drawing.Point(10, 94);
            this.ParentProjectFolderName.Name = "ParentProjectFolderName";
            this.ParentProjectFolderName.ReadOnly = true;
            this.ParentProjectFolderName.Size = new System.Drawing.Size(247, 21);
            this.ParentProjectFolderName.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Change goal:";
            // 
            // Goal
            // 
            this.Goal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Goal.Location = new System.Drawing.Point(10, 237);
            this.Goal.Multiline = true;
            this.Goal.Name = "Goal";
            this.Goal.Size = new System.Drawing.Size(326, 84);
            this.Goal.TabIndex = 9;
            this.Goal.TextChanged += new System.EventHandler(this.Goal_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Change owner:";
            // 
            // SelectOwnerButton
            // 
            this.SelectOwnerButton.Location = new System.Drawing.Point(265, 138);
            this.SelectOwnerButton.Name = "SelectOwnerButton";
            this.SelectOwnerButton.Size = new System.Drawing.Size(73, 23);
            this.SelectOwnerButton.TabIndex = 19;
            this.SelectOwnerButton.Text = "Select...";
            this.SelectOwnerButton.UseVisualStyleBackColor = true;
            this.SelectOwnerButton.Click += new System.EventHandler(this.SelectOwnerButton_Click);
            // 
            // VersionOwnerFullName
            // 
            this.VersionOwnerFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionOwnerFullName.Location = new System.Drawing.Point(10, 138);
            this.VersionOwnerFullName.Name = "VersionOwnerFullName";
            this.VersionOwnerFullName.ReadOnly = true;
            this.VersionOwnerFullName.Size = new System.Drawing.Size(247, 21);
            this.VersionOwnerFullName.TabIndex = 18;
            // 
            // Reason
            // 
            this.Reason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Reason.FormattingEnabled = true;
            this.Reason.Items.AddRange(new object[] {
            "Customer",
            "Design"});
            this.Reason.Location = new System.Drawing.Point(10, 187);
            this.Reason.Name = "Reason";
            this.Reason.Size = new System.Drawing.Size(247, 21);
            this.Reason.TabIndex = 21;
            this.Reason.SelectedIndexChanged += new System.EventHandler(this.Reason_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Change reason:";
            // 
            // CreateVersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelCreateVersionButton;
            this.ClientSize = new System.Drawing.Size(348, 373);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Reason);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SelectOwnerButton);
            this.Controls.Add(this.VersionOwnerFullName);
            this.Controls.Add(this.ErrorMessage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CancelCreateVersionButton);
            this.Controls.Add(this.CreateVersionButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SelectProjectButton);
            this.Controls.Add(this.ParentProjectFolderName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Goal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateVersionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New project change";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ErrorMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button CancelCreateVersionButton;
        private System.Windows.Forms.Button CreateVersionButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SelectProjectButton;
        private System.Windows.Forms.TextBox ParentProjectFolderName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Goal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SelectOwnerButton;
        private System.Windows.Forms.TextBox VersionOwnerFullName;
        private System.Windows.Forms.ComboBox Reason;
        private System.Windows.Forms.Label label4;
    }
}