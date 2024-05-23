using MechanicalSyncApp.Core.SolidWorksInterop.API;
using Serilog;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop
{
    public class SolidWorksModelExporter : ISolidWorksModelExporter
    {
        private readonly ISolidWorksModelLoader modelLoader;
        private readonly ILogger logger;

        public SolidWorksModelExporter(ISolidWorksStarter solidWorksStarter, ILogger logger)
        {
            if (solidWorksStarter is null)
            {
                throw new ArgumentNullException(nameof(solidWorksStarter));
            }
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            modelLoader = new SolidWorksModelLoader(solidWorksStarter, logger);
        }

        public async Task ExportModelAsync(string sourceFile, string[] outputFiles)
        {
            await Task.Run(() => ExportModel(sourceFile, outputFiles));
        }

        public async Task ExportModelAsync(string sourceFile, string outputFile)
        {
            await Task.Run(() => ExportModel(sourceFile, outputFile));
        }

        public void ExportModel(string sourceFile, string[] outputFiles)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));

            if (!File.Exists(sourceFile))
                throw new FileNotFoundException(nameof(sourceFile));

            if (outputFiles is null)
                throw new ArgumentNullException(nameof(outputFiles));

            ModelDoc2 model = null;
            try
            {
                model = modelLoader.LoadModel(sourceFile);

                foreach (var outputFilePath in outputFiles)
                {
                    logger.Debug($"Exporting {sourceFile} into {outputFilePath}...");

                    var outputDirectory = Path.GetDirectoryName(outputFilePath);

                    if (!Directory.Exists(outputDirectory))
                        Directory.CreateDirectory(outputDirectory);

                    int errs = -1;
                    int warns = -1;

                    bool success = model.Extension.SaveAs(outputFilePath, (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
                       (int)swSaveAsOptions_e.swSaveAsOptions_Silent, null, ref errs, ref warns);

                    if (!success)
                        throw new Exception(
                            $"Failed to export {sourceFile} into {outputFilePath}: " + ParseSaveError((swFileSaveError_e)errs)
                        );

                    logger.Debug($"Successfully exported {sourceFile} into {outputFilePath}.");
                }
            }
            finally
            {
                modelLoader.UnloadModel(model);
            }
        }

        private static string ParseSaveError(swFileSaveError_e err)
        {
            var errors = new List<string>();

            if (err.HasFlag(swFileSaveError_e.swFileLockError))
                errors.Add("File lock error");

            if (err.HasFlag(swFileSaveError_e.swFileNameContainsAtSign))
                errors.Add("File name cannot contain the at symbol(@)");

            if (err.HasFlag(swFileSaveError_e.swFileNameEmpty))
                errors.Add("File name cannot be empty");

            if (err.HasFlag(swFileSaveError_e.swFileSaveAsBadEDrawingsVersion))
                errors.Add("Bad eDrawings data");

            if (err.HasFlag(swFileSaveError_e.swFileSaveAsDoNotOverwrite))
                errors.Add("Cannot overwrite an existing file");

            if (err.HasFlag(swFileSaveError_e.swFileSaveAsInvalidFileExtension))
                errors.Add("File name extension does not match the SOLIDWORKS document type");

            if (err.HasFlag(swFileSaveError_e.swFileSaveAsNameExceedsMaxPathLength))
                errors.Add("File name cannot exceed 255 characters");

            if (err.HasFlag(swFileSaveError_e.swFileSaveAsNotSupported))
                errors.Add("Save As operation is not supported in this environment");

            if (err.HasFlag(swFileSaveError_e.swFileSaveFormatNotAvailable))
                errors.Add("Save As file type is not valid");

            if (err.HasFlag(swFileSaveError_e.swFileSaveRequiresSavingReferences))
                errors.Add("Saving an assembly with renamed components requires saving the references");

            if (err.HasFlag(swFileSaveError_e.swGenericSaveError))
                errors.Add("Generic error");

            if (err.HasFlag(swFileSaveError_e.swReadOnlySaveError))
                errors.Add("File is readonly");

            if (errors.Count == 0)
                errors.Add("Unknown error");

            return string.Join("; ", errors);
        }

        public void ExportModel(string sourceFile, string outputFile)
        {
            ExportModel(sourceFile, new string[] { outputFile });
        }
    }
}
