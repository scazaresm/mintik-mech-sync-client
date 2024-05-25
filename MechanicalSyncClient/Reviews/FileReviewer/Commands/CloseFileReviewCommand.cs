using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.SolidWorksInterop;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Reviews.FileReviewer.Commands
{
    public class CloseFileReviewCommand : IFileReviewerCommandAsync
    {
        private readonly ILogger logger;

        public IFileReviewer Reviewer { get; }

        public CloseFileReviewCommand(IFileReviewer reviewer, ILogger logger)
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug("CloseFileReviewCommand begins...");

            var ui = Reviewer.Args.UI;
            ui.CloseFileReviewButton.Enabled = false;

            var parentForm = ui.ChangeRequestsGrid.FindForm();
            var parentWasTopMost = parentForm.TopMost;
            parentForm.TopMost = false;

            try
            {
                var fileName = Path.GetFileName(Reviewer.Metadata.RelativeFilePath);

                var starter = Reviewer.Args.SolidWorksStarter as SolidWorksStarter;
                starter.SolidWorksApp.CloseDoc(fileName);
                await Reviewer.RefreshReviewTargetsAsync();

                logger.Debug("CloseFileReviewCommand complete.");
            }
            catch (Exception ex)
            {
                var message = $"Failed to close file review: ${ex.Message}";

                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ui.HideReviewPanel();
                ui.CloseFileReviewButton.Enabled = true;
                parentForm.TopMost = parentWasTopMost;
            }
        }
    }
}
