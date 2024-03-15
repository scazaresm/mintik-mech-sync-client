using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MechanicalSyncApp.UI
{

    public class ReviewableDrawingsTreeView : ReviewableFilesTreeView
    {
        public ReviewableDrawingsTreeView(IMechSyncServiceClient serviceClient, LocalReview review, ILogger logger) 
            : base(serviceClient, review, logger)
        {
        }

        public override async Task<List<FileMetadata>> FetchReviewableFileMetadata()
        {
            return await new ReviewableFileMetadataFetcher(
                serviceClient, review.RemoteVersion.Id, logger
            ).FetchReviewableDrawingsAsync();
        }
    }
}
