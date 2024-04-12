using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.SolidWorksInterop.API;
using MechanicalSyncApp.Reviews.FileReviewer;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Args
{
    public class FileReviewerArgs
    {
        public IAuthenticationServiceClient AuthServiceClient { get; set; }

        public IMechSyncServiceClient SyncServiceClient { get; set; }

        public FileReviewerUI UI { get; set; }

        public LocalReview Review { get; set; }

        public ILogger Logger { get; set; }

        public string TempWorkingCopyDirectory { get; set; }

        public ISolidWorksStarter SolidWorksStarter { get; set; }

        public void Validate()
        {
            if (AuthServiceClient == null)
                throw new ArgumentNullException(nameof(AuthServiceClient));

            if (SyncServiceClient == null)
                throw new ArgumentNullException(nameof(SyncServiceClient));

            if (UI == null)
                throw new ArgumentNullException(nameof(UI));

            if (Review == null)
                throw new ArgumentNullException(nameof(Review));

            if (Logger == null)
                throw new ArgumentNullException(nameof(Logger));

            if (TempWorkingCopyDirectory == null)
                throw new ArgumentNullException(nameof(TempWorkingCopyDirectory));

            if (SolidWorksStarter == null)
                throw new ArgumentNullException(nameof(SolidWorksStarter));
        }
    }
}
