using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Util;
using MechanicalSyncApp.Properties;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher.States
{
    public class ValidateDrawingsState : DeliverablePublisherState
    {
        private readonly IReviewableFileMetadataFetcher drawingFetcher;
        private readonly IDrawingValidator drawingValidator;
        private readonly ILogger logger;

        public static ConcurrentDictionary<string, FileMetadata> DrawingValidationCache = new ConcurrentDictionary<string, FileMetadata>();

        public CancellationTokenSource CancellationTokenSource { get; set; }

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
            var ui = Publisher.UI;

            try
            {
                logger.Debug("Starting ValidateDrawingsState...");

                ui.StatusLabel.Text = "Validating drawings...";
                ui.ValidateButton.Text = "Cancel validation";
                ui.ValidateButton.Enabled = true;
                ui.ValidateButton.Image = Resources.reject_24;

                ui.PublishSelectedButton.Enabled = false;
                ui.ViewBlockersButton.Enabled = true;
                ui.CancelSelectedButton.Enabled = false;

                var remoteVersion = Publisher.Synchronizer.Version.RemoteVersion;

                var allDrawings = await drawingFetcher.FetchReviewableDrawingsAsync();

                // skip ignored drawings for this version
                allDrawings = allDrawings.Where((d) =>
                    !remoteVersion.IgnoreDrawings.Contains(Path.GetFileName(d.RelativeFilePath))
                ).ToList();

                var viewer = Publisher.UI.ReviewableDrawingsViewer;

                var localVersionDirectory = Publisher.Synchronizer.Version.LocalDirectory;

                int processedCount = 0;
                ui.DrawingsGridView.Rows.Clear();
                foreach (var drawing in allDrawings)
                {
                    CancellationTokenSource?.Token.ThrowIfCancellationRequested();

                    ui.StatusLabel.Text = $"Validating {Path.GetFileName(drawing.RelativeFilePath)}...";

                    // add drawing to grid and show as validating
                    viewer.AddDrawing(drawing, "Validating...");
                    ui.DrawingsGridView.ClearSelection();

                    // replace server's full file path with local full file path
                    drawing.FullFilePath = Path.Combine(localVersionDirectory, drawing.RelativeFilePath.Replace('/', Path.DirectorySeparatorChar));


                    // validate drawing if approved and not already published
                    if (drawing.ApprovalCount > 0 && !drawing.IsPublished)
                    {
                        var cachedValidation = DrawingValidationCache.ContainsKey(drawing.Id) ? DrawingValidationCache[drawing.Id] : null;

                        // if validation is cached and file checksum is same, then use cached validation and avoid SolidWorks operations to speed up
                        if (cachedValidation != null && drawing.FileChecksum == cachedValidation.FileChecksum)
                        {
                            drawing.ValidationIssues = cachedValidation.ValidationIssues;
                        }
                        else
                        {
                            // no good cached validation was found, validate drawing with SolidWorks
                            await drawingValidator.ValidateAsync(drawing);

                            // update cache with latest validation
                            if (DrawingValidationCache.ContainsKey(drawing.Id))
                                DrawingValidationCache[drawing.Id] = drawing;
                            else
                                DrawingValidationCache.TryAdd(drawing.Id, drawing);
                        }
                    }

                    // update publishing status on the grid
                    if (viewer.DrawingLookup.ContainsKey(drawing.Id))
                    {
                        var drawingRow = viewer.DrawingLookup[drawing.Id];
                        drawingRow.Tag = drawing;

                        var publishingStatusCell = viewer.DrawingLookup[drawing.Id].Cells["PublishingStatus"];
                        publishingStatusCell.Value = drawing.PublishingStatus.GetDescription();
                        publishingStatusCell.Style = drawing.GetPublishingStatusCellStyle();
                    }
                    processedCount++;
                    UpdateProgress(allDrawings.Count, processedCount);
                }
                logger.Debug("ValidateDrawingsState complete.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ui.PublishSelectedButton.Enabled = false;
                ui.ViewBlockersButton.Enabled = false;
                ui.CancelSelectedButton.Enabled = false;
                ui.ValidateButton.Text = "Validate";
                ui.StatusLabel.Text = "Ready";
                ui.Progress.Visible = false;
                ui.DrawingsGridView.Enabled = true;

                ui.ValidateButton.Image = Resources.inspect_deliverables_24;
            }
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
