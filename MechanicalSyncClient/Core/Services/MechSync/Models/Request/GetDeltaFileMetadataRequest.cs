using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class GetDeltaFileMetadataRequest
    {
        public string TargetVersionId { get; set; }
        public string SourceVersionId { get; set; }
    }
}
