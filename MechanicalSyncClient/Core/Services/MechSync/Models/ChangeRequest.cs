using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class ChangeRequest
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string Change { get; set; }
        public string Status { get; set; }
        public string DesignerComments { get; set; }
    }
}
