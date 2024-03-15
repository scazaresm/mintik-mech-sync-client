using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Publishing.DeliverablePublisher
{
    public interface IDeliverablePublisher
    {
        ConcurrentQueue<FileMetadata> PublishingQueue { get; }
        DeliverablePublisherUI UI { get; }
        ISolidWorksStarter SolidWorksStarter { get; }
        IVersionSynchronizer Synchronizer { get; }

        Task AnalyzeDeliverablesAsync();
        Task CancelPublishAsync(List<FileMetadata> deliverablesToCancel);
        Task PublishAsync(List<FileMetadata> deliverablesToPublish);
        Task RunStepAsync();
        void SetState(DeliverablePublisherState state);
    } 
}