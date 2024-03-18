using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IPublishDeliverablesStrategy
    {
        List<string> Deliverables { get; }
        string FullProjectPublishingDirectory { get; }
        string RelativeProjectPublishingDirectory { get; }
        string RevisionSuffix { get; }

        Task PublishAsync(FileMetadata validDrawing);
    }
}
