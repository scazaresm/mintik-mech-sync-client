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
        public IDrawingReviewer Reviewer { get; }

        public ApproveDrawingCommand(IDrawingReviewer reviewer)
        {
            Reviewer = reviewer;
        }

        public async Task RunAsync()
        {
            Log.Debug("Starting ApproveDrawingCommand...");
            var UI = Reviewer.UI;
            try
            {
                var Review = Reviewer.Review;
                var ReviewTarget = Reviewer.ReviewTarget;

                var confirmation = MessageBox.Show(
                  "Approve this drawing?", "Approve drawing",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );
                if (confirmation != DialogResult.Yes) return;

                UI.ApproveDrawingButton.Enabled = false;
                UI.MarkupStatus.Text = "Approving drawing...";

                Log.Debug("Retrieving latest Review from server...");
                var recentReview = await Reviewer.SyncServiceClient.GetReviewAsync(Review.RemoteReview.Id) 
                    ?? throw new Exception("Review could not be found in server.");

                Log.Debug("Retrieving latest ReviewTarget from server...");
                var recentReviewTarget = recentReview.Targets.Find((target) => target.Id == ReviewTarget.Id) 
                    ?? throw new Exception("Review target could not be found in server.");

                Log.Debug("Checking if ReviewTarget has changed in server while user was reviewing the file...");
                if (recentReviewTarget.UpdatedAt != ReviewTarget.UpdatedAt)
                {
                    Log.Debug("Changes were detected in the remote ReviewTarget, needs to be reopened and reviewed again!");
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
                Log.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UI.ApproveDrawingButton.Enabled = true;
                UI.MarkupStatus.Text = "Ready";
                Log.Debug("ApproveDrawingCommand complete.");
            }
        }
    }
}
