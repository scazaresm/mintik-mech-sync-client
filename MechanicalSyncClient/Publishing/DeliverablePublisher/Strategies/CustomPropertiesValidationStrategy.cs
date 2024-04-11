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
    public class CustomPropertiesValidationStrategy : ICustomPropertiesValidationStrategy
    {
        private readonly IModelPropertiesRetriever modelPropertiesRetriever;
        private readonly SyncGlobalConfig globalConfig;
        private readonly ILogger logger;

        private readonly string PART_DOCUMENT_EXTENSION = ".SLDPRT";
        private readonly string ASSY_DOCUMENT_EXTENSION = ".SLDASM";

        public CustomPropertiesValidationStrategy(
                IModelPropertiesRetriever modelPropertiesRetriever,
                SyncGlobalConfig globalConfig,
                ILogger logger
            )
        {
            this.modelPropertiesRetriever = modelPropertiesRetriever ?? throw new ArgumentNullException(nameof(modelPropertiesRetriever));
            this.globalConfig = globalConfig ?? throw new ArgumentNullException(nameof(globalConfig));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ValidateAsync(FileMetadata drawing)
        {
            var associatedModelPath = GetAssociatedModelPath(drawing);
            var associatedModelExtension = Path.GetExtension(associatedModelPath);

            var customProperties = await modelPropertiesRetriever.GetAllCustomPropertyValuesAsync(associatedModelPath);
            var mandatoryCustomProperties = GetMandatoryCustomPropertiesList(associatedModelExtension);

            // validate mandatory custom properties
            foreach(var mandatoryProperty in mandatoryCustomProperties)
            {
                // check that mandatory custom property exists and is not empty
                bool isValidProperty = customProperties.ContainsKey(mandatoryProperty) &&
                    customProperties[mandatoryProperty].Trim() != string.Empty;

                if (isValidProperty) continue;
  
                var issue = $"Mandatory custom property '{mandatoryProperty}' is missing or empty.";
                drawing.ValidationIssues.Add(issue);
                logger.Debug($"Issue encountered on drawing {Path.GetFileName(drawing.RelativeFilePath)}, {issue}");
            }

            // store the custom properties on the drawing
            drawing.CustomProperties.Clear();
            drawing.CustomProperties.AddRange(
                customProperties.Keys.Select((customPropertyName) =>
                    new CustomProperty()
                    {
                        Name = customPropertyName,
                        Value = customProperties[customPropertyName]
                    }
                )
            );
        }

        private string GetAssociatedModelPath(FileMetadata drawing)
        {
            var drawingExtension = Path.GetExtension(drawing.FullFilePath);

            var partFilePath = drawing.FullFilePath.Replace(drawingExtension, PART_DOCUMENT_EXTENSION);
            var assemblyFilePath = drawing.FullFilePath.Replace(drawingExtension, ASSY_DOCUMENT_EXTENSION);

            return File.Exists(partFilePath) ? partFilePath 
                : File.Exists(assemblyFilePath) ? assemblyFilePath 
                : throw new Exception($"Unable to find associated model file for drawing {drawing.FullFilePath}"); 
        }

        private List<string> GetMandatoryCustomPropertiesList(string associatedModelExtension)
        {
            string mandatoryCustomPropertiesConfig = "";

            if (associatedModelExtension.ToUpper() == PART_DOCUMENT_EXTENSION)
                mandatoryCustomPropertiesConfig = globalConfig.MandatoryPartCustomProperties;
            else if (associatedModelExtension.ToUpper() == ASSY_DOCUMENT_EXTENSION)
                mandatoryCustomPropertiesConfig = globalConfig.MandatoryAssyCustomProperties;

            return mandatoryCustomPropertiesConfig.Split('|').ToList();
        }
    }
}
