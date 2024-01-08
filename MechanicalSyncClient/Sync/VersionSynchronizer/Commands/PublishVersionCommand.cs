using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI.Forms;
using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class PublishVersionCommand : IVersionSynchronizerCommandAsync
    {
        public IVersionSynchronizer Synchronizer { get; private set; }

        public PublishVersionCommand(IVersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public async Task RunAsync()
        {
            Synchronizer.SetState(new IdleState());
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new IndexRemoteFilesState());
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new IndexLocalFiles());
            await Synchronizer.RunStepAsync();

            var syncCheckState = new SyncCheckState();
            Synchronizer.SetState(syncCheckState);
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new IdleState());
            await Synchronizer.RunStepAsync();

            if (syncCheckState.Summary.HasChanges)
            {
                MessageBox.Show(
                    "Your local copy has changes that need to be uploaded to server before publishing, please use the Sync remote button.",
                    "Unsynced changed", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return;
            }

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
