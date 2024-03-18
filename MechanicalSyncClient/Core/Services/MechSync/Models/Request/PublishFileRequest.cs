using MechanicalSyncApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class PublishFileRequest
    {
        public string FileMetadataId { get; set; }
        public string Revision { get; set; }
        public List<string> Deliverables { get; set; }
        public List<CustomProperty> CustomProperties { get; set; }
    }
}
