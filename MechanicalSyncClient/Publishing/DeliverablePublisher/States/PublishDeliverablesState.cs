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
            var syncServiceClient = Publisher.Synchronizer.SyncServiceClient;
            var publishingIndexByPartNumber = Publisher.Synchronizer.PublishingIndexByPartNumber;
            var publishingSummaryDirectory = Path.Combine(
                Publisher.Synchronizer.BasePublishingDirectory,
                Publisher.Synchronizer.RelativePublishingSummaryDirectory
            );

            if (!Directory.Exists(publishingSummaryDirectory))
                Directory.CreateDirectory(publishingSummaryDirectory);

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
                    await publishStrategy.PublishAsync(drawing);

                    var publishing = await PublishFileInDatabaseAsync(syncServiceClient, drawing);

                    WritePublishingSummaryAsJsonFile(publishing, publishingSummaryDirectory);
                   
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

        private async Task<FilePublishing> PublishFileInDatabaseAsync(
                IMechSyncServiceClient syncServiceClient, FileMetadata drawing
            )
        {
            return await syncServiceClient.PublishFileAsync(new PublishFileRequest()
            {
                FileMetadataId = drawing.Id,
                Revision = drawing.Revision,
                Deliverables = publishStrategy.Deliverables,
                CustomProperties = drawing.CustomProperties,
            });
        }

        private void WritePublishingSummaryAsJsonFile(FilePublishing publishing, string publishingSummaryDirectory)
        {
            var designerEmail = Publisher.Synchronizer.AuthServiceClient.LoggedUserDetails.Email;
            var projectName = Publisher.Synchronizer.Version.RemoteProject.FolderName;

            var relativeFilePaths = publishing.Deliverables.Select((filePath) => 
                filePath.Replace(publishStrategy.FullProjectPublishingDirectory, "")
            ).ToList();

            var publishingSummary = new PublishingSummary()
            {
                DesignerEmail = designerEmail,
                ProjectName = projectName,
                RelativeProjectDirectory = publishStrategy.RelativeProjectPublishingDirectory,
                ManufacturingMetadata = new ManufacturingMetadata()
                {
                    DrawingName = publishing.PartNumber,
                    DrawingRevision = publishing.Revision,
                    CustomProperties = publishing.CustomProperties,
                },
                RelativeFilePaths = relativeFilePaths
            };

            var publishingSummaryJson = JsonUtils.SerializeWithCamelCase(publishingSummary);
            var summaryFilePath = Path.Combine(
                publishingSummaryDirectory,
                $"{publishing.PartNumber}_{publishing.Id}.json"
            );
            File.WriteAllText(summaryFilePath, publishingSummaryJson);
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
