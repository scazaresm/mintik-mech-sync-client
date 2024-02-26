using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class ReviewTarget
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string TargetId { get; set; }
        public string Status { get; set; }
        public List<ChangeRequest> ChangeRequests { get; set; }
        public List<string> Observations { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
