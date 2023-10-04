using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class GetFilesMetadataRequest
    {
        public string ProjectId { get; set; }
        public string VersionFolder { get; set; }
    }
}
