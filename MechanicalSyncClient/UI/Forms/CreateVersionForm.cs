using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.MechSync.Exceptions;
using MechanicalSyncApp.Core.Services.MechSync;
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
    public partial class CreateVersionForm : Form
    {
        public UserDetails VersionOwner { get; set; }
        public Project ParentProject { get; set; }


        public CreateVersionForm()
        {
            InitializeComponent();
        }

        private async void CreateVersionButton_Click(object sender, EventArgs e)
        {
            CreateVersionButton.Enabled = true;
            await CreateVersion();
            CreateVersionButton.Enabled = false;
        }

        private void SelectOwnerButton_Click(object sender, EventArgs e)
        {
            var userSelector = new UserSelectorForm(
                "Select user",
                "Select the user who will be the owner for the new version:"
            )
            { IncludeMyself = true };

            var response = userSelector.ShowDialog();

            if (response == DialogResult.OK)
            {
                VersionOwnerFullName.Text = userSelector.SelectedUserDetails.FullName;
                VersionOwner = userSelector.SelectedUserDetails;
            }
            else
            {
                VersionOwnerFullName.Text = "";
                VersionOwner = null;
            }
            ValidateData();
        }

        private void SelectProjectButton_Click(object sender, EventArgs e)
        {
            var projectSelector = new ProjectSelectorForm(
                "Select project", 
                "Select a project to create the new version from:"
            )
            { PublishedOnly = true };

            var response = projectSelector.ShowDialog();

            if (response == DialogResult.OK) 
            {
                ParentProjectFolderName.Text = projectSelector.SelectedProject.FolderName;
                ParentProject = projectSelector.SelectedProject;
            }
            else
            {
                ParentProjectFolderName.Text = "";
                ParentProject = null;
            }
            ValidateData();
        }

        private void ValidateData()
        {
            CreateVersionButton.Enabled = 
                ParentProject != null && 
                VersionOwner != null && 
                Goal.Text.Length > 0 && 
                Reason.Text.Length > 0;
        }

        private void Goal_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private async Task CreateVersion()
        {
            try
            {
                ErrorMessage.Visible = false;
                await MechSyncServiceClient.Instance.CreateVersionAsync(new CreateVersionRequest()
                {
                    ProjectId = ParentProject.Id,
                    OwnerId = VersionOwner.Id,
                    Goal = Goal.Text,
                    Reason = Reason.Text,
                });
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reason_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }
    }
}
