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
        DeliverablePublisherUI UI { get; }
        ISolidWorksStarter SolidWorksStarter { get; }
        IVersionSynchronizer Synchronizer { get; }

        Task AnalyzeDeliverablesAsync();
        Task CancelPublishAsync(List<FileMetadata> toBeCancelled);
        Task PublishAsync(List<FileMetadata> toBePublished);
        Task RunStepAsync();
        void SetState(DeliverablePublisherState state);
    } 
}