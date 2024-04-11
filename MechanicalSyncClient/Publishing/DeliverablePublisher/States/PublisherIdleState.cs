using MechanicalSyncApp.Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher.States
{
    public class PublisherIdleState : DeliverablePublisherState
    {
        private readonly ILogger logger;

        public PublisherIdleState(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task RunAsync()
        {
            logger.Debug("State machine moved to PublisherIdleState, delaying for 1000ms.");
            await Task.Delay(1000);
        }

        public override void UpdateUI()
        {

        }
    }
}
