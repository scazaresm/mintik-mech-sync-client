using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
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
    public partial class ProjectSelectorForm : Form
    {
        public IMechSyncServiceClient ServiceClient { get; set; } = MechSyncServiceClient.Instance;

        public Project SelectedProject { get; set; }

        public bool PublishedOnly { get; set; } = false;

        public ProjectSelectorForm()
        {
            InitializeComponent();
        }

        public ProjectSelectorForm(string title, string message)
        {
            InitializeComponent();
            Text = title;
            MessageLabel.Text = message;
        }

        private void ProjectSelectorForm_Load(object sender, EventArgs e)
        {
            PopulateProjects();
        }

        private void SearchFilter_TextChanged(object sender, EventArgs e)
        {
            PopulateProjects();
        }

        private async void PopulateProjects()
        {
            var projects = PublishedOnly
                ? await ServiceClient.GetPublishedProjectsAsync()
                : await ServiceClient.GetAllProjectsAsync();

            var searchTarget = SearchFilter.Text.ToLower();

            var filter = searchTarget == null || searchTarget.Length == 0
                ? projects
                : projects.Where(p => p.FolderName.ToLower().Contains(searchTarget)).ToList();

            ProjectList.SelectedItems.Clear();
            ProjectList.Items.Clear();
            foreach (var project in filter)
            {
                var item = new ListViewItem(project.FolderName);
                item.SubItems.Add(project.CreatedAt.ToShortDateString());
                item.Tag = project;

                ProjectList.Items.Add(item);
            }
        }

        private void ProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OkButton.Enabled = ProjectList.SelectedItems.Count > 0;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (ProjectList.SelectedItems.Count < 1) return;
            SelectedProject = ProjectList.SelectedItems[0].Tag as Project;
            DialogResult = DialogResult.OK;
        }

        private void CancelProjectSelectButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
