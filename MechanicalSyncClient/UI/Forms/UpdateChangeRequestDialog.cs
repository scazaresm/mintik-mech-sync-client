using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using Serilog;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI.Forms
{
    public partial class UpdateChangeRequestDialog : Form
    {
        public ChangeRequest ChangeRequest { get; private set; }

        private readonly IVersionSynchronizer synchronizer;
        private readonly ILogger logger;

        private string tempImageFile;

        public bool ReadOnly
        {
            get
            {
                return DesignerComments.ReadOnly;
            }
            set
            {
                ChangeStatus.Enabled = !value;
                DesignerComments.ReadOnly = value;
            }
        }

        public UpdateChangeRequestDialog(
                ChangeRequest changeRequest,
                IVersionSynchronizer synchronizer,
                ILogger logger
            )
        {
            InitializeComponent();
            ChangeRequest = changeRequest ?? throw new ArgumentNullException(nameof(changeRequest));
            this.synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void CancelActionButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private async void UpdateChangeRequestDialog_Load(object sender, EventArgs e)
        {
            await DownloadChangeRequestImage();

            ChangeDescription.Text = ChangeRequest.ChangeDescription;
            ChangeStatus.Text = ChangeRequest.Status;
            DesignerComments.Text = ChangeRequest.DesignerComments;
        }

        private async Task DownloadChangeRequestImage()
        {
            logger.Debug("Download change request image...");

            tempImageFile = Path.Combine(Path.GetTempPath(), ChangeRequest.Id);

            CleanupImageFile(tempImageFile);

            await synchronizer.SyncServiceClient.DownloadFileAsync(new DownloadFileRequest()
            {
                VersionFolder = "AssyReview",
                RelativeEquipmentPath = synchronizer.Version.RemoteProject.RelativeEquipmentPath,
                RelativeFilePath = $"{ChangeRequest.Id}-change.png",
                LocalFilename = tempImageFile
            });
            DetailsPictureBox.Image = Image.FromFile(tempImageFile);
        }

        private void CleanupImageFile(string imageFilePath)
        {
            if ( File.Exists(imageFilePath) )
                File.Delete(imageFilePath);
        }

        private void UpdateChangeRequestDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            DetailsPictureBox.Image.Dispose();
            DetailsPictureBox.Image = null;
            CleanupImageFile(tempImageFile);
        }

        private void ChangeStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeRequest.Status = ChangeStatus.Text;
            OkButton.Enabled = ValidateData();
        }

        private void DesignerComments_TextChanged(object sender, EventArgs e)
        {
            ChangeRequest.DesignerComments = DesignerComments.Text;
            OkButton.Enabled = ValidateData();
        }

        private bool ValidateData()
        {
            return ChangeStatus.Text != "Discarded" || DesignerComments.Text.Length > 0;
        }
    }
}
