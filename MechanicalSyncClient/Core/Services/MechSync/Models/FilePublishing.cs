using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class FilePublishing
    {
        public string Id { get; set; }
        public string VersionId { get; set; }
        public string FileMetadataId { get; set; }
        public string RelativeFilePath { get; set; }
        public DateTime PublishedAt { get; set; }
        public List<string> Deliverables { get; set; }
    }
}
