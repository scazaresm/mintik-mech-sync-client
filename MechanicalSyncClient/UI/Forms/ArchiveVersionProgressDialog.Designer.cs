namespace MechanicalSyncApp.UI.Forms
{
    partial class ArchiveVersionProgressDialog
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
            this.OkButton = new System.Windows.Forms.Button();
            this.ArchivingMessage = new System.Windows.Forms.Label();
            this.ArchivingProgressBar = new System.Windows.Forms.ProgressBar();
            this.ArchivingIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ArchivingIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OkButton.Location = new System.Drawing.Point(367, 167);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // ArchivingMessage
            // 
            this.ArchivingMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ArchivingMessage.Location = new System.Drawing.Point(82, 21);
            this.ArchivingMessage.Name = "ArchivingMessage";
            this.ArchivingMessage.Size = new System.Drawing.Size(360, 96);
            this.ArchivingMessage.TabIndex = 5;
            this.ArchivingMessage.Text = "Archiving...";
            // 
            // ArchivingProgressBar
            // 
            this.ArchivingProgressBar.Location = new System.Drawing.Point(14, 130);
            this.ArchivingProgressBar.Name = "ArchivingProgressBar";
            this.ArchivingProgressBar.Size = new System.Drawing.Size(428, 23);
            this.ArchivingProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ArchivingProgressBar.TabIndex = 6;
            // 
            // ArchivingIcon
            // 
            this.ArchivingIcon.Location = new System.Drawing.Point(22, 33);
            this.ArchivingIcon.Name = "ArchivingIcon";
            this.ArchivingIcon.Size = new System.Drawing.Size(48, 48);
            this.ArchivingIcon.TabIndex = 7;
            this.ArchivingIcon.TabStop = false;
            // 
            // ArchiveVersionProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.OkButton;
            this.ClientSize = new System.Drawing.Size(455, 202);
            this.ControlBox = false;
            this.Controls.Add(this.ArchivingIcon);
            this.Controls.Add(this.ArchivingMessage);
            this.Controls.Add(this.ArchivingProgressBar);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArchiveVersionProgressDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Archive design";
            this.Load += new System.EventHandler(this.PublishVersionProgressDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ArchivingIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label ArchivingMessage;
        private System.Windows.Forms.ProgressBar ArchivingProgressBar;
        private System.Windows.Forms.PictureBox ArchivingIcon;
    }
}