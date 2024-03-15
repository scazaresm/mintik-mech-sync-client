using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using MechanicalSyncApp.Core.Util;
using MechanicalSyncApp.Publishing.DeliverablePublisher.Strategies;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher.States
{
    public class ValidateDrawingsState : DeliverablePublisherState
    {
        private readonly IReviewableFileMetadataFetcher metadataFetcher;
        private readonly ILogger logger;

        public ValidateDrawingsState(IReviewableFileMetadataFetcher metadataFetcher, ILogger logger)
        {
            this.metadataFetcher = metadataFetcher ?? throw new ArgumentNullException(nameof(metadataFetcher));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task RunAsync()
        {
            logger.Debug("Starting ValidateDrawingsState...");

            var ui = Publisher.UI;
            ui.StatusLabel.Text = "Validating drawings...";

            var allDrawings = await metadataFetcher.FetchReviewableDrawingsAsync();
            var viewer = Publisher.UI.ReviewableDrawingsViewer;

            var localVersionDirectory = Publisher.Synchronizer.Version.LocalDirectory;
            var projectFolderName = Publisher.Synchronizer.Version.RemoteProject.FolderName;
            var relativePublishingDirectory = Path.Combine(DateTime.Now.Year.ToString(), projectFolderName, "PDF");

            var validator = new DrawingValidator(
                GetDrawingRevisionValidationStrategy(relativePublishingDirectory),
                logger
            );

            int validatedDrawingsCount = 0;

            foreach (var drawing in allDrawings)
            {
                ui.StatusLabel.Text = $"Validating {Path.GetFileName(drawing.RelativeFilePath)}...";

                // add drawing to grid and show as validating
                viewer.AddDrawing(drawing, "Validating...");
                ui.DrawingsGridView.ClearSelection();

                // replace server's full file path with local path
                drawing.FullFilePath = Path.Combine(localVersionDirectory, drawing.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar));

                // validate drawing if not already published
                if (!drawing.IsPublished)
                    await validator.ValidateAsync(drawing);

                // update publishing status on the grid
                if (viewer.DrawingLookup.ContainsKey(drawing.Id))
                {
                    var drawingRow = viewer.DrawingLookup[drawing.Id];
                    drawingRow.Tag = drawing;
                    viewer.DrawingLookup[drawing.Id].Cells[3].Value = drawing.PublishingStatus.GetDescription();
                }

                validatedDrawingsCount++;
                UpdateProgress(allDrawings.Count, validatedDrawingsCount);
            }
            ui.MainToolStrip.Enabled = true;
            ui.StatusLabel.Text = "Ready";
            ui.Progress.Visible = false;

            logger.Debug("ValidateDrawingsState complete.");
        }

        public override void UpdateUI()
        {
            Publisher.UI.Initialize();
        }

        private void UpdateProgress(int totalDrawingsCount, int validatedDrawingsCount)
        {
            var ui = Publisher.UI;
            if (!ui.Progress.Visible) ui.Progress.Visible = true;

            // compute progress, check division by zero
            int progress = totalDrawingsCount > 0
                ? (int)((double)validatedDrawingsCount / totalDrawingsCount * 100.0)
                : 0;

            if (ui.Progress != null && progress >= 0 && progress <= 100)
                ui.Progress.Value = progress;
        }

        private IDrawingRevisionValidationStrategy GetDrawingRevisionValidationStrategy(string relativePublishingDirectory)
        {
            return new DefaultDrawingRevisionValidationStrategy(
                new NextDrawingRevisionCalculator(relativePublishingDirectory, logger),
                new DrawingRevisionRetriever(Publisher.SolidWorksStarter, logger),
                logger
            );
        }
    }
}
