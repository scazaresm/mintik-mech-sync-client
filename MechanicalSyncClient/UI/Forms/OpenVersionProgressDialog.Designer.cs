namespace MechanicalSyncApp.UI.Forms
{
    partial class OpenVersionProgressDialog
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
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(12, 75);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(493, 23);
            this.Progress.TabIndex = 0;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(12, 55);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(35, 13);
            this.Status.TabIndex = 1;
            this.Status.Text = "label1";
            // 
            // OpeningLegend
            // 
            this.OpeningLegend.AutoSize = true;
            this.OpeningLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpeningLegend.Location = new System.Drawing.Point(12, 18);
            this.OpeningLegend.Name = "OpeningLegend";
            this.OpeningLegend.Size = new System.Drawing.Size(69, 16);
            this.OpeningLegend.TabIndex = 2;
            this.OpeningLegend.Text = "Opening ";
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(430, 145);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OpenVersionProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(517, 180);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OpeningLegend);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.Progress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenVersionProgressDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open version";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpenVersionProgressDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Label OpeningLegend;
        private System.Windows.Forms.Button CancelBtn;
    }
}