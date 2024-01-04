using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Reviews.DrawingReviewer
{
    public class DrawingReviewer : IDrawingReviewer
    {
        public IDrawingReviewerUI UI { get; }
        public IMechSyncServiceClient SyncServiceClient { get; }

        private DeltaDrawingsTreeView deltaDrawingsTreeView;

        public LocalReview Review { get; }

        private FileMetadata drawingMetadata;
        private ReviewTarget reviewTarget;

        private string tempDownloadedDrawingFile;
        private string tempDownloadedMarkupFile;
        private string tempUploadedMarkupFile;

        public DrawingReviewer(IMechSyncServiceClient serviceClient,
                               IDrawingReviewerUI ui,
                               LocalReview review
            )
        {
            // validate inputs and assign properties
            SyncServiceClient = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));
            UI = ui ?? throw new ArgumentNullException(nameof(ui));
            Review = review ?? throw new ArgumentNullException(nameof(review));
        }

        private void DeltaDrawingsTreeView_OnOpenDrawingForReview(object sender, OpenDrawingForReviewEventArgs e)
        {
            _ = OpenReviewTarget(e.ReviewTarget);
        }

        public async Task OpenReviewTarget(ReviewTarget reviewTarget)
        {
            try
            {
                this.reviewTarget = reviewTarget;
                drawingMetadata = await SyncServiceClient.GetFileMetadataAsync(reviewTarget.TargetId);
                tempDownloadedDrawingFile = Path.GetTempFileName().Replace(".tmp", ".slddrw");

                UI.ShowDrawingMarkupPanel();
                UI.ShowDownloadProgress();

                await SyncServiceClient.DownloadFileAsync(new DownloadFileRequest()
                {
                    LocalFilename = tempDownloadedDrawingFile,
                    RelativeEquipmentPath = Review.RemoteProject.RelativeEquipmentPath,
                    RelativeFilePath = drawingMetadata.RelativeFilePath,
                    VersionFolder = "Ongoing"
                }, UI.ReportDownloadProgress);

                UI.HideDownloadProgress();

                UI.LoadDrawing(tempDownloadedDrawingFile, DrawingReviewerControl_OpenDocError);
                UI.SetHeaderText(
                    $"Reviewing drawing {Path.GetFileNameWithoutExtension(drawingMetadata.RelativeFilePath)} from {Review}"
                );

                // if drawing has 'Reviewing' status, it shall have a corresponding markup file on server
                if (reviewTarget.Status == "Reviewing")
                    await LoadMarkupFileFromServer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void CloseReviewTarget()
        {
            // dispose the drawing viewer
            UI.DisposeDrawing();

            // cleanup temporary files
            if (File.Exists(tempDownloadedDrawingFile))
                File.Delete(tempDownloadedDrawingFile);

            if (File.Exists(tempDownloadedMarkupFile))
                File.Delete(tempDownloadedMarkupFile);

            if (File.Exists(tempUploadedMarkupFile))
                File.Delete(tempUploadedMarkupFile);

            // clear field values
            tempDownloadedDrawingFile = null;
            tempDownloadedMarkupFile = null;
            tempUploadedMarkupFile = null;
            drawingMetadata = null;
            reviewTarget = null;

            // update UI
            UI.HideDrawingMarkupPanel();
            UI.SetHeaderText($"Reviewing {Review}");
            _ = RefreshDeltaDrawings();
        }

        private void DrawingReviewerControl_OpenDocError(string FileName, int ErrorCode, string ErrorString)
        {
            MessageBox.Show($"Failed to open drawing {FileName}: Error {ErrorCode}, {ErrorString}");
            CloseReviewTarget();
        }

        public async Task RefreshDeltaDrawings()
        {
            await deltaDrawingsTreeView.Refresh();
        }

        public void InitializeUI()
        {
            deltaDrawingsTreeView = new DeltaDrawingsTreeView(SyncServiceClient, Review);
            deltaDrawingsTreeView.AttachTreeView(UI.DeltaDrawingsTreeView);
            deltaDrawingsTreeView.OnOpenDrawingForReview += DeltaDrawingsTreeView_OnOpenDrawingForReview;

            UI.HideDrawingMarkupPanel();
            UI.SetHeaderText($"Reviewing {Review}");
            UI.ZoomButton.Click += ZoomButton_Click;
            UI.PanButton.Click += PanButton_Click;
            UI.ChangeRequestButton.Click += ChangeRequestButton_Click;
            UI.SaveProgressButton.Click += SaveProgressButton_Click;
            UI.CloseDrawingButton.Click += CloseDrawingButton_Click;
        }

        private async Task LoadMarkupFileFromServer()
        {
            if (reviewTarget.Status != "Reviewing") return;
            try
            {
                // download the markup file from server and save it in a temporary file
                tempDownloadedMarkupFile = Path.GetTempFileName().Replace(".tmp", ".markup");

                await SyncServiceClient.DownloadFileAsync(new DownloadFileRequest()
                {
                    VersionFolder = "Markup",
                    RelativeEquipmentPath = Review.RemoteProject.RelativeEquipmentPath,
                    LocalFilename = tempDownloadedMarkupFile,
                    RelativeFilePath = $"{reviewTarget.TargetId}.markup"
                });

                // open the markup file in the eDrawings viewer
                UI.DrawingReviewerControl.OpenMarkupFile(tempDownloadedMarkupFile);
            }
            catch(Exception ex)
            { 
                MessageBox.Show(
                    $"Failed to load markup file, {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }
        }

        private void SaveProgressButton_Click(object sender, EventArgs e)
        {
            _ = SaveReviewProgressAsync();
        }

        private async Task SaveReviewProgressAsync()
        {
            if (reviewTarget == null) return;

            try
            {
                // use eDrawings to save the current markup file to a temporary file
                tempUploadedMarkupFile = Path.Combine(Path.GetTempPath(), reviewTarget.TargetId);
                UI.DrawingReviewerControl.SaveMarkupFile(tempUploadedMarkupFile);

                // eDrawings will add the file extension
                tempUploadedMarkupFile += ".All Reviews.markup";

                // eDrawings will save the file asynchronously but we don't have a callback to know when
                // it has finished, so we need to monitor file size
                await WaitForStableFileSize(tempUploadedMarkupFile);

                // once we have the temporary markup file, upload it to server
                await SyncServiceClient.UploadFileAsync(new UploadFileRequest()
                {
                    VersionId = Review.RemoteReview.VersionId,
                    LocalFilePath = tempUploadedMarkupFile,
                    VersionFolder = "Markup",
                    RelativeEquipmentPath = Review.RemoteProject.RelativeEquipmentPath,
                    RelativeFilePath = $"{reviewTarget.TargetId}.markup"
                });

                // put review target status to 'Reviewing' if not already set. This condition shall be met
                // in order to load the corresponding markup file when this review target is open again
                if (reviewTarget.Status == "Reviewing") return;

                reviewTarget = await SyncServiceClient.UpdateReviewTarget(new UpdateReviewTargetRequest()
                {
                    ReviewId = Review.RemoteReview.Id,
                    ReviewTargetId = reviewTarget.Id,
                    Status = "Reviewing"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void CloseDrawingButton_Click(object sender, EventArgs e)
        {
            CloseReviewTarget();
        }

        private void ChangeRequestButton_Click(object sender, EventArgs e)
        {
            UI.DrawingReviewerControl.SetCloudWithLeaderMarkupOperator();
        }

        private void PanButton_Click(object sender, EventArgs e)
        {
            UI.DrawingReviewerControl.SetPanOperator();
        }

        private void ZoomButton_Click(object sender, EventArgs e)
        {
            UI.DrawingReviewerControl.SetZoomOperator();
        }

        private async Task WaitForStableFileSize(string filePath)
        {
            long previousSize = -1;
            FileInfo fileInfo = new FileInfo(filePath);

            // Monitor the file size until it remains unchanged for a specific duration
            while (true)
            {
                await Task.Delay(1000);

                if (fileInfo.Exists)
                {
                    long currentSize = fileInfo.Length;

                    if (currentSize == previousSize)
                    {
                        // The file size is not changing
                        Console.WriteLine($"File size stable at {currentSize} bytes");
                        break;
                    }
                    else
                    {
                        // Update the previous size and continue monitoring
                        previousSize = currentSize;
                    }
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                    break;
                }
            }
        }
    }
}
