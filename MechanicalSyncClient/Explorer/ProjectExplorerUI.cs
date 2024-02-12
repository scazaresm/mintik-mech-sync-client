using MechanicalSyncApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Explorer
{
    public class ProjectExplorerUI : IProjectExplorerUI
    {
        #region Form controls

        public ToolStripTextBox FileSearchFilter { get; set; }
        public SplitContainer ExplorerContainer {  get; set; }
        public ListView ProjectList {  get; set; }
        public Label ProjectFolderNameLabel { get; set; }
        public TextBox ProjectSearchFilter { get; set; }
        public ToolStripComboBox VersionSelector { get; set; }
        public ToolStripButton CloseProjectButton {  get; set; }
        public ToolStripButton RefreshFilesButton {  get; set; }
        public ListView FileList { get; set; }  
        public ToolStripStatusLabel VersionFilesStatusLabel { get; set; }

        #endregion

        public void ShowProjectList()
        {
            ExplorerContainer.Panel1Collapsed = false;
            ExplorerContainer.Panel2Collapsed = true;
        }

        public void ShowProjectFiles()
        {
            ExplorerContainer.Panel1Collapsed = true;
            ExplorerContainer.Panel2Collapsed = false;
        }

    }
}
