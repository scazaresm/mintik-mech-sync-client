namespace MechanicalSyncApp.UI.Forms
{
    partial class CreateEditUserForm
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
            this.CreateLabel = new System.Windows.Forms.Label();
            this.CancelCreateUserButton = new System.Windows.Forms.Button();
            this.CreateUserButton = new System.Windows.Forms.Button();
            this.FirstName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LastName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.EmailConfirmation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Email = new System.Windows.Forms.TextBox();
            this.Role = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Enabled = new System.Windows.Forms.CheckBox();
            this.DisplayName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ErrorMessage.Location = new System.Drawing.Point(17, 41);
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.Size = new System.Drawing.Size(326, 36);
            this.ErrorMessage.TabIndex = 10;
            this.ErrorMessage.Text = "Error";
            this.ErrorMessage.Visible = false;
            // 
            // CreateLabel
            // 
            this.CreateLabel.AutoSize = true;
            this.CreateLabel.BackColor = System.Drawing.SystemColors.Control;
            this.CreateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CreateLabel.Location = new System.Drawing.Point(12, 9);
            this.CreateLabel.Name = "CreateLabel";
            this.CreateLabel.Size = new System.Drawing.Size(149, 29);
            this.CreateLabel.TabIndex = 9;
            this.CreateLabel.Text = "Create user";
            // 
            // CancelCreateUserButton
            // 
            this.CancelCreateUserButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelCreateUserButton.Location = new System.Drawing.Point(202, 337);
            this.CancelCreateUserButton.Name = "CancelCreateUserButton";
            this.CancelCreateUserButton.Size = new System.Drawing.Size(75, 23);
            this.CancelCreateUserButton.TabIndex = 8;
            this.CancelCreateUserButton.Text = "Cancel";
            this.CancelCreateUserButton.UseVisualStyleBackColor = true;
            this.CancelCreateUserButton.Click += new System.EventHandler(this.CancelCreateUserButton_Click);
            // 
            // CreateUserButton
            // 
            this.CreateUserButton.Enabled = false;
            this.CreateUserButton.Location = new System.Drawing.Point(283, 337);
            this.CreateUserButton.Name = "CreateUserButton";
            this.CreateUserButton.Size = new System.Drawing.Size(75, 23);
            this.CreateUserButton.TabIndex = 7;
            this.CreateUserButton.Text = "Create";
            this.CreateUserButton.UseVisualStyleBackColor = true;
            this.CreateUserButton.Click += new System.EventHandler(this.CreateUserButton_Click);
            // 
            // FirstName
            // 
            this.FirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstName.Location = new System.Drawing.Point(17, 184);
            this.FirstName.Name = "FirstName";
            this.FirstName.Size = new System.Drawing.Size(166, 21);
            this.FirstName.TabIndex = 2;
            this.FirstName.TextChanged += new System.EventHandler(this.FirstName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "First name:";
            // 
            // LastName
            // 
            this.LastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastName.Location = new System.Drawing.Point(192, 184);
            this.LastName.Name = "LastName";
            this.LastName.Size = new System.Drawing.Size(166, 21);
            this.LastName.TabIndex = 3;
            this.LastName.TextChanged += new System.EventHandler(this.LastName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Last name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Confirm Email:";
            // 
            // EmailConfirmation
            // 
            this.EmailConfirmation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailConfirmation.Location = new System.Drawing.Point(17, 134);
            this.EmailConfirmation.Name = "EmailConfirmation";
            this.EmailConfirmation.Size = new System.Drawing.Size(341, 21);
            this.EmailConfirmation.TabIndex = 1;
            this.EmailConfirmation.TextChanged += new System.EventHandler(this.EmailConfirmation_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Email:";
            // 
            // Email
            // 
            this.Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Email.Location = new System.Drawing.Point(17, 86);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(341, 21);
            this.Email.TabIndex = 0;
            this.Email.TextChanged += new System.EventHandler(this.Email_TextChanged);
            // 
            // Role
            // 
            this.Role.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Role.FormattingEnabled = true;
            this.Role.Items.AddRange(new object[] {
            "Designer",
            "Viewer",
            "Admin"});
            this.Role.Location = new System.Drawing.Point(192, 233);
            this.Role.Name = "Role";
            this.Role.Size = new System.Drawing.Size(166, 21);
            this.Role.TabIndex = 5;
            this.Role.SelectedIndexChanged += new System.EventHandler(this.Role_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(189, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Role:";
            // 
            // Enabled
            // 
            this.Enabled.AutoSize = true;
            this.Enabled.Checked = true;
            this.Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Enabled.Location = new System.Drawing.Point(293, 284);
            this.Enabled.Name = "Enabled";
            this.Enabled.Size = new System.Drawing.Size(65, 17);
            this.Enabled.TabIndex = 6;
            this.Enabled.Text = "Enabled";
            this.Enabled.UseVisualStyleBackColor = true;
            this.Enabled.CheckedChanged += new System.EventHandler(this.Enabled_CheckedChanged);
            // 
            // DisplayName
            // 
            this.DisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayName.Location = new System.Drawing.Point(17, 233);
            this.DisplayName.Name = "DisplayName";
            this.DisplayName.Size = new System.Drawing.Size(166, 21);
            this.DisplayName.TabIndex = 4;
            this.DisplayName.TextChanged += new System.EventHandler(this.DisplayName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Display name or nickname:";
            // 
            // CreateEditUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelCreateUserButton;
            this.ClientSize = new System.Drawing.Size(375, 379);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DisplayName);
            this.Controls.Add(this.Enabled);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Role);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EmailConfirmation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LastName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FirstName);
            this.Controls.Add(this.CancelCreateUserButton);
            this.Controls.Add(this.CreateUserButton);
            this.Controls.Add(this.ErrorMessage);
            this.Controls.Add(this.CreateLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateEditUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create user";
            this.Load += new System.EventHandler(this.CreateEditUserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ErrorMessage;
        private System.Windows.Forms.Label CreateLabel;
        private System.Windows.Forms.Button CancelCreateUserButton;
        private System.Windows.Forms.Button CreateUserButton;
        private System.Windows.Forms.TextBox FirstName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LastName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox EmailConfirmation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.ComboBox Role;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox Enabled;
        private System.Windows.Forms.TextBox DisplayName;
        private System.Windows.Forms.Label label3;
    }
}