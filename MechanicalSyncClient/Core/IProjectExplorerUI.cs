using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Core
{
    public interface IProjectExplorerUI
    {
        SplitContainer ExplorerContainer { get; set; }
        ListView ProjectList { get; set; }

        Label ProjectFolderNameLabel {  get; set; }

        ToolStripComboBox VersionSelector { get; set; }

        ToolStripButton CloseProjectButton { get; set; }

        ToolStripButton RefreshFilesButton { get; set; }

        ToolStripTextBox FileSearchFilter { get; set; }

        ListView FileList { get; set; }

        TextBox ProjectSearchFilter { get; set; }

        ToolStripStatusLabel VersionFilesStatusLabel { get; set; }

        void ShowProjectList();
        void ShowProjectFiles();
    }
}
