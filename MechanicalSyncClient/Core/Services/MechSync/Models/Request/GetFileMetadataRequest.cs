using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class GetFileMetadataRequest
    {
        public string VersionId { get; set; }
        public string RelativeFilePath { get; set; }
    }
}
