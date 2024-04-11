using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public enum ChangeRequestStatus
    {
        Pending,
        Done,
        Discarded,
        Closed
    }

    public class ChangeRequest
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string ChangeDescription { get; set; }
        public string Status { get; set; } = ChangeRequestStatus.Pending.ToString();
        public string DesignerComments { get; set; }

        [JsonIgnore]
        public Image DetailsImage { get; set; }

        [JsonIgnore]
        public ReviewTarget Parent { get; set; }
    }
}
