using MechanicalSyncApp.Core;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class PublishVersionProgressDialog : Form
    {
        public bool IsPublishingComplete { get; set; }
        public bool IsPublishingSuccess { get; set; }

        private readonly string publishingJobId;

        public PublishVersionProgressDialog(string publishingJobId)
        {
            InitializeComponent();
            this.publishingJobId = publishingJobId;
        }

        private async void PublishVersionProgressDialog_Load(object sender, EventArgs e)
        {
            await UpdatePublishingDetailsAsync();
        }

        private async Task UpdatePublishingDetailsAsync()
        {
            OkButton.Enabled = false;
            Job publishingJob;
            List<string> completeStatuses = new List<string> { "processed", "error" };

            do
            {
                try
                {
                    publishingJob = await MechSyncServiceClient.Instance.GetJobAsync(publishingJobId);

                    PublishingInstructionsLabel.Text = GetPublishingInstructions(publishingJob.Status);
                    PublishingStatusLabel.Text = GetPublishingStatus(publishingJob.Status);
                    PublishingStatusPictureBox.Image = GetPublishingStatusImage(publishingJob.Status);

                    await Task.Delay(500);

                    IsPublishingComplete = publishingJob != null && completeStatuses.Contains(publishingJob.Status.ToLower());
                    IsPublishingSuccess = publishingJob != null && publishingJob.Status.ToLower() == "processed";
                } 
                catch (Exception)
                {
                    PublishingInstructionsLabel.Text = "We couldn't get updates from server, hold on...";
                    PublishingStatusLabel.Text = "Retrying...";
                    PublishingStatusPictureBox.Image = Properties.Resources.error_icon_48;
                }
            }
            while (!IsPublishingComplete);

            OkButton.Enabled = true;
        }

        private string GetPublishingInstructions(string jobStatus)
        {
            switch (jobStatus.ToLower())
            {
                case "queued": 
                    return "Hold on while we publish your changes...";

                case "processing":
                    return "Almost there, we still working on publishing your changes...";

                case "processed":
                    return "Thanks for your work contribution!";

                case "error":
                default:
                    return "An error has occurred while publishing, please try again.";
            }
        }

        private string GetPublishingStatus(string jobStatus)
        {
            switch (jobStatus.ToLower())
            {
                case "queued":
                    return "Getting ready...";

                case "processing":
                    return "Publishing...";

                case "processed":
                    return "Published!";

                case "error":
                default:
                    return "Error";
            }
        }

        private Bitmap GetPublishingStatusImage(string status)
        {
            switch(status.ToLower())
            {
                case "queued": return Properties.Resources.scheduled_48; 
                case "processing": return Properties.Resources.paper_plane_48;
                case "processed": return Properties.Resources.ok_icon_48;

                case "error":
                default: 
                    return Properties.Resources.error_icon_48;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
