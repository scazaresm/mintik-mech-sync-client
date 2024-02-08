using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Reviews.DrawingReviewer.Commands
{
    public class SaveDrawingReviewProgressCommand : IDrawingReviewerCommandAsync
    {
        public IDrawingReviewer Reviewer { get; }

        public SaveDrawingReviewProgressCommand(IDrawingReviewer reviewer)
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
        }

        public async Task RunAsync()
        {
            var UI = Reviewer.UI;

            UI.SaveProgressButton.Enabled = false;
            UI.MarkupStatus.Text = "Saving progress...";

            // upload the current eDrawings markup file
            var uploadedMarkup = await new DrawingMarkupFileUploader(Reviewer).UploadAsync();

            if (uploadedMarkup && Reviewer.ReviewTarget.Status != "Reviewing")
            {
                // put review target status to 'Reviewing' if not already set. This condition shall be met
                // in order to load the corresponding markup file when this review target is open again
                Reviewer.ReviewTarget = await Reviewer.SyncServiceClient.UpdateReviewTargetAsync(new UpdateReviewTargetRequest()
                {
                    ReviewId = Reviewer.Review.RemoteReview.Id,
                    ReviewTargetId = Reviewer.ReviewTarget.Id,
                    Status = "Reviewing"
                });
                UI.SetReviewTargetStatusText(Reviewer.ReviewTarget.Status);
            }
            UI.SaveProgressButton.Enabled = true;
            UI.MarkupStatus.Text = "Ready";
        }
    }
}
