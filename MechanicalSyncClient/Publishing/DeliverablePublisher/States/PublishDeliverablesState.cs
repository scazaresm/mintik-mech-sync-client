using MechanicalSyncApp.Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher.States
{
    public class PublishDeliverablesState : DeliverablePublisherState
    {
        private readonly ILogger logger;

        public PublishDeliverablesState(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override Task RunAsync()
        {
            throw new NotImplementedException();
        }

        public override void UpdateUI()
        {
            throw new NotImplementedException();
        }
    }
}
