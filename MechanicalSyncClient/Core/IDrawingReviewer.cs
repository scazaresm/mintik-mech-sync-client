using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IDrawingReviewer
    {
        IDrawingReviewerUI UI { get; }

        IMechSyncServiceClient SyncServiceClient { get; }

        LocalReview Review { get; }

        void InitializeUI();

        Task OpenReviewTarget(ReviewTarget reviewTarget);

        void CloseReviewTarget();

        Task RefreshDeltaDrawings();
    }
}
