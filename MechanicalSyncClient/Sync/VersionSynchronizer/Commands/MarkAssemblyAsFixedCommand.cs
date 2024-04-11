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

    public class MarkAssemblyAsFixedCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;
        private Review assemblyReview;
        private ReviewTarget assemblyReviewTarget;

        public IVersionSynchronizer Synchronizer { get; }

        public MarkAssemblyAsFixedCommand(IVersionSynchronizer synchronizer, ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            assemblyReview = Synchronizer?.CurrentAssemblyReview ?? throw new NullReferenceException(nameof(Synchronizer.CurrentAssemblyReview));
            assemblyReviewTarget = Synchronizer?.CurrentAssemblyReviewTarget ?? throw new NullReferenceException(nameof(Synchronizer.CurrentAssemblyReviewTarget));
        }

        public async Task RunAsync()
        {
            var ui = Synchronizer.UI;
            var grid = ui.AssemblyChangeRequestGrid;

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
                    "Are you sure to mark this assembly as fixed?",
                    "Mark as fixed", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );

                if (response != DialogResult.Yes) return;

                await Synchronizer.SyncServiceClient.UpdateReviewTargetAsync(new UpdateReviewTargetRequest()
                {
                    ReviewId = assemblyReview.Id,
                    ReviewTargetId = assemblyReviewTarget.Id,
                    Status = "Fixed"
                });

                Synchronizer.UI.AssemblyReviewsSplit.Panel2Collapsed = true;
                await Synchronizer.UI.AssemblyReviewsExplorer.Refresh();
            }
            catch(Exception ex)
            {
                var message = $"Could not mark assembly as fixed: {ex.Message}";
                logger.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
