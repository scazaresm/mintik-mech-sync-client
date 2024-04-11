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

    public class ManufacturingMetadata
    {
        // Drawing file name without extension
        public string DrawingName { get; set; }

        // Revision string as set in the SolidWorks revision table (annotations) within the drawing
        public string DrawingRevision { get; set; } = string.Empty;
        
        // Custom properties from referenced model, either Part or Assembly
        public List<CustomProperty> CustomProperties { get; set; } = new List<CustomProperty>();
    }
}
