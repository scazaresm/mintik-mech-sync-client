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

namespace MechanicalSyncApp.Reviews.DrawingReviewer
{
    public class DrawingMarkupFileUploader
    {
        private readonly IDrawingReviewer reviewer;

        public DrawingMarkupFileUploader(IDrawingReviewer drawingReviewer)
        {
            reviewer = drawingReviewer ?? throw new ArgumentNullException(nameof(drawingReviewer));
        }

        public async Task<bool> UploadAsync()
        {
            var UI = reviewer.UI;
            var Review = reviewer.Review;
            var ReviewTarget = reviewer.ReviewTarget;

            if (reviewer.ReviewTarget == null) return false;

            // use eDrawings to save the current markup file to a temporary file
            reviewer.TempUploadedMarkupFile = Path.Combine(Path.GetTempPath(), ReviewTarget.TargetId);
            UI.DrawingReviewerControl.OpenMarkupFile(reviewer.TempUploadedMarkupFile);

            // eDrawings will add the file extension
            reviewer.TempUploadedMarkupFile += ".All Reviews.markup";

            // eDrawings saves the file asynchronously but the API doesn't expose a callback to know when
            // it has finished, so we need to monitor file size
            await WaitForStableFileSize(reviewer.TempUploadedMarkupFile);

            // eDrawings will not create a file if there is no markup content
            if (!File.Exists(reviewer.TempUploadedMarkupFile))
            {
                MessageBox.Show("You must add at least one change request.",
                    "No content",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
                return false;
            }

            // once we got the temporary markup file, upload it to server
            await reviewer.SyncServiceClient.UploadFileAsync(new UploadFileRequest()
            {
                VersionId = Review.RemoteReview.VersionId,
                LocalFilePath = reviewer.TempUploadedMarkupFile,
                VersionFolder = "Markup",
                RelativeEquipmentPath = Review.RemoteProject.RelativeEquipmentPath,
                RelativeFilePath = $"{ReviewTarget.TargetId}.markup"
            });
            return true;
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
