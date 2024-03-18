using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
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
        private readonly ILogger logger;

        private readonly string PublishDrawingsToFormats = ".PDF|.DWG";
        private readonly string PublishPartsToFormats = ".STEP";
        private readonly string PublishAssembliesToFormats = "";

        public string RevisionSuffix { get; private set; }
        public string FullProjectPublishingDirectory { get; private set; }
        public string RelativeProjectPublishingDirectory { get; private set; }
        public List<string> Deliverables { get; private set; } = new List<string>();

        public PublishDeliverablesStrategy(
                ISolidWorksModelExporter modelExporter,
                string fullProjectPublishingDirectory,
                string relativeProjectPublishingDirectory,
                ILogger logger
            )
        {
            this.modelExporter = modelExporter ?? throw new ArgumentNullException(nameof(modelExporter));
            FullProjectPublishingDirectory = fullProjectPublishingDirectory ?? throw new ArgumentNullException(nameof(fullProjectPublishingDirectory));
            RelativeProjectPublishingDirectory = relativeProjectPublishingDirectory ?? throw new ArgumentNullException(nameof(relativeProjectPublishingDirectory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishAsync(FileMetadata validDrawing)
        {
            if (!Directory.Exists(FullProjectPublishingDirectory))
                throw new DirectoryNotFoundException($"The project publishing directory does not exist at {FullProjectPublishingDirectory}");

            if (!File.Exists(validDrawing.FullFilePath))
                throw new FileNotFoundException($"The drawing does not exist at {validDrawing.FullFilePath}");

            RevisionSuffix = BuildRevisionSuffix(validDrawing.Revision);

            Deliverables.Clear();
            await PublishDrawingDeliverablesAsync(validDrawing);

            // export part or assembly deliverables
            var drawingExtension = Path.GetExtension(validDrawing.FullFilePath);
            var partFilePath = validDrawing.FullFilePath.Replace(drawingExtension, ".SLDPRT");
            var assemblyFilePath = validDrawing.FullFilePath.Replace(drawingExtension, ".SLDASM");

            if (File.Exists(partFilePath))
            {
                await PublishPartDeliverablesAsync(validDrawing, partFilePath);
            }
            else if (File.Exists(assemblyFilePath))
            {
                await PublishAssemblyDeliverablesAsync(validDrawing, assemblyFilePath);
            }
            else
            {
                throw new FileNotFoundException(
                    $"Could not find a part or assembly associated to drawing {validDrawing.FullFilePath}."
                );
            }
        }

        private async Task PublishDrawingDeliverablesAsync(FileMetadata validDrawing)
        {
            var drawingExtension = Path.GetExtension(validDrawing.FullFilePath);
            var drawingFileNameWithoutExtension = Path.GetFileNameWithoutExtension(validDrawing.FullFilePath);

            var expectedDrawingOutputFiles = PublishDrawingsToFormats
                .Split('|')
                .Select((formatExtension) =>
                    Path.Combine(
                        FullProjectPublishingDirectory,
                        formatExtension.Replace(".", ""),
                        $"{drawingFileNameWithoutExtension}{RevisionSuffix}{formatExtension}"
                    )
                );

            if (expectedDrawingOutputFiles.Count() == 0) return;

            await modelExporter.ExportModelAsync(validDrawing.FullFilePath, expectedDrawingOutputFiles.ToArray());

            bool allFilesExported = expectedDrawingOutputFiles.Where(
                (file) => File.Exists(file)
            ).Count() == expectedDrawingOutputFiles.Count();

            if (!allFilesExported)
                throw new Exception($"Could not publish all expected output formats for drawing {validDrawing.FullFilePath}");

            Deliverables.AddRange(expectedDrawingOutputFiles);
        }

        private async Task PublishPartDeliverablesAsync(FileMetadata validDrawing, string partFilePath)
        {
            var drawingFileNameWithoutExtension = Path.GetFileNameWithoutExtension(validDrawing.FullFilePath);

            var expectedPartOutputFiles = PublishPartsToFormats
                .Split('|')
                .Select((formatExtension) =>
                    Path.Combine(
                        FullProjectPublishingDirectory,
                        formatExtension.Replace(".", ""),
                        $"{drawingFileNameWithoutExtension}{RevisionSuffix}{formatExtension}"
                    )
                );

            if (expectedPartOutputFiles.Count() == 0) return;

            await modelExporter.ExportModelAsync(partFilePath, expectedPartOutputFiles.ToArray());

            bool allFilesExported = expectedPartOutputFiles.Where(
                (file) => File.Exists(file)
            ).Count() == expectedPartOutputFiles.Count();

            if (!allFilesExported)
                throw new Exception($"Could not publish all expected output formats for part {partFilePath}");

            Deliverables.AddRange(expectedPartOutputFiles);
        }

        private async Task PublishAssemblyDeliverablesAsync(FileMetadata validDrawing, string assemblyFilePath)
        {
            var drawingFileNameWithoutExtension = Path.GetFileNameWithoutExtension(validDrawing.FullFilePath);

            var expectedAssemblyOutputFiles = PublishAssembliesToFormats
                .Split('|')
                .Select((formatExtension) =>
                    Path.Combine(
                        FullProjectPublishingDirectory,
                        formatExtension.Replace(".", ""),
                        $"{drawingFileNameWithoutExtension}{RevisionSuffix}{formatExtension}"
                    )
                );

            if (expectedAssemblyOutputFiles.Count() == 0) return;

            await modelExporter.ExportModelAsync(assemblyFilePath, expectedAssemblyOutputFiles.ToArray());

            bool allFilesExported = expectedAssemblyOutputFiles.Where(
                (file) => File.Exists(file)
            ).Count() == expectedAssemblyOutputFiles.Count();

            if (!allFilesExported)
                throw new Exception($"Could not publish all expected output formats for assembly {assemblyFilePath}");

            Deliverables.AddRange(expectedAssemblyOutputFiles);
        }

        private string BuildRevisionSuffix(string revision)
        {
            return revision == "A" ? string.Empty : $"-{revision}";
        }
    }
}
