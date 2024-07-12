namespace MechanicalSyncApp.UI.Forms
{
    partial class IgnoreDrawingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.IgnoredDrawingsGrid = new System.Windows.Forms.DataGridView();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelIgnoreButton = new System.Windows.Forms.Button();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.UnselectAllButton = new System.Windows.Forms.Button();
            this.selectionColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.drawingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.IgnoredDrawingsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(365, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ignored drawings will not be considered for 2D review and deliverable publishing." +
    "";
            // 
            // IgnoredDrawingsGrid
            // 
            this.IgnoredDrawingsGrid.AllowUserToAddRows = false;
            this.IgnoredDrawingsGrid.AllowUserToDeleteRows = false;
            this.IgnoredDrawingsGrid.AllowUserToOrderColumns = true;
            this.IgnoredDrawingsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IgnoredDrawingsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selectionColumn,
            this.drawingColumn});
            this.IgnoredDrawingsGrid.Location = new System.Drawing.Point(15, 60);
            this.IgnoredDrawingsGrid.Name = "IgnoredDrawingsGrid";
            this.IgnoredDrawingsGrid.ReadOnly = true;
            this.IgnoredDrawingsGrid.RowHeadersVisible = false;
            this.IgnoredDrawingsGrid.Size = new System.Drawing.Size(365, 229);
            this.IgnoredDrawingsGrid.TabIndex = 1;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(305, 347);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelIgnoreButton
            // 
            this.CancelIgnoreButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelIgnoreButton.Location = new System.Drawing.Point(224, 347);
            this.CancelIgnoreButton.Name = "CancelIgnoreButton";
            this.CancelIgnoreButton.Size = new System.Drawing.Size(75, 23);
            this.CancelIgnoreButton.TabIndex = 3;
            this.CancelIgnoreButton.Text = "Cancel";
            this.CancelIgnoreButton.UseVisualStyleBackColor = true;
            // 
            // SelectAllButton
            // 
            this.SelectAllButton.Location = new System.Drawing.Point(15, 295);
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.Size = new System.Drawing.Size(75, 23);
            this.SelectAllButton.TabIndex = 4;
            this.SelectAllButton.Text = "Select all";
            this.SelectAllButton.UseVisualStyleBackColor = true;
            this.SelectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
            // 
            // UnselectAllButton
            // 
            this.UnselectAllButton.Location = new System.Drawing.Point(96, 295);
            this.UnselectAllButton.Name = "UnselectAllButton";
            this.UnselectAllButton.Size = new System.Drawing.Size(75, 23);
            this.UnselectAllButton.TabIndex = 5;
            this.UnselectAllButton.Text = "Unselect all";
            this.UnselectAllButton.UseVisualStyleBackColor = true;
            this.UnselectAllButton.Click += new System.EventHandler(this.UnselectAllButton_Click);
            // 
            // selectionColumn
            // 
            this.selectionColumn.HeaderText = "";
            this.selectionColumn.Name = "selectionColumn";
            this.selectionColumn.ReadOnly = true;
            this.selectionColumn.Width = 30;
            // 
            // drawingColumn
            // 
            this.drawingColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.drawingColumn.HeaderText = "Drawing";
            this.drawingColumn.Name = "drawingColumn";
            this.drawingColumn.ReadOnly = true;
            this.drawingColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // IgnoreDrawingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelIgnoreButton;
            this.ClientSize = new System.Drawing.Size(393, 392);
            this.Controls.Add(this.UnselectAllButton);
            this.Controls.Add(this.SelectAllButton);
            this.Controls.Add(this.CancelIgnoreButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.IgnoredDrawingsGrid);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IgnoreDrawingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ignore drawings";
            this.Load += new System.EventHandler(this.IgnoreDrawingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IgnoredDrawingsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView IgnoredDrawingsGrid;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelIgnoreButton;
        private System.Windows.Forms.Button SelectAllButton;
        private System.Windows.Forms.Button UnselectAllButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawingColumn;
    }
}