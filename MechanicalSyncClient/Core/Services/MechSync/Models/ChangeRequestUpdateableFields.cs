using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class ChangeRequestUpdateableFields
    {
        public string ChangeDescription { get; set; }
        public string Status { get; set; }
        public string DesignerComments { get; set; }
    }
}
