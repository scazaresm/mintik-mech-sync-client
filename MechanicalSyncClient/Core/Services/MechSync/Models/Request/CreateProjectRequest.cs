using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class CreateProjectRequest
    {
        public string FolderName { get; set; }
        public string InitialVersionOwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
