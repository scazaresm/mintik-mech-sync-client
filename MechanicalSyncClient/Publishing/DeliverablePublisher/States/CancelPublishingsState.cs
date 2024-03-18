using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher.States
{
    public class CancelPublishingsState : DeliverablePublisherState
    {
        private readonly ILogger logger;
        private readonly ICancelPublishingStrategy cancelStrategy;
        private readonly List<FileMetadata> drawings;

        public CancelPublishingsState(
                ICancelPublishingStrategy cancelStrategy,
                List<FileMetadata> drawings, 
                ILogger logger
            )
        {
            this.cancelStrategy = cancelStrategy ?? throw new ArgumentNullException(nameof(cancelStrategy));
            this.drawings = drawings ?? throw new ArgumentNullException(nameof(drawings));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task RunAsync()
        {
            var ui = Publisher.UI;
            var drawingLookup = ui.ReviewableDrawingsViewer.DrawingLookup;
            var publishingIndexByPartNumber = Publisher.Synchronizer.PublishingIndexByPartNumber;

            var processedCount = 0;
            foreach (var drawing in drawings)
            {
                try
                {
                    var partNumber = Path.GetFileNameWithoutExtension(drawing.FullFilePath);

                    if (!publishingIndexByPartNumber.ContainsKey(partNumber))
                        throw new InvalidOperationException("could not find publishing object.");

                    await cancelStrategy.CancelAsync(publishingIndexByPartNumber[partNumber]);
                    publishingIndexByPartNumber.TryRemove(partNumber, out FilePublishing cancelledPublishing);
                    drawingLookup[drawing.Id].Cells["PublishingStatus"].Value = "Cancelled";
                }
                catch(Exception ex)
                {
                    drawingLookup[drawing.Id].Cells["PublishingStatus"].Value = $"Publishing cancel failed: {ex.Message}";
                }
                UpdateProgress(drawings.Count, processedCount);
            }
            await Publisher.ValidateDrawingsAsync();
        }

        public override void UpdateUI()
        {
            var ui = Publisher.UI;

            ui.Progress.Value = 0;
            ui.Progress.Visible = true;
            ui.CancelSelectedButton.Enabled = false;
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
