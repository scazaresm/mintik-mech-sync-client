using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class UpdateReviewTargetRequest
    {
        public string ReviewId { get; set; }

        public string ReviewTargetId { get; set; }

        public string Status { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
