using MechanicalSyncApp.Core;
using MechanicalSyncApp.Core.Services.MechSync;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Sync
{
    public class ReviewableFileMetadataFetcher : IReviewableFileMetadataFetcher
    {
        private readonly Dictionary<string, int> FileApprovalCountIndex = new Dictionary<string, int>();

        private readonly Dictionary<string, FilePublishing> FilePublishingIndex = new Dictionary<string, FilePublishing>();

        private readonly IMechSyncServiceClient serviceClient;
        private readonly ILogger logger;

        public string VersionId { get; private set; }

        private SyncGlobalConfig globalConfig;

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
            if (globalConfig is null)
                globalConfig = await serviceClient.GetGlobalConfigAsync();

            var deltaFileMetadata = await serviceClient.GetDeltaFileMetadataAsync(new GetDeltaFileMetadataRequest()
            {
                TargetVersionId = VersionId
            });
            await FetchFileApprovals();

            var reviewableAssemblies = deltaFileMetadata
                .Where(m => 
                    Regex.IsMatch(m.RelativeFilePath, globalConfig.ReviewableAssyRegex, RegexOptions.IgnoreCase)
                )
                .ToList();

            foreach(var assembly in reviewableAssemblies)
            {
                if (FileApprovalCountIndex.ContainsKey(assembly.Id))
                    assembly.ApprovalCount = FileApprovalCountIndex[assembly.Id];
                else
                    assembly.ApprovalCount = 0;
            }
            return reviewableAssemblies;
        }

        public async Task<List<FileMetadata>> FetchReviewableDrawingsAsync()
        {
            if (globalConfig is null)
                globalConfig = await serviceClient.GetGlobalConfigAsync();

            var deltaFileMetadata = await serviceClient.GetDeltaFileMetadataAsync(new GetDeltaFileMetadataRequest()
            {
                TargetVersionId = VersionId
            });
            await FetchFileApprovals();
            await FetchFilePublishings();

            var reviewableDrawings = deltaFileMetadata
                .Where(m => 
                        Regex.IsMatch(m.RelativeFilePath, globalConfig.ReviewableDrawingRegex, RegexOptions.IgnoreCase)
                )
                .ToList();

            foreach(var drawing in reviewableDrawings)
            {
                if (FileApprovalCountIndex.ContainsKey(drawing.Id))
                    drawing.ApprovalCount = FileApprovalCountIndex[drawing.Id];
                else
                    drawing.ApprovalCount = 0;

                var partNumber = Path.GetFileNameWithoutExtension(drawing.FullFilePath);
                drawing.IsPublished = FilePublishingIndex.ContainsKey(partNumber);
            }
            return reviewableDrawings;
        }

        private async Task FetchFileApprovals()
        {
            var reviews = await serviceClient.GetVersionReviewsAsync(VersionId);

            FileApprovalCountIndex.Clear();

            foreach (var review in reviews)
            {
                foreach (var target in review.Targets)
                {
                    if (target.Status != "Approved") continue;

                    if (!FileApprovalCountIndex.ContainsKey(target.TargetId))
                        FileApprovalCountIndex[target.TargetId] = 1; // found the first approval for this file, start from 1
                    else
                        FileApprovalCountIndex[target.TargetId]++; // found the next approval for this file, increment by one
                }
            }
        }

        private async Task FetchFilePublishings()
        {
            var publishings = await serviceClient.GetVersionFilePublishingsAsync(VersionId);

            foreach(var publishing in publishings)
            {
                var index = publishing.PartNumber.Replace('/', Path.DirectorySeparatorChar);

                if (!FilePublishingIndex.ContainsKey(index))
                    FilePublishingIndex.Add(index, publishing);
                else
                    FilePublishingIndex[index] = publishing;
            }
        }
    }
}