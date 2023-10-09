using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Sync.ProjectSynchronizer.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.States
{
    public class HandleFileSyncEventsState : ProjectSynchronizerState
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

            if (totalEvents == 0)
            {
                ui.SyncProgressBar.Value = 0;
                ui.SyncProgressBar.Visible = false;
                return;
            }

            int progress = (int)((double)handledEvents / totalEvents * 100.0);
            if(progress <= 100)
                ui.SyncProgressBar.Value = progress;
        }

        public override async Task RunTransitionLogicAsync()
        {
            var client = Synchronizer.ServiceClient;
            var monitor = Synchronizer.ChangeMonitor;

            // initialize chain of handlers
            fileCreatedHandler = new FileCreatedEventHandler(client, this);
            fileChangedHandler = new FileChangedEventHandler(client, this);
            fileDeletedHandler = new FileDeletedEventHandler(client, this);

            fileCreatedHandler.NextHandler = fileChangedHandler;
            fileChangedHandler.NextHandler = fileDeletedHandler;

            try
            {
                if (totalEvents == 0 && handledEvents == 0)
                {
                    totalEvents = Synchronizer.ChangeMonitor.GetTotalInQueue();
                    Console.WriteLine($"Starting to handle {totalEvents} events.");
                }
                else
                {
                    totalEvents = Synchronizer.ChangeMonitor.GetTotalInQueue() + handledEvents;
                }

                var nextEvent = monitor.DequeueEvent();
                if (nextEvent != null)
                {
                    await fileCreatedHandler.HandleAsync(nextEvent);
                    handledEvents++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (monitor.IsEventQueueEmpty())
            {
                Synchronizer.SetState(new MonitorFileSyncEventsState());
                _ = Synchronizer.RunTransitionLogicAsync();
            }
            else
            {
                Synchronizer.SetState(this);
                _ = Synchronizer.RunTransitionLogicAsync();
            }
        }
    }
}
