using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Args;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using MechanicalSyncApp.Core.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher.Strategies
{
    public class PublishDeliverablesToFolderStrategy : IPublishDeliverablesStrategy
    {
        private readonly ILogger logger;

        private readonly string PublishDrawingsToFormats = ".PDF|.DWG";
        private readonly string PublishPartsToFormats = ".STEP";
        private readonly string PublishAssembliesToFormats = "";

        public string RevisionSuffix { get; private set; }

        private readonly PublishDeliverablesToFolderStrategyArgs args;

        private List<string> deliverables = new List<string>();

        public PublishDeliverablesToFolderStrategy(
                PublishDeliverablesToFolderStrategyArgs args,
                ILogger logger
            )
        {
            this.args = args ?? throw new ArgumentNullException(nameof(args));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            ValidateArgs();
        }

        public async Task<FilePublishing> PublishAsync(FileMetadata validDrawing)
        {
            if (!Directory.Exists(args.FullPublishingDirectory))
                throw new DirectoryNotFoundException($"The project publishing directory does not exist at {args.FullPublishingDirectory}");

            if (!File.Exists(validDrawing.FullFilePath))
                throw new FileNotFoundException($"The drawing does not exist at {validDrawing.FullFilePath}");

            RevisionSuffix = BuildRevisionSuffix(validDrawing.Revision);

            deliverables.Clear();
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
            var publishing = await InsertFilePublishingDocument(validDrawing);
            WritePublishingSummaryAsJsonFile(publishing, args.SummaryFileDirectory);
            return publishing;
        }

        private async Task<FilePublishing> InsertFilePublishingDocument(FileMetadata validDrawing)
        {
            return await args.SyncServiceClient.PublishFileAsync(new PublishFileRequest()
            {
                FileMetadataId = validDrawing.Id,
                Revision = validDrawing.Revision,
                Deliverables = deliverables,
                CustomProperties = validDrawing.CustomProperties,
            });
        }

        private void ValidateArgs()
        {
            if (args.SyncServiceClient == null)
            {
                throw new ArgumentNullException(nameof(args.SyncServiceClient), "SyncServiceClient cannot be null.");
            }

            if (args.ModelExporter == null)
            {
                throw new ArgumentNullException(nameof(args.ModelExporter), "ModelExporter cannot be null.");
            }

            if (string.IsNullOrEmpty(args.FullPublishingDirectory))
            {
                throw new ArgumentNullException(nameof(args.FullPublishingDirectory), "FullPublishingDirectory cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(args.RelativePublishingDirectory))
            {
                throw new ArgumentNullException(nameof(args.RelativePublishingDirectory), "RelativePublishingDirectory cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(args.DesignerEmail))
            {
                throw new ArgumentNullException(nameof(args.DesignerEmail), "DesignerEmail cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(args.ProjectName))
            {
                throw new ArgumentNullException(nameof(args.ProjectName), "ProjectName cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(args.SummaryFileDirectory))
            {
                throw new ArgumentNullException(nameof(args.SummaryFileDirectory), "SummaryFileDirectory cannot be null or empty.");
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
                        args.FullPublishingDirectory,
                        formatExtension.Replace(".", ""),
                        $"{drawingFileNameWithoutExtension}{RevisionSuffix}{formatExtension}"
                    )
                );

            if (expectedDrawingOutputFiles.Count() == 0) return;

            await args.ModelExporter.ExportModelAsync(validDrawing.FullFilePath, expectedDrawingOutputFiles.ToArray());

            bool allFilesExported = expectedDrawingOutputFiles.Where(
                (file) => File.Exists(file)
            ).Count() == expectedDrawingOutputFiles.Count();

            if (!allFilesExported)
                throw new Exception($"Could not publish all expected output formats for drawing {validDrawing.FullFilePath}");

            deliverables.AddRange(expectedDrawingOutputFiles);
        }

        private async Task PublishPartDeliverablesAsync(FileMetadata validDrawing, string partFilePath)
        {
            var drawingFileNameWithoutExtension = Path.GetFileNameWithoutExtension(validDrawing.FullFilePath);

            var expectedPartOutputFiles = PublishPartsToFormats
                .Split('|')
                .Select((formatExtension) =>
                    Path.Combine(
                        args.FullPublishingDirectory,
                        formatExtension.Replace(".", ""),
                        $"{drawingFileNameWithoutExtension}{RevisionSuffix}{formatExtension}"
                    )
                );

            if (expectedPartOutputFiles.Count() == 0) return;

            await args.ModelExporter.ExportModelAsync(partFilePath, expectedPartOutputFiles.ToArray());

            bool allFilesExported = expectedPartOutputFiles.Where(
                (file) => File.Exists(file)
            ).Count() == expectedPartOutputFiles.Count();

            if (!allFilesExported)
                throw new Exception($"Could not publish all expected output formats for part {partFilePath}");

            deliverables.AddRange(expectedPartOutputFiles);
        }

        private async Task PublishAssemblyDeliverablesAsync(FileMetadata validDrawing, string assemblyFilePath)
        {
            var drawingFileNameWithoutExtension = Path.GetFileNameWithoutExtension(validDrawing.FullFilePath);

            var expectedAssemblyOutputFiles = PublishAssembliesToFormats
                .Split('|')
                .Select((formatExtension) =>
                    Path.Combine(
                        args.FullPublishingDirectory,
                        formatExtension.Replace(".", ""),
                        $"{drawingFileNameWithoutExtension}{RevisionSuffix}{formatExtension}"
                    )
                );

            if (expectedAssemblyOutputFiles.Count() == 0) return;

            await args.ModelExporter.ExportModelAsync(assemblyFilePath, expectedAssemblyOutputFiles.ToArray());

            bool allFilesExported = expectedAssemblyOutputFiles.Where(
                (file) => File.Exists(file)
            ).Count() == expectedAssemblyOutputFiles.Count();

            if (!allFilesExported)
                throw new Exception($"Could not publish all expected output formats for assembly {assemblyFilePath}");

            deliverables.AddRange(expectedAssemblyOutputFiles);
        }

        private string BuildRevisionSuffix(string revision)
        {
            return revision == "A" ? string.Empty : $"-{revision}";
        }

        private void WritePublishingSummaryAsJsonFile(FilePublishing publishing, string publishingSummaryDirectory)
        {
            if (!Directory.Exists(publishingSummaryDirectory))
                Directory.CreateDirectory(publishingSummaryDirectory);

            var relativeFilePaths = publishing.Deliverables.Select((filePath) =>
                filePath.Replace(args.FullPublishingDirectory, "")
            ).ToList();

            var publishingSummary = new PublishingSummary()
            {
                DesignerEmail = args.DesignerEmail,
                ProjectName = args.ProjectName,
                RelativeProjectDirectory = args.RelativePublishingDirectory,
                ManufacturingMetadata = new ManufacturingMetadata()
                {
                    DrawingName = publishing.PartNumber,
                    DrawingRevision = publishing.Revision,
                    CustomProperties = publishing.CustomProperties,
                },
                RelativeFilePaths = relativeFilePaths
            };

            var publishingSummaryJson = JsonUtils.SerializeWithCamelCase(publishingSummary);
            var summaryFilePath = Path.Combine(
                publishingSummaryDirectory,
                $"{publishing.PartNumber}_{publishing.Id}.json"
            );
            File.WriteAllText(summaryFilePath, publishingSummaryJson);
        }

    }
}
