using MechanicalSyncApp.Core.SolidWorksInterop.API;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.SolidWorksInterop
{
    public class ModelPropertiesRetriever : IModelPropertiesRetriever
    {
        private readonly ISolidWorksModelLoader modelLoader;
        private readonly ILogger logger;

        public ModelPropertiesRetriever(ISolidWorksStarter solidWorksStarter, ILogger logger)
        {
            if (solidWorksStarter is null)
            {
                throw new ArgumentNullException(nameof(solidWorksStarter));
            }
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            modelLoader = new SolidWorksModelLoader(solidWorksStarter, logger);
        }

        public async Task<Dictionary<string, string>> GetAllCustomPropertyValuesAsync(string filePath)
        {
            return await Task.Run(() => GetAllCustomPropertyValues(filePath));
        }

        public Dictionary<string, string> GetAllCustomPropertyValues(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(nameof(filePath));

            var model = modelLoader.LoadModel(filePath);

            logger.Debug($"Getting custom properties from {filePath}...");
            var manager = model.Extension.CustomPropertyManager[""];
            var allPropertyNames = (string[])manager.GetNames();

            var properties = new Dictionary<string, string>();

            foreach (var propertyName in allPropertyNames)
            {
                manager.Get2(propertyName, out _, out string evaluatedValue);
                properties.Add(propertyName, evaluatedValue);
            }
            logger.Debug($"Successfully retrieved {properties.Count()} custom properties.");
            modelLoader.UnloadModel(model);
            return properties;
        }
    }
}
