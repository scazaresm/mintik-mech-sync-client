using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Reviews.DrawingReviewer.Commands
{
    public class CloseDrawingReviewCommand : IDrawingReviewerCommandAsync
    {
        public IDrawingReviewer Reviewer { get; }

        public CloseDrawingReviewCommand(IDrawingReviewer reviewer)
        {
            Reviewer = reviewer ?? throw new ArgumentNullException(nameof(reviewer));
        }

        public async Task RunAsync()
        {
            var UI = Reviewer.UI;

            // dispose the drawing viewer
            UI.DisposeDrawing();

            // cleanup temporary files
            if (File.Exists(Reviewer.TempDownloadedDrawingFile))
                File.Delete(Reviewer.TempDownloadedDrawingFile);

            if (File.Exists(Reviewer.TempDownloadedMarkupFile))
                File.Delete(Reviewer.TempDownloadedMarkupFile);

            if (File.Exists(Reviewer.TempUploadedMarkupFile))
                File.Delete(Reviewer.TempUploadedMarkupFile);

            // clear field values
            Reviewer.TempDownloadedDrawingFile = null;
            Reviewer.TempDownloadedMarkupFile = null;
            Reviewer.TempUploadedMarkupFile = null;
            Reviewer.DrawingMetadata = null;
            Reviewer.ReviewTarget = null;

            // update UI
            UI.HideDrawingMarkupPanel();
            UI.SetHeaderText($"Reviewing {Reviewer.Review}");
            await Reviewer.RefreshDeltaTargetsAsync();
        }
    }
}
