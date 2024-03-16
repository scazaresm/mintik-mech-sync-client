using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
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
    public class PublishDeliverablesStrategy : IPublishDeliverablesStrategy
    {
        private readonly ISolidWorksModelExporter modelExporter;
        private readonly string projectPublishingDirectory;
        private readonly ILogger logger;

        private readonly string PublishDrawingsToFormats = ".PDF|.DWG";
        private readonly string PublishPartsToFormats = ".STEP";
        private readonly string PublishAssembliesToFormats = "";

        public PublishDeliverablesStrategy(
                ISolidWorksModelExporter modelExporter,
                string projectPublishingDirectory,
                ILogger logger
            )
        {
            this.modelExporter = modelExporter ?? throw new ArgumentNullException(nameof(modelExporter));
            this.projectPublishingDirectory = projectPublishingDirectory ?? throw new ArgumentNullException(nameof(projectPublishingDirectory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public async Task PublishAsync(FileMetadata validDrawing)
        {
            if (!Directory.Exists(projectPublishingDirectory))
                throw new DirectoryNotFoundException($"The project publishing directory does not exist at {projectPublishingDirectory}");

            if (!File.Exists(validDrawing.FullFilePath))
                throw new FileNotFoundException($"The drawing does not exist at {validDrawing.FullFilePath}");

            await PublishDrawingDeliverablesAsync(validDrawing);

            // export part or assembly deliverables
            var drawingExtension = Path.GetExtension(validDrawing.FullFilePath);
            var partFilePath = validDrawing.FullFilePath.Replace(drawingExtension, ".SLDPRT");
            var assemblyFilePath = validDrawing.FullFilePath.Replace(drawingExtension, ".SLDASM");

            if (File.Exists(partFilePath))
            {
                await PublishPartDeliverablesAsync(validDrawing);
            }
            else if (File.Exists(assemblyFilePath))
            {
                await PublishAssemblyDeliverablesAsync(validDrawing);
            }
            else
            {
                throw new FileNotFoundException(
                    $"Could not find a part or assembly associated to the drawing {validDrawing.FullFilePath}."
                );
            }
        }

        private async Task PublishDrawingDeliverablesAsync(FileMetadata validDrawing)
        {
            var drawingExtension = Path.GetExtension(validDrawing.FullFilePath);
            var drawingFileNameWithoutExtension = Path.GetFileNameWithoutExtension(validDrawing.FullFilePath);
            var revisionSuffix = GetRevisionSuffix(validDrawing.Revision);

            var drawingOutputFiles = PublishDrawingsToFormats
                .Split('|')
                .Select((formatExtension) =>
                    Path.Combine(
                        projectPublishingDirectory,
                        formatExtension.Replace(".", ""),
                        $"{drawingFileNameWithoutExtension}{revisionSuffix}{formatExtension}"
                    )
                );

            if (drawingOutputFiles.Count() == 0) return;

            await modelExporter.ExportModelAsync(validDrawing.FullFilePath, drawingOutputFiles.ToArray());

            bool allFilesExported = drawingOutputFiles.Where(
                (file) => File.Exists(file) == true
            ).Count() == drawingOutputFiles.Count();

            if (!allFilesExported)
                throw new Exception($"Could not publish all expected drawing output formats for {validDrawing.FullFilePath}");
        }

        private async Task PublishPartDeliverablesAsync(FileMetadata validDrawing)
        {

        }

        private async Task PublishAssemblyDeliverablesAsync(FileMetadata validDrawing)
        {

        }

        private string GetRevisionSuffix(string revision)
        {
            return revision == "A" ? string.Empty : $"-{revision}";
        }
    }
}
