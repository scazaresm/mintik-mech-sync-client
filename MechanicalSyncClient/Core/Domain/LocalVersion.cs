using MechanicalSyncApp.Core.Services.MechSync.Models;
using System.IO;

namespace MechanicalSyncApp.Core.Domain
{
    public class LocalVersion
    {
        private const string MY_WORK_FOLDER_NAME = "My Work";

        public Version RemoteVersion { get; set; }
        public Project RemoteProject { get; set; }
        public string LocalDirectory { get; set; }

        public LocalVersion(Version remoteVersion, Project remoteProject, string workspaceDirectory)
        {
            RemoteVersion = remoteVersion ?? throw new System.ArgumentNullException(nameof(remoteVersion));
            RemoteProject = remoteProject ?? throw new System.ArgumentNullException(nameof(remoteProject));

            if(workspaceDirectory == null)
                throw new System.ArgumentNullException(nameof(workspaceDirectory));

            LocalDirectory = Path.Combine(
                workspaceDirectory,
                MY_WORK_FOLDER_NAME,
                ToString()
            );
        }

        public override string ToString()
        {
            return $"{RemoteProject.FolderName} V{RemoteVersion.Major}";
        }
    }
}

