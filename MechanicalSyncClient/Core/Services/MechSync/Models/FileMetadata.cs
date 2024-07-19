using MechanicalSyncApp.Core.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        [Description("Error")]
        Error,
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
        public List<string> ValidationIssues { get; set;  } = new List<string>();

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
        public List<CustomProperty> CustomProperties { get; set; } = new List<CustomProperty>();

        [JsonIgnore]
        public ReviewTarget ReviewTarget { get; set; }

        public DataGridViewCellStyle GetPublishingStatusCellStyle()
        {
            var style = new DataGridViewCellStyle();

            var status = PublishingStatus;

            switch (status)
            {
                case PublishingStatus.Blocked:
                    style.BackColor = Color.DarkRed;
                    style.ForeColor = Color.White;
                    break;

                case PublishingStatus.WaitingForApproval:
                    style.BackColor = Color.LightGray;
                    style.ForeColor = Color.Black;
                    break;

                case PublishingStatus.Published:
                    style.BackColor = Color.DarkGreen;
                    style.ForeColor = Color.White;
                    break;

                case PublishingStatus.Ready:
                    style.BackColor = Color.LightYellow;
                    style.ForeColor = Color.Black;
                    break;

                default:
                    style.BackColor = Color.White;
                    style.ForeColor = Color.Black;
                    break;
            }
            return style;
        }
    }
}
