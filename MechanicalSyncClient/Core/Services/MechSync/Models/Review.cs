using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class Review
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string VersionId { get; set; }
        public string ReviewerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TargetType { get; set; }
        public string Status { get; set; }
        public DateTime FinishedAt { get; set; }

        public List<ReviewTarget> Targets { get; set; }
    }
}
