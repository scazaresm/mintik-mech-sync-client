using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Domain
{
    public enum PublishingType
    {
        New,
        Aggregated,
        Rework
    }

    public enum PublishingChangeReason 
    {
        Design,
        Customer
    }

    public class PublishingSummary
    {
        public string DesignerEmail { get; set; }
        public string ProjectName { get; set; }
        public string RelativeProjectDirectory { get; set; }
        public ManufacturingMetadata ManufacturingMetadata { get; set; }
        public List<string> RelativeFilePaths { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
    }
}
