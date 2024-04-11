using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync.VersionSynchronizer;
using MechanicalSyncApp.UI;
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
        SyncCheckSummary OnlineWorkSummary { get; set; }

        string FileExtensionFilter { get; }

        VersionSynchronizerUI UI { get; }

        LocalVersion Version { get; }

        IMechSyncServiceClient SyncServiceClient { get; }

        IAuthenticationServiceClient AuthServiceClient { get; }

        IVersionChangeMonitor ChangeMonitor { get; }

        string SnapshotDirectory { get; }

        ConcurrentDictionary<string, FileMetadata> LocalFileIndex { get; set; }
        ConcurrentDictionary<string, FileMetadata> RemoteFileIndex { get; }

        Review CurrentDrawingReview { get; set; }
        ReviewTarget CurrentDrawingReviewTarget { get; set; }
        ConcurrentDictionary<string, FilePublishing> PublishingIndexByPartNumber { get; }
        string BasePublishingDirectory { get; set; }
        string RelativePublishingSummaryDirectory { get; set; }
        ReviewTarget CurrentAssemblyReviewTarget { get; set; }
        Review CurrentAssemblyReview { get; set; }

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
        Task PublishDeliverablesAsync();
        Task TransferOwnershipAsync();
        Task OpenDrawingForViewingAsync(OpenReviewTargetForViewingEventArgs e);

        // UI related methods
        void InitializeUI();
        void UpdateUI();
        Task OpenAssemblyForViewingAsync(OpenReviewTargetForViewingEventArgs e);
    }
}
