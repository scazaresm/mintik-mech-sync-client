using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.UI;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class OpenAssyReviewForViewingCommand : IVersionSynchronizerCommandAsync
    {
        private readonly Review review;
        private readonly ReviewTarget reviewTarget;
        private readonly FileMetadata assemblyMetadata;
        private static Dictionary<string, UserDetails> userDetailsIndex = new Dictionary<string, UserDetails>();
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer { get; }

        public OpenAssyReviewForViewingCommand(
            IVersionSynchronizer synchronizer,
            OpenReviewTargetForViewingEventArgs e,
            ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));    
            review = e.Review;
            reviewTarget = e.ReviewTarget;
            assemblyMetadata = e.Metadata;
        }

        public async Task RunAsync()
        {
            var ui = Synchronizer.UI;
            var titleLabel = Synchronizer.UI.AssemblyReviewViewerTitle;
            var grid = Synchronizer.UI.AssemblyChangeRequestGrid;

            try
            {
                ui.AssemblyReviewsSplit.Panel2Collapsed = false;
                ui.MarkAssemblyAsFixedButton.Visible = reviewTarget.Status == ReviewTargetStatus.Rejected.ToString();

                Synchronizer.CurrentAssemblyReview = review;
                Synchronizer.CurrentAssemblyReviewTarget = reviewTarget;
      

                logger.Debug("Preparing UI elements...");
                ui.SetDeliverableStatusText(ui.AssemblyReviewStatus, reviewTarget.Status);

                logger.Debug("Getting reviewer details...");
                var reviewerDetails = await GetReviewerDetailsAsync(review.ReviewerId);
                titleLabel.Text = $"Assembly {Path.GetFileName(assemblyMetadata.RelativeFilePath)} reviewed by {reviewerDetails.FullName}";

                grid.Rows.Clear();

                foreach(var changeRequest in reviewTarget.ChangeRequests)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(
                        grid,
                        changeRequest.ChangeDescription,
                        changeRequest.Status,
                        changeRequest.DesignerComments
                    );
                    row.Tag = changeRequest;

                    grid.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                var message = $"Failed to open assembly review: {ex.Message}";
                logger.Error(message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<UserDetails> GetReviewerDetailsAsync(string userId)
        {
            if (!userDetailsIndex.ContainsKey(userId))
                userDetailsIndex[userId] = await Synchronizer.AuthServiceClient.GetUserDetailsAsync(userId);

            return userDetailsIndex[userId];
        }
    }
}
