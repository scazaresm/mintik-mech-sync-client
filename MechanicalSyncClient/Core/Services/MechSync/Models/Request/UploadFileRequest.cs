using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class UploadFileRequest
    {
        public string LocalFilePath { get; set; }
        public string VersionId { get; set; }
        public string RelativeEquipmentPath { get; set; }
        public string RelativeFilePath { get; set; }
    }
}
