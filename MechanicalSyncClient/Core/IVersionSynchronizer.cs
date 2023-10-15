using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync.VersionSynchronizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IVersionSynchronizer : IDisposable
    {
        string FileExtensionFilter { get; }

        VersionSynchronizerUI UI { get; }

        LocalVersion Version { get; }

        IMechSyncServiceClient ServiceClient { get; }
        IVersionChangeMonitor ChangeMonitor { get; }

        Dictionary<string, FileMetadata> LocalFileIndex { get; }
        Dictionary<string, FileMetadata> RemoteFileIndex { get; }

        // State related methods
        VersionSynchronizerState GetState();
        void SetState(VersionSynchronizerState state);
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
