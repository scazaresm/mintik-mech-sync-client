namespace MechanicalSyncApp.UI.Forms
{
    partial class AboutDialog
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
            this.AboutText = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.MechanicalSyncLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // AboutText
            // 
            this.AboutText.Location = new System.Drawing.Point(20, 84);
            this.AboutText.Name = "AboutText";
            this.AboutText.Size = new System.Drawing.Size(330, 87);
            this.AboutText.TabIndex = 1;
            this.AboutText.Text = "Designed and developed by Sergio Cazares.\r\n2023 - 2024\r\n\r\nFeel free to send a mes" +
    "sage to scazares.dev@gmail.com for any question or comment.\r\n";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(292, 174);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MechanicalSyncApp.Properties.Resources.hecho_en_mexico_logo_png_transparent;
            this.pictureBox1.Location = new System.Drawing.Point(292, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 69);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.ForeColor = System.Drawing.Color.Black;
            this.VersionLabel.Location = new System.Drawing.Point(20, 51);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(78, 13);
            this.VersionLabel.TabIndex = 9;
            this.VersionLabel.Text = "Version 1.0.0.0";
            // 
            // MechanicalSyncLabel
            // 
            this.MechanicalSyncLabel.AutoSize = true;
            this.MechanicalSyncLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MechanicalSyncLabel.Location = new System.Drawing.Point(19, 16);
            this.MechanicalSyncLabel.Name = "MechanicalSyncLabel";
            this.MechanicalSyncLabel.Size = new System.Drawing.Size(169, 24);
            this.MechanicalSyncLabel.TabIndex = 0;
            this.MechanicalSyncLabel.Text = "Mechanical Sync";
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 209);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.AboutText);
            this.Controls.Add(this.MechanicalSyncLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Mechanical Sync";
            this.Load += new System.EventHandler(this.FrmAboutDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label AboutText;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label MechanicalSyncLabel;
    }
}