﻿namespace MechanicalSyncApp.UI.Forms
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
            this.ChangeDescription = new System.Windows.Forms.TextBox();
            this.ChangeRequestToolStrip = new System.Windows.Forms.ToolStrip();
            this.OkButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.ToolStripButton();
            this.EditButton = new System.Windows.Forms.ToolStripButton();
            this.PasteImageButton = new System.Windows.Forms.ToolStripButton();
            this.DetailsPicture = new System.Windows.Forms.PictureBox();
            this.RejectChangeButton = new System.Windows.Forms.ToolStripButton();
            this.AcceptChangeButton = new System.Windows.Forms.ToolStripButton();
            this.CancelActionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ChangeRequestToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DetailsPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 40);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DetailsPicture);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ChangeDescription);
            this.splitContainer1.Size = new System.Drawing.Size(564, 258);
            this.splitContainer1.SplitterDistance = 287;
            this.splitContainer1.TabIndex = 0;
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
            // 
            // ChangeRequestToolStrip
            // 
            this.ChangeRequestToolStrip.AutoSize = false;
            this.ChangeRequestToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ChangeRequestToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AcceptChangeButton,
            this.RejectChangeButton,
            this.EditButton,
            this.DeleteButton,
            this.PasteImageButton});
            this.ChangeRequestToolStrip.Location = new System.Drawing.Point(8, 12);
            this.ChangeRequestToolStrip.Name = "ChangeRequestToolStrip";
            this.ChangeRequestToolStrip.Size = new System.Drawing.Size(568, 25);
            this.ChangeRequestToolStrip.TabIndex = 1;
            this.ChangeRequestToolStrip.Text = "toolStrip1";
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
            // DeleteButton
            // 
            this.DeleteButton.Image = global::MechanicalSyncApp.Properties.Resources.delete_icon_24;
            this.DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(60, 22);
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.ToolTipText = "Reject assembly";
            // 
            // EditButton
            // 
            this.EditButton.Image = global::MechanicalSyncApp.Properties.Resources.pencil_24;
            this.EditButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(47, 22);
            this.EditButton.Text = "Edit";
            this.EditButton.ToolTipText = "Reject assembly";
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
            // DetailsPicture
            // 
            this.DetailsPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsPicture.Image = global::MechanicalSyncApp.Properties.Resources.image_placeholder_350x350;
            this.DetailsPicture.InitialImage = null;
            this.DetailsPicture.Location = new System.Drawing.Point(0, 0);
            this.DetailsPicture.Name = "DetailsPicture";
            this.DetailsPicture.Size = new System.Drawing.Size(287, 258);
            this.DetailsPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DetailsPicture.TabIndex = 0;
            this.DetailsPicture.TabStop = false;
            // 
            // RejectChangeButton
            // 
            this.RejectChangeButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RejectChangeButton.Image = global::MechanicalSyncApp.Properties.Resources.thumbs_down_24;
            this.RejectChangeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RejectChangeButton.Name = "RejectChangeButton";
            this.RejectChangeButton.Size = new System.Drawing.Size(101, 22);
            this.RejectChangeButton.Text = "Reject change";
            this.RejectChangeButton.ToolTipText = "Reject assembly";
            // 
            // AcceptChangeButton
            // 
            this.AcceptChangeButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.AcceptChangeButton.Image = global::MechanicalSyncApp.Properties.Resources.thumbs_up_24;
            this.AcceptChangeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AcceptChangeButton.Name = "AcceptChangeButton";
            this.AcceptChangeButton.Size = new System.Drawing.Size(106, 22);
            this.AcceptChangeButton.Text = "Accept change";
            this.AcceptChangeButton.ToolTipText = "Reject assembly";
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
            this.ChangeRequestToolStrip.ResumeLayout(false);
            this.ChangeRequestToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DetailsPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox DetailsPicture;
        private System.Windows.Forms.TextBox ChangeDescription;
        private System.Windows.Forms.ToolStrip ChangeRequestToolStrip;
        private System.Windows.Forms.ToolStripButton DeleteButton;
        private System.Windows.Forms.ToolStripButton EditButton;
        private System.Windows.Forms.ToolStripButton PasteImageButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.ToolStripButton AcceptChangeButton;
        private System.Windows.Forms.ToolStripButton RejectChangeButton;
        private System.Windows.Forms.Button CancelActionButton;
    }
}