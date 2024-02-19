namespace MechanicalSyncApp.UI.Forms
{
    partial class DesignFileComparatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignFileComparatorForm));
            this.MainContainer = new System.Windows.Forms.SplitContainer();
            this.LeftHandTitle = new System.Windows.Forms.Label();
            this.RightHandTitle = new System.Windows.Forms.Label();
            this.LeftHandToolStrip = new System.Windows.Forms.ToolStrip();
            this.LeftMeasureButton = new System.Windows.Forms.ToolStripButton();
            this.LeftRotateButton = new System.Windows.Forms.ToolStripButton();
            this.LeftPanButton = new System.Windows.Forms.ToolStripButton();
            this.LeftSelectButton = new System.Windows.Forms.ToolStripButton();
            this.LeftZoomToAreaButton = new System.Windows.Forms.ToolStripButton();
            this.LeftZoomButton = new System.Windows.Forms.ToolStripButton();
            this.RightHandToolStrip = new System.Windows.Forms.ToolStrip();
            this.RightMeasureButton = new System.Windows.Forms.ToolStripButton();
            this.RightRotateButton = new System.Windows.Forms.ToolStripButton();
            this.RightPanButton = new System.Windows.Forms.ToolStripButton();
            this.RightSelectButton = new System.Windows.Forms.ToolStripButton();
            this.RightZoomToAreaButton = new System.Windows.Forms.ToolStripButton();
            this.RightZoomButton = new System.Windows.Forms.ToolStripButton();
            this.RightHandNotFound = new System.Windows.Forms.Label();
            this.LeftHandNotFound = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).BeginInit();
            this.MainContainer.Panel1.SuspendLayout();
            this.MainContainer.Panel2.SuspendLayout();
            this.MainContainer.SuspendLayout();
            this.LeftHandToolStrip.SuspendLayout();
            this.RightHandToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainContainer
            // 
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.Location = new System.Drawing.Point(0, 0);
            this.MainContainer.Name = "MainContainer";
            // 
            // MainContainer.Panel1
            // 
            this.MainContainer.Panel1.Controls.Add(this.LeftHandToolStrip);
            this.MainContainer.Panel1.Controls.Add(this.LeftHandTitle);
            this.MainContainer.Panel1.Controls.Add(this.LeftHandNotFound);
            // 
            // MainContainer.Panel2
            // 
            this.MainContainer.Panel2.Controls.Add(this.RightHandToolStrip);
            this.MainContainer.Panel2.Controls.Add(this.RightHandTitle);
            this.MainContainer.Panel2.Controls.Add(this.RightHandNotFound);
            this.MainContainer.Size = new System.Drawing.Size(874, 505);
            this.MainContainer.SplitterDistance = 437;
            this.MainContainer.TabIndex = 0;
            // 
            // LeftHandTitle
            // 
            this.LeftHandTitle.AutoSize = true;
            this.LeftHandTitle.BackColor = System.Drawing.Color.Black;
            this.LeftHandTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftHandTitle.ForeColor = System.Drawing.Color.White;
            this.LeftHandTitle.Location = new System.Drawing.Point(12, 9);
            this.LeftHandTitle.Name = "LeftHandTitle";
            this.LeftHandTitle.Size = new System.Drawing.Size(104, 18);
            this.LeftHandTitle.TabIndex = 0;
            this.LeftHandTitle.Text = "Left hand file";
            this.LeftHandTitle.Visible = false;
            // 
            // RightHandTitle
            // 
            this.RightHandTitle.AutoSize = true;
            this.RightHandTitle.BackColor = System.Drawing.Color.Black;
            this.RightHandTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightHandTitle.ForeColor = System.Drawing.Color.White;
            this.RightHandTitle.Location = new System.Drawing.Point(12, 9);
            this.RightHandTitle.Name = "RightHandTitle";
            this.RightHandTitle.Size = new System.Drawing.Size(115, 18);
            this.RightHandTitle.TabIndex = 1;
            this.RightHandTitle.Text = "Right hand file";
            this.RightHandTitle.Visible = false;
            // 
            // LeftHandToolStrip
            // 
            this.LeftHandToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LeftHandToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.LeftHandToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LeftMeasureButton,
            this.LeftRotateButton,
            this.LeftPanButton,
            this.LeftSelectButton,
            this.LeftZoomToAreaButton,
            this.LeftZoomButton});
            this.LeftHandToolStrip.Location = new System.Drawing.Point(0, 466);
            this.LeftHandToolStrip.Name = "LeftHandToolStrip";
            this.LeftHandToolStrip.Size = new System.Drawing.Size(437, 39);
            this.LeftHandToolStrip.TabIndex = 1;
            this.LeftHandToolStrip.Text = "toolStrip1";
            // 
            // LeftMeasureButton
            // 
            this.LeftMeasureButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LeftMeasureButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftMeasureButton.Image = global::MechanicalSyncApp.Properties.Resources.tape_measure_32;
            this.LeftMeasureButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LeftMeasureButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LeftMeasureButton.Name = "LeftMeasureButton";
            this.LeftMeasureButton.Size = new System.Drawing.Size(36, 36);
            this.LeftMeasureButton.Text = "Measure";
            this.LeftMeasureButton.Click += new System.EventHandler(this.LeftMeasureButton_Click);
            // 
            // LeftRotateButton
            // 
            this.LeftRotateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LeftRotateButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftRotateButton.Image = global::MechanicalSyncApp.Properties.Resources.rotate_icon_32;
            this.LeftRotateButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LeftRotateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LeftRotateButton.Name = "LeftRotateButton";
            this.LeftRotateButton.Size = new System.Drawing.Size(36, 36);
            this.LeftRotateButton.Text = "Rotate";
            this.LeftRotateButton.Click += new System.EventHandler(this.LeftRotateButton_Click);
            // 
            // LeftPanButton
            // 
            this.LeftPanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LeftPanButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftPanButton.Image = global::MechanicalSyncApp.Properties.Resources.move_icon_32;
            this.LeftPanButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LeftPanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LeftPanButton.Name = "LeftPanButton";
            this.LeftPanButton.Size = new System.Drawing.Size(36, 36);
            this.LeftPanButton.Text = "Pan";
            this.LeftPanButton.Click += new System.EventHandler(this.LeftPanButton_Click);
            // 
            // LeftSelectButton
            // 
            this.LeftSelectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LeftSelectButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftSelectButton.Image = global::MechanicalSyncApp.Properties.Resources.cursor_32;
            this.LeftSelectButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LeftSelectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LeftSelectButton.Name = "LeftSelectButton";
            this.LeftSelectButton.Size = new System.Drawing.Size(36, 36);
            this.LeftSelectButton.Text = "Select";
            this.LeftSelectButton.Click += new System.EventHandler(this.LeftSelectButton_Click);
            // 
            // LeftZoomToAreaButton
            // 
            this.LeftZoomToAreaButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LeftZoomToAreaButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftZoomToAreaButton.Image = global::MechanicalSyncApp.Properties.Resources.zoom_fit_32;
            this.LeftZoomToAreaButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LeftZoomToAreaButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LeftZoomToAreaButton.Name = "LeftZoomToAreaButton";
            this.LeftZoomToAreaButton.Size = new System.Drawing.Size(36, 36);
            this.LeftZoomToAreaButton.Text = "Zoom to area";
            this.LeftZoomToAreaButton.Click += new System.EventHandler(this.LeftZoomToAreaButton_Click);
            // 
            // LeftZoomButton
            // 
            this.LeftZoomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LeftZoomButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftZoomButton.Image = global::MechanicalSyncApp.Properties.Resources.zoom_32;
            this.LeftZoomButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LeftZoomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LeftZoomButton.Name = "LeftZoomButton";
            this.LeftZoomButton.Size = new System.Drawing.Size(36, 36);
            this.LeftZoomButton.Text = "Zoom";
            this.LeftZoomButton.Click += new System.EventHandler(this.LeftZoomButton_Click);
            // 
            // RightHandToolStrip
            // 
            this.RightHandToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RightHandToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.RightHandToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RightMeasureButton,
            this.RightRotateButton,
            this.RightPanButton,
            this.RightSelectButton,
            this.RightZoomToAreaButton,
            this.RightZoomButton});
            this.RightHandToolStrip.Location = new System.Drawing.Point(0, 466);
            this.RightHandToolStrip.Name = "RightHandToolStrip";
            this.RightHandToolStrip.Size = new System.Drawing.Size(433, 39);
            this.RightHandToolStrip.TabIndex = 2;
            this.RightHandToolStrip.Text = "toolStrip2";
            // 
            // RightMeasureButton
            // 
            this.RightMeasureButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RightMeasureButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RightMeasureButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightMeasureButton.Image = global::MechanicalSyncApp.Properties.Resources.tape_measure_32;
            this.RightMeasureButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.RightMeasureButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RightMeasureButton.Name = "RightMeasureButton";
            this.RightMeasureButton.Size = new System.Drawing.Size(36, 36);
            this.RightMeasureButton.Text = "Measure";
            this.RightMeasureButton.Click += new System.EventHandler(this.RightMeasureButton_Click);
            // 
            // RightRotateButton
            // 
            this.RightRotateButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RightRotateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RightRotateButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightRotateButton.Image = global::MechanicalSyncApp.Properties.Resources.rotate_icon_32;
            this.RightRotateButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.RightRotateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RightRotateButton.Name = "RightRotateButton";
            this.RightRotateButton.Size = new System.Drawing.Size(36, 36);
            this.RightRotateButton.Text = "Rotate";
            this.RightRotateButton.Click += new System.EventHandler(this.RightRotateButton_Click);
            // 
            // RightPanButton
            // 
            this.RightPanButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RightPanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RightPanButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightPanButton.Image = global::MechanicalSyncApp.Properties.Resources.move_icon_32;
            this.RightPanButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.RightPanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RightPanButton.Name = "RightPanButton";
            this.RightPanButton.Size = new System.Drawing.Size(36, 36);
            this.RightPanButton.Text = "Pan";
            this.RightPanButton.Click += new System.EventHandler(this.RightPanButton_Click);
            // 
            // RightSelectButton
            // 
            this.RightSelectButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RightSelectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RightSelectButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightSelectButton.Image = global::MechanicalSyncApp.Properties.Resources.cursor_32;
            this.RightSelectButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.RightSelectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RightSelectButton.Name = "RightSelectButton";
            this.RightSelectButton.Size = new System.Drawing.Size(36, 36);
            this.RightSelectButton.Text = "Select";
            this.RightSelectButton.Click += new System.EventHandler(this.RightSelectButton_Click);
            // 
            // RightZoomToAreaButton
            // 
            this.RightZoomToAreaButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RightZoomToAreaButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RightZoomToAreaButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightZoomToAreaButton.Image = global::MechanicalSyncApp.Properties.Resources.zoom_fit_32;
            this.RightZoomToAreaButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.RightZoomToAreaButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RightZoomToAreaButton.Name = "RightZoomToAreaButton";
            this.RightZoomToAreaButton.Size = new System.Drawing.Size(36, 36);
            this.RightZoomToAreaButton.Text = "Zoom to area";
            this.RightZoomToAreaButton.Click += new System.EventHandler(this.RightZoomToAreaButton_Click);
            // 
            // RightZoomButton
            // 
            this.RightZoomButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RightZoomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RightZoomButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightZoomButton.Image = global::MechanicalSyncApp.Properties.Resources.zoom_32;
            this.RightZoomButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.RightZoomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RightZoomButton.Name = "RightZoomButton";
            this.RightZoomButton.Size = new System.Drawing.Size(36, 36);
            this.RightZoomButton.Text = "Zoom";
            this.RightZoomButton.Click += new System.EventHandler(this.RightZoomButton_Click);
            // 
            // RightHandNotFound
            // 
            this.RightHandNotFound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightHandNotFound.Location = new System.Drawing.Point(0, 0);
            this.RightHandNotFound.Name = "RightHandNotFound";
            this.RightHandNotFound.Size = new System.Drawing.Size(433, 505);
            this.RightHandNotFound.TabIndex = 3;
            this.RightHandNotFound.Text = "File does not exist";
            this.RightHandNotFound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RightHandNotFound.Visible = false;
            // 
            // LeftHandNotFound
            // 
            this.LeftHandNotFound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftHandNotFound.Location = new System.Drawing.Point(0, 0);
            this.LeftHandNotFound.Name = "LeftHandNotFound";
            this.LeftHandNotFound.Size = new System.Drawing.Size(437, 505);
            this.LeftHandNotFound.TabIndex = 4;
            this.LeftHandNotFound.Text = "File does not exist";
            this.LeftHandNotFound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LeftHandNotFound.Visible = false;
            // 
            // DesignFileComparatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 505);
            this.Controls.Add(this.MainContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DesignFileComparatorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compare files";
            this.MainContainer.Panel1.ResumeLayout(false);
            this.MainContainer.Panel1.PerformLayout();
            this.MainContainer.Panel2.ResumeLayout(false);
            this.MainContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).EndInit();
            this.MainContainer.ResumeLayout(false);
            this.LeftHandToolStrip.ResumeLayout(false);
            this.LeftHandToolStrip.PerformLayout();
            this.RightHandToolStrip.ResumeLayout(false);
            this.RightHandToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainContainer;
        private System.Windows.Forms.Label LeftHandTitle;
        private System.Windows.Forms.Label RightHandTitle;
        private System.Windows.Forms.ToolStrip LeftHandToolStrip;
        private System.Windows.Forms.ToolStripButton LeftMeasureButton;
        private System.Windows.Forms.ToolStripButton LeftRotateButton;
        private System.Windows.Forms.ToolStripButton LeftPanButton;
        private System.Windows.Forms.ToolStripButton LeftSelectButton;
        private System.Windows.Forms.ToolStripButton LeftZoomToAreaButton;
        private System.Windows.Forms.ToolStripButton LeftZoomButton;
        private System.Windows.Forms.ToolStrip RightHandToolStrip;
        private System.Windows.Forms.ToolStripButton RightMeasureButton;
        private System.Windows.Forms.ToolStripButton RightRotateButton;
        private System.Windows.Forms.ToolStripButton RightPanButton;
        private System.Windows.Forms.ToolStripButton RightSelectButton;
        private System.Windows.Forms.ToolStripButton RightZoomToAreaButton;
        private System.Windows.Forms.ToolStripButton RightZoomButton;
        private System.Windows.Forms.Label LeftHandNotFound;
        private System.Windows.Forms.Label RightHandNotFound;
    }
}