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

        private IMechSyncServiceClient client;

        public PublishVersionCommand(IVersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            client = MechSyncServiceClient.Instance;
        }

        public async Task RunAsync()
        {
            string publishingJobId = Synchronizer.Version.RemoteVersion.PublishingJobId;

            // if there is no existing publishing job for this version, then create a new one
            if (publishingJobId == null)
            {
                // show verification list and ask for confirmation
                var verificationDialog = new PublishVersionVerificationDialog(Synchronizer);
                var verification = verificationDialog.ShowDialog();
                if (verification != DialogResult.OK) return;

                // create the new publishing job and use the id
                var createdJob = await client.PublishVersionAsync(
                    new PublishVersionRequest()
                    {
                        VersionId = Synchronizer.Version.RemoteVersion.Id
                    }
                );
                publishingJobId = createdJob.Id;
                Synchronizer.Version.RemoteVersion.PublishingJobId = createdJob.Id;
            }

            // show publishing progress
            var progressDialog = new PublishVersionProgressDialog(publishingJobId);
            progressDialog.ShowDialog();
            if (!progressDialog.IsPublishingSuccess) return;

            // publish job has succeeded, move local copy to recycle bin so the designer
            // cannot work on this version anymore
            if (Directory.Exists(Synchronizer.Version.LocalDirectory))
            {
                await Task.Run(() => FileSystem.DeleteDirectory(
                    Synchronizer.Version.LocalDirectory,
                    UIOption.OnlyErrorDialogs,
                    RecycleOption.SendToRecycleBin
                ));
            }

            // close this version
            await new CloseVersionCommand(Synchronizer).RunAsync();
        }
    }
}
