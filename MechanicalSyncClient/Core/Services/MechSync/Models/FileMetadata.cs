using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
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
    }
}
