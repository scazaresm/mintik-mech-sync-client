using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.SolidWorksInterop.API;

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
        public LocalVersion Version { get; set; }
    }
}
