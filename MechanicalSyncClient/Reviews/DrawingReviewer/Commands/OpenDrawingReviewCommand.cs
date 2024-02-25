using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Reviews.DrawingReviewer.Commands
{
    public class OpenDrawingReviewCommand : IDrawingReviewerCommandAsync
    {
        private readonly ReviewTarget reviewTarget;

        public IDrawingReviewer Reviewer { get; }

        public OpenDrawingReviewCommand(IDrawingReviewer drawingReviewer, ReviewTarget reviewTarget)
        {
            Reviewer = drawingReviewer ?? throw new ArgumentNullException(nameof(drawingReviewer));
            this.reviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));
        }

        public async Task RunAsync()
        {
            var UI = Reviewer.UI;

            try
            {
                Reviewer.ReviewTarget = reviewTarget ?? throw new ArgumentNullException(nameof(reviewTarget));

                UI.MarkupStatus.Text = "Opening drawing...";
                UI.SetReviewTargetStatusText(reviewTarget.Status);
                UI.SetReviewControlsEnabled(
                    Reviewer.StatusesHavingReviewControlsEnabled.Contains(reviewTarget.Status)
                );

                Reviewer.DrawingMetadata = await Reviewer.SyncServiceClient.GetFileMetadataAsync(reviewTarget.TargetId);
                UI.SetHeaderText(
                    $"Reviewing {Path.GetFileNameWithoutExtension(Reviewer.DrawingMetadata.RelativeFilePath)} from {Reviewer.Review}"
                );

                Reviewer.TempDownloadedDrawingFile = Path.GetTempFileName().Replace(".tmp", ".slddrw");

                UI.ShowDrawingMarkupPanel();
                UI.ShowDownloadProgress();

                await Reviewer.SyncServiceClient.DownloadFileAsync(new DownloadFileRequest()
                {
                    LocalFilename = Reviewer.TempDownloadedDrawingFile,
                    RelativeEquipmentPath = Reviewer.Review.RemoteProject.RelativeEquipmentPath,
                    RelativeFilePath = Reviewer.DrawingMetadata.RelativeFilePath,
                    VersionFolder = "Ongoing"
                }, UI.ReportDownloadProgress);

                UI.HideDownloadProgress();
                UI.LoadDrawing(Reviewer.TempDownloadedDrawingFile, Reviewer.DrawingReviewerControl_OpenDocError);

                if (Reviewer.StatusesHavingMarkupFile.Contains(reviewTarget.Status))
                {
                    // download the markup file from server
                    await Reviewer.DownloadMarkupFileAsync();

                    // open the markup file in the eDrawings viewer
                    UI.DrawingReviewerControl.OpenMarkupFile(Reviewer.TempDownloadedMarkupFile);
                }

                Reviewer.UI.MarkupStatus.Text = "Ready";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not open drawing for review, {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                await Reviewer.CloseReviewTargetAsync();
            }
        }
    }
}
