﻿using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
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
        public IVersionSynchronizer Synchronizer { get; private set; }

        public SyncCheckSummary Summary { get; set; }

        public bool ConfirmBeforeSync { get; set; } = true;
        public bool NotifyWhenComplete { get; set; } = true;

        public SyncRemoteCommand(IVersionSynchronizer synchronizer)
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

                Synchronizer.SetState(new IndexRemoteFilesState());
                await Synchronizer.RunStepAsync();

                Synchronizer.SetState(new IndexLocalFiles());
                await Synchronizer.RunStepAsync();

                var syncCheckState = new SyncCheckState();
                Synchronizer.SetState(syncCheckState);
                await Synchronizer.RunStepAsync();

                Summary = syncCheckState.Summary;

                if (Summary.HasChanges)
                {
                    if(ConfirmBeforeSync)
                    {
                        var response = MessageBox.Show(
                            "Apply sync changes?", "Validate changes", 
                            MessageBoxButtons.YesNo, 
                            MessageBoxIcon.Question
                        );

                        if (response != DialogResult.Yes)
                        {
                            await Synchronizer.WorkOfflineAsync();
                            return;
                        }
                    }
                    Synchronizer.SetState(new ProcessSyncCheckSummaryState(syncCheckState.Summary));
                    await Synchronizer.RunStepAsync();
                }

                Synchronizer.SetState(new MonitorFileSyncEventsState());
                await Synchronizer.RunStepAsync();

                if(NotifyWhenComplete)
                {
                    MessageBox.Show(
                        "The remote server is already synced with your local copy.",
                        "Synced remote",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
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
