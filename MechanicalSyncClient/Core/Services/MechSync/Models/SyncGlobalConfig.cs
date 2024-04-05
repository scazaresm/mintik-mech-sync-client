using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class SyncGlobalConfig
    {
        public string MandatoryAssyCustomProperties { get; set; }
        public string MandatoryPartCustomProperties { get; set; }
        public string ReviewableAssyRegex { get; set; }
        public string ReviewableDrawingRegex { get; set; }
    }
}
