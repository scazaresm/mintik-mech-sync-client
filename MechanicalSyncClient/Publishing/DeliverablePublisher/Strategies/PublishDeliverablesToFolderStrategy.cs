using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Args;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var fileChecksum = new Sha256FileChecksumCalculator().CalculateChecksum(validDrawing.FullFilePath);
            if (fileChecksum != validDrawing.FileChecksum)
                throw new InvalidOperationException("Detected file modifications since the last file validation, run validation and try again.");

            RevisionSuffix = BuildRevisionSuffix(validDrawing.Revision);

            deliverables.Clear();
            await PublishModelDeliverablesAsync(validDrawing.FullFilePath, PublishDrawingsToFormats);

            // export part or assembly deliverables
            var drawingExtension = Path.GetExtension(validDrawing.FullFilePath);
            var partFilePath = validDrawing.FullFilePath.Replace(drawingExtension, ".SLDPRT");
            var assemblyFilePath = validDrawing.FullFilePath.Replace(drawingExtension, ".SLDASM");

            if (File.Exists(partFilePath))
            {
                await PublishModelDeliverablesAsync(partFilePath, PublishPartsToFormats);
            }
            else if (File.Exists(assemblyFilePath))
            {
                await PublishModelDeliverablesAsync(assemblyFilePath, PublishAssembliesToFormats);
            }
            else
            {
                throw new FileNotFoundException(
                    $"Could not find a part or assembly associated to drawing {validDrawing.FullFilePath}."
                );
            }
            var publishing = await InsertFilePublishingDocument(validDrawing);
            WritePublishingSummaryAsJsonFile(publishing, validDrawing, args.SummaryFileDirectory);
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

            if (args.Version == null)
            {
                throw new ArgumentNullException(nameof(args.Version), "Version cannot be null.");
            }

            if (string.IsNullOrEmpty(args.SummaryFileDirectory))
            {
                throw new ArgumentNullException(nameof(args.SummaryFileDirectory), "SummaryFileDirectory cannot be null or empty.");
            }
        }

        private async Task PublishModelDeliverablesAsync(string modelFilePath, string outputFormats)
        {
            if (outputFormats.Trim() == string.Empty)
                return; // nothing to publish

            var partNumber = Path.GetFileNameWithoutExtension(modelFilePath);

            // publish to a temporary directory
            var tempPublishingDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            // delete the temporary directory if already exists
            if (Directory.Exists(tempPublishingDirectory))
                DirectoryUtils.SafeDeleteTempDirectory(tempPublishingDirectory);

            // create a new temporary directory
            Directory.CreateDirectory(tempPublishingDirectory);

            // determine all temporary file names depending on the file formats that we need to export
            var tempOutputFiles = outputFormats
               .Split('|')
               .Select((formatExtension) =>
                   Path.Combine(
                       tempPublishingDirectory,
                       formatExtension.Replace(".", ""),
                       $"{partNumber}{RevisionSuffix}{formatExtension}"
                   )
               );

            if (tempOutputFiles.Count() == 0)
                return; // nothing to publish

            // determine the final output file paths based on temporary file paths, and associate them (temp & final)
            Dictionary<string, string> finalOutputFiles = new Dictionary<string, string>();
            foreach (string tempFile in tempOutputFiles)
            {
                finalOutputFiles.Add(
                    tempFile,
                    tempFile.Replace(tempPublishingDirectory, args.FullPublishingDirectory)
                );
            }

            try
            {
                // export all formats to temporary file paths
                await args.ModelExporter.ExportModelAsync(modelFilePath, tempOutputFiles.ToArray());

                // validate all temp files got exported
                bool allTempFilesExported = tempOutputFiles.Where(
                    (file) => File.Exists(file)
                ).Count() == tempOutputFiles.Count();

                if (!allTempFilesExported)
                    throw new Exception($"Could not publish all expected output formats for {modelFilePath}");

                // copy temp files to the final paths
                foreach (string tempFile in tempOutputFiles)
                {
                    var finalOutputFilePath = finalOutputFiles[tempFile];

                    Directory.CreateDirectory(Path.GetDirectoryName(finalOutputFilePath));
                    File.Copy(tempFile, finalOutputFilePath, true);
                }

                // validate all temp files got copied to the publishing directory
                bool allOutputFilesCopied = finalOutputFiles.Values.Where(
                    (file) => File.Exists(file)
                ).Count() == finalOutputFiles.Values.Count();

                if (!allOutputFilesCopied)
                    throw new Exception($"Could not copy all temp files to publishing directory for {modelFilePath}");

                deliverables.AddRange(finalOutputFiles.Values);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Cleanup temp publishing directory
                if (Directory.Exists(tempPublishingDirectory))
                    DirectoryUtils.SafeDeleteTempDirectory(tempPublishingDirectory);
            }
        }

        private string BuildRevisionSuffix(string revision)
        {
            return revision == "A" ? string.Empty : $"-{revision}";
        }

        private void WritePublishingSummaryAsJsonFile(
            FilePublishing publishing,
            FileMetadata validDrawing,
            string publishingSummaryDirectory)
        {
            if (!Directory.Exists(publishingSummaryDirectory))
                Directory.CreateDirectory(publishingSummaryDirectory);

            var relativeFilePaths = publishing.Deliverables.Select((filePath) =>
                filePath.Replace(args.FullPublishingDirectory, "")
            ).ToList();

            var publishingType = DeterminePublishingType(args.Version, validDrawing);

            var publishingSummary = new PublishingSummary()
            {
                DesignerEmail = args.DesignerEmail,
                ProjectName = args.Version.RemoteProject.FolderName,
                RelativeProjectDirectory = args.RelativePublishingDirectory,
                Reason = args.Version.RemoteVersion.Reason,
                Type = publishingType,
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

        public string DeterminePublishingType(LocalVersion version, FileMetadata validDrawing)
        {
            if (version.RemoteVersion.Major == 1 && validDrawing.Revision == "A")
                return "New";
            else if (version.RemoteVersion.Major > 1 && validDrawing.Revision == "A")
                return "Added";
            else if (validDrawing.Revision != "A")
                return "Rework";

            return "Unknown";
        }
    }
}
