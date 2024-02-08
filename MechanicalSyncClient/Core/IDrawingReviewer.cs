using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.Core
{
    public interface IDrawingReviewer
    {
        string[] StatusesHavingMarkupFile { get; }

        string[] StatusesHavingReviewControlsEnabled { get; }

        IDrawingReviewerUI UI { get; }

        IMechSyncServiceClient SyncServiceClient { get; }

        DeltaDrawingsTreeView DeltaDrawingsTreeView { get; }

        LocalReview Review { get; }

        FileMetadata DrawingMetadata { get; set; }
        ReviewTarget ReviewTarget { get; set; }

        string TempDownloadedDrawingFile { get; set; }
        string TempDownloadedMarkupFile { get; set; }
        string TempUploadedMarkupFile { get; set; }


        Task InitializeUiAsync();

        Task OpenReviewTargetAsync(ReviewTarget reviewTarget);

        Task CloseReviewTargetAsync();


        Task ApproveReviewTargetAsync();

        Task RejectReviewTargetAsync();

        Task RefreshDeltaTargetsAsync();


        Task<bool> UploadMarkupFileAsync();

        Task DownloadMarkupFileAsync();

        Task SaveReviewProgressAsync();


        void DrawingReviewerControl_OpenDocError(string FileName, int ErrorCode, string ErrorString);

    }
}
