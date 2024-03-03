namespace MechanicalSyncApp.UI.Forms
{
    partial class DesignFilesAnalysisDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignFilesAnalysisDialog));
            this.AnalysisProgressBar = new System.Windows.Forms.ProgressBar();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.DetailsLabel = new System.Windows.Forms.Label();
            this.CancelAnalysisButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AnalysisProgressBar
            // 
            this.AnalysisProgressBar.Location = new System.Drawing.Point(12, 96);
            this.AnalysisProgressBar.Name = "AnalysisProgressBar";
            this.AnalysisProgressBar.Size = new System.Drawing.Size(493, 23);
            this.AnalysisProgressBar.TabIndex = 0;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Location = new System.Drawing.Point(9, 23);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(197, 16);
            this.StatusLabel.TabIndex = 1;
            this.StatusLabel.Text = "Connecting to SolidWorks...";
            // 
            // DetailsLabel
            // 
            this.DetailsLabel.AutoSize = true;
            this.DetailsLabel.Location = new System.Drawing.Point(9, 60);
            this.DetailsLabel.Name = "DetailsLabel";
            this.DetailsLabel.Size = new System.Drawing.Size(70, 13);
            this.DetailsLabel.TabIndex = 2;
            this.DetailsLabel.Text = "Please wait...";
            // 
            // CancelAnalysisButton
            // 
            this.CancelAnalysisButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelAnalysisButton.Location = new System.Drawing.Point(430, 145);
            this.CancelAnalysisButton.Name = "CancelAnalysisButton";
            this.CancelAnalysisButton.Size = new System.Drawing.Size(75, 23);
            this.CancelAnalysisButton.TabIndex = 4;
            this.CancelAnalysisButton.Text = "Cancel";
            this.CancelAnalysisButton.UseVisualStyleBackColor = true;
            this.CancelAnalysisButton.Click += new System.EventHandler(this.CancelAnalysisButton_Click);
            // 
            // DesignFilesAnalysisDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelAnalysisButton;
            this.ClientSize = new System.Drawing.Size(517, 180);
            this.Controls.Add(this.CancelAnalysisButton);
            this.Controls.Add(this.DetailsLabel);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.AnalysisProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DesignFilesAnalysisDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Design files analysis";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DesignFilesAnalysisDialog_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar AnalysisProgressBar;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label DetailsLabel;
        private System.Windows.Forms.Button CancelAnalysisButton;
    }
}