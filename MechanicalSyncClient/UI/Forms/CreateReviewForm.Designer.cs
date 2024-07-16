namespace MechanicalSyncApp.UI.Forms
{
    partial class CreateReviewForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.SelectVersionButton = new System.Windows.Forms.Button();
            this.OngoingVersionText = new System.Windows.Forms.TextBox();
            this.ReviewType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CancelCreateReviewButton = new System.Windows.Forms.Button();
            this.CreateReviewButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ErrorMessage.Location = new System.Drawing.Point(10, 45);
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.Size = new System.Drawing.Size(326, 36);
            this.ErrorMessage.TabIndex = 10;
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
            this.label5.Size = new System.Drawing.Size(159, 29);
            this.label5.TabIndex = 9;
            this.label5.Text = "New Review";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Ongoing version:";
            // 
            // SelectVersionButton
            // 
            this.SelectVersionButton.Location = new System.Drawing.Point(263, 93);
            this.SelectVersionButton.Name = "SelectVersionButton";
            this.SelectVersionButton.Size = new System.Drawing.Size(73, 23);
            this.SelectVersionButton.TabIndex = 12;
            this.SelectVersionButton.Text = "Select...";
            this.SelectVersionButton.UseVisualStyleBackColor = true;
            this.SelectVersionButton.Click += new System.EventHandler(this.SelectVersionButton_Click);
            // 
            // OngoingVersionText
            // 
            this.OngoingVersionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OngoingVersionText.Location = new System.Drawing.Point(10, 93);
            this.OngoingVersionText.Name = "OngoingVersionText";
            this.OngoingVersionText.ReadOnly = true;
            this.OngoingVersionText.Size = new System.Drawing.Size(245, 21);
            this.OngoingVersionText.TabIndex = 11;
            this.OngoingVersionText.TextChanged += new System.EventHandler(this.OngoingVersionText_TextChanged);
            // 
            // ReviewType
            // 
            this.ReviewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReviewType.FormattingEnabled = true;
            this.ReviewType.Items.AddRange(new object[] {
            "2D Files (Drawings)",
            "3D Files (Assemblies)"});
            this.ReviewType.Location = new System.Drawing.Point(10, 141);
            this.ReviewType.Name = "ReviewType";
            this.ReviewType.Size = new System.Drawing.Size(245, 21);
            this.ReviewType.TabIndex = 14;
            this.ReviewType.SelectedIndexChanged += new System.EventHandler(this.ReviewType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Review type:";
            // 
            // CancelCreateReviewButton
            // 
            this.CancelCreateReviewButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelCreateReviewButton.Location = new System.Drawing.Point(180, 194);
            this.CancelCreateReviewButton.Name = "CancelCreateReviewButton";
            this.CancelCreateReviewButton.Size = new System.Drawing.Size(75, 23);
            this.CancelCreateReviewButton.TabIndex = 17;
            this.CancelCreateReviewButton.Text = "Cancel";
            this.CancelCreateReviewButton.UseVisualStyleBackColor = true;
            this.CancelCreateReviewButton.Click += new System.EventHandler(this.CancelCreateReviewButton_Click);
            // 
            // CreateReviewButton
            // 
            this.CreateReviewButton.Enabled = false;
            this.CreateReviewButton.Location = new System.Drawing.Point(261, 194);
            this.CreateReviewButton.Name = "CreateReviewButton";
            this.CreateReviewButton.Size = new System.Drawing.Size(75, 23);
            this.CreateReviewButton.TabIndex = 16;
            this.CreateReviewButton.Text = "Create";
            this.CreateReviewButton.UseVisualStyleBackColor = true;
            this.CreateReviewButton.Click += new System.EventHandler(this.CreateReviewButton_Click);
            // 
            // CreateReviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelCreateReviewButton;
            this.ClientSize = new System.Drawing.Size(346, 232);
            this.Controls.Add(this.CancelCreateReviewButton);
            this.Controls.Add(this.CreateReviewButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ReviewType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SelectVersionButton);
            this.Controls.Add(this.OngoingVersionText);
            this.Controls.Add(this.ErrorMessage);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateReviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New review";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ErrorMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SelectVersionButton;
        private System.Windows.Forms.TextBox OngoingVersionText;
        private System.Windows.Forms.ComboBox ReviewType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CancelCreateReviewButton;
        private System.Windows.Forms.Button CreateReviewButton;
    }
}