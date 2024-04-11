using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;

namespace MechanicalSyncApp.Reviews.DrawingReviewer.Commands
{
    public class ApproveDrawingCommand : IDrawingReviewerCommandAsync
    {
        private readonly ILogger logger;

        public IDrawingReviewer Reviewer { get; }

        public ApproveDrawingCommand(IDrawingReviewer reviewer, ILogger logger)
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("Starting ApproveDrawingCommand...");
            var ui = Reviewer.UI;
            try
            {
                var Review = Reviewer.Review;
                var ReviewTarget = Reviewer.ReviewTarget;

                var confirmation = MessageBox.Show(
                  "Approve this drawing?", "Approve drawing",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );
                if (confirmation != DialogResult.Yes) return;

                ui.ApproveDrawingButton.Enabled = false;
                ui.MarkupStatus.Text = "Approving drawing...";

                logger.Debug("Retrieving latest Review from server...");
                var recentReview = await Reviewer.SyncServiceClient.GetReviewAsync(Review.RemoteReview.Id) 
                    ?? throw new Exception("Review could not be found in server.");

                logger.Debug("Retrieving latest ReviewTarget from server...");
                var recentReviewTarget = recentReview.Targets.Find((target) => target.Id == ReviewTarget.Id) 
                    ?? throw new Exception("Review target could not be found in server.");

                logger.Debug("Checking if ReviewTarget has changed in server while user was reviewing the file...");
                if (recentReviewTarget.UpdatedAt != ReviewTarget.UpdatedAt)
                {
                    logger.Debug("Changes were detected in the remote ReviewTarget, needs to be reopened and reviewed again!");
                    MessageBox.Show(
                        "This file has been modified while you were reviewing it, it will be automatically reopened with latest changes and please review it again.",
                        "File has changed",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                    );
                    ReviewTarget.UpdatedAt = recentReviewTarget.UpdatedAt;
                    await new OpenDrawingReviewCommand(Reviewer, ReviewTarget).RunAsync();
                    return;
                }

                // put status as approved
                Reviewer.ReviewTarget = await Reviewer.SyncServiceClient.UpdateReviewTargetAsync(new UpdateReviewTargetRequest()
                {
                    ReviewId = Review.RemoteReview.Id,
                    ReviewTargetId = ReviewTarget.Id,
                    Status = "Approved"
                });
                await Reviewer.CloseReviewTargetAsync();
            }
            catch(Exception ex)
            {
                var message = $"Could not approve drawing: {ex}";
                logger.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ui.ApproveDrawingButton.Enabled = true;
                ui.MarkupStatus.Text = "Ready";
                logger.Debug("ApproveDrawingCommand complete.");
            }
        }
    }
}
