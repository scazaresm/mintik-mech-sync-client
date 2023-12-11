using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class VersionOwner
    {
        public string UserId { get; set; }
        public DateTime IsOwnerSince { get; set; }
        public string SyncChecksum { get; set; }
    }
}
