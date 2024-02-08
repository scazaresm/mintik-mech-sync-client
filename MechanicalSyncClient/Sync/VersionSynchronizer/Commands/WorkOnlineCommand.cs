using MechanicalSyncApp.Core;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class WorkOnlineCommand : IVersionSynchronizerCommandAsync
    {
        public IVersionSynchronizer Synchronizer { get; private set; }

        public WorkOnlineCommand(VersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public async Task RunAsync()
        {
            Log.Information("Starting WorkOnlineCommand...");

            var confirmation = MessageBox.Show(
                "Are you sure to start working online?", "Go online",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question
            );

            Log.Information($"\tAsking user to confirm before going online, answer: {confirmation}");

            if (confirmation != DialogResult.Yes) {
                Log.Information("\tGoing online has been aborted by user.");
                return;
            }

            var UI = Synchronizer.UI;
            try
            {
                Log.Information("Disabling tool strip buttons while working online...");

                UI.SynchronizerToolStrip.Enabled = false;
                UI.WorkOnlineButton.Visible = false;
                UI.WorkOfflineButton.Visible = true;
                UI.SyncRemoteButton.Visible = false;

                Synchronizer.SetState(new IndexRemoteFilesState());
                await Synchronizer.RunStepAsync();

                Synchronizer.SetState(new IndexLocalFiles());
                await Synchronizer.RunStepAsync();

                var syncCheckState = new SyncCheckState();
                Synchronizer.SetState(syncCheckState);
                await Synchronizer.RunStepAsync();

                if (syncCheckState.Summary.HasChanges)
                {
                    var response = MessageBox.Show("Apply sync changes?", "Validate changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (response != DialogResult.Yes)
                    {
                        await Synchronizer.WorkOfflineAsync();
                        return;
                    }
                    Synchronizer.SetState(new ProcessSyncCheckSummaryState(syncCheckState.Summary));
                    await Synchronizer.RunStepAsync();
                }

                Synchronizer.ChangeMonitor.StartMonitoring();
                UI.FileViewer.PopulateFiles();

                Synchronizer.SetState(new MonitorFileSyncEventsState());
                _ = Synchronizer.RunStepAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                await Synchronizer.WorkOfflineAsync();
            }
            Log.Information("Finished WorkOnlineCommand.");
        }
    }
}
