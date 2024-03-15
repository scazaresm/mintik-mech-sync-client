using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher.Strategies
{
    public class DefaultDrawingRevisionValidationStrategy : IDrawingRevisionValidationStrategy
    {
        private readonly INextDrawingRevisionCalculator drawingRevisionCalculator;
        private readonly IDrawingRevisionRetriever drawingRevisionRetriever;
        private readonly ILogger logger;

        public DefaultDrawingRevisionValidationStrategy(
                INextDrawingRevisionCalculator drawingRevisionCalculator,
                IDrawingRevisionRetriever drawingRevisionRetriever,
                ILogger logger
            )
        {
            this.drawingRevisionCalculator = drawingRevisionCalculator ?? throw new ArgumentNullException(nameof(drawingRevisionCalculator));
            this.drawingRevisionRetriever = drawingRevisionRetriever ?? throw new ArgumentNullException(nameof(drawingRevisionRetriever));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ValidateAsync(FileMetadata drawing)
        {
            if (drawing is null)
                throw new ArgumentNullException($"Argument cannot be null: {nameof(drawing)}");

            if (!File.Exists(drawing.FullFilePath))
                throw new FileNotFoundException($"Drawing file not found, make sure that FullFilePath contains a valid file path.");

            logger.Debug($"Validating drawing {Path.GetFileName(drawing.RelativeFilePath)}");

            var drawingFileName = Path.GetFileName(drawing.RelativeFilePath);
            var expectedRevision = drawingRevisionCalculator.GetNextRevision(drawingFileName);
            var drawingRevision = await drawingRevisionRetriever.GetRevisionAsync(drawing.FullFilePath);

            if (expectedRevision != drawingRevision)
            {
                var issue = drawingRevision == string.Empty
                    ? "Revision not found."
                    : $"Incorrect revision: expected '{expectedRevision}' but found '{drawingRevision}'.";

                drawing.ValidationIssues.Add(issue);
            }
        }
    }
}
