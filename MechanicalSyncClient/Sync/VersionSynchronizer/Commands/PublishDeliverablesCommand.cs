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
            var ui = Synchronizer.UI;
            try
            {
                ui.SynchronizerToolStrip.Enabled = false;

                if (!Directory.Exists(Synchronizer.BasePublishingDirectory))
                    throw new Exception(
                        $"The base publishing directory could not be found at {Synchronizer.BasePublishingDirectory}, " +
                        $"if this is a shared folder location please make sure it is configured properly and try again."
                    );

                var syncRemoteCommand = new SyncRemoteCommand(Synchronizer, logger)
                {
                    NotifyWhenComplete = false,
                    EnableToolStripWhenComplete = false,
                };
                await syncRemoteCommand.RunAsync();

                if (!syncRemoteCommand.Complete)
                {
                    MessageBox.Show(
                        "Your local copy has changes and needs to be synced with the remote server before publishing deliverables, please try again.",
                        "Local copy has changes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                    return;
                }
                ui.SynchronizerToolStrip.Enabled = false;

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
                var message = ex.Message;
                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ui.SynchronizerToolStrip.Enabled = true;
            }
        }

        private async Task CheckWorkingCopySyncedAsync()
        {
            Synchronizer.SetState(new IndexRemoteFilesState(logger));
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new IndexPublishingsState(logger));
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new IndexLocalFilesState(logger));
            await Synchronizer.RunStepAsync();

            var syncCheckState = new SyncCheckState(logger) { RethrowException = true };
            Synchronizer.SetState(syncCheckState);
            await Synchronizer.RunStepAsync();

            Synchronizer.SetState(new SynchronizerIdleState(logger));
            await Synchronizer.RunStepAsync();

            Synchronizer.UI.SynchronizerToolStrip.Enabled = true;

            if (syncCheckState.Summary.HasChanges)
            {
                throw new InvalidOperationException(
                    "Your local copy has changes that need to be uploaded to server before publishing, please use the Sync remote button first."
                ); ;
            }
        }
    }
}
