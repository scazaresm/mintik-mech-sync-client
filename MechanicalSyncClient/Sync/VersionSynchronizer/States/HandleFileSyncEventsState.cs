using MechanicalSyncApp.Core;
using MechanicalSyncApp.Sync.VersionSynchronizer.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.VersionSynchronizer.States
{
    public class HandleFileSyncEventsState : VersionSynchronizerState
    {
        private FileCreatedEventHandler fileCreatedHandler;
        private FileChangedEventHandler fileChangedHandler;
        private FileDeletedEventHandler fileDeletedHandler;

        private long totalEvents;
        private long handledEvents;

        public override void UpdateUI()
        {
            var ui = Synchronizer.UI;
            ui.SyncProgressBar.Visible = true;
            ui.SynchronizerToolStrip.Enabled = false;
            ui.StatusLabel.Text = "Syncing remote server...";
            ui.SyncProgressBar.Value = 0;
            ui.SyncProgressBar.Visible = true;
        }

        public override async Task RunAsync()
        {
            InitializeChainOfHandlers();

            var monitor = Synchronizer.ChangeMonitor;
            try
            {
                totalEvents = monitor.GetTotalInQueue();
                handledEvents = 0;
                Console.WriteLine($"Starting to handle {totalEvents} events.");

                while (!monitor.IsEventQueueEmpty())
                {
                    var nextEvent = monitor.DequeueEvent();
                    if (nextEvent != null)
                    {
                        Synchronizer.UI.StatusLabel.Text = $"Syncing {nextEvent.RelativeFilePath}";
                        await fileCreatedHandler.HandleAsync(nextEvent);
                        handledEvents++;
                    }
                    UpdateProgress();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                var ui = Synchronizer.UI;
                ui.SyncProgressBar.Value = 0;
                ui.SyncProgressBar.Visible = false;

                if (monitor.IsMonitoring())
                    Synchronizer.SetState(new MonitorFileSyncEventsState());
                else
                    Synchronizer.SetState(new IdleState());

                _ = Synchronizer.RunStepAsync();
            }
        }

        private void InitializeChainOfHandlers()
        {
            var client = Synchronizer.ServiceClient;
            
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

            int progress = (int)((double)handledEvents / totalEvents * 100.0);
            if (progress >= 0 && progress <= 100)
                ui.SyncProgressBar.Value = progress;
        }
    }
}
