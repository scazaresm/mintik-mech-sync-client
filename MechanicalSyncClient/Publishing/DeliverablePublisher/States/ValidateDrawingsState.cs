using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Util;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher.States
{
    public class ValidateDrawingsState : DeliverablePublisherState
    {
        private readonly IReviewableFileMetadataFetcher drawingFetcher;
        private readonly IDrawingValidator drawingValidator;
        private readonly ILogger logger;

        public ValidateDrawingsState(
                IReviewableFileMetadataFetcher drawingFetcher, 
                IDrawingValidator drawingValidator,
                ILogger logger
            )
        {
            this.drawingFetcher = drawingFetcher ?? throw new ArgumentNullException(nameof(drawingFetcher));
            this.drawingValidator = drawingValidator ?? throw new ArgumentNullException(nameof(drawingValidator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task RunAsync()
        {
            logger.Debug("Starting ValidateDrawingsState...");

            var ui = Publisher.UI;
            ui.StatusLabel.Text = "Validating drawings...";

            var allDrawings = await drawingFetcher.FetchReviewableDrawingsAsync();
            var viewer = Publisher.UI.ReviewableDrawingsViewer;

            var localVersionDirectory = Publisher.Synchronizer.Version.LocalDirectory;

            int processedCount = 0;
            ui.DrawingsGridView.Rows.Clear();
            foreach (var drawing in allDrawings)
            {
                ui.StatusLabel.Text = $"Validating {Path.GetFileName(drawing.RelativeFilePath)}...";

                // add drawing to grid and show as validating
                viewer.AddDrawing(drawing, "Validating...");
                ui.DrawingsGridView.ClearSelection();

                // replace server's full file path with local full file path
                drawing.FullFilePath = Path.Combine(localVersionDirectory, drawing.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar));

                // validate drawing if approved and not already published
                if (drawing.ApprovalCount > 0 && !drawing.IsPublished)
                    await drawingValidator.ValidateAsync(drawing);

                // update publishing status on the grid
                if (viewer.DrawingLookup.ContainsKey(drawing.Id))
                {
                    var drawingRow = viewer.DrawingLookup[drawing.Id];
                    drawingRow.Tag = drawing;
                    viewer.DrawingLookup[drawing.Id].Cells["PublishingStatus"].Value = drawing.PublishingStatus.GetDescription();
                }

                processedCount++;
                UpdateProgress(allDrawings.Count, processedCount);
            }
            ui.MainToolStrip.Enabled = true;
            ui.StatusLabel.Text = "Ready";
            ui.Progress.Visible = false;
            ui.DrawingsGridView.Enabled = true;
            ui.PublishSelectedButton.Enabled = false;
            ui.CancelSelectedButton.Enabled = false;
            ui.ValidateButton.Enabled = true;

            logger.Debug("ValidateDrawingsState complete.");
        }

        public override void UpdateUI()
        {
            var ui = Publisher.UI;
            ui.Initialize();
            ui.Progress.Value = 0;
            ui.Progress.Visible = true;
            ui.DrawingsGridView.Enabled = false;
            ui.ValidateButton.Enabled = false;
        }

        private void UpdateProgress(int totalCount, int processedCount)
        {
            var ui = Publisher.UI;

            // compute progress, check division by zero
            int progress = totalCount > 0
                ? (int)((double)processedCount / totalCount * 100.0)
                : 0;

            if (ui.Progress != null && progress >= 0 && progress <= 100)
                ui.Progress.Value = progress;
        }

    }
}
