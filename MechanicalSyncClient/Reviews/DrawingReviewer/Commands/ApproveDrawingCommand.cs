using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            var UI = Reviewer.UI;
            var Review = Reviewer.Review;
            var ReviewTarget = Reviewer.ReviewTarget;

            var confirmation = MessageBox.Show(
              "Approve this drawing?", "Approve drawing",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question
            );
            if (confirmation != DialogResult.Yes) return;

            UI.ApproveDrawingButton.Enabled = false;
            UI.MarkupStatus.Text = "Approving drawing...";

            // put status as approved
            Reviewer.ReviewTarget = await Reviewer.SyncServiceClient.UpdateReviewTargetAsync(new UpdateReviewTargetRequest()
            {
                ReviewId = Review.RemoteReview.Id,
                ReviewTargetId = ReviewTarget.Id,
                Status = "Approved"
            });

            UI.ApproveDrawingButton.Enabled = true;
            UI.MarkupStatus.Text = "Ready";

            await Reviewer.CloseReviewTargetAsync();
        }
    }
}
