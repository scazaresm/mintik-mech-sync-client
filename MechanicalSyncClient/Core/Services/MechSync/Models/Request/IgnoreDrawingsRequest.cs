using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class IgnoreDrawingsRequest
    {
        public string VersionId { get; set; }
        public List<string> ToIgnore { get; set; } = new List<string>();
        public List<string> ToStopIgnoring { get; set; } = new List<string>();
    }
}
