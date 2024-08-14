using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Sync;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
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
            var assemblies = await new ReviewableFileMetadataFetcher(
                serviceClient, review.RemoteVersion.Id, logger
            ).FetchReviewableAssembliesAsync();

            var remoteVersion = review.RemoteVersion;

            // skip assemblies which are being ignored on this version, do not need review
            return assemblies.Where((d) =>
                !remoteVersion.IgnoreAssemblies.Contains(Path.GetFileName(d.RelativeFilePath))
            ).ToList();
        }
    }
}
