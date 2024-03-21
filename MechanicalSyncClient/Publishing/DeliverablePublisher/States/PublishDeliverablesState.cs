using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher.States
{
    public class PublishDeliverablesState : DeliverablePublisherState
    {
        private readonly List<FileMetadata> validDrawings;
        private readonly IPublishDeliverablesStrategy publishStrategy;
        private readonly ILogger logger;

        public PublishDeliverablesState(
                List<FileMetadata> validDrawings, 
                IPublishDeliverablesStrategy publishStrategy,
                ILogger logger
            )
        {
            this.validDrawings = validDrawings ?? throw new ArgumentNullException(nameof(validDrawings));
            this.publishStrategy = publishStrategy ?? throw new ArgumentNullException(nameof(publishStrategy));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task RunAsync()
        {
            var ui = Publisher.UI;
            var drawingLookup = ui.ReviewableDrawingsViewer.DrawingLookup;
            var publishingIndexByPartNumber = Publisher.Synchronizer.PublishingIndexByPartNumber;


            int processedCount = 0;
            foreach (var drawing in validDrawings)
            {
                try
                {
                    if (!drawingLookup.ContainsKey(drawing.Id))
                        throw new Exception("Drawing not found in lookup.");

                    var partNumber = Path.GetFileNameWithoutExtension(drawing.FullFilePath);

                    // is the drawing already on the publishing index?
                    if (publishingIndexByPartNumber.ContainsKey(partNumber))
                        throw new Exception("Drawing is already published.");

                    // execute publishing strategy
                    drawingLookup[drawing.Id].Cells["PublishingStatus"].Value = "Publishing...";
                    ui.StatusLabel.Text = $"Publishing {partNumber}...";
                    var publishing = await publishStrategy.PublishAsync(drawing);

                    // set source file as read only to avoid further changes after publish
                    SetSourceFilesAsReadOnly(drawing);

                    // add this publishing to the index, so that we can identify the drawing as published
                    publishingIndexByPartNumber.TryAdd(partNumber, publishing);
                    drawingLookup[drawing.Id].Cells["PublishingStatus"].Value = "Published";
                }
                catch(Exception ex)
                {
                    drawingLookup[drawing.Id].Cells["PublishingStatus"].Value = $"Publishing failed: {ex.Message}";
                }
                processedCount++;
                UpdateProgress(validDrawings.Count, processedCount);
            }
            ui.Progress.Visible = false;
            ui.StatusLabel.Text = "Ready";
            ui.DrawingsGridView.Enabled = true;
            ui.ValidateButton.Enabled = true;
        }

        public override void UpdateUI()
        {
            var ui = Publisher.UI;
            ui.Progress.Value = 0;
            ui.Progress.Visible = true;
            ui.DrawingsGridView.ClearSelection();
            ui.DrawingsGridView.Enabled = false;
            ui.PublishSelectedButton.Enabled = false;
            ui.CancelSelectedButton.Enabled = false;
            ui.ViewBlockersButton.Enabled = false;
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

        private void SetSourceFilesAsReadOnly(FileMetadata drawing)
        {
            var baseDirectory = Path.GetDirectoryName(drawing.FullFilePath);
            var partNumber = Path.GetFileNameWithoutExtension(drawing.FullFilePath);

            var sourceFiles = Directory.GetFiles(baseDirectory)
                .Where(file => Path.GetFileNameWithoutExtension(file) == partNumber)
                .ToArray();

            foreach (string file in sourceFiles)
                File.SetAttributes(file, File.GetAttributes(file) | FileAttributes.ReadOnly);
        }
    }
}
