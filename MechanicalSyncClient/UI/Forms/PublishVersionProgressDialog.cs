﻿using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class PublishVersionProgressDialog : Form
    {
        private readonly string versionId;
        private readonly string localDirectory;

        public bool IsPublishingComplete { get; set; }
        public bool IsPublishingSuccess { get; set; }
        public IVersionSynchronizer Synchronizer { get; }

        public PublishVersionProgressDialog(IVersionSynchronizer synchronizer, string versionId, string localDirectory)
        {
            InitializeComponent();
            Synchronizer = synchronizer;
            this.versionId = versionId ?? throw new ArgumentNullException(nameof(versionId));
            this.localDirectory = localDirectory ?? throw new ArgumentNullException(nameof(localDirectory));
        }

        private async void PublishVersionProgressDialog_Load(object sender, EventArgs e)
        {
            await UpdatePublishingDetailsAsync();
        }

        private async Task UpdatePublishingDetailsAsync()
        {
            Version version;
            PublishingIcon.Image = Properties.Resources.paper_plane_48;
            PublishingMessage.Text = "Publishing your changes, please wait...";
            OkButton.Enabled = false;
            PublishingProgressBar.Visible = true;
            do
            {
                try
                {
                    version = await Synchronizer.SyncServiceClient.GetVersionAsync(versionId);
                    IsPublishingComplete = version.Status.ToLower() != "publishing";
                    IsPublishingSuccess = version.Status.ToLower() == "latest";
                    await Task.Delay(2000);
                } 
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            while (!IsPublishingComplete);

            if (IsPublishingSuccess && Directory.Exists(localDirectory))
            {
                PublishingMessage.Text = "Almost there, cleaning up your local workspace...";
                    await Task.Run(() => FileSystem.DeleteDirectory(
                    localDirectory,
                    UIOption.OnlyErrorDialogs,
                    RecycleOption.SendToRecycleBin
                ));
            }

            PublishingMessage.Text = IsPublishingSuccess
                ? "Your changes were successfully published, thanks for your hard work!"
                : "There was a problem publishing your changes, please try again later " +
                  "and reach your IT department if the issue persists." + Environment.NewLine + Environment.NewLine +
                  "No worries, we got you covered and any publishing progress was rollbacked.";

            PublishingIcon.Image = IsPublishingSuccess
                ? Properties.Resources.ok_icon_48
                : Properties.Resources.error_icon_48;

            OkButton.Enabled = true;
            PublishingProgressBar.Visible = false;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
