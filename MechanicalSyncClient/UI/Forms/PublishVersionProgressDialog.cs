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

namespace MechanicalSyncApp.UI.Forms
{
    public partial class PublishVersionProgressDialog : Form
    {
        public bool IsSuccess { get; set; }

        private readonly IVersionSynchronizer synchronizer;

        public PublishVersionProgressDialog(IVersionSynchronizer synchronizer)
        {
            InitializeComponent();
            this.synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }
        private async void PublishVersionProgressDialog_Load(object sender, EventArgs e)
        {
            await UpdateDetailsAsync();
        }

        private async Task UpdateDetailsAsync()
        {
            string publishJobId = synchronizer.Version.RemoteVersion.PublishJobId;
            PublishJob publishJob;
            List<string> completeStatuses = new List<string> { "published", "error" };

            do
            {
                publishJob = await MechSyncServiceClient.Instance.GetPublishJobAsync(publishJobId);
                InstructionsLabel.Text = GetInstructions(publishJob.Status);
                StatusLabel.Text = publishJob.Status;
                StatusPicture.Image = GetStatusImage(publishJob.Status);

                await Task.Delay(500);
            }
            while (publishJob != null && !completeStatuses.Contains(publishJob.Status.ToLower()));

            IsSuccess = publishJob.Status.ToLower() == "published";
        }

        private string GetInstructions(string status)
        {
            switch (status.ToLower())
            {
                case "queued": 
                    return "Hold on while we publish your changes...";

                case "publishing":
                    return "We still working on publishing your changes...";

                case "published":
                    return "Thanks for your contribution!";

                case "error":
                default:
                    return "An error has occurred while publishing, please try again.";
            }
        }

        private Bitmap GetStatusImage(string status)
        {
            switch(status.ToLower())
            {
                case "queued": return Properties.Resources.scheduled_48;
                case "publishing": return Properties.Resources.paper_plane_48;
                case "published": return Properties.Resources.ok_icon_48;

                case "error":
                default: 
                    return Properties.Resources.error_icon_48;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (IsSuccess) DialogResult = DialogResult.OK;
        }
    }
}
