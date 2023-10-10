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

        private long totalEvents = 0;
        private long handledEvents = 0;

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
            if(progress >= 0 && progress <= 100)
                ui.SyncProgressBar.Value = progress;
        }

        public override async Task RunAsync()
        {
            var client = Synchronizer.ServiceClient;
            var monitor = Synchronizer.ChangeMonitor;

            try
            {
                // initialize chain of handlers
                fileCreatedHandler = new FileCreatedEventHandler(client, this);
                fileChangedHandler = new FileChangedEventHandler(client, this);
                fileDeletedHandler = new FileDeletedEventHandler(client, this);

                fileCreatedHandler.NextHandler = fileChangedHandler;
                fileChangedHandler.NextHandler = fileDeletedHandler;

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
                    Synchronizer.UI.StatusLabel.Text = $"Syncing {nextEvent.RelativePath}";
                    await fileCreatedHandler.HandleAsync(nextEvent);
                    handledEvents++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (monitor.IsEventQueueEmpty())
                {
                    Synchronizer.SetState(new MonitorFileSyncEventsState());
                    _ = Synchronizer.RunStepAsync();
                }
                else
                {
                    Synchronizer.SetState(this);
                    _ = Synchronizer.RunStepAsync();
                }
            }
        }
    }
}
