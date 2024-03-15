using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Domain
{
    public enum ReferencedModelType
    {
        Part = 1,
        Assembly = 2
    }

    public class ManufacturingMetadata
    {
        // The file metadata for the source drawing file where all this data comes from
        [JsonIgnore]
        public FileMetadata DrawingFile { get; private set; }

        // Model type which is referenced by the drawing file, either Part or Assembly
        public ReferencedModelType ReferencedModelType { get; private set; }

        // Drawing file name without extension
        public string DrawingName { get; set; }

        // Revision string as set in the SolidWorks revision table (annotations) within the drawing
        public string DrawingRevision { get; set; } = string.Empty;
        
        // Custom properties from referenced model, either Part or Assembly
        public List<CustomProperty> CustomProperties { get; set; } = new List<CustomProperty>();

        public ManufacturingMetadata(FileMetadata drawingFile)
        {
            DrawingFile = drawingFile ?? throw new ArgumentNullException(nameof(drawingFile));
        }
    }
}
