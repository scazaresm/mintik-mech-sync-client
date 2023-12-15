using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class Job
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string JobType { get; set; }
        public string VersionId { get; set; }
        public string Status { get; set; }
        public string QueuedBy { get; set; }         
        public DateTime QueuedAt { get; set; }
        public DateTime ProcessedAt { get; set; }
        public string ErrorMessage { get; set; }
    }
}
