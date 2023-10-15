using MechanicalSyncApp.Core.Services.MechSync.Models;

namespace MechanicalSyncApp.Core.Domain
{
    public class LocalVersion
    {
        public Version RemoteVersion { get; }
        public Project RemoteProject { get; }
        public string LocalDirectory { get; }

        public LocalVersion(Version remoteVersion, Project remoteProject, string localDirectory)
        {
            RemoteVersion = remoteVersion ?? throw new System.ArgumentNullException(nameof(remoteVersion));
            RemoteProject = remoteProject ?? throw new System.ArgumentNullException(nameof(remoteProject));
            LocalDirectory = localDirectory ?? throw new System.ArgumentNullException(nameof(localDirectory));
        }

        public override string ToString()
        {
            return $"{RemoteProject.FolderName} V{RemoteVersion.Major}";
        }
    }
}

