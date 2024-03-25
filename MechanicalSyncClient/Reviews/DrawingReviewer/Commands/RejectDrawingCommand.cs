﻿using MechanicalSyncApp.Core;
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
    public class RejectDrawingCommand : IDrawingReviewerCommandAsync
    {
        public IDrawingReviewer Reviewer { get; }

        public RejectDrawingCommand(IDrawingReviewer reviewer)
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
        }

        public async Task RunAsync()
        {
            Log.Debug("Starting RejectDrawingCommand...");
            var UI = Reviewer.UI;
            try
            {
                var Review = Reviewer.Review;
                var ReviewTarget = Reviewer.ReviewTarget;

                var confirmation = MessageBox.Show("Reject this drawing?", "Reject drawing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation != DialogResult.Yes) return;

                Log.Debug("Retrieving latest Review from server...");
                var latestReview = await Reviewer.SyncServiceClient.GetReviewAsync(Review.RemoteReview.Id) 
                    ?? throw new Exception("Latest review could not be found in server.");

                Log.Debug("Retrieving latest ReviewTarget from server...");
                var latestReviewTarget = latestReview.Targets.Find((target) => target.Id == ReviewTarget.Id) 
                    ?? throw new Exception("Latest review target could not be found in server.");

                if (latestReviewTarget.UpdatedAt != ReviewTarget.UpdatedAt)
                {
                    Log.Debug("Changes were detected in the remote ReviewTarget, needs to be reopened and reviewed again!");
                    MessageBox.Show(
                        "This file has been modified while you were reviewing it, it will be automatically reopened with latest changes and please review it again.",
                        "File has changed",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                    );
                    ReviewTarget.UpdatedAt = latestReviewTarget.UpdatedAt;
                    await new OpenDrawingReviewCommand(Reviewer, ReviewTarget).RunAsync();
                    return;
                }

                UI.RejectDrawingButton.Enabled = false;
                UI.MarkupStatus.Text = "Rejecting drawing...";

                // upload the current eDrawings markup file
                var uploadedMarkup = await new DrawingMarkupFileUploader(Reviewer).UploadAsync();

                if (uploadedMarkup)
                {
                    // put status as rejected
                    Reviewer.ReviewTarget = await Reviewer.SyncServiceClient.UpdateReviewTargetAsync(new UpdateReviewTargetRequest()
                    {
                        ReviewId = Reviewer.Review.RemoteReview.Id,
                        ReviewTargetId = Reviewer.ReviewTarget.Id,
                        Status = "Rejected"
                    });
                    await Reviewer.CloseReviewTargetAsync();
                }
            }
            catch(Exception ex)
            {
                var message = $"Could not reject drawing: {ex}";
                Log.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UI.RejectDrawingButton.Enabled = true;
                UI.MarkupStatus.Text = "Ready";
                Log.Debug("RejectDrawingCommand complete.");
            }
        }
    }
}