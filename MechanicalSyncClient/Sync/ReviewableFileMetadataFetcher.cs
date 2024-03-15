using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using Serilog;
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
        //private readonly string ASSEMBLY_FILTER_REGEX = @"^*\-a.*\.sldasm$";
        private readonly string ASSEMBLY_FILTER_REGEX = @"\.sldasm$";

        private readonly Dictionary<string, int> FileApprovalCount = new Dictionary<string, int>();

        private readonly IMechSyncServiceClient serviceClient;
        private readonly ILogger logger;

        public string VersionId { get; private set; }

        public ReviewableFileMetadataFetcher(IMechSyncServiceClient serviceClient, string versionId, ILogger logger)
        {
            this.serviceClient = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));
            VersionId = versionId ?? throw new ArgumentNullException(nameof(versionId));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ReviewableFileMetadataFetcher(IVersionSynchronizer synchronizer, ILogger logger)
        {
            if (synchronizer is null)
            {
                throw new ArgumentNullException(nameof(synchronizer));
            }
            serviceClient = synchronizer.SyncServiceClient;
            VersionId = synchronizer.Version.RemoteVersion.Id;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<FileMetadata>> FetchReviewableAssembliesAsync()
        {
            var deltaFileMetadata = await serviceClient.GetDeltaFileMetadataAsync(new GetDeltaFileMetadataRequest()
            {
                TargetVersionId = VersionId
            });
            await FetchFileApprovals();

            var reviewableAssemblies = deltaFileMetadata
                .Where(m => Regex.IsMatch(m.RelativeFilePath, ASSEMBLY_FILTER_REGEX, RegexOptions.IgnoreCase))
                .ToList();

            foreach(var assembly in reviewableAssemblies)
            {
                if (FileApprovalCount.ContainsKey(assembly.Id))
                    assembly.ApprovalCount = FileApprovalCount[assembly.Id];
                else
                    assembly.ApprovalCount = 0;
            }
            return reviewableAssemblies;
        }

        public async Task<List<FileMetadata>> FetchReviewableDrawingsAsync()
        {
            var deltaFileMetadata = await serviceClient.GetDeltaFileMetadataAsync(new GetDeltaFileMetadataRequest()
            {
                TargetVersionId = VersionId
            });
            await FetchFileApprovals();

            var reviewableDrawings = deltaFileMetadata
                .Where(m => Regex.IsMatch(m.RelativeFilePath, DRAWING_FILTER_REGEX, RegexOptions.IgnoreCase))
                .ToList();

            foreach(var drawing in reviewableDrawings)
            {
                if (FileApprovalCount.ContainsKey(drawing.Id))
                    drawing.ApprovalCount = FileApprovalCount[drawing.Id];
                else
                    drawing.ApprovalCount = 0;
            }
            return reviewableDrawings;
        }

        private async Task FetchFileApprovals()
        {
            var reviews = await serviceClient.GetVersionReviewsAsync(VersionId);

            FileApprovalCount.Clear();

            foreach (var review in reviews)
            {
                foreach (var target in review.Targets)
                {
                    if (target.Status != "Approved") continue;

                    if (!FileApprovalCount.ContainsKey(target.TargetId))
                        FileApprovalCount[target.TargetId] = 1;
                    else
                        FileApprovalCount[target.TargetId]++;
                }
            }
        }
    }
}