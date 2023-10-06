namespace MechanicalSyncApp.Core.Domain
{
    public class LocalProject
    {
        public string RemoteProjectId { get; set; }
        public string RemoteVersionId { get; set; }
        public string LocalDirectory { get; set; }

        public LocalProject(string remoteId, string remoteVersionId, string localDirectory)
        {
            RemoteProjectId = remoteId;
            RemoteVersionId = remoteVersionId;
            LocalDirectory = localDirectory;
        }
    }
}

