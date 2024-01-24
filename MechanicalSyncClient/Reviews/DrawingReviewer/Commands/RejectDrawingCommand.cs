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
    public class RejectDrawingCommand : IDrawingReviewerCommandAsync
    {
        public IDrawingReviewer Reviewer { get; }

        public RejectDrawingCommand(IDrawingReviewer reviewer)
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
        }

        public async Task RunAsync()
        {
            var UI = Reviewer.UI;

            var confirmation = MessageBox.Show(
                "Reject this drawing?", "Reject drawing",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question
            );
            if (confirmation != DialogResult.Yes) return;

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
            UI.RejectDrawingButton.Enabled = true;
            UI.MarkupStatus.Text = "Ready";
        }
    }
}
