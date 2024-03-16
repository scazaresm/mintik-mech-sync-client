using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher.Strategies
{
    public class DrawingRevisionValidationStrategy : IDrawingRevisionValidationStrategy
    {
        private readonly INextDrawingRevisionCalculator drawingRevisionCalculator;
        private readonly IDrawingRevisionRetriever drawingRevisionRetriever;
        private readonly ILogger logger;

        public DrawingRevisionValidationStrategy(
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

            string drawingFileName = Path.GetFileName(drawing.RelativeFilePath);
            string drawingFileNameWithoutExtension = Path.GetFileNameWithoutExtension(drawing.FullFilePath);

            logger.Debug($"Validating revision on drawing {drawingFileName}...");

            var expectedRevision = drawingRevisionCalculator.GetNextRevision(drawingFileNameWithoutExtension);
            drawing.Revision = await drawingRevisionRetriever.GetRevisionAsync(drawing.FullFilePath);

            if (expectedRevision != drawing.Revision)
            {
                var issue = drawing.Revision == string.Empty
                    ? "Revision not found."
                    : $"Incorrect revision: expected '{expectedRevision}' but found '{drawing.Revision}'.";

                drawing.ValidationIssues.Add(issue);

                logger.Debug($"Issue encountered on drawing {drawingFileName}, {issue}");
            }
        }
    }
}
