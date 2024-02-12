using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Models
{
    public class AggregatedProjectDetails
    {
        public Project ProjectDetails { get; set; }
        public Version LatestVersion { get; set; }
        public Version OngoingVersion { get; set; }
        public List<Version> VersionHistory { get; set; }

        public override string ToString()
        {
            return ProjectDetails.FolderName;
        }
    }
}
