namespace MechanicalSyncApp.UI.Forms
{
    partial class UpdateChangeRequestDialog
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
            this.CancelActionButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DesignerComments = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChangeStatus = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DetailsPictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(14, 45);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.DetailsPictureBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ChangeDescription);
            this.splitContainer1.Size = new System.Drawing.Size(564, 258);
            this.splitContainer1.SplitterDistance = 287;
            this.splitContainer1.TabIndex = 4;
            // 
            // DetailsPictureBox
            // 
            this.DetailsPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsPictureBox.Image = global::MechanicalSyncApp.Properties.Resources.image_placeholder_350x350;
            this.DetailsPictureBox.InitialImage = null;
            this.DetailsPictureBox.Location = new System.Drawing.Point(0, 0);
            this.DetailsPictureBox.Name = "DetailsPictureBox";
            this.DetailsPictureBox.Size = new System.Drawing.Size(287, 258);
            this.DetailsPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DetailsPictureBox.TabIndex = 0;
            this.DetailsPictureBox.TabStop = false;
            this.DetailsPictureBox.DoubleClick += new System.EventHandler(this.DetailsPictureBox_DoubleClick);
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
            // CancelActionButton
            // 
            this.CancelActionButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelActionButton.Location = new System.Drawing.Point(449, 530);
            this.CancelActionButton.Name = "CancelActionButton";
            this.CancelActionButton.Size = new System.Drawing.Size(75, 23);
            this.CancelActionButton.TabIndex = 7;
            this.CancelActionButton.Text = "Cancel";
            this.CancelActionButton.UseVisualStyleBackColor = true;
            this.CancelActionButton.Click += new System.EventHandler(this.CancelActionButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(530, 530);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 318);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Change request details";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.DesignerComments);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.ChangeStatus);
            this.groupBox2.Location = new System.Drawing.Point(12, 349);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(593, 175);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Designer feedback";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Comments:";
            // 
            // DesignerComments
            // 
            this.DesignerComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DesignerComments.Location = new System.Drawing.Point(14, 91);
            this.DesignerComments.Multiline = true;
            this.DesignerComments.Name = "DesignerComments";
            this.DesignerComments.Size = new System.Drawing.Size(564, 68);
            this.DesignerComments.TabIndex = 2;
            this.DesignerComments.TextChanged += new System.EventHandler(this.DesignerComments_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Change status:";
            // 
            // ChangeStatus
            // 
            this.ChangeStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChangeStatus.FormattingEnabled = true;
            this.ChangeStatus.Items.AddRange(new object[] {
            "Pending",
            "Done",
            "Discarded"});
            this.ChangeStatus.Location = new System.Drawing.Point(14, 43);
            this.ChangeStatus.Name = "ChangeStatus";
            this.ChangeStatus.Size = new System.Drawing.Size(197, 21);
            this.ChangeStatus.TabIndex = 0;
            this.ChangeStatus.SelectedIndexChanged += new System.EventHandler(this.ChangeStatus_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(285, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Double click the image to open it in Windows image viewer";
            // 
            // UpdateChangeRequestDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelActionButton;
            this.ClientSize = new System.Drawing.Size(618, 565);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CancelActionButton);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateChangeRequestDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update change request";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateChangeRequestDialog_FormClosing);
            this.Load += new System.EventHandler(this.UpdateChangeRequestDialog_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DetailsPictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox DetailsPictureBox;
        private System.Windows.Forms.TextBox ChangeDescription;
        private System.Windows.Forms.Button CancelActionButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox DesignerComments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ChangeStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}