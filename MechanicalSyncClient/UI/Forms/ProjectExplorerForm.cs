using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Explorer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class ProjectExplorerForm : Form
    {
        private ProjectExplorer projectExplorer;

        public ProjectExplorerForm()
        {
            InitializeComponent();

            IProjectExplorerUI ui = new ProjectExplorerUI()
            {
                ProjectSearchFilter = ProjectSearchFilter,
                FileSearchFilter = FileSearchFilter,
                ExplorerContainer = ExplorerContainer,
                ProjectList = ProjectList,
                FileList = FileList,
                ProjectFolderNameLabel = ProjectFolderNameLabel,
                VersionSelector = VersionSelector,
                CloseProjectButton = CloseProjectButton,
                RefreshFilesButton = RefreshFilesButton,
                VersionFilesStatusLabel = VersionFilesStatusLabel,
            };
            projectExplorer = new ProjectExplorer(
                AuthenticationServiceClient.Instance,
                MechSyncServiceClient.Instance,
                ui
            );

            _ = projectExplorer.RefreshProjectListAsync();
        }

        private  void ProjectExplorerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var loggedUser = AuthenticationServiceClient.Instance.LoggedUserDetails;

            if (loggedUser == null)
                throw new Exception("Unexpected null value on logged user.");

            if (loggedUser.Role == "Viewer")
            {
                var confirmation = MessageBox.Show(
                    "Are you sure to quit the project explorer?", "Exit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );

                if (confirmation != DialogResult.Yes)
                    e.Cancel = true;
            }
            else
            {
                VersionSynchronizerForm.Instance.Show();
            }
        }
    }
}
