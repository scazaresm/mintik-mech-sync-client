using MechanicalSyncApp.Core.Services.MechSync.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.Core.Domain
{
    public class LocalReview
    {
        private const string MY_WORK_FOLDER_NAME = "My Work";
        
        public Version RemoteVersion { get; set; }
        public Project RemoteProject { get; set; }
        public Review RemoteReview { get; set; }

        public LocalReview(Version remoteVersion, Project remoteProject, Review remoteReview)
        {
            RemoteVersion = remoteVersion ?? throw new ArgumentNullException(nameof(remoteVersion));
            RemoteProject = remoteProject ?? throw new ArgumentNullException(nameof(remoteProject));
            RemoteReview = remoteReview ?? throw new ArgumentNullException(nameof(remoteReview));
        }

        public override string ToString()
        {
            return $"{RemoteProject.FolderName} V{RemoteVersion.Major}"; ;
        }
    }
}
