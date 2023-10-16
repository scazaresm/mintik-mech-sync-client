using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync.VersionSynchronizer;
using System;
using System.Collections.Concurrent;
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

        OngoingVersion Version { get; }

        IMechSyncServiceClient ServiceClient { get; }
        IVersionChangeMonitor ChangeMonitor { get; }

        ConcurrentDictionary<string, FileMetadata> LocalFileIndex { get; set; }
        Dictionary<string, FileMetadata> RemoteFileIndex { get; }

        // State related methods
        VersionSynchronizerState GetState();
        void SetState(VersionSynchronizerState state);
        Task RunStepAsync();

        // Business logic
        Task WorkOnlineAsync();
        Task WorkOfflineAsync();
        Task SynchronizeVersionAsync();

        // UI related methods
        void InitializeUI();
        void UpdateUI();
    }
}
