using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync.VersionSynchronizer;
using MechanicalSyncApp.UI.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        ConcurrentDictionary<string, FileMetadata> RemoteFileIndex { get; }

        // State related methods
        VersionSynchronizerState GetState();
        void SetState(VersionSynchronizerState state);
        Task RunStepAsync();

        // Commands
        Task OpenVersionAsync();
        Task WorkOnlineAsync();
        Task WorkOfflineAsync();
        Task SyncRemoteAsync();
        Task CloseVersionAsync();
        Task PublishVersionAsync();
        Task TransferOwnershipAsync();

        // UI related methods
        void InitializeUI();
        void UpdateUI();
    }
}
