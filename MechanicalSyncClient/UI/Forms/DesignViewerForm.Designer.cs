
namespace MechanicalSyncApp.UI.Forms
{
    partial class DesignViewerForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ViewerPanel = new System.Windows.Forms.Panel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.PanButton = new System.Windows.Forms.ToolStripButton();
            this.SelectButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomToAreaButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.PanButton,
            this.SelectButton,
            this.ZoomToAreaButton,
            this.ZoomButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 526);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(823, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ViewerPanel
            // 
            this.ViewerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewerPanel.Location = new System.Drawing.Point(0, 0);
            this.ViewerPanel.Name = "ViewerPanel";
            this.ViewerPanel.Size = new System.Drawing.Size(823, 526);
            this.ViewerPanel.TabIndex = 1;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = global::MechanicalSyncApp.Properties.Resources.tape_measure_32;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(96, 36);
            this.toolStripButton1.Text = "Measure";
            this.toolStripButton1.Click += new System.EventHandler(this.MeasureButton_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = global::MechanicalSyncApp.Properties.Resources.rotate_icon_32;
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(84, 36);
            this.toolStripButton2.Text = "Rotate";
            this.toolStripButton2.Click += new System.EventHandler(this.RotateButton_Click);
            // 
            // PanButton
            // 
            this.PanButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.PanButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanButton.Image = global::MechanicalSyncApp.Properties.Resources.move_icon_32;
            this.PanButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PanButton.Name = "PanButton";
            this.PanButton.Size = new System.Drawing.Size(67, 36);
            this.PanButton.Text = "Pan";
            this.PanButton.Click += new System.EventHandler(this.PanButton_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.SelectButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectButton.Image = global::MechanicalSyncApp.Properties.Resources.cursor_32;
            this.SelectButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SelectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(80, 36);
            this.SelectButton.Text = "Select";
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // ZoomToAreaButton
            // 
            this.ZoomToAreaButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZoomToAreaButton.Image = global::MechanicalSyncApp.Properties.Resources.zoom_fit_32;
            this.ZoomToAreaButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZoomToAreaButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomToAreaButton.Name = "ZoomToAreaButton";
            this.ZoomToAreaButton.Size = new System.Drawing.Size(127, 36);
            this.ZoomToAreaButton.Text = "Zoom to area";
            this.ZoomToAreaButton.Click += new System.EventHandler(this.ZoomToAreaButton_Click);
            // 
            // ZoomButton
            // 
            this.ZoomButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZoomButton.Image = global::MechanicalSyncApp.Properties.Resources.zoom_32;
            this.ZoomButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZoomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomButton.Name = "ZoomButton";
            this.ZoomButton.Size = new System.Drawing.Size(80, 36);
            this.ZoomButton.Text = "Zoom";
            this.ZoomButton.Click += new System.EventHandler(this.ZoomButton_Click);
            // 
            // DesignViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(823, 565);
            this.Controls.Add(this.ViewerPanel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DesignViewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DesignViewerForm";
            this.Load += new System.EventHandler(this.DesignViewerForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton PanButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton SelectButton;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Panel ViewerPanel;
        private System.Windows.Forms.ToolStripButton ZoomButton;
        private System.Windows.Forms.ToolStripButton ZoomToAreaButton;
    }
}