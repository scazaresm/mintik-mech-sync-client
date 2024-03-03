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
    public class AssemblyPartListRetriever : IAssemblyPartListRetriever
    {
        private readonly ISolidWorksModelLoader modelLoader;
        private readonly ILogger logger;

        public string PathsRelativeTo { get; set; } = string.Empty;

        public AssemblyPartListRetriever(ISolidWorksStarter solidWorksStarter, ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            modelLoader = new SolidWorksModelLoader(solidWorksStarter, logger);
        }

        public async Task<HashSet<string>> ExtractDistinctPartListAsync(string assemblyFilePath)
        {
            return await Task.Run(() => ExtractDistinctPartList(assemblyFilePath));
        }

        public HashSet<string> ExtractDistinctPartList(string assemblyFilePath)
        {
            ModelDoc2 model = null;
            var parts = new HashSet<string>();
            try
            {
                model = modelLoader.LoadModel(assemblyFilePath);

                if (model.GetType() != (int)swDocumentTypes_e.swDocASSEMBLY)
                    throw new Exception("Model is not an assembly!");

                logger.Debug($"Extracting part list from {model.GetTitle()}...");
                var assembly = (AssemblyDoc)model;
                var assemblyComponents = assembly.GetComponents(false);

                foreach (Component2 component in assemblyComponents)
                {
                    var componentType = component.GetSuppression();
                    if (componentType == (int)swComponentSuppressionState_e.swComponentSuppressed)
                        continue; // skip suppressed

                    var componentFilePath = component.GetPathName();
                    if (componentFilePath.ToLower().EndsWith(".sldprt"))
                    {
                        var partFilePath = componentFilePath;

                        // optionally cut the part file path to make it relative to a given base path
                        if (PathsRelativeTo != string.Empty && Directory.Exists(PathsRelativeTo))
                            partFilePath = partFilePath.Replace(PathsRelativeTo, "");
                        
                        parts.Add(partFilePath);
                    }
                }
                logger.Debug($"Successufully extracted file paths for {parts.Count} parts linked to assembly {model.GetTitle()}.");
            }
            finally
            {
                if (model != null)
                    modelLoader.UnloadModel(model);
            }
            return parts;
        }
    }
}
