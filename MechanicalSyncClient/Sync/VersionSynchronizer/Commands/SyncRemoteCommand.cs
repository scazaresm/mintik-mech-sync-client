using MechanicalSyncApp.Core;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class SyncRemoteCommand : VersionSynchronizerCommandAsync
    {
        public VersionSynchronizer Synchronizer { get; private set; }

        public SyncRemoteCommand(VersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public async Task RunAsync()
        {
            try
            {
                Synchronizer.ChangeMonitor.StopMonitoring();

                Synchronizer.SetState(new IdleState());
                await Synchronizer.RunStepAsync();

                Synchronizer.SetState(new IndexLocalFiles());
                await Synchronizer.RunStepAsync();

                Synchronizer.SetState(new IndexRemoteFilesState());
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

                Synchronizer.SetState(new MonitorFileSyncEventsState());
                await Synchronizer.RunStepAsync();

                MessageBox.Show(
                    "The remote server is already synced with your local copy.",
                    "Synced remote",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // always go back to idle state
                Synchronizer.SetState(new IdleState());
                await Synchronizer.RunStepAsync();
            }
        }
    }
}
