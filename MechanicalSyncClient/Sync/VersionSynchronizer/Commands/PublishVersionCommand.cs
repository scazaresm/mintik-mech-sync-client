using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.UI.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class PublishVersionCommand : VersionSynchronizerCommandAsync
    {
        public IVersionSynchronizer Synchronizer { get; private set; }

        private IMechSyncServiceClient client;

        public PublishVersionCommand(IVersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            client = MechSyncServiceClient.Instance;
        }

        public async Task RunAsync()
        {
            string publishJobId = Synchronizer.Version.RemoteVersion.PublishJobId;

            // if no existing publish job for this version, then create a new one
            if (publishJobId == null)
            {
                // show verification list and ask for confirmation
                var confirmationDialog = new PublishVersionConfirmationDialog(Synchronizer);
                var confirmation = confirmationDialog.ShowDialog();
                if (confirmation != DialogResult.OK) return;

                // create the new publish job and use the id
                var createdJob = await client.CreatePublishJobAsync(
                    new CreatePublishJobRequest()
                    {
                        VersionId = Synchronizer.Version.RemoteVersion.Id
                    }
                );
                publishJobId = createdJob.Id;
                Synchronizer.Version.RemoteVersion.PublishJobId = createdJob.Id;
            }

            var progressDialog = new PublishVersionProgressDialog(Synchronizer);
            var response = progressDialog.ShowDialog();
            if (response != DialogResult.OK) return;

            await new CloseVersionCommand(Synchronizer).RunAsync();
        }
    }
}
