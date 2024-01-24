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
    public class DrawingMarkupFileDownloader
    {
        private readonly IDrawingReviewer reviewer;

        public DrawingMarkupFileDownloader(IDrawingReviewer reviewer)
        {
            this.reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
        }

        public async Task DownloadAsync()
        {
            try
            {
                var UI = reviewer.UI;

                // download the markup file from server and save it to a temporary file
                reviewer.TempDownloadedMarkupFile = Path.GetTempFileName().Replace(".tmp", ".markup");

                await reviewer.SyncServiceClient.DownloadFileAsync(new DownloadFileRequest()
                {
                    VersionFolder = "Markup",
                    RelativeEquipmentPath = reviewer.Review.RemoteProject.RelativeEquipmentPath,
                    LocalFilename = reviewer.TempDownloadedMarkupFile,
                    RelativeFilePath = $"{reviewer.ReviewTarget.TargetId}.markup"
                });

                // open the markup file in the eDrawings viewer
                UI.DrawingReviewerControl.OpenMarkupFile(reviewer.TempDownloadedMarkupFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load markup file, {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
