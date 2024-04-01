using MechanicalSyncApp.Core.Args;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IAssemblyReviewer
    {
        AssemblyReviewerArgs Args { get; }
        ReviewTarget ReviewTarget { get; set; }
        FileMetadata AssemblyMetadata { get; set; }

        Task OpenReviewTargetAsync(ReviewTarget reviewTarget);
        Task CloseReviewTargetAsync();
        Task CreateChangeRequestAsync();
        Task InitializeUiAsync();
        Task ViewChangeRequestAsync(ChangeRequest changeRequest);
        Task RefreshReviewTargetsAsync();
        Task RejectAssemblyAsync();
    }
}
