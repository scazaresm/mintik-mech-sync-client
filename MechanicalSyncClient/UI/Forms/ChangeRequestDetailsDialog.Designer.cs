namespace MechanicalSyncApp.UI.Forms
{
    partial class ChangeRequestDetailsDialog
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DetailsPictureBox = new System.Windows.Forms.PictureBox();
            this.ChangeDescription = new System.Windows.Forms.TextBox();
            this.ChangeRequestToolStrip = new System.Windows.Forms.ToolStrip();
            this.DeleteButton = new System.Windows.Forms.ToolStripButton();
            this.PasteImageButton = new System.Windows.Forms.ToolStripButton();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelActionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DetailsPictureBox)).BeginInit();
            this.ChangeRequestToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 40);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DetailsPictureBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ChangeDescription);
            this.splitContainer1.Size = new System.Drawing.Size(564, 258);
            this.splitContainer1.SplitterDistance = 287;
            this.splitContainer1.TabIndex = 0;
            // 
            // DetailsPictureBox
            // 
            this.DetailsPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsPictureBox.Image = global::MechanicalSyncApp.Properties.Resources.image_placeholder_350x350;
            this.DetailsPictureBox.InitialImage = null;
            this.DetailsPictureBox.Location = new System.Drawing.Point(0, 0);
            this.DetailsPictureBox.Name = "DetailsPictureBox";
            this.DetailsPictureBox.Size = new System.Drawing.Size(287, 258);
            this.DetailsPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DetailsPictureBox.TabIndex = 0;
            this.DetailsPictureBox.TabStop = false;
            // 
            // ChangeDescription
            // 
            this.ChangeDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeDescription.Location = new System.Drawing.Point(0, 0);
            this.ChangeDescription.Multiline = true;
            this.ChangeDescription.Name = "ChangeDescription";
            this.ChangeDescription.ReadOnly = true;
            this.ChangeDescription.Size = new System.Drawing.Size(273, 258);
            this.ChangeDescription.TabIndex = 0;
            this.ChangeDescription.TextChanged += new System.EventHandler(this.ChangeDescription_TextChanged);
            // 
            // ChangeRequestToolStrip
            // 
            this.ChangeRequestToolStrip.AutoSize = false;
            this.ChangeRequestToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ChangeRequestToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteButton,
            this.PasteImageButton});
            this.ChangeRequestToolStrip.Location = new System.Drawing.Point(8, 12);
            this.ChangeRequestToolStrip.Name = "ChangeRequestToolStrip";
            this.ChangeRequestToolStrip.Size = new System.Drawing.Size(568, 25);
            this.ChangeRequestToolStrip.TabIndex = 1;
            this.ChangeRequestToolStrip.Text = "toolStrip1";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Image = global::MechanicalSyncApp.Properties.Resources.delete_icon_24;
            this.DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(60, 22);
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.ToolTipText = "Delete change request";
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // PasteImageButton
            // 
            this.PasteImageButton.Image = global::MechanicalSyncApp.Properties.Resources.paste_24;
            this.PasteImageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteImageButton.Name = "PasteImageButton";
            this.PasteImageButton.Size = new System.Drawing.Size(91, 22);
            this.PasteImageButton.Text = "Paste image";
            this.PasteImageButton.ToolTipText = "Reject assembly";
            this.PasteImageButton.Click += new System.EventHandler(this.PasteImageButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(501, 308);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelActionButton
            // 
            this.CancelActionButton.Location = new System.Drawing.Point(420, 308);
            this.CancelActionButton.Name = "CancelActionButton";
            this.CancelActionButton.Size = new System.Drawing.Size(75, 23);
            this.CancelActionButton.TabIndex = 3;
            this.CancelActionButton.Text = "Cancel";
            this.CancelActionButton.UseVisualStyleBackColor = true;
            this.CancelActionButton.Click += new System.EventHandler(this.CancelActionButton_Click);
            // 
            // ChangeRequestDetailsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 343);
            this.Controls.Add(this.CancelActionButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.ChangeRequestToolStrip);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeRequestDetailsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Request Details";
            this.Load += new System.EventHandler(this.ChangeRequestDetailsDialog_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DetailsPictureBox)).EndInit();
            this.ChangeRequestToolStrip.ResumeLayout(false);
            this.ChangeRequestToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox DetailsPictureBox;
        private System.Windows.Forms.TextBox ChangeDescription;
        private System.Windows.Forms.ToolStrip ChangeRequestToolStrip;
        private System.Windows.Forms.ToolStripButton DeleteButton;
        private System.Windows.Forms.ToolStripButton PasteImageButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelActionButton;
    }
}