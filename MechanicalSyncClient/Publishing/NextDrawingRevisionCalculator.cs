using MechanicalSyncApp.Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher
{
    public class NextDrawingRevisionCalculator : INextDrawingRevisionCalculator
    {
        private readonly string relativePublishingDirectory;
        private readonly ILogger logger;

        public bool UseInitialRevisionSuffix { get; private set; } = false;

        public string BasePublishingDirectory { get; set; } = @"Z:\MANUFACTURING\";

        public RegexOptions RegexOptions { get; set; } = RegexOptions.IgnoreCase;

        public string PublishedFileExtension { get; set; } = ".pdf";

        public NextDrawingRevisionCalculator(string relativePublishingDirectory, ILogger logger)
        {
            this.relativePublishingDirectory = relativePublishingDirectory ?? throw new ArgumentNullException(nameof(relativePublishingDirectory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string GetNextRevision(string drawingFilename)
        {
            if (drawingFilename is null)
                throw new ArgumentNullException($"Argument cannot be null ({nameof(drawingFilename)})");

            logger.Debug($"Calculating next revision for drawing {drawingFilename}...");

            if (!Directory.Exists(BasePublishingDirectory))
                throw new DirectoryNotFoundException(
                    $"Could not determine a publishing location because the base directory does not exist at '{BasePublishingDirectory}'"
                );

            var publishingLocation = Path.Combine(BasePublishingDirectory, relativePublishingDirectory);

            if (!Directory.Exists(publishingLocation))
                Directory.CreateDirectory(publishingLocation);

            var allExistingPublishings = Directory.EnumerateFiles(publishingLocation).Where(file =>
                Regex.IsMatch(Path.GetFileName(file), $@"^{drawingFilename}.*\{PublishedFileExtension}", RegexOptions)
            );

            var nextRevision = "";

            if (allExistingPublishings.Count() == 0)
            {
                nextRevision = "A"; // no publishings yet, so this is initial revision
            }
            else
            {
                var publishingsWithRevisionSuffix = Directory.EnumerateFiles(publishingLocation).Where(file =>
                    Regex.IsMatch(Path.GetFileName(file), $@"^{drawingFilename}-.*\{PublishedFileExtension}", RegexOptions)
                );

                var numericRevision = !UseInitialRevisionSuffix
                    ? publishingsWithRevisionSuffix.Count() + 2  // existing revisions + initial revision (without suffix) + next revision 
                    : publishingsWithRevisionSuffix.Count() + 1; // existing revisions + next revision

                nextRevision = GetRevisionString(numericRevision);
            }
            logger.Debug($"Next revision for drawing {drawingFilename} should be '{nextRevision}'.");
            return nextRevision;
        }

        private string GetRevisionString(int revisionNumber)
        {
            string result = string.Empty;
            while (revisionNumber > 0)
            {
                int remainder = (revisionNumber - 1) % 26;  // 26 chars in alphabet
                result = (char)('A' + remainder) + result;
                revisionNumber = (revisionNumber - 1) / 26;
            }
            return result;
        }
    }
}
