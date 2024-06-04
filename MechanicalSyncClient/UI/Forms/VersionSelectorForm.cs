using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.UI.Forms
{

    public partial class VersionSelectorForm : Form
    {
        private readonly string statusFilter;

        private readonly IAuthenticationServiceClient authServiceClient = AuthenticationServiceClient.Instance;
        private readonly IMechSyncServiceClient mechSyncService = MechSyncServiceClient.Instance;

        private readonly List<string> allowedStatuses = new List<string>() { "Ongoing", "Latest", "History" };

        private readonly Dictionary<string, Project> projectCache = new Dictionary<string, Project>();
        private readonly Dictionary<string, UserDetails> userDetailsIndex = new Dictionary<string, UserDetails>();

        public SelectedVersionDetails SelectedVersionDetails { get; set; }

        public VersionSelectorForm()
        {
            InitializeComponent();
            _ = PopulateVersionsAsync();
        }

        public VersionSelectorForm(string statusFilter)
        {
            InitializeComponent();
            if(!allowedStatuses.Contains(statusFilter))
                throw new Exception($"Status '{statusFilter}' is not allowed as filter.");
            this.statusFilter = statusFilter;
            MessageLabel.Text = GetMessageLabelText();
            _ = PopulateVersionsAsync();
        }

        private async Task PopulateVersionsAsync()
        {
            try
            {
                List<Version> versions;

                if (statusFilter == null)
                    versions = await mechSyncService.GetAllVersionsAsync();
                else
                    versions = await mechSyncService.GetVersionsWithStatusAsync(statusFilter);

                await PopulateProjectsCacheAsync();

                VersionList.SelectedItems.Clear();
                VersionList.Items.Clear();
                foreach (var version in versions)
                {
                    if (!projectCache.ContainsKey(version.ProjectId))
                        continue;

                    var project = projectCache[version.ProjectId];
                    var ownerDetails = await GetOwnerDetailsAsync(version.Owner.UserId);

                    if (!EvaluateSearchFilter(project, version, ownerDetails)) continue;
                
                    var versionItem = new ListViewItem()
                    {
                        Text = $"{project.FolderName} V{version.Major}",
                        Tag = new SelectedVersionDetails()
                        {
                            Version = version,
                            Project = project,
                            OwnerDetails  = ownerDetails
                        },
                    };
                    versionItem.SubItems.Add(ownerDetails.FullName);
                    VersionList.Items.Add(versionItem);
                }
            }
            catch (Exception ex) 
            {
                Log.Error($"Failed to retrieve versions from server: {ex}");
                MessageBox.Show($"Failed to retrieve versions from server: {ex}");
            }
        }

        private async Task<UserDetails> GetOwnerDetailsAsync(string ownerId)
        {
            if (!userDetailsIndex.ContainsKey(ownerId))
                userDetailsIndex[ownerId] = await authServiceClient.GetUserDetailsAsync(ownerId);

            return userDetailsIndex[ownerId];
        }

        private bool EvaluateSearchFilter(Project project, Version version, UserDetails ownerDetails)
        {
            if (project is null)
                throw new ArgumentNullException(nameof(project));

            if (version is null)
                throw new ArgumentNullException(nameof(version));

            if (ownerDetails is null)
                throw new ArgumentNullException(nameof(ownerDetails));

            string filterText = SearchFilter.Text.ToLower();

            return filterText == string.Empty ||
                project.FolderName.ToLower().Contains(filterText) ||
                $"v{version.Major}".Contains(filterText) ||
                ownerDetails.FullName.ToLower().Contains(filterText);
        }

        private string GetMessageLabelText()
        {
            switch (statusFilter)
            {
                case "Ongoing":
                    return "Select an ongoing version:";
                case "Latest":
                    return "Select a latest version:";
                case "History":
                    return "Select a version from history:";
                default:
                    return "Select a version:";
            }
        }

        private async void SearchFilter_TextChanged(object sender, EventArgs e)
        {
            await PopulateVersionsAsync();
        }

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshButton.Enabled = false;
            await PopulateVersionsAsync();
            RefreshButton.Enabled = true;
        }

        private void VersionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OkButton.Enabled = VersionList.SelectedItems.Count > 0;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (VersionList.SelectedItems.Count <= 0) return;
            SelectedVersionDetails = VersionList.SelectedItems[0].Tag as SelectedVersionDetails;
            DialogResult = DialogResult.OK;
        }

        private void CancelVersionSelectButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private async Task PopulateProjectsCacheAsync()
        {
            var allProjects = await mechSyncService.GetAllProjectsAsync();

            projectCache.Clear();
            foreach (var project in allProjects)
            {
                projectCache.Add(project.Id, project);
            }
        }
    }
}
