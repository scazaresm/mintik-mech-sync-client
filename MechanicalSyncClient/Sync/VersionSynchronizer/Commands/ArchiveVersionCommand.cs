using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class ArchiveVersionCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer { get; set; }

        public ArchiveVersionCommand(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            string versionId = Synchronizer.Version.RemoteVersion.Id;

            string localDirectory = Synchronizer.Version.LocalDirectory;

            await Synchronizer.SyncServiceClient.ArchiveVersionAsync(new ArchiveVersionRequest()
            {
                VersionId = versionId
            });

            var progressDialog = new ArchiveVersionProgressDialog(Synchronizer, versionId, localDirectory);
            progressDialog.ShowDialog();

            if (progressDialog.IsArchivingSuccess)
                await new CloseVersionCommand(Synchronizer, logger).RunAsync();
        }
    }
}
