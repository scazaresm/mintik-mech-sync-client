using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Util;
using MechanicalSyncApp.Sync;
using MechanicalSyncApp.UI;
using MechanicalSyncApp.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher
{
    public class DeliverablePublisherUI
    {
        public ReviewableDrawingsGrid ReviewableDrawingsViewer { get; set; }

        public DataGridView DrawingsGridView { get; set; }

        public ToolStripButton ViewBlockersButton { get; set; }

        public ToolStripLabel StatusLabel { get; set; }

        public ToolStripProgressBar Progress { get; set; }

        public ToolStrip MainToolStrip { get; set; }

        public StatusStrip MainStatusStrip { get; set; }

        public void Initialize()
        {
            ReviewableDrawingsViewer = new ReviewableDrawingsGrid();
            ReviewableDrawingsViewer.AttachDataGridView(DrawingsGridView);
            
            MainToolStrip.Enabled = false;
            StatusLabel.Text = "Initializing...";
            Progress.Visible = false;

            DrawingsGridView.SelectionChanged += DrawingsGridView_SelectionChanged;
            ViewBlockersButton.Click += ViewBlockersButton_Click;
        }

        private void DrawingsGridView_SelectionChanged(object sender, EventArgs e)
        {
            var selectedRows = DrawingsGridView.SelectedRows;

            if (selectedRows == null)
                return;

            ViewBlockersButton.Enabled = 
                selectedRows.Count == 1 && 
                (selectedRows[0].Tag as FileMetadata).PublishingStatus == PublishingStatus.Blocked;
        }

        private void ViewBlockersButton_Click(object sender, EventArgs e)
        {
            var selectedRows = DrawingsGridView.SelectedRows;

            if (selectedRows == null || selectedRows.Count != 1)
                return;

            var selectedDrawing = selectedRows[0].Tag as FileMetadata;
            new PublishingBlockersForm(selectedDrawing).ShowDialog();
        }
    }
}
