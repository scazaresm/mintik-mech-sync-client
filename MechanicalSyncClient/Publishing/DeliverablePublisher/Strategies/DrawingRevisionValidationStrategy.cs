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

        private readonly string PART_DOCUMENT_EXTENSION = ".SLDPRT";
        private readonly string ASSY_DOCUMENT_EXTENSION = ".SLDASM";

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

            // drawing revision is not required for assembly drawings
            var associatedModelPath = GetAssociatedModelPath(drawing);

            if (Path.GetExtension(associatedModelPath).ToUpper() == ASSY_DOCUMENT_EXTENSION)
            {
                drawing.Revision = "A";
                logger.Debug($"Skipping drawing revision validation for assembly drawing {drawing.FullFilePath}");
                return;
            }

            var expectedRevision = drawingRevisionCalculator.GetNextRevision(drawingFileNameWithoutExtension);

            // store the current revision on the drawing
            drawing.Revision = await drawingRevisionRetriever.GetRevisionAsync(drawing.FullFilePath);

            // check that drawing revision is as expected
            if (expectedRevision != drawing.Revision)
            {
                var issue = drawing.Revision == string.Empty
                    ? "Revision not found."
                    : $"Incorrect revision: expected '{expectedRevision}' but found '{drawing.Revision}'.";

                drawing.ValidationIssues.Add(issue);

                logger.Debug($"Issue encountered on drawing {drawingFileName}, {issue}");
            }
        }

        private string GetAssociatedModelPath(FileMetadata drawing)
        {
            var drawingExtension = Path.GetExtension(drawing.FullFilePath);

            var partFilePath = drawing.FullFilePath.Replace(drawingExtension, PART_DOCUMENT_EXTENSION);
            var assemblyFilePath = drawing.FullFilePath.Replace(drawingExtension, ASSY_DOCUMENT_EXTENSION);

            return File.Exists(partFilePath) ? partFilePath
                : File.Exists(assemblyFilePath) ? assemblyFilePath
                : throw new Exception($"Unable to find associated model file for drawing {drawing.FullFilePath}");
        }
    }
}
