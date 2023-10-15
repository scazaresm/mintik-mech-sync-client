using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class Project
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string CreatedBy { get; set; }
        public string FolderName { get; set; }
        public string RelativeEquipmentPath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
