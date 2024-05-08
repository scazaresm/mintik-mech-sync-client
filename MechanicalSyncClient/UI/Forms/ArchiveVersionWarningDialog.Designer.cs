namespace MechanicalSyncApp.UI.Forms
{
    partial class ArchiveVersionWarningDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchiveVersionWarningDialog));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Agree = new System.Windows.Forms.CheckBox();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.CancelArchivingButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MechanicalSyncApp.Properties.Resources.Microsoft_Fluentui_Emoji_3d_Warning_3d_512;
            this.pictureBox1.Location = new System.Drawing.Point(16, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(86, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(108, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 61);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // Agree
            // 
            this.Agree.AutoSize = true;
            this.Agree.Location = new System.Drawing.Point(16, 122);
            this.Agree.Name = "Agree";
            this.Agree.Size = new System.Drawing.Size(282, 17);
            this.Agree.TabIndex = 2;
            this.Agree.Text = "I understand the above warning and want to continue.";
            this.Agree.UseVisualStyleBackColor = true;
            this.Agree.CheckedChanged += new System.EventHandler(this.Agree_CheckedChanged);
            // 
            // ContinueButton
            // 
            this.ContinueButton.Enabled = false;
            this.ContinueButton.Location = new System.Drawing.Point(398, 161);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(75, 23);
            this.ContinueButton.TabIndex = 4;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // CancelArchivingButton
            // 
            this.CancelArchivingButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelArchivingButton.Location = new System.Drawing.Point(317, 161);
            this.CancelArchivingButton.Name = "CancelArchivingButton";
            this.CancelArchivingButton.Size = new System.Drawing.Size(75, 23);
            this.CancelArchivingButton.TabIndex = 5;
            this.CancelArchivingButton.Text = "Cancel";
            this.CancelArchivingButton.UseVisualStyleBackColor = true;
            this.CancelArchivingButton.Click += new System.EventHandler(this.CancelArchivingButton_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(108, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(365, 37);
            this.label2.TabIndex = 6;
            this.label2.Text = "If you need to modify the design or publish deliverables after archiving, then a " +
    "new version must be created.";
            // 
            // ArchiveVersionWarningDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelArchivingButton;
            this.ClientSize = new System.Drawing.Size(493, 200);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CancelArchivingButton);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.Agree);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArchiveVersionWarningDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirm before archiving";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Agree;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Button CancelArchivingButton;
        private System.Windows.Forms.Label label2;
    }
}