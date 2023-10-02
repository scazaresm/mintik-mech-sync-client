using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Sync.ProjectSynchronizer.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Sync.ProjectSynchronizer.States
{
    public class ProcessChangeEventsState : ProjectSynchronizerState
    {
        private readonly FileCreatedEventHandler fileCreatedHandler;
        private readonly FileChangedEventHandler fileChangedHandler;
        private readonly FileDeletedEventHandler fileDeletedHandler;

        public ProcessChangeEventsState(MechSyncServiceClient client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            // initialize chain of handlers
            fileCreatedHandler = new FileCreatedEventHandler(client);
            fileChangedHandler = new FileChangedEventHandler(client, this);
            fileDeletedHandler = new FileDeletedEventHandler(client);

            fileCreatedHandler.NextHandler = fileChangedHandler;
            fileChangedHandler.NextHandler = fileDeletedHandler;
        }

        public override void UpdateUI()
        {
        }

        public override async Task RunTransitionLogicAsync()
        {
            var monitor = Synchronizer.ChangeMonitor;
            try
            {
                var nextEvent = monitor.DequeueEvent();
                await fileCreatedHandler.HandleAsync(nextEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (monitor.IsEventQueueEmpty())
            {
                Synchronizer.SetState(new WaitForChangeEventsState());
            }
            else
            {
                Synchronizer.SetState(this);
            }
            await Synchronizer.RunTransitionLogicAsync();
        }
    }
}
