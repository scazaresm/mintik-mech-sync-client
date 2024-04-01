using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.SolidWorksInterop;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Reviews.AssemblyReviewer.Commands
{
    public class CloseAssemblyReviewCommand : IAssemblyReviewerCommandAsync
    {
        private readonly ILogger logger;

        public IAssemblyReviewer Reviewer { get; }

        public CloseAssemblyReviewCommand(IAssemblyReviewer reviewer, ILogger logger)
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("CloseAssemblyReviewCommand begins...");

            var ui = Reviewer.Args.UI;
            ui.CloseAssemblyButton.Enabled = false;
            try
            {
                var fileName = Path.GetFileName(Reviewer.AssemblyMetadata.RelativeFilePath);

                var starter = (Reviewer.Args.SolidWorksStarter as SolidWorksStarter);
                starter.SolidWorksApp.CloseDoc(fileName);

                logger.Debug("CloseAssemblyReviewCommand complete.");
            }
            catch (Exception ex)
            {
                var message = $"Failed to close assembly review: ${ex.Message}";

                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ui.HideReviewPanel();
                ui.CloseAssemblyButton.Enabled = true;
            }
        }
    }
}
