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
        private readonly ILogger logger;

        private readonly string PART_DOCUMENT_EXTENSION = ".SLDPRT";

        private readonly string MANDATORY_CUSTOM_PROPERTIES = "ASSEMBLY|DESCRIPTION|QTY";

        public CustomPropertiesValidationStrategy(
                IModelPropertiesRetriever modelPropertiesRetriever,
                ILogger logger
            )
        {
            this.modelPropertiesRetriever = modelPropertiesRetriever ?? throw new ArgumentNullException(nameof(modelPropertiesRetriever));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ValidateAsync(FileMetadata drawing)
        {
            var partDocumentPath = GetPartDocumentPath(drawing);

            if (!File.Exists(partDocumentPath))
                throw new FileNotFoundException($"Part document not found {partDocumentPath}");

            var customProperties = await modelPropertiesRetriever.GetAllCustomPropertyValuesAsync(partDocumentPath);
            var mandatoryCustomProperties = GetMandatoryCustomPropertiesList();

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

        private string GetPartDocumentPath(FileMetadata drawing)
        {
            var drawingExtension = Path.GetExtension(drawing.FullFilePath);
            return drawing.FullFilePath.Replace(drawingExtension, PART_DOCUMENT_EXTENSION);
        }

        private List<string> GetMandatoryCustomPropertiesList()
        {
            return MANDATORY_CUSTOM_PROPERTIES.Split('|').ToList();
        }
    }
}
