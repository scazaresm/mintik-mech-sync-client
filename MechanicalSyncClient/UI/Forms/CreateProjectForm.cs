using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Exceptions;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
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
    public partial class CreateProjectForm : Form
    {
        private UserDetails initialVersionOwner;

        public CreateProjectForm()
        {
            InitializeComponent();
        }

        private void CancelCreateProjectButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private async void CreateProjectButton_Click(object sender, EventArgs e)
        {
            CreateProjectButton.Enabled = false;
            await CreateProject();
            CreateProjectButton.Enabled = true;
        }

        private void FolderNameTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void SelectUserButton_Click(object sender, EventArgs e)
        {
            var userSelector = new UserSelectorForm(
                "Select user",
                "Select the user who will be assigned as owner to the new project's initial version:"
            ) {
                IncludeMyself = true
            };

            var response = userSelector.ShowDialog();

            if (response == DialogResult.OK)
            {
                initialVersionOwner = userSelector.SelectedUserDetails;
                InitialVersionOwnerTextBox.Text = initialVersionOwner.FullName;
            }
            else
            {
                initialVersionOwner = null;
                InitialVersionOwnerTextBox.Text = "";
            }
            ValidateData();
        }

        private void ValidateData()
        {
            CreateProjectButton.Enabled = FolderNameTextBox.Text.Length > 0 && initialVersionOwner != null;
        }

        private async Task CreateProject()
        {
            try
            {
                ErrorMessage.Visible = false;
                await MechSyncServiceClient.Instance.CreateProjectAsync(new CreateProjectRequest()
                {
                    FolderName = FolderNameTextBox.Text,
                    InitialVersionOwnerId = initialVersionOwner.Id
                });
                DialogResult = DialogResult.OK;
            }
            catch (ProjectFolderAlreadyExistsException)
            {
                ErrorMessage.Text = "A folder with such name already exists, try another.";
                ErrorMessage.Visible = true;
            }
            catch (InvalidProjectFolderException)
            {
                ErrorMessage.Text = "Folder name is invalid, try another.";
                ErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
