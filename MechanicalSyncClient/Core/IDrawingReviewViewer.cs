using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core
{
    public interface IDrawingReviewViewer
    {
    
        void InitializeUI();

        Task OpenReviewMarkupAsync(ReviewTarget reviewTarget);

        Task CloseReviewMarkupAsync();

        Task RefreshDrawingReviewsAsync();
    }
}
