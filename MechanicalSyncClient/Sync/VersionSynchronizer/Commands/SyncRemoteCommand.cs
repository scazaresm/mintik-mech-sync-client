﻿using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Sync.VersionSynchronizer.States;
using MechanicalSyncApp.UI.Forms;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.Commands
{
    public class SyncRemoteCommand : IVersionSynchronizerCommandAsync
    {
        public IVersionSynchronizer Synchronizer { get; private set; }

        public SyncCheckSummary Summary { get; set; }

        public bool NotifyWhenComplete { get; set; } = true;

        public SyncRemoteCommand(IVersionSynchronizer synchronizer)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
        }

        public async Task RunAsync()
        {
            Log.Debug($"Starting SyncRemoteCommand, versionId = {Synchronizer.Version.RemoteVersion.Id} ...");
            try
            {
                if (Synchronizer.Version.RemoteVersion.Status != "Ongoing")
                {
                    Log.Error($"Cannot sync changes because this version is not in Ongoing status.");

                    throw new InvalidOperationException(
                        "Cannot sync changes because this version is not in Ongoing status."
                    );
                }
                Synchronizer.ChangeMonitor.StopMonitoring();

                Synchronizer.SetState(new IndexRemoteFilesState());
                await Synchronizer.RunStepAsync();

                Synchronizer.SetState(new IndexLocalFiles());
                await Synchronizer.RunStepAsync();

                var syncCheckState = new SyncCheckState();
                Synchronizer.SetState(syncCheckState);
                await Synchronizer.RunStepAsync();

                Summary = syncCheckState.Summary;

                // something went wrong during sync check
                if (Summary.ExceptionObject != null) 
                    throw Summary.ExceptionObject;
                
                // sync check was ok, now sync the changes (if any)
                if (Summary.HasChanges)
                {
                    var response = new SyncCheckSummaryForm(Synchronizer, Summary).ShowDialog();

                    if (response != DialogResult.OK)
                    {
                        await Synchronizer.WorkOfflineAsync();
                        return;
                    }

                    Synchronizer.SetState(new ProcessSyncCheckSummaryState(syncCheckState.Summary));
                    await Synchronizer.RunStepAsync();
                }

                Synchronizer.SetState(new MonitorFileSyncEventsState());
                await Synchronizer.RunStepAsync();

                if (NotifyWhenComplete)
                {
                    var syncCompleteMessage = Summary.HasChanges
                            ? "The remote server has been synced with your local copy."
                            : "The remote server is already synced with your local copy.";

                    Log.Debug(syncCompleteMessage);

                    MessageBox.Show(
                        syncCompleteMessage,
                        "Synced remote",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                Synchronizer.SetState(new IdleState());
                await Synchronizer.RunStepAsync();
            }
            catch (IOException ex)
            {
                Log.Error($"Could not sync remote because verion files seem to be used by other process: {ex}");
                MessageBox.Show(
                    "Could not sync remote, make sure that your version files are not being used by another process (such like SolidWorks) and try again.",
                    "Files already in use",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to sync remote: {ex} {ex?.InnerException?.Message}";
                Log.Error(errorMessage);
                MessageBox.Show(
                    errorMessage, "Sync error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                // always go back to idle state
                Synchronizer.SetState(new IdleState());
                await Synchronizer.RunStepAsync();
            }
            Log.Debug("Completed SyncRemoteCommand...");
        }
    }
}
