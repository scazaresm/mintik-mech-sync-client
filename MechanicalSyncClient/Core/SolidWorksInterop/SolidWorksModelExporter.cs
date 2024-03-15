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
                    bool success = model.Extension.SaveAs(outputFilePath, (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
                       (int)swSaveAsOptions_e.swSaveAsOptions_Silent, null, 0, 0);

                    if (!success)
                        throw new Exception($"Failed to export {sourceFile} into {outputFilePath}, " +
                            "make sure that source file exists and that you are passing supported " +
                            "file extensions in the output files.");

                    logger.Debug($"Successfully exported {sourceFile} into {outputFilePath}.");
                }
            }
            finally
            {
                modelLoader.UnloadModel(model);
            }
        }

        public void ExportModel(string sourceFile, string outputFile)
        {
            ExportModel(sourceFile, new string[] { outputFile });
        }
    }
}
