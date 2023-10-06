using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Response
{
    public class GetFilesMetadataResponse
    {
        public string ProjectId { get; set; }
        public string VersionId { get; set; }
        public List<FileMetadata> Files { get; set; }

        public string Error { get; set; }
    }
}
