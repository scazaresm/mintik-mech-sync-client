using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using MechanicalSyncApp.Publishing.DeliverablePublisher.Strategies;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            foreach (var drawing in validDrawings)
            {
                if (!drawingLookup.ContainsKey(drawing.Id)) continue;

                drawingLookup[drawing.Id].Cells["PublishingStatus"].Value = "Publishing...";
                await publishStrategy.PublishAsync(drawing);
                drawingLookup[drawing.Id].Cells["PublishingStatus"].Value = "Published";
            }
        }

        public override void UpdateUI()
        {
        }
    }
}
