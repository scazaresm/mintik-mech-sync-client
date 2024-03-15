using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using MechanicalSyncApp.Publishing.DeliverablePublisher.States;
using MechanicalSyncApp.Sync;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher
{
    public class DeliverablePublisher : IDeliverablePublisher
    {
        private DeliverablePublisherState state;

        public ConcurrentQueue<FileMetadata> PublishingQueue { get; private set; } = new ConcurrentQueue<FileMetadata>();

        public DeliverablePublisherUI UI { get; }
        public ISolidWorksStarter SolidWorksStarter { get; }
        public IVersionSynchronizer Synchronizer { get; } 

        private readonly ILogger logger;


        public DeliverablePublisher(IVersionSynchronizer synchronizer, 
                                    ISolidWorksStarter solidWorksStarter,
                                    DeliverablePublisherUI ui, 
                                    ILogger logger)
        {
            Synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            UI = ui ?? throw new ArgumentNullException(nameof(ui));
            SolidWorksStarter = solidWorksStarter ?? throw new ArgumentNullException(nameof(solidWorksStarter));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void SetState(DeliverablePublisherState state)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state));
            this.state.SetPublisher(this);
            this.state.UpdateUI();
        }

        public async Task RunStepAsync()
        {
            if (state != null)
                await state.RunAsync();
            else
                throw new InvalidOperationException("Trying to run a step before actually setting the current step.");
        }

        public async Task AnalyzeDeliverablesAsync()
        {
            IReviewableFileMetadataFetcher fetcher = new ReviewableFileMetadataFetcher(Synchronizer, logger);

            SetState(new ValidateDrawingsState(fetcher, logger));
            await RunStepAsync();
        }

        public async Task PublishAsync(List<FileMetadata> toPublish)
        {

        }

        public async Task CancelPublishAsync(List<FileMetadata> toCancel)
        {
        }
    }
}
