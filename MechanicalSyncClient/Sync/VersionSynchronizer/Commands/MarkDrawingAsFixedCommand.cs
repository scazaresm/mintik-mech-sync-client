using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class MarkDrawingAsFixedCommand : IVersionSynchronizerCommandAsync
    {
        private readonly Review drawingReview;
        private readonly ReviewTarget drawingReviewTarget;

        public IVersionSynchronizer Synchronizer { get; set; }

        public MarkDrawingAsFixedCommand(IVersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            drawingReview = Synchronizer?.CurrentDrawingReview ?? throw new NullReferenceException(nameof(Synchronizer.CurrentDrawingReview));
            drawingReviewTarget = Synchronizer?.CurrentDrawingReviewTarget ?? throw new NullReferenceException(nameof(Synchronizer.CurrentDrawingReviewTarget));
        }

        public async Task RunAsync()
        {
            var response = MessageBox.Show(
                "Are you sure to mark this drawing as fixed?", 
                "Mark as fixed", MessageBoxButtons.YesNo, MessageBoxIcon.Question
            );

            if (response != DialogResult.Yes) return;

            await Synchronizer.SyncServiceClient.UpdateReviewTargetAsync(new UpdateReviewTargetRequest()
            {
                ReviewId = drawingReview.Id,
                ReviewTargetId = drawingReviewTarget.Id,
                Status = "Fixed"
            });

            Synchronizer.UI.DrawingReviewer.Dispose(); 
            Synchronizer.UI.SetDefaultDrawingReviewControls();
            Synchronizer.UI.DrawingReviewsSplit.Panel2Collapsed = true;
            await Synchronizer.UI.DrawingReviewsExplorer.Refresh();
        }
    }
}
