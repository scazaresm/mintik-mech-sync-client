namespace MechanicalSyncApp.UI.Forms
{
    partial class PublishVersionProgressDialog
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
            this.PublishingStatusLabel = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.PublishingInstructionsLabel = new System.Windows.Forms.Label();
            this.PublishingStatusPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PublishingStatusPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusLabel
            // 
            this.PublishingStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PublishingStatusLabel.Location = new System.Drawing.Point(69, 55);
            this.PublishingStatusLabel.Name = "StatusLabel";
            this.PublishingStatusLabel.Size = new System.Drawing.Size(284, 48);
            this.PublishingStatusLabel.TabIndex = 0;
            this.PublishingStatusLabel.Text = "Queued";
            this.PublishingStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OkButton.Location = new System.Drawing.Point(278, 130);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // InstructionsLabel
            // 
            this.PublishingInstructionsLabel.AutoSize = true;
            this.PublishingInstructionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PublishingInstructionsLabel.Location = new System.Drawing.Point(12, 14);
            this.PublishingInstructionsLabel.Name = "InstructionsLabel";
            this.PublishingInstructionsLabel.Size = new System.Drawing.Size(246, 16);
            this.PublishingInstructionsLabel.TabIndex = 5;
            this.PublishingInstructionsLabel.Text = "Hold on while we publish your changes...";
            // 
            // StatusPicture
            // 
            this.PublishingStatusPictureBox.Image = global::MechanicalSyncApp.Properties.Resources.error_icon_48;
            this.PublishingStatusPictureBox.Location = new System.Drawing.Point(15, 55);
            this.PublishingStatusPictureBox.Name = "StatusPicture";
            this.PublishingStatusPictureBox.Size = new System.Drawing.Size(48, 48);
            this.PublishingStatusPictureBox.TabIndex = 4;
            this.PublishingStatusPictureBox.TabStop = false;
            // 
            // PublishVersionProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.OkButton;
            this.ClientSize = new System.Drawing.Size(365, 165);
            this.Controls.Add(this.PublishingInstructionsLabel);
            this.Controls.Add(this.PublishingStatusPictureBox);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.PublishingStatusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PublishVersionProgressDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Publish version";
            this.Load += new System.EventHandler(this.PublishVersionProgressDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PublishingStatusPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PublishingStatusLabel;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.PictureBox PublishingStatusPictureBox;
        private System.Windows.Forms.Label PublishingInstructionsLabel;
    }
}