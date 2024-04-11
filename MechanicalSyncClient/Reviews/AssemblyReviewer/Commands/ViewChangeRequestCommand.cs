using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Reviews.AssemblyReviewer.Commands
{
    public class ViewChangeRequestCommand : IAssemblyReviewerCommandAsync
    {
        private readonly ChangeRequest changeRequest;

        public IAssemblyReviewer Reviewer { get; }
        
        private readonly ILogger logger;

        public ViewChangeRequestCommand(
                IAssemblyReviewer reviewer, 
                ChangeRequest changeRequest,
                ILogger logger
            )
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
            this.changeRequest = changeRequest ?? throw new ArgumentNullException(nameof(changeRequest));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug($"ViewEditChangeRequestCommand begins...");

            var ui = Reviewer.Args.UI;
            var remoteRelativeImagePath = $"{changeRequest.Id}-change.png";
            var tempDownloadedImagePath = Path.Combine(Path.GetTempPath(), remoteRelativeImagePath);

            try
            {
                var syncService = Reviewer.Args.SyncServiceClient;
                var relativeEquipmentPath = Reviewer.Args.Review.RemoteProject.RelativeEquipmentPath;

                changeRequest.Parent = Reviewer.ReviewTarget;

                CleanupTempPictureFile(tempDownloadedImagePath);

                logger.Debug($"Downloading image from server: {remoteRelativeImagePath}");
                await syncService.DownloadFileAsync(new DownloadFileRequest()
                {
                    LocalFilename = tempDownloadedImagePath,
                    RelativeEquipmentPath = relativeEquipmentPath,
                    VersionFolder = "AssyReview",
                    RelativeFilePath = remoteRelativeImagePath,
                });

                using (var downloadedImage = Image.FromFile(tempDownloadedImagePath)) 
                {
                    changeRequest.DetailsImage = downloadedImage;

                    logger.Debug($"Showing change request details dialog...");
                    var dialog = new ChangeRequestDetailsDialog(changeRequest);
                    dialog.OnDelete += ViewChangeRequestCommand_OnDelete;

                    var result = dialog.ShowDialog();
                }
                logger.Debug($"ViewChangeRequestCommand complete.");
            }
            catch(Exception ex)
            {
                var message = $"Failed to open change request for viewing: {ex.Message}";
                logger.Error(message, ex );
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CleanupTempPictureFile(tempDownloadedImagePath);
            }
        }

        private void ViewChangeRequestCommand_OnDelete(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Delete change request?", 
                "Delete", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                var syncService = Reviewer.Args.SyncServiceClient;
                var ui = Reviewer.Args.UI;
                var reviewTarget = Reviewer.ReviewTarget;

                syncService.DeleteChangeRequestAsync(changeRequest.Id);

                // remove change request from review target
                reviewTarget.ChangeRequests.Remove(changeRequest);

                // remove change request from grid in UI
                var allRows = ui.ChangeRequestsGrid.Rows.Cast<DataGridViewRow>();
                var rowToDelete = allRows.First((row) => (row.Tag as ChangeRequest).Id == changeRequest.Id);
                ui.ChangeRequestsGrid.Rows.RemoveAt(rowToDelete.Index);

                (sender as Form).Close();
            }
            catch(Exception ex) 
            {
                var message = $"Failed to delete change request: {ex.Message}";
                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CleanupTempPictureFile(string filePath)
        {
            try
            {
                if (filePath != string.Empty && File.Exists(filePath))
                {
                    logger.Debug($"Cleaning up temp picture file at {filePath}");
                    File.Delete(filePath);
                }
            }
            catch(Exception ex)
            {
                logger.Error($"Failed to cleanup temp picture file: {ex.Message}");
            }
        }
    }
}
