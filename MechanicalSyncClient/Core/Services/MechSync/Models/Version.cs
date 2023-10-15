using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class Version
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string ProjectId { get; set; }
        public string Level { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public string Goal { get; set; }
        public string CreatedBy { get; set; }
        public VersionOwner Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<VersionOwner> OwnerHistory { get; set; }
        public List<FileMetadata> FinalFileMetadata { get; set; }
    }
}
