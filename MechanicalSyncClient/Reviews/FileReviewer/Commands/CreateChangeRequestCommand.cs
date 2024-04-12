using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace MechanicalSyncApp.Reviews.FileReviewer.Commands
{
    public class CreateChangeRequestCommand : IFileReviewerCommandAsync
    {
        private readonly ILogger logger;
        public IFileReviewer Reviewer { get; }


        public CreateChangeRequestCommand(IFileReviewer reviewer, ILogger logger)
        {
            Reviewer = reviewer;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("CreateChangeRequestCommand begins...");

            var ui = Reviewer.Args.UI;
            var syncService = Reviewer.Args.SyncServiceClient;
            var review = Reviewer.Args.Review;

            var tempImagePath = "";
            logger.Debug($"Defined temporary change request image path: {tempImagePath}");

            try
            {
                var newChangeRequestDialog = new ChangeRequestDetailsDialog(ui.ChangeRequestInput.Text.Trim());

                logger.Debug("Displaying change request details dialog...");
                var result = newChangeRequestDialog.ShowDialog();

                if (result != DialogResult.OK)
                {
                    logger.Debug("User has cancelled the creation of the new change request.");
                    return;
                }

                logger.Debug("Creating the new change request...");
                var changeRequest = await syncService.CreateChangeRequestAsync(
                    Reviewer.ReviewTarget.Id,
                    newChangeRequestDialog.ChangeRequest.ChangeDescription
                );
                changeRequest.Parent = Reviewer.ReviewTarget;

                tempImagePath = Path.Combine(Path.GetTempPath(), $"{changeRequest.Id}-change.png");

                logger.Debug("Saving temporary change request image...");
                newChangeRequestDialog.ChangeRequest.DetailsImage.Save(tempImagePath, ImageFormat.Png);

                logger.Debug("Uploading change request image...");
                await syncService.UploadFileAsync(new UploadFileRequest()
                {
                    VersionId = review.RemoteVersion.Id,
                    LocalFilePath = tempImagePath,
                    VersionFolder = "FileReview",
                    RelativeEquipmentPath = review.RemoteProject.RelativeEquipmentPath,
                    RelativeFilePath = Path.GetFileName(tempImagePath)
                });

                logger.Debug("Adding the new change request to the UI grid and memory state...");
                ui.AddChangeRequestToGrid(changeRequest);
                Reviewer.ReviewTarget.ChangeRequests.Add(changeRequest);

                // put review target status to 'Reviewing' if not already set.
                if (Reviewer.ReviewTarget.Status != "Reviewing")
                {
                    var updatedTarget = await Reviewer.Args.SyncServiceClient.UpdateReviewTargetAsync(new UpdateReviewTargetRequest()
                    {
                        ReviewId = Reviewer.Args.Review.RemoteReview.Id,
                        ReviewTargetId = Reviewer.ReviewTarget.Id,
                        Status = "Reviewing"
                    });
                    Reviewer.ReviewTarget.Status = updatedTarget.Status;
                    ui.SetReviewTargetStatusText(Reviewer.ReviewTarget.Status);
                }
                logger.Debug("CreateChangeRequestCommand complete.");
            }
            catch (Exception ex)
            {
                var message = $"Failed to create change request: {ex.Message}";
                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                logger.Debug("Cleaning up change request input and releasing focus...");
                ui.ChangeRequestInput.Text = string.Empty;
                ui.ChangeRequestInput.Parent.Focus();

                CleanupTempImagePath(tempImagePath);
            }
        }

        private void CleanupTempImagePath(string tempImagePath)
        {
            if (tempImagePath != string.Empty && File.Exists(tempImagePath))
            {
                logger.Debug("Cleaning up existing temporary image...");
                File.Delete(tempImagePath);
            }
        }
    }

}
