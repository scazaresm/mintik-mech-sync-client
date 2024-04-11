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
        private readonly string projectPublishingDirectory;
        private readonly ILogger logger;

        public bool UseInitialRevisionSuffix { get; private set; } = false;

        public RegexOptions RegexOptions { get; set; } = RegexOptions.IgnoreCase;


        public NextDrawingRevisionCalculator(string projectPublishingDirectory, ILogger logger)
        {
            this.projectPublishingDirectory = projectPublishingDirectory ?? throw new ArgumentNullException(nameof(projectPublishingDirectory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string GetNextRevision(string drawingFileNameWithoutExtension)
        {
            if (drawingFileNameWithoutExtension is null)
                throw new ArgumentNullException($"Argument cannot be null ({nameof(drawingFileNameWithoutExtension)})");

            logger.Debug($"Calculating next revision for drawing {drawingFileNameWithoutExtension}...");

            if (!Directory.Exists(projectPublishingDirectory))
                Directory.CreateDirectory(projectPublishingDirectory);

            var pdfDirectory = Path.Combine(projectPublishingDirectory, "PDF");

            if (!Directory.Exists(pdfDirectory))
                Directory.CreateDirectory(pdfDirectory);

            var allExistingPublishings = Directory.EnumerateFiles(pdfDirectory).Where(file =>
                Regex.IsMatch(Path.GetFileName(file), $@"^{drawingFileNameWithoutExtension}.*\.pdf", RegexOptions)
            );

            var nextRevision = "";

            if (allExistingPublishings.Count() == 0)
            {
                nextRevision = "A"; // no publishings yet, so this is initial revision
            }
            else
            {
                var publishingsWithRevisionSuffix = Directory.EnumerateFiles(pdfDirectory).Where(file =>
                    Regex.IsMatch(Path.GetFileName(file), $@"^{drawingFileNameWithoutExtension}-.*\.pdf", RegexOptions)
                );

                var numericRevision = !UseInitialRevisionSuffix
                    ? publishingsWithRevisionSuffix.Count() + 2  // existing revisions + initial revision (without suffix) + next revision 
                    : publishingsWithRevisionSuffix.Count() + 1; // existing revisions + next revision

                nextRevision = GetRevisionString(numericRevision);
            }
            logger.Debug($"Next revision for drawing {drawingFileNameWithoutExtension} should be '{nextRevision}'.");
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
