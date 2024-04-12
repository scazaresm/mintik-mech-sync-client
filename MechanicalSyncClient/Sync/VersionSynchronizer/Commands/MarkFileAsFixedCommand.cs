using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Reviews.DrawingReviewer;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{

    public class MarkFileAsFixedCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;
        private Review review;
        private ReviewTarget reviewTarget;

        public IVersionSynchronizer Synchronizer { get; }

        public MarkFileAsFixedCommand(IVersionSynchronizer synchronizer, ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            review = Synchronizer?.CurrentFileReview ?? throw new NullReferenceException(nameof(Synchronizer.CurrentFileReview));
            reviewTarget = Synchronizer?.CurrentFileReviewTarget ?? throw new NullReferenceException(nameof(Synchronizer.CurrentFileReviewTarget));
        }

        public async Task RunAsync()
        {
            var ui = Synchronizer.UI;
            var grid = ui.FileChangeRequestGrid;

            try
            {
                var pendingChanges = grid.Rows.Cast<DataGridViewRow>().Where(
                    (row) => (row.Tag as ChangeRequest).Status == "Pending"
                ).ToList();

                if (pendingChanges.Any())
                {
                    MessageBox.Show(
                        "There are pending change requests on this review, please mark all them either as 'Done' or 'Discarded'.",
                        "Pending change requests",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                    return;
                }

                var response = MessageBox.Show(
                    "Are you sure to mark this file as fixed?",
                    "Mark as fixed", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );

                if (response != DialogResult.Yes) return;

                await Synchronizer.SyncServiceClient.UpdateReviewTargetAsync(new UpdateReviewTargetRequest()
                {
                    ReviewId = review.Id,
                    ReviewTargetId = reviewTarget.Id,
                    Status = "Fixed"
                });

                Synchronizer.UI.FileReviewsSplit.Panel2Collapsed = true;
                await Synchronizer.UI.ReviewsExplorer.Refresh();
            }
            catch(Exception ex)
            {
                var message = $"Could not mark file as fixed: {ex.Message}";
                logger.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
