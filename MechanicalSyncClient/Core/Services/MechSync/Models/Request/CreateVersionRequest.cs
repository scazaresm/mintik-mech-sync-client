using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class CreateVersionRequest
    {
        public string ProjectId { get; set; }
        public string Goal { get; set; }
        public string OwnerId { get; set; }
        public string Reason { get; set; }
    }
}
