using MechanicalSyncApp.Core.SolidWorksInterop.API;
using Serilog;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop
{
    public enum SolidWorksDocumentType
    {
        Part = 1,
        Assembly = 2,
        Drawing = 3,
    }

    public class SolidWorksModelLoader : ISolidWorksModelLoader
    {
        private readonly ISolidWorksStarter solidWorksStarter;
        private readonly ILogger logger;


        public int OpenDocOptions { get; set; } = (int)swOpenDocOptions_e.swOpenDocOptions_RapidDraft;


        public SolidWorksModelLoader(ISolidWorksStarter solidWorksStarter, ILogger logger)
        {
            this.solidWorksStarter = solidWorksStarter ?? throw new ArgumentNullException(nameof(solidWorksStarter));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ModelDoc2> LoadModelAsync(string filePath)
        {
            return await Task.Run(() => LoadModel(filePath));
        }

        public ModelDoc2 LoadModel(string filePath)
        {
            var app = (solidWorksStarter as SolidWorksStarter).SolidWorksApp;

            if (app == null)
            {
                throw new Exception("SolidWorks starter does not have a valid app reference, please ask the starter to start SolidWorks first.");
            }

            if (filePath is null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(nameof(filePath));
            }

            int errors = 0;
            int warnings = 0;

            logger.Debug($"Loading model from {filePath}...");
            var model = app.OpenDoc6(
                filePath, GetModelType(filePath), OpenDocOptions, "", errors, warnings
            );
            logger.Debug($"Successfully loaded model {model.GetTitle()}.");

            return model ?? throw new ArgumentException($"Failed to open file. Errors {errors}, Warnings {warnings}");
        }

        public void UnloadModel(ModelDoc2 model)
        {
            if (model == null)
            {
                logger.Debug("Model object is null, nothing to unload.");
                return;
            }
            logger.Debug($"Unloading model {model.GetTitle()}...");
            (solidWorksStarter as SolidWorksStarter).SolidWorksApp.CloseDoc(model.GetTitle());
            logger.Debug($"Successfully unloaded model.");
        }

        private int GetModelType(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();

            var fileType =
                fileExtension.EndsWith(".sldprt") ? SolidWorksDocumentType.Part :
                fileExtension.EndsWith(".sldasm") ? SolidWorksDocumentType.Assembly :
                fileExtension.EndsWith(".slddrw") ? SolidWorksDocumentType.Drawing :
                    throw new ArgumentException("Unknown file type");

            return (int)fileType;
        }
    }
}
