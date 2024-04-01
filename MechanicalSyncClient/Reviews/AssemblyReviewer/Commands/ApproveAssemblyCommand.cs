using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Reviews.AssemblyReviewer.Commands
{
    public class ApproveAssemblyCommand : IAssemblyReviewerCommandAsync
    {
        private readonly ILogger logger;

        public IAssemblyReviewer Reviewer { get; }

        public ApproveAssemblyCommand(IAssemblyReviewer reviewer, ILogger logger)
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("Starting AppoveAssemblyCommand...");
            var ui = Reviewer.Args.UI;
            try
            {
                var Review = Reviewer.Args.Review;
                var ReviewTarget = Reviewer.ReviewTarget; 
                
                var newChangeRequests = ReviewTarget.ChangeRequests.Where((cr) =>
                    cr.Status == ChangeRequestStatus.Pending.ToString()
                );

                if (newChangeRequests.Count() > 0)
                {
                    MessageBox.Show(
                        "Delete all change requests with pending status before approving this assembly.",
                        "Could not approve assembly",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }

                var confirmation = MessageBox.Show(
                  "Approve this assembly?", "Approve assembly",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );
                if (confirmation != DialogResult.Yes) return;

                ui.ApproveAssemblyButton.Enabled = false;
                ui.StatusLabel.Text = "Approving assembly...";

                logger.Debug("Retrieving latest Review from server...");
                var recentReview = await Reviewer.Args.SyncServiceClient.GetReviewAsync(Review.RemoteReview.Id)
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
                    await new OpenAssemblyReviewCommand(Reviewer, ReviewTarget, logger).RunAsync();
                    return;
                }

                // put status as approved
                Reviewer.ReviewTarget = await Reviewer.Args.SyncServiceClient.UpdateReviewTargetAsync(new UpdateReviewTargetRequest()
                {
                    ReviewId = Review.RemoteReview.Id,
                    ReviewTargetId = ReviewTarget.Id,
                    Status = "Approved"
                });
                await Reviewer.CloseReviewTargetAsync();
            }
            catch (Exception ex)
            {
                var message = $"Could not approve assembly: {ex}";
                logger.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ui.ApproveAssemblyButton.Enabled = true;
                ui.StatusLabel.Text = "Ready";
                logger.Debug("AppoveAssemblyCommand complete.");
            }
        }
    }
}
