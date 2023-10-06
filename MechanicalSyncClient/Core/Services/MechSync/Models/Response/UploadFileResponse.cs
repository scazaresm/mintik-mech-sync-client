using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Response
{
    public class UploadFileResponse
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string Error { get; set; }
        public string Message { get; set; }
        public string FullFilePath { get; set; }
        public string RelativeFilePath { get; set; }
        public string FileChecksum { get; set; }
    }
}
