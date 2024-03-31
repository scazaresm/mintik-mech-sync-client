using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Reviews.AssemblyReviewer.Commands
{
    public class ViewEditChangeRequestCommand : IAssemblyReviewerCommandAsync
    {
        private readonly ChangeRequest changeRequest;

        public IAssemblyReviewer Reviewer { get; }
        
        private readonly ILogger logger;

        public ViewEditChangeRequestCommand(
                IAssemblyReviewer reviewer, 
                ChangeRequest changeRequest,
                ILogger logger
            )
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
            this.changeRequest = changeRequest ?? throw new ArgumentNullException(nameof(changeRequest));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            logger.Debug($"ViewEditChangeRequestCommand begins...");

            var ui = Reviewer.Args.UI;
            var remoteRelativeImagePath = $"{changeRequest.Id}-change.png";
            var localTempImagePath = Path.Combine(Path.GetTempPath(), remoteRelativeImagePath);

            try
            {
                var syncService = Reviewer.Args.SyncServiceClient;
                var relativeEquipmentPath = Reviewer.Args.Review.RemoteProject.RelativeEquipmentPath;

                changeRequest.Parent = Reviewer.ReviewTarget;

                CleanupTempPictureFile(localTempImagePath);

                logger.Debug($"Downloading picture from server: {remoteRelativeImagePath}");
                await syncService.DownloadFileAsync(new DownloadFileRequest()
                {
                    LocalFilename = localTempImagePath,
                    RelativeEquipmentPath = relativeEquipmentPath,
                    VersionFolder = "AssyReview",
                    RelativeFilePath = remoteRelativeImagePath,
                });

                using (var downloadedImage = Image.FromFile(localTempImagePath)) 
                {
                    changeRequest.DetailsPicture = downloadedImage;

                    logger.Debug($"Showing change request details dialog...");
                    var result = new ChangeRequestDetailsDialog(changeRequest).ShowDialog();

                    if (result != DialogResult.OK)
                    {
                        logger.Debug($"User has closed the viewer without editing the change request.");
                        logger.Debug($"ViewEditChangeRequestCommand complete.");
                        return;
                    }
                }
                logger.Debug($"User has edited the change request and saved the changes.");



                logger.Debug($"ViewEditChangeRequestCommand complete.");
            }
            catch(Exception ex)
            {
                var message = $"Failed to open change request for viewing: {ex.Message}";
                logger.Error(message, ex );
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CleanupTempPictureFile(localTempImagePath);
            }
        }

        public void CleanupTempPictureFile(string filePath)
        {
            logger.Debug($"Cleaning up temp picture file...");
            try
            {
                if (filePath != string.Empty && File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch(Exception ex)
            {
                logger.Error($"Failed to cleanup temp picture file: {ex.Message}");
            }
        }
    }
}
