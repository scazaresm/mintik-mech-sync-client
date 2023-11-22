namespace MechanicalSyncApp.UI.Forms
{
    partial class OpenVersionDialog
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
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.Status = new System.Windows.Forms.Label();
            this.OpeningLegend = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(12, 71);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(493, 23);
            this.Progress.TabIndex = 0;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(9, 46);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(35, 13);
            this.Status.TabIndex = 1;
            this.Status.Text = "label1";
            // 
            // OpeningLegend
            // 
            this.OpeningLegend.AutoSize = true;
            this.OpeningLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpeningLegend.Location = new System.Drawing.Point(8, 9);
            this.OpeningLegend.Name = "OpeningLegend";
            this.OpeningLegend.Size = new System.Drawing.Size(75, 18);
            this.OpeningLegend.TabIndex = 2;
            this.OpeningLegend.Text = "Opening ";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(430, 141);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OpenVersionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 180);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OpeningLegend);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.Progress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenVersionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open version";
            this.Load += new System.EventHandler(this.InitializeVersionSynchronizerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Label OpeningLegend;
        private System.Windows.Forms.Button CancelButton;
    }
}