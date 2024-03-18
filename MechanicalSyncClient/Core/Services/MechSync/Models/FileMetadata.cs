using MechanicalSyncApp.Core.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public enum PublishingStatus
    {
        [Description("Unknown")]
        Unknown,

        [Description("Ready")]
        Ready,

        [Description("Waiting for approval")]
        WaitingForApproval,

        [Description("Blocked")]
        Blocked,

        [Description("Published")]
        Published,
    }

    public class FileMetadata
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string VersionId { get; set; }
        public string FullFilePath { get; set; }
        public string RelativeFilePath { get; set; }
        public string FileChecksum { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; }

        [JsonIgnore]
        public int ApprovalCount { get; set; } = 0;

        [JsonIgnore]
        public List<string> ValidationIssues { get; } = new List<string>();

        [JsonIgnore]
        public PublishingStatus PublishingStatus
        {
            get
            {
                if (IsPublished)
                    return PublishingStatus.Published;

                if (ApprovalCount > 0 && ValidationIssues.Count == 0)
                    return PublishingStatus.Ready;

                if (ApprovalCount == 0)
                    return PublishingStatus.WaitingForApproval;

                if (ValidationIssues.Count > 0)
                    return PublishingStatus.Blocked;

                return PublishingStatus.Unknown;
            }
        }

        [JsonIgnore]
        public bool IsPublished { get; set; } = false;

        [JsonIgnore]
        public string Revision { get; set; }

        [JsonIgnore]
        public List<CustomProperty> CustomProperties { get; } = new List<CustomProperty>();
    }
}
