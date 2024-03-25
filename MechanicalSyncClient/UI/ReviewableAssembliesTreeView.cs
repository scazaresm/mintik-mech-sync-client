using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.UI
{
    public class ReviewableAssembliesTreeView : ReviewableFilesTreeView
    {
        public ReviewableAssembliesTreeView(IMechSyncServiceClient serviceClient, LocalReview review, ILogger logger) 
            : base(serviceClient, review, logger)
        {
        }

        public override async Task<List<FileMetadata>> FetchReviewableFileMetadata()
        {
            return await new ReviewableFileMetadataFetcher(
                serviceClient, review.RemoteVersion.Id, logger
            ).FetchReviewableAssembliesAsync();
        }
    }
}
