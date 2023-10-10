using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IProjectSynchronizer : IDisposable
    {
        ProjectSynchronizerUI UI { get; }

        LocalProject LocalProject { get; }

        IMechSyncServiceClient ServiceClient { get; }
        IProjectChangeMonitor ChangeMonitor { get; }

        Dictionary<string, FileMetadata> LocalFileIndex { get; }
        Dictionary<string, FileMetadata> RemoteFileIndex { get; }

        // State related methods
        ProjectSynchronizerState GetState();
        void SetState(ProjectSynchronizerState state);
        Task RunStepAsync();

        // Business logic
        Task StartMonitoringEvents();
        Task StopMonitoringEvents();
        Task Sync();

        // UI related methods
        void InitializeUI();
        void UpdateUI();
    }
}
