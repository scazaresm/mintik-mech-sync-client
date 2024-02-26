using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class FixReviewTargetRequest
    {
        public string ReviewId { get; set; }
        public string TargetId { get; set; }
    }
}
