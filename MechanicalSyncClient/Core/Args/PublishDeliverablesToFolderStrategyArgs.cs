using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Args
{
    public class PublishDeliverablesToFolderStrategyArgs
    {
        public IMechSyncServiceClient SyncServiceClient { get; set; }
        public ISolidWorksModelExporter ModelExporter { get; set; }
        public string FullPublishingDirectory { get; set; }
        public string RelativePublishingDirectory { get; set; }
        public string SummaryFileDirectory { get; set; }
        public string DesignerEmail { get; set; }
        public string ProjectName { get; set; }
    }
}
