using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync
{
    public class ReviewableFileMetadataFetcher : IReviewableFileMetadataFetcher
    {
        private readonly string DRAWING_FILTER_REGEX = @"\.slddrw$";
        private readonly string ASSEMBLY_FILTER_REGEX = @"^a.*\.sldasm$";

        private readonly IMechSyncServiceClient serviceClient;

        public string VersionId { get; private set; }

        public ReviewableFileMetadataFetcher(IMechSyncServiceClient serviceClient, string versionId)
        {
            this.serviceClient = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));
            VersionId = versionId ?? throw new ArgumentNullException(nameof(versionId));
        }

        public async Task<List<FileMetadata>> FetchReviewableAssembliesAsync()
        {
            var deltaFileMetadata = await serviceClient.GetDeltaFileMetadataAsync(new GetDeltaFileMetadataRequest()
            {
                TargetVersionId = VersionId
            });

            return deltaFileMetadata
                .Where(m => Regex.IsMatch(m.RelativeFilePath, ASSEMBLY_FILTER_REGEX, RegexOptions.IgnoreCase))
                .ToList();
        }

        public async Task<List<FileMetadata>> FetchReviewableDrawingsAsync()
        {
            var deltaFileMetadata = await serviceClient.GetDeltaFileMetadataAsync(new GetDeltaFileMetadataRequest()
            {
                TargetVersionId = VersionId
            });

            return deltaFileMetadata
                .Where(m => Regex.IsMatch(m.RelativeFilePath, DRAWING_FILTER_REGEX, RegexOptions.IgnoreCase))
                .ToList();
        }
    }
}