using SolidWorks.Interop.sldworks;
using System;
using System.Threading.Tasks;
using Serilog;

namespace MechanicalSyncApp.Core.SolidWorksInterop
{
    public class DrawingRevisionRetriever : IDrawingRevisionRetriever
    {
        private readonly ISolidWorksModelLoader modelLoader;
        private readonly ILogger logger;

        public DrawingRevisionRetriever(ISolidWorksStarter solidWorksStarter, ILogger logger)
        {
            if (solidWorksStarter is null)
            {
                throw new ArgumentNullException(nameof(solidWorksStarter));
            }
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            modelLoader = new SolidWorksModelLoader(solidWorksStarter, logger);
        }

        public async Task<string> GetRevisionAsync(string drawingPath)
        {
            return await Task.Run(() => GetRevision(drawingPath));
        }

        public string GetRevision(string drawingPath)
        {
            ModelDoc2 model = null;
            try
            {
                model = modelLoader.LoadModel(drawingPath);

                logger.Debug($"Retrieving drawing revision from {model.GetTitle()}...");
                DrawingDoc drawing = (DrawingDoc)model;
                Sheet sheet = (Sheet)drawing.GetCurrentSheet();
                if (sheet == null)
                {
                    logger.Warning("No active sheet found in the drawing, returning empty string.");
                    return "";
                }

                var revisionTable = sheet.RevisionTable;
                if (revisionTable == null)
                {
                    logger.Warning("Could not get revision table from model, returning empty string.");
                    return "";
                }
                logger.Debug($"Drawing revision is {revisionTable.CurrentRevision}");

                return revisionTable.CurrentRevision;
            }
            finally
            {
                modelLoader.UnloadModel(model);
            }
        }
    }
}
