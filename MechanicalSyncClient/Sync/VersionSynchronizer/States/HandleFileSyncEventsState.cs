using MechanicalSyncApp.Core;
using MechanicalSyncApp.Sync.VersionSynchronizer.EventHandlers;
using Serilog;
using System;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.States
{
    public class HandleFileSyncEventsState : VersionSynchronizerState
    {
        private FileCreatedEventHandler fileCreatedHandler;
        private FileChangedEventHandler fileChangedHandler;
        private FileDeletedEventHandler fileDeletedHandler;

        private long totalEvents;
        private long handledEvents;

        private bool syncErrorOccurred = false;

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.SyncProgressBar.Visible = true;
            ui.StatusLabel.Text = "Syncing remote server...";
            ui.SyncProgressBar.Value = 0;
            ui.SyncProgressBar.Visible = true;

            ui.SynchronizerToolStrip.Enabled = true;

            ui.SyncRemoteButton.Visible = true;
            ui.SyncRemoteButton.Enabled = false;

            ui.WorkOnlineButton.Visible = false;

            ui.WorkOfflineButton.Enabled = false;
            ui.WorkOfflineButton.Visible = true;
        }

        public override async Task RunAsync()
        {
            InitializeChainOfHandlers();

            var monitor = Synchronizer.ChangeMonitor;
            var ui = Synchronizer.UI;
            try
            {
                totalEvents = monitor.GetTotalInQueue();
                handledEvents = 0;
                Log.Debug($"Starting to handle {totalEvents} events.");

                while (!monitor.IsEventQueueEmpty())
                {
                    var nextEvent = monitor.PeekNextEvent();
                    if (nextEvent != null)
                    {
                        if(syncErrorOccurred)
                            ui.StatusLabel.Text = "Failed to synchronize remote server, retrying...";
                        else
                            ui.StatusLabel.Text = $"Syncing {nextEvent.RelativeFilePath}";

                        await fileCreatedHandler.HandleAsync(nextEvent);
                        handledEvents++;
                        monitor.DequeueEvent();
                    }
                    UpdateProgress();
                    syncErrorOccurred = false;
                }
                ui.SyncProgressBar.Visible = false;
                ui.SyncProgressBar.Value = 0;
            }
            catch (Exception ex)
            {
                syncErrorOccurred = true;
                Log.Error(ex.ToString());

                // when monitoring we want to retry, otherwise we want to propagate the exception and abort sync
                if (!monitor.IsMonitoring())
                    throw ex;
            }
            finally
            {
                if (monitor.IsMonitoring() && syncErrorOccurred)
                {
                    Log.Debug("We still in online mode but an error occurred, let's retry...");
                    Synchronizer.SetState(this);
                }
                else if(monitor.IsMonitoring() && !syncErrorOccurred)
                {
                    Log.Debug("We still in online mode without errors, just keep monitoring...");
                    Synchronizer.SetState(new MonitorFileSyncEventsState());
                }
                else
                {
                    Log.Debug("We are in offline mode now, stop monitoring...");
                    Synchronizer.SetState(new IdleState());
                }
                _ = Synchronizer.RunStepAsync();
            }
        }

        private void InitializeChainOfHandlers()
        {
            var client = Synchronizer.SyncServiceClient;
            
            fileCreatedHandler = new FileCreatedEventHandler(client, this);
            fileChangedHandler = new FileChangedEventHandler(client, this);
            fileDeletedHandler = new FileDeletedEventHandler(client, this);

            fileCreatedHandler.NextHandler = fileChangedHandler;
            fileChangedHandler.NextHandler = fileDeletedHandler;
        }

        private void UpdateProgress()
        {
            var ui = Synchronizer.UI;

            // consider new events as they arrive, new + handled = total events
            totalEvents = Synchronizer.ChangeMonitor.GetTotalInQueue() + handledEvents;

            // compute progress, check division by zero
            int progress = totalEvents > 0 
                ? (int)((double)handledEvents / totalEvents * 100.0) 
                : 0;

            if (ui.SyncProgressBar != null && progress >= 0 && progress <= 100)
                ui.SyncProgressBar.Value = progress;
        }
    }
}
