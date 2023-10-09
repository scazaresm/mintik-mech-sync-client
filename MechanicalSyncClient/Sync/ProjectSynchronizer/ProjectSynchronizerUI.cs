using MechanicalSyncApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
    public class ProjectSynchronizerUI
    {
        public FileViewer FileViewer { get; private set; }

        // Form components
        public ListView FileViewerListView { get; set; }
        public ToolStrip SynchronizerToolStrip { get; set; }
        public ToolStripStatusLabel StatusLabel { get; set; }
        public ToolStripButton StartWorkingButton { get; set; }
        public ToolStripButton StopWorkingButton { get; set; }
        public ToolStripButton SyncRemoteButton { get; set; }
        public ToolStripProgressBar SyncProgressBar { get; set; }

        public void InitializeFileViewer(LocalProject localProject)
        {
            if (localProject is null)
            {
                throw new ArgumentNullException(nameof(localProject));
            }
            FileViewer = new FileViewer(localProject.LocalDirectory);
        }
    }
}
