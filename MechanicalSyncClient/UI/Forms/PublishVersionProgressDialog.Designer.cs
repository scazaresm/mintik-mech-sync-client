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
            this.OkButton = new System.Windows.Forms.Button();
            this.PublishingMessage = new System.Windows.Forms.Label();
            this.PublishingProgressBar = new System.Windows.Forms.ProgressBar();
            this.PublishingIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PublishingIcon)).BeginInit();
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
            // PublishingMessage
            // 
            this.PublishingMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PublishingMessage.Location = new System.Drawing.Point(82, 21);
            this.PublishingMessage.Name = "PublishingMessage";
            this.PublishingMessage.Size = new System.Drawing.Size(360, 96);
            this.PublishingMessage.TabIndex = 5;
            this.PublishingMessage.Text = "Publishing";
            // 
            // PublishingProgressBar
            // 
            this.PublishingProgressBar.Location = new System.Drawing.Point(14, 130);
            this.PublishingProgressBar.Name = "PublishingProgressBar";
            this.PublishingProgressBar.Size = new System.Drawing.Size(428, 23);
            this.PublishingProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.PublishingProgressBar.TabIndex = 6;
            // 
            // PublishingIcon
            // 
            this.PublishingIcon.Location = new System.Drawing.Point(22, 33);
            this.PublishingIcon.Name = "PublishingIcon";
            this.PublishingIcon.Size = new System.Drawing.Size(48, 48);
            this.PublishingIcon.TabIndex = 7;
            this.PublishingIcon.TabStop = false;
            // 
            // PublishVersionProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.OkButton;
            this.ClientSize = new System.Drawing.Size(455, 202);
            this.ControlBox = false;
            this.Controls.Add(this.PublishingIcon);
            this.Controls.Add(this.PublishingMessage);
            this.Controls.Add(this.PublishingProgressBar);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PublishVersionProgressDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Publishing in progress";
            this.Load += new System.EventHandler(this.PublishVersionProgressDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PublishingIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label PublishingMessage;
        private System.Windows.Forms.ProgressBar PublishingProgressBar;
        private System.Windows.Forms.PictureBox PublishingIcon;
    }
}