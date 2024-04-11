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

        public ToolStripButton PublishSelectedButton { get; set; }

        public ToolStripButton CancelSelectedButton { get; set; }

        public ToolStripButton ValidateButton { get; set; }

        public ToolStripLabel StatusLabel { get; set; }

        public ToolStripProgressBar Progress { get; set; }

        public ToolStrip MainToolStrip { get; set; }

        public StatusStrip MainStatusStrip { get; set; }

        public void Initialize()
        {
            ReviewableDrawingsViewer = new ReviewableDrawingsGrid();
            ReviewableDrawingsViewer.AttachDataGridView(DrawingsGridView);
            
            MainToolStrip.Enabled = false;
            ViewBlockersButton.Enabled = false;
            PublishSelectedButton.Enabled = false;
            CancelSelectedButton.Enabled = false;
            StatusLabel.Text = "Initializing...";
            Progress.Visible = false;
        }

    
    }
}
