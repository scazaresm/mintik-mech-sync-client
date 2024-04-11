using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher.Strategies
{
    public class CancelPublishingStrategy : ICancelPublishingStrategy
    {
        private readonly IMechSyncServiceClient syncServiceClient;
        private readonly ILogger logger;

        public CancelPublishingStrategy(
                IMechSyncServiceClient syncServiceClient,
                ILogger logger
            )
        {
            this.syncServiceClient = syncServiceClient ?? throw new ArgumentNullException(nameof(syncServiceClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CancelAsync(FilePublishing publishing)
        {
            if (publishing is null)
            {
                throw new ArgumentNullException(nameof(publishing));
            }
            await syncServiceClient.DeleteFilePublishingAsync(publishing.Id);
        }

    }
}
