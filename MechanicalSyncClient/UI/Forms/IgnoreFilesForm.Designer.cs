namespace MechanicalSyncApp.UI.Forms
{
    partial class IgnoreFilesForm
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
            this.selectionColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.drawingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelIgnoreButton = new System.Windows.Forms.Button();
            this.SelectAllDrawingsButton = new System.Windows.Forms.Button();
            this.UnselectAllDrawingsButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.drawingsTab = new System.Windows.Forms.TabPage();
            this.assembliesTab = new System.Windows.Forms.TabPage();
            this.IgnoredAssembliesGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnselectAllAssembliesButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectAllAssembliesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.IgnoredDrawingsGrid)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.drawingsTab.SuspendLayout();
            this.assembliesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IgnoredAssembliesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(404, 23);
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
            this.IgnoredDrawingsGrid.Location = new System.Drawing.Point(16, 37);
            this.IgnoredDrawingsGrid.Name = "IgnoredDrawingsGrid";
            this.IgnoredDrawingsGrid.RowHeadersVisible = false;
            this.IgnoredDrawingsGrid.Size = new System.Drawing.Size(404, 244);
            this.IgnoredDrawingsGrid.TabIndex = 1;
            // 
            // selectionColumn
            // 
            this.selectionColumn.HeaderText = "";
            this.selectionColumn.Name = "selectionColumn";
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
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(380, 372);
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
            this.CancelIgnoreButton.Location = new System.Drawing.Point(299, 372);
            this.CancelIgnoreButton.Name = "CancelIgnoreButton";
            this.CancelIgnoreButton.Size = new System.Drawing.Size(75, 23);
            this.CancelIgnoreButton.TabIndex = 3;
            this.CancelIgnoreButton.Text = "Cancel";
            this.CancelIgnoreButton.UseVisualStyleBackColor = true;
            // 
            // SelectAllDrawingsButton
            // 
            this.SelectAllDrawingsButton.Location = new System.Drawing.Point(16, 287);
            this.SelectAllDrawingsButton.Name = "SelectAllDrawingsButton";
            this.SelectAllDrawingsButton.Size = new System.Drawing.Size(75, 23);
            this.SelectAllDrawingsButton.TabIndex = 4;
            this.SelectAllDrawingsButton.Text = "Select all";
            this.SelectAllDrawingsButton.UseVisualStyleBackColor = true;
            this.SelectAllDrawingsButton.Click += new System.EventHandler(this.SelectAllDrawingsButton_Click);
            // 
            // UnselectAllDrawingsButton
            // 
            this.UnselectAllDrawingsButton.Location = new System.Drawing.Point(97, 287);
            this.UnselectAllDrawingsButton.Name = "UnselectAllDrawingsButton";
            this.UnselectAllDrawingsButton.Size = new System.Drawing.Size(75, 23);
            this.UnselectAllDrawingsButton.TabIndex = 5;
            this.UnselectAllDrawingsButton.Text = "Unselect all";
            this.UnselectAllDrawingsButton.UseVisualStyleBackColor = true;
            this.UnselectAllDrawingsButton.Click += new System.EventHandler(this.UnselectAllDrawingsButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.drawingsTab);
            this.tabControl1.Controls.Add(this.assembliesTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(447, 350);
            this.tabControl1.TabIndex = 6;
            // 
            // drawingsTab
            // 
            this.drawingsTab.Controls.Add(this.IgnoredDrawingsGrid);
            this.drawingsTab.Controls.Add(this.UnselectAllDrawingsButton);
            this.drawingsTab.Controls.Add(this.label1);
            this.drawingsTab.Controls.Add(this.SelectAllDrawingsButton);
            this.drawingsTab.Location = new System.Drawing.Point(4, 22);
            this.drawingsTab.Name = "drawingsTab";
            this.drawingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.drawingsTab.Size = new System.Drawing.Size(439, 324);
            this.drawingsTab.TabIndex = 0;
            this.drawingsTab.Text = "Drawings";
            this.drawingsTab.UseVisualStyleBackColor = true;
            // 
            // assembliesTab
            // 
            this.assembliesTab.Controls.Add(this.IgnoredAssembliesGrid);
            this.assembliesTab.Controls.Add(this.UnselectAllAssembliesButton);
            this.assembliesTab.Controls.Add(this.label2);
            this.assembliesTab.Controls.Add(this.SelectAllAssembliesButton);
            this.assembliesTab.Location = new System.Drawing.Point(4, 22);
            this.assembliesTab.Name = "assembliesTab";
            this.assembliesTab.Padding = new System.Windows.Forms.Padding(3);
            this.assembliesTab.Size = new System.Drawing.Size(439, 324);
            this.assembliesTab.TabIndex = 1;
            this.assembliesTab.Text = "Assemblies";
            this.assembliesTab.UseVisualStyleBackColor = true;
            // 
            // IgnoredAssembliesGrid
            // 
            this.IgnoredAssembliesGrid.AllowUserToAddRows = false;
            this.IgnoredAssembliesGrid.AllowUserToDeleteRows = false;
            this.IgnoredAssembliesGrid.AllowUserToOrderColumns = true;
            this.IgnoredAssembliesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IgnoredAssembliesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1});
            this.IgnoredAssembliesGrid.Location = new System.Drawing.Point(16, 37);
            this.IgnoredAssembliesGrid.Name = "IgnoredAssembliesGrid";
            this.IgnoredAssembliesGrid.RowHeadersVisible = false;
            this.IgnoredAssembliesGrid.Size = new System.Drawing.Size(404, 244);
            this.IgnoredAssembliesGrid.TabIndex = 7;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 30;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Assembly";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // UnselectAllAssembliesButton
            // 
            this.UnselectAllAssembliesButton.Location = new System.Drawing.Point(97, 287);
            this.UnselectAllAssembliesButton.Name = "UnselectAllAssembliesButton";
            this.UnselectAllAssembliesButton.Size = new System.Drawing.Size(75, 23);
            this.UnselectAllAssembliesButton.TabIndex = 9;
            this.UnselectAllAssembliesButton.Text = "Unselect all";
            this.UnselectAllAssembliesButton.UseVisualStyleBackColor = true;
            this.UnselectAllAssembliesButton.Click += new System.EventHandler(this.UnselectAllAssembliesButton_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(365, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ignored assemblies will not be considered for 3D review.";
            // 
            // SelectAllAssembliesButton
            // 
            this.SelectAllAssembliesButton.Location = new System.Drawing.Point(16, 287);
            this.SelectAllAssembliesButton.Name = "SelectAllAssembliesButton";
            this.SelectAllAssembliesButton.Size = new System.Drawing.Size(75, 23);
            this.SelectAllAssembliesButton.TabIndex = 8;
            this.SelectAllAssembliesButton.Text = "Select all";
            this.SelectAllAssembliesButton.UseVisualStyleBackColor = true;
            this.SelectAllAssembliesButton.Click += new System.EventHandler(this.SelectAllAssembliesButton_Click);
            // 
            // IgnoreFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelIgnoreButton;
            this.ClientSize = new System.Drawing.Size(471, 415);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.CancelIgnoreButton);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IgnoreFilesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ignore files";
            this.Load += new System.EventHandler(this.IgnoreFilesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IgnoredDrawingsGrid)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.drawingsTab.ResumeLayout(false);
            this.assembliesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IgnoredAssembliesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView IgnoredDrawingsGrid;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelIgnoreButton;
        private System.Windows.Forms.Button SelectAllDrawingsButton;
        private System.Windows.Forms.Button UnselectAllDrawingsButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawingColumn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage drawingsTab;
        private System.Windows.Forms.TabPage assembliesTab;
        private System.Windows.Forms.DataGridView IgnoredAssembliesGrid;
        private System.Windows.Forms.Button UnselectAllAssembliesButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SelectAllAssembliesButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    }
}