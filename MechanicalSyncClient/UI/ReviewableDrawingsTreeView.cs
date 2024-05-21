using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var drawings = await new ReviewableFileMetadataFetcher(
                serviceClient, review.RemoteVersion.Id, logger
            ).FetchReviewableDrawingsAsync();

            var remoteVersion = review.RemoteVersion;

            // skip drawings which are being ignored on this version, do not need review
            return drawings.Where((d) => 
                !remoteVersion.IgnoreDrawings.Contains( Path.GetFileName(d.RelativeFilePath) )
            ).ToList();
        }
    }
}
