using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Reviews.AssemblyReviewer.Commands
{
    public class OpenAssemblyReviewCommand : IAssemblyReviewerCommandAsync
    {
        public IAssemblyReviewer Reviewer { get; }

        private readonly ReviewTarget reviewTarget;
        private readonly ILogger logger;

        public OpenAssemblyReviewCommand(
                IAssemblyReviewer reviewer,
                ReviewTarget reviewTarget, 
                ILogger logger
            ) 
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
            this.reviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("OpenAssemblyReviewCommand begins...");

            var ui = Reviewer.Args.UI;
            var starter = Reviewer.Args.SolidWorksStarter;
            var syncService = Reviewer.Args.SyncServiceClient;
            var isAlreadyReviewed = reviewTarget.Status != ReviewTargetStatus.Pending.ToString() &&
              reviewTarget.Status != ReviewTargetStatus.Reviewing.ToString();

            try
            {
                ui.ChangeRequestInput.Enabled = false;
                ui.ReviewToolStrip.Enabled = false;
                ui.ChangeRequestsGrid.Enabled = false;
                ui.ChangeRequestSplit.Panel2Collapsed = isAlreadyReviewed;
                ui.RejectAssemblyButton.Enabled = !isAlreadyReviewed;
                ui.ApproveAssemblyButton.Enabled = !isAlreadyReviewed;
                ui.SetReviewTargetStatusText(reviewTarget.Status);
                ui.StatusLabel.Text = "Loading assembly...";

                ui.PopulateChangeRequestGrid(reviewTarget);
                ui.ShowReviewPanel();

                Reviewer.ReviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
                Reviewer.AssemblyMetadata = await syncService.GetFileMetadataAsync(reviewTarget.TargetId);
                
                var assemblyFilePath = Path.Combine(
                    Reviewer.Args.TempWorkingCopyDirectory,
                    Reviewer.AssemblyMetadata.RelativeFilePath
                );

                ui.SetHeaderText(
                   $"Reviewing {Path.GetFileNameWithoutExtension(assemblyFilePath)} from {Reviewer.Args.Review}"
                );
                await new SolidWorksModelLoader(starter, logger).LoadModelAsync(assemblyFilePath);

                ui.ReviewToolStrip.Enabled = true;
                ui.StatusLabel.Text = "Ready.";
                ui.ChangeRequestInput.Enabled = true;
                ui.ChangeRequestsGrid.Enabled = true;

                logger.Debug("OpenAssemblyReviewCommand complete.");
            }
            catch(Exception ex)
            {
                var message = $"Failed to open assembly: {ex.Message}";
                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ui.HideReviewPanel();
            }
        }
    }
}
