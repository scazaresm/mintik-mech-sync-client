﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models.Request
{
    public class CreateReviewRequest
    {
        public string VersionId { get; set; }
        public string TargetType { get; set; }
        public string ReviewerId { get; set; }
    }
}
