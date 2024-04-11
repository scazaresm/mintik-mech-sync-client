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
    public class DrawingValidator : IDrawingValidator
    {
        private readonly IDrawingRevisionValidationStrategy drawingRevisionValidationStrategy;
        private readonly ICustomPropertiesValidationStrategy customPropertiesValidationStrategy;
        private readonly ILogger logger;

        public DrawingValidator(IDrawingRevisionValidationStrategy drawingRevisionValidationStrategy,
                                ICustomPropertiesValidationStrategy customPropertiesValidationStrategy,
                                ILogger logger)
        {
            this.drawingRevisionValidationStrategy = drawingRevisionValidationStrategy ?? throw new ArgumentNullException(nameof(drawingRevisionValidationStrategy));
            this.customPropertiesValidationStrategy = customPropertiesValidationStrategy ?? throw new ArgumentNullException(nameof(customPropertiesValidationStrategy));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ValidateAsync(FileMetadata drawing)
        {
            drawing.ValidationIssues.Clear();

            if (drawing.ApprovalCount <= 0) return;

            // validate drawing revision
            await drawingRevisionValidationStrategy.ValidateAsync(drawing);

            // validate custom properties for associated part document
            await customPropertiesValidationStrategy.ValidateAsync(drawing);
        }
    }
}
