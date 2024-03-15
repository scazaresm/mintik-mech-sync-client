using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using Microsoft.VisualBasic.FileIO;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class ArchiveVersionProgressDialog : Form
    {
        private const string ARCHIVING_VERSION_STATUS = "archiving";
        private const string ARCHIVE_SUCCESS_STATUS = "latest";

        private readonly string versionId;
        private readonly string localDirectory;

        public bool IsArchivingComplete { get; set; }
        public bool IsArchivingSuccess { get; set; }
        public IVersionSynchronizer Synchronizer { get; }

        public ArchiveVersionProgressDialog(IVersionSynchronizer synchronizer, string versionId, string localDirectory)
        {
            InitializeComponent();
            Synchronizer = synchronizer;
            this.versionId = versionId ?? throw new ArgumentNullException(nameof(versionId));
            this.localDirectory = localDirectory ?? throw new ArgumentNullException(nameof(localDirectory));
        }

        private async void PublishVersionProgressDialog_Load(object sender, EventArgs e)
        {
            await UpdateArchivingDetailsAsync();
        }

        private async Task UpdateArchivingDetailsAsync()
        {
            Log.Debug("Archiving is happening on the server...");

            Version version;
            ArchivingIcon.Image = Properties.Resources.archive_24;
            ArchivingMessage.Text = "Archiving design data in server, please wait...";
            OkButton.Enabled = false;
            ArchivingProgressBar.Visible = true;
            do
            {
                try
                {

                    version = await Synchronizer.SyncServiceClient.GetVersionAsync(versionId);
                    IsArchivingComplete = version.Status.ToLower() != ARCHIVING_VERSION_STATUS;
                    IsArchivingSuccess = version.Status.ToLower() == ARCHIVE_SUCCESS_STATUS;
                    Log.Debug($"Polling version status: Complete = {IsArchivingComplete}");

                    await Task.Delay(2000);
                } 
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            while (!IsArchivingComplete);

            if (IsArchivingSuccess && Directory.Exists(localDirectory))
            {
                Log.Debug("Archiving succeeded, sending local folder to recycle bin.");

                ArchivingMessage.Text = "Cleaning up your local workspace...";
                await Task.Run(() => MoveFolderToRecycleBin());
            }

            ArchivingMessage.Text = IsArchivingSuccess
                ? "This version has been successfully archived! Please open a new version if further design changes are required in future."
                : "There was a problem archiving the version, please try again later " +
                  "and reach your IT department if the issue persists." + Environment.NewLine + Environment.NewLine +
                  "No worries, we got you covered and any archiving progress was rollbacked.";

            ArchivingIcon.Image = IsArchivingSuccess
                ? Properties.Resources.ok_icon_48
                : Properties.Resources.error_icon_48;

            OkButton.Enabled = true;
            ArchivingProgressBar.Visible = false;

            if (IsArchivingSuccess)
                Log.Debug("Archiving complete.");
            else
                Log.Debug("Archiving has failed, check the mech-sync-service logs in docker for more details.");
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void MoveFolderToRecycleBin()
        {
            try
            {
                FileSystem.DeleteDirectory(localDirectory, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            }
            catch(OperationCanceledException)
            {
                Log.Debug("Version was successfully archived but local copy could not be sent to recycle bin due to user cancel, shall be deleted manually.");
                MessageBox.Show(
                    "This version has been archived on server but your local copy could not be sent to recycle bin, please delete it manually.",
                    "Could not remove local copy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch(Exception ex)
            {
                Log.Error($"Failed to move folder to recycle bin: {ex}");
            }
        }
    }
}
