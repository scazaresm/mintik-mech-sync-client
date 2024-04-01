using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;
using System.Linq;
using MechanicalSyncApp.Core.Services.MechSync.Models;

namespace MechanicalSyncApp.Reviews.AssemblyReviewer.Commands
{
    public class RejectAssemblyCommand : IAssemblyReviewerCommandAsync
    {
        private readonly ILogger logger;

        public IAssemblyReviewer Reviewer { get; }

        public RejectAssemblyCommand(IAssemblyReviewer reviewer, ILogger logger)
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("Starting RejectAssemblyCommand...");
            var ui = Reviewer.Args.UI;
            try
            {
                var Review = Reviewer.Args.Review;
                var ReviewTarget = Reviewer.ReviewTarget;

                var newChangeRequests = ReviewTarget.ChangeRequests.Where((cr) => 
                    cr.Status == ChangeRequestStatus.Pending.ToString()
                );

                if (newChangeRequests.Count() == 0)
                {
                    MessageBox.Show(
                        "At least one new change request is required in order to reject this assembly.", 
                        "Could not reject assembly", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Exclamation);
                    return;
                }

                var confirmation = MessageBox.Show("Reject this assembly?", "Reject assembly", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation != DialogResult.Yes) return;

                ui.RejectAssemblyButton.Enabled = false;
                ui.StatusLabel.Text = "Rejecting assembly...";

                logger.Debug("Retrieving latest Review from server...");
                var latestReview = await Reviewer.Args.SyncServiceClient.GetReviewAsync(Review.RemoteReview.Id)
                    ?? throw new Exception("Latest review could not be found in server.");

                logger.Debug("Retrieving latest ReviewTarget from server...");
                var latestReviewTarget = latestReview.Targets.Find((target) => target.Id == ReviewTarget.Id)
                    ?? throw new Exception("Latest review target could not be found in server.");

                if (latestReviewTarget.UpdatedAt != ReviewTarget.UpdatedAt)
                {
                    logger.Debug("Changes were detected in the remote ReviewTarget, needs to be reopened and reviewed again!");
                    MessageBox.Show(
                        "This file has been modified while you were reviewing it, it will be automatically reopened with latest changes and please review it again.",
                        "File has changed",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                    );
                    ReviewTarget.UpdatedAt = latestReviewTarget.UpdatedAt;
                    await new OpenAssemblyReviewCommand(Reviewer, ReviewTarget, logger).RunAsync();
                    return;
                }

                // put status as rejected
                Reviewer.ReviewTarget = await Reviewer.Args.SyncServiceClient.UpdateReviewTargetAsync(new UpdateReviewTargetRequest()
                {
                    ReviewId = Reviewer.Args.Review.RemoteReview.Id,
                    ReviewTargetId = Reviewer.ReviewTarget.Id,
                    Status = "Rejected"
                });
                await Reviewer.CloseReviewTargetAsync();
            }
            catch (Exception ex)
            {
                var message = $"Could not reject the assembly: {ex}";
                logger.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ui.RejectAssemblyButton.Enabled = true;
                ui.StatusLabel.Text = "Ready";
                logger.Debug("RejectAssemblyCommand complete.");
            }
        }
    }
}
