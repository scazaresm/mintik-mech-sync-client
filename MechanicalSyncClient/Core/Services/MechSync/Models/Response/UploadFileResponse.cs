using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Response
{
    public class UploadFileResponse
    {
        public string Error { get; set; }
        public string Message { get; set; }
        public string FullFilePath { get; set; }
        public string ExpectedFileChecksum { get; set; }
        public string FileChecksum { get; set; }
    }
}
