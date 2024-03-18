using MechanicalSyncApp.Core.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class FilePublishing
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string VersionId { get; set; }
        public string FileMetadataId { get; set; }
        public string PartNumber { get; set; }
        public string Revision { get; set; }
        public DateTime PublishedAt { get; set; }
        public string PublishedBy { get; set; }
        public List<string> Deliverables { get; set; }
        public List<CustomProperty> CustomProperties { get; set; }
    }
}
