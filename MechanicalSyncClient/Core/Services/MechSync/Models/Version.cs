using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class Version
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string ProjectId { get; set; }
        public string Status { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public string Goal { get; set; }
        public string CreatedBy { get; set; }
        public VersionOwner Owner { get; set; }
        public VersionOwner NextOwner { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<VersionOwner> OwnerHistory { get; set; }
        public string Reason { get; set; }
        public HashSet<string> IgnoreDrawings { get; set; } = new HashSet<string>();
    }
}
