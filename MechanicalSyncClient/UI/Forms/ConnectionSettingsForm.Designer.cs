namespace MechanicalSyncApp.UI.Forms
{
    partial class ConnectionSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionSettingsForm));
            this.ApplyServerChanges = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerTimeout = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.RemoteServer = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ServerTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // ApplyServerChanges
            // 
            this.ApplyServerChanges.Enabled = false;
            this.ApplyServerChanges.Location = new System.Drawing.Point(12, 130);
            this.ApplyServerChanges.Name = "ApplyServerChanges";
            this.ApplyServerChanges.Size = new System.Drawing.Size(75, 23);
            this.ApplyServerChanges.TabIndex = 4;
            this.ApplyServerChanges.Text = "Apply";
            this.ApplyServerChanges.UseVisualStyleBackColor = true;
            this.ApplyServerChanges.Click += new System.EventHandler(this.ApplyServerChanges_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Timeout (seconds)";
            // 
            // ServerTimeout
            // 
            this.ServerTimeout.Location = new System.Drawing.Point(12, 92);
            this.ServerTimeout.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ServerTimeout.Name = "ServerTimeout";
            this.ServerTimeout.Size = new System.Drawing.Size(71, 20);
            this.ServerTimeout.TabIndex = 2;
            this.ServerTimeout.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ServerTimeout.ValueChanged += new System.EventHandler(this.ServerTimeout_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Remote server URL: \r\n(full URL with port, for example: http://192.168.100.10:8085" +
    ")";
            // 
            // RemoteServer
            // 
            this.RemoteServer.Location = new System.Drawing.Point(12, 43);
            this.RemoteServer.Name = "RemoteServer";
            this.RemoteServer.Size = new System.Drawing.Size(297, 20);
            this.RemoteServer.TabIndex = 0;
            this.RemoteServer.TextChanged += new System.EventHandler(this.RemoteServer_TextChanged);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(234, 130);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ConnectionSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 164);
            this.Controls.Add(this.ApplyServerChanges);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ServerTimeout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.RemoteServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectionSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection settings";
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ServerTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox RemoteServer;
        private System.Windows.Forms.NumericUpDown ServerTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ApplyServerChanges;
        private System.Windows.Forms.Button CloseButton;
    }
}