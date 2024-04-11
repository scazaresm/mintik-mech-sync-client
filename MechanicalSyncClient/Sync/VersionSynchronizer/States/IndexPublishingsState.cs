using MechanicalSyncApp.Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.States
{
    public class IndexPublishingsState : VersionSynchronizerState
    {
        private readonly ILogger logger;

        public IndexPublishingsState(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task RunAsync()
        {
            var versionId = Synchronizer.Version.RemoteVersion.Id;
            var publishingIndexByPartNumber = Synchronizer.PublishingIndexByPartNumber;

            var publishings = await Synchronizer.SyncServiceClient.GetVersionFilePublishingsAsync(versionId);

            publishingIndexByPartNumber.Clear();
            foreach(var publishing in publishings) 
                publishingIndexByPartNumber.TryAdd(publishing.PartNumber, publishing);  
        }

        public override void UpdateUI()
        {
            Synchronizer.UI.StatusLabel.Text = "Indexing publishings...";
            Synchronizer.UI.SynchronizerToolStrip.Enabled = false;
        }
    }
}
