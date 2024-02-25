using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
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
    public partial class CreateReviewForm : Form
    {
        private SelectedVersionDetails selectedVersionDetails;

        public CreateReviewForm()
        {
            InitializeComponent();
        }

        private void SelectVersionButton_Click(object sender, EventArgs e)
        {
            var versionSelector = new VersionSelectorForm("Ongoing");
            var result = versionSelector.ShowDialog();

            if (result == DialogResult.OK)
            {
                selectedVersionDetails = versionSelector.SelectedVersionDetails;
                OngoingVersionText.Text = selectedVersionDetails.ToString();
            }
            else
                OngoingVersionText.Text = string.Empty;
        }

        private async void CreateReviewButton_Click(object sender, EventArgs e)
        {
            CreateReviewButton.Enabled = false;
            await CreateReviewAsync();
            CreateReviewButton.Enabled = true;
        }

        private async Task CreateReviewAsync()
        {
            try
            {
                await MechSyncServiceClient.Instance.CreateReviewAsync(new CreateReviewRequest()
                {
                    ReviewerId = AuthenticationServiceClient.Instance.LoggedUserDetails.Id,
                    TargetType = ReviewType.SelectedIndex == 1 ? "AssemblyFile" : "DrawingFile",
                    VersionId = selectedVersionDetails.Version.Id
                });
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                Log.Error($"Failed to create review: {ex}");
                MessageBox.Show($"Failed to create review: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelCreateReviewButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OngoingVersionText_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void ReviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void ValidateData()
        {
            var isValidData = OngoingVersionText.Text != string.Empty && ReviewType.SelectedIndex >= 0;
            CreateReviewButton.Enabled = isValidData;
        }
    }
}
