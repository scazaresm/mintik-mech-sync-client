using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Database;
using MechanicalSyncApp.Database.Domain;
using MechanicalSyncApp.Sync.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync.States
{
    public class ProcessQueuedEventsState : ProjectSynchronizerState
    {
        private readonly MechSyncServiceClient client;

        private FileCreatedHandler fileCreatedHandler;

        public ProcessQueuedEventsState(MechSyncServiceClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            InitializeProcessingChain();
        }

        public void InitializeProcessingChain()
        {
            fileCreatedHandler = new FileCreatedHandler(client);
        }

        public override void UpdateUI()
        {
        }

        public override async Task RunTransitionLogicAsync()
        {
            var monitor = synchronizer.Monitor;

            try
            {
                var nextEvent = monitor.DequeueEvent();
                await fileCreatedHandler.HandleAsync(nextEvent);
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            if(monitor.IsEventQueueEmpty())
            {
                synchronizer.SetState(new IdleState());
            }
            else
            {
                synchronizer.SetState(this);
            }
            await synchronizer.RunTransitionLogicAsync();
        }
    }
}
