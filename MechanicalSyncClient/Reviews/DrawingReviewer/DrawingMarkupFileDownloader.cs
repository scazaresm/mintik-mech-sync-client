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
using Serilog;
using MechanicalSyncApp.Core.Util;

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
                reviewer.TempDownloadedMarkupFile = PathUtils.GetTempFileNameWithExtension(".markup");

                await reviewer.SyncServiceClient.DownloadFileAsync(new DownloadFileRequest()
                {
                    VersionFolder = "Markup",
                    RelativeEquipmentPath = reviewer.Review.RemoteProject.RelativeEquipmentPath,
                    LocalFilename = reviewer.TempDownloadedMarkupFile,
                    RelativeFilePath = $"{reviewer.ReviewTarget.TargetId}.markup"
                });
            }
            catch (Exception ex)
            {
                var message = $"Failed to load markup file, {ex.Message}";
                Log.Error(message);
                MessageBox.Show(message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
