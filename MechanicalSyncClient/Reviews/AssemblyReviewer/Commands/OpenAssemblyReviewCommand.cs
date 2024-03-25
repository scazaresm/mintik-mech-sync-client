using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            var ui = Reviewer.Args.UI;
            var starter = Reviewer.Args.SolidWorksStarter;
            var syncService = Reviewer.Args.SyncServiceClient;

            try
            {
                ui.ReviewToolStrip.Enabled = false;
                ui.ShowReviewPanel();

                Reviewer.ReviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
                Reviewer.AssemblyMetadata = await syncService.GetFileMetadataAsync(reviewTarget.TargetId);

                ui.SetHeaderText(
                   $"Reviewing {Path.GetFileNameWithoutExtension(Reviewer.AssemblyMetadata.RelativeFilePath)} from {Reviewer.Args.Review}"
                );

                var assemblyFilePath = Path.Combine(Reviewer.Args.TempWorkingCopyDirectory, Reviewer.AssemblyMetadata.RelativeFilePath);
                await new SolidWorksModelLoader(starter, logger).LoadModelAsync(assemblyFilePath);

                ui.ReviewToolStrip.Enabled = true;
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
