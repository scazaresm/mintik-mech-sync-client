using MechanicalSyncApp.Core.Domain;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Sync;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.UI
{
 
    public class ReviewableDrawingsTreeView : ReviewableFilesTreeView
    {
        public ReviewableDrawingsTreeView(IMechSyncServiceClient serviceClient, LocalReview review) 
            : base(serviceClient, review)
        {
        }

        public override async Task<List<FileMetadata>> FetchReviewableFileMetadata()
        {
            return await new ReviewableFileMetadataFetcher(
                serviceClient, review.RemoteVersion.Id
            ).FetchReviewableDrawingsAsync();
        }
    }
}
