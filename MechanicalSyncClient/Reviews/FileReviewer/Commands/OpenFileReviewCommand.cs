using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Reviews.FileReviewer.Commands
{
    public class OpenFileReviewCommand : IFileReviewerCommandAsync
    {
        public IFileReviewer Reviewer { get; }

        private readonly ReviewTarget reviewTarget;
        private readonly ILogger logger;

        public OpenFileReviewCommand(
                IFileReviewer reviewer,
                ReviewTarget reviewTarget,
                ILogger logger
            )
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
            this.reviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("OpenFileReviewCommand begins...");

            var ui = Reviewer.Args.UI;
            var starter = Reviewer.Args.SolidWorksStarter;
            var syncService = Reviewer.Args.SyncServiceClient;
            var isAlreadyReviewed = reviewTarget.Status != ReviewTargetStatus.Pending.ToString() &&
              reviewTarget.Status != ReviewTargetStatus.Reviewing.ToString();
            var isFixed = reviewTarget.Status == ReviewTargetStatus.Fixed.ToString();

            try
            {
                ui.ChangeRequestInput.Text = "Type here to create a new change request...";
                ui.ChangeRequestInput.Enabled = false;
                ui.ReviewToolStrip.Enabled = false;
                ui.ChangeRequestsGrid.Enabled = false;
                ui.ChangeRequestSplit.Panel2Collapsed = isAlreadyReviewed && !isFixed;
                ui.RejectFileButton.Enabled = !isAlreadyReviewed || isFixed;
                ui.ApproveFileButton.Enabled = !isAlreadyReviewed || isFixed;
                ui.SetReviewTargetStatusText(reviewTarget.Status);
                ui.StatusLabel.Text = "Loading file...";

                ui.PopulateChangeRequestGrid(reviewTarget);
                ui.ShowReviewPanel();

                Reviewer.ReviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
                Reviewer.Metadata = await syncService.GetFileMetadataAsync(reviewTarget.TargetId);

                var filePath = Path.Combine(
                    Reviewer.Args.TempWorkingCopyDirectory,
                    Reviewer.Metadata.RelativeFilePath
                );

                ui.SetHeaderText(
                   $"Reviewing {Path.GetFileNameWithoutExtension(filePath)} from {Reviewer.Args.Review}"
                );
                await new SolidWorksModelLoader(starter, logger).LoadModelAsync(filePath);

                ui.ReviewToolStrip.Enabled = true;
                ui.StatusLabel.Text = "Ready.";
                ui.ChangeRequestInput.Enabled = true;
                ui.ChangeRequestsGrid.Enabled = true;

                logger.Debug("OpenFileReviewCommand complete.");
            }
            catch (Exception ex)
            {
                var message = $"Failed to open file: {ex.Message}";
                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ui.HideReviewPanel();
            }
        }
    }
}
