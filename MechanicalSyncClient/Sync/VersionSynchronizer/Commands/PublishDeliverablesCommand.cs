using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.SolidWorksInterop;
using MechanicalSyncApp.Sync.VersionSynchronizer.Exceptions;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using Serilog.Core;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class PublishDeliverablesCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer { get; private set; }

        public PublishDeliverablesCommand(IVersionSynchronizer synchronizer, ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            try
            {
                bool workingCopySynced = await CheckWorkingCopySyncedAsync();
                if (!workingCopySynced) return;

                using (var solidWorksStarter = new SolidWorksStarter(logger)
                {
                    SolidWorksExePath = @"C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS (2)\SLDWORKS.exe",
                    Hidden = true,
                    ShowSplash = false,
                    SolidWorksStartTimeoutSeconds = 60,
                })
                {
                    await solidWorksStarter.StartSolidWorksAsync();

                    var form = new DeliverablePublishingForm(Synchronizer, solidWorksStarter, logger);
                    form.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                var message = $"Publishing has failed: {ex}";
                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> CheckWorkingCopySyncedAsync()
        {
            Synchronizer.SetState(new SynchronizerIdleState(logger));
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new IndexRemoteFilesState(logger));
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new IndexLocalFiles(logger));
            await Synchronizer.RunStepAsync();

            var syncCheckState = new SyncCheckState(logger) { RethrowException = true };
            Synchronizer.SetState(syncCheckState);
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new SynchronizerIdleState(logger));
            await Synchronizer.RunStepAsync();

            if (syncCheckState.Summary.HasChanges)
            {
                logger.Debug("Local copy has changes that need to be uploaded to server before publishing, asked user to hit the Sync remote button first.");

                MessageBox.Show(
                    "Your local copy has changes that need to be uploaded to server before publishing, please use the Sync remote button first.",
                    "Unsynced changed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return false;
            }
            return true;
        }
    }
}
