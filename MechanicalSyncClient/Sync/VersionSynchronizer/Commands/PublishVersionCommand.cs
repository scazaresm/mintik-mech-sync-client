using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.UI.Forms;
using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class PublishVersionCommand : VersionSynchronizerCommandAsync
    {
        public IVersionSynchronizer Synchronizer { get; private set; }

        public PublishVersionCommand(IVersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public async Task RunAsync()
        {
            string versionId = Synchronizer.Version.RemoteVersion.Id;
            string localDirectory = Synchronizer.Version.LocalDirectory;

            await Synchronizer.SyncServiceClient.PublishVersionAsync(new PublishVersionRequest()
            {
                VersionId = versionId
            });

            var progressDialog = new PublishVersionProgressDialog(Synchronizer, versionId, localDirectory);
            progressDialog.ShowDialog();

            if(progressDialog.IsPublishingSuccess)
                await new CloseVersionCommand(Synchronizer).RunAsync();
        }
    }
}
