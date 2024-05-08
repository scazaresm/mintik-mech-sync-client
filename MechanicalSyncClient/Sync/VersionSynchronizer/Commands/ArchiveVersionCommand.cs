using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class ArchiveVersionCommand : IVersionSynchronizerCommandAsync
    {
        private readonly ILogger logger;

        public IVersionSynchronizer Synchronizer { get; set; }

        public ArchiveVersionCommand(IVersionSynchronizer syncrhonizer, ILogger logger)
        {
            Synchronizer = syncrhonizer ?? throw new ArgumentNullException(nameof(syncrhonizer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            try
            {
                Synchronizer.UI.SynchronizerToolStrip.Enabled = false;

                string versionId = Synchronizer.Version.RemoteVersion.Id;

                string localDirectory = Synchronizer.Version.LocalDirectory;

                // show review summary
                var nextStepResult = new ReviewSummaryForm(Synchronizer, logger).ShowDialog(); ;

                if (nextStepResult != DialogResult.OK)
                    return;

                // archiving must be confirmed by user
                var confirmationResult = new ArchiveVersionWarningDialog().ShowDialog();

                if (confirmationResult != DialogResult.OK)
                    return;

                await Synchronizer.SyncServiceClient.ArchiveVersionAsync(new ArchiveVersionRequest()
                {
                    VersionId = versionId
                });

                var progressDialog = new ArchiveVersionProgressDialog(Synchronizer, versionId, localDirectory);
                progressDialog.ShowDialog();

                if (progressDialog.IsArchivingSuccess)
                    await new CloseVersionCommand(Synchronizer, logger).RunAsync();
            }
            catch (Exception ex) 
            {
                var message = $"Unable to archive version: {ex.Message}";
                logger.Error(message, ex);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Synchronizer.UI.SynchronizerToolStrip.Enabled = true;
            }
        }
    }
}
