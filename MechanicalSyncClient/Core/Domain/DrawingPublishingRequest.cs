using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Domain
{
    public class DrawingPublishingRequest
    {
        public string ProjectName { get; set; }
        public string RelativeProjectDirectory { get; set; }

        public ManufacturingMetadata ManufacturingMetadata { get; set; }

        public List<string> RelativeFilePaths { get; set; }
    }
}
