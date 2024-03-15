using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using MechanicalSyncApp.Publishing.DeliverablePublisher;
using MechanicalSyncApp.Publishing.DeliverablePublisher.Strategies;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing
{
    public class DrawingValidator
    {
        private readonly ISolidWorksStarter solidWorksStarter;
        private readonly string relativePublishingDirectory;
        private readonly ILogger logger;

        public DrawingValidator(ISolidWorksStarter solidWorksStarter,
                                string relativePublishingDirectory,
                                ILogger logger)
        {
            this.solidWorksStarter = solidWorksStarter ?? throw new ArgumentNullException(nameof(solidWorksStarter));
            this.relativePublishingDirectory = relativePublishingDirectory ?? throw new ArgumentNullException(nameof(relativePublishingDirectory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ValidateAsync(FileMetadata drawing)
        {
            drawing.ValidationIssues.Clear();

            // validate drawing revision
            await new DefaultDrawingRevisionValidationStrategy(
                solidWorksStarter, 
                new NextDrawingRevisionCalculator(relativePublishingDirectory),
                logger
            ).ValidateAsync(drawing);
        }
    }
}
